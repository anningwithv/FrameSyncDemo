/*************************************************
	作者: Plane
	邮箱: 1785275942@qq.com	
	功能: PVP战斗管理器

    //=================*=================\\
           教学官网：www.qiqiker.com
           官方微信服务号: qiqikertuts
           Plane老师微信: PlaneZhong
               ~~获取更多教学资讯~~
    \\=================*=================//
*************************************************/

using PEMath;
using PEPhysx;
using HOKProtocol;
using UnityEngine;
using System.Collections.Generic;

public class FightMgr : MonoBehaviour {
    MapRoot mapRoot;
    EnvColliders logicEnv;

    Transform transFollow;

    //英雄角色集合
    List<Hero> heroLst;
    List<Tower> towerLst;
    List<Soldier> soldierLst;
    List<Bullet> bulletLst;
    List<LogicTimer> timerLst;

    int waveIndex = 0;
    int income = 450;

    public void Init(List<BattleHeroData> battleHeroLst, MapCfg mapCfg) {
        heroLst = new List<Hero>();
        towerLst = new List<Tower>();
        soldierLst = new List<Soldier>();
        bulletLst = new List<Bullet>();
        timerLst = new List<LogicTimer>();

        //初始化碰撞环境
        InitEnv();
        //防御塔
        InitTower(mapCfg);
        //英雄
        InitHero(battleHeroLst, mapCfg);
        //小兵
        ++waveIndex;
        //delay 以后 出生小兵。。。
        void CreateSoldier() {
            CreateSoldierBatch(mapCfg, TeamEnum.Blue);
            CreateSoldierBatch(mapCfg, TeamEnum.Red);
        }
        LogicTimer pt = new LogicTimer(CreateSoldier, mapCfg.bornDelay, mapCfg.waveInterval);
        timerLst.Add(pt);

        InitIncome();
    }

    void InitIncome() {
        LogicTimer pt = new LogicTimer(() => {
            ++income;
            BattleSys.Instance.RefreshIncome(income);
        }, 0, 2000);
        timerLst.Add(pt);
    }

    public void UnInit() {
        heroLst.Clear();
        towerLst.Clear();
        soldierLst.Clear();
        bulletLst.Clear();
        CalcRule.blueTeamSoldier.Clear();
        CalcRule.redTeamSoldier.Clear();
    }

    void InitHero(List<BattleHeroData> battleHeroLst, MapCfg mapCfg) {
        int sep = battleHeroLst.Count / 2;
        Hero[] blueTeamHero = new Hero[sep];
        Hero[] redTeamHero = new Hero[sep];
        for(int i = 0; i < battleHeroLst.Count; i++) {
            HeroData hd = new HeroData {
                heroID = battleHeroLst[i].heroID,
                posIndex = i,
                userName = battleHeroLst[i].userName,
                unitCfg = ResSvc.Instance.GetUnitConfigByID(battleHeroLst[i].heroID)
            };

            Hero hero;
            if(i < sep) {
                hd.teamEnum = TeamEnum.Blue;
                hd.bornPos = mapCfg.blueBorn + new PEVector3(0, 0, i * (PEInt)1.5f);
                hero = new Hero(hd);
                blueTeamHero[i] = hero;
            }
            else {
                hd.teamEnum = TeamEnum.Red;
                hd.bornPos = mapCfg.redBorn + new PEVector3(0, 0, (i - sep) * (PEInt)1.5f);
                hero = new Hero(hd);
                redTeamHero[i - sep] = hero;
            }
            hero.LogicInit();
            heroLst.Add(hero);
        }

        CalcRule.blueTeamHero = blueTeamHero;
        CalcRule.redTeamHero = redTeamHero;
    }

    void InitTower(MapCfg mapCfg) {
        int sep = mapCfg.towerIDArr.Length / 2;
        Tower[] blueTeamTower = new Tower[sep];
        Tower[] redTeamTower = new Tower[sep];
        for(int i = 0; i < mapCfg.towerIDArr.Length; i++) {
            TowerData td = new TowerData {
                towerID = mapCfg.towerIDArr[i],
                towerIndex = i,
                unitCfg = ResSvc.Instance.GetUnitConfigByID(mapCfg.towerIDArr[i])
            };

            Tower tower;
            if(i < sep) {
                td.teamEnum = TeamEnum.Blue;
                td.bornPos = mapCfg.towerPosArr[i];
                tower = new Tower(td);
                blueTeamTower[i] = tower;
            }
            else {
                td.teamEnum = TeamEnum.Red;
                td.bornPos = mapCfg.towerPosArr[i];
                tower = new Tower(td);
                redTeamTower[i - sep] = tower;
            }
            tower.LogicInit();
            towerLst.Add(tower);
        }

        CalcRule.blueTeamTower = blueTeamTower;
        CalcRule.redTeamTower = redTeamTower;
    }

    void CreateSoldierBatch(MapCfg cfg, TeamEnum team) {
        int[] idArr;
        PEVector3[] posArr;
        if(team == TeamEnum.Blue) {
            idArr = cfg.blueSoldierIDArr;
            posArr = cfg.blueSoldierPosArr;
        }
        else {
            idArr = cfg.redSoldierIDArr;
            posArr = cfg.redSoldierPosArr;
        }

        for(int i = 0; i < idArr.Length; i++) {
            SoldierData sd = new SoldierData {
                soldierID = idArr[i],
                waveIndex = waveIndex,
                orderIndex = i,
                soldierName = "soldier_" + idArr[i],
                teamEnum = team,
                bornPos = posArr[i],
                unitCfg = ResSvc.Instance.GetUnitConfigByID(idArr[i]),
            };

            LogicTimer pt = new LogicTimer(() => {
                Soldier soldier = new Soldier(sd);
                soldier.LogicInit();
                if(sd.teamEnum == TeamEnum.Blue) {
                    CalcRule.blueTeamSoldier.Add(soldier);
                }
                else {
                    CalcRule.redTeamSoldier.Add(soldier);
                }
                soldierLst.Add(soldier);
            }, (i / 2) * cfg.bornInterval);
            timerLst.Add(pt);
        }
    }

    void InitEnv() {
        Transform transMapRoot = GameObject.FindGameObjectWithTag("MapRoot").transform;
        mapRoot = transMapRoot.GetComponent<MapRoot>();
        List<ColliderConfig> envColliCfgLst = GenerateEnvColliCfgs(mapRoot.transEnvCollider);
        logicEnv = new EnvColliders {
            envColliCfgLst = envColliCfgLst
        };
        logicEnv.Init();
    }

    List<ColliderConfig> GenerateEnvColliCfgs(Transform transEnvRoot) {
        List<ColliderConfig> envColliCfgLst = new List<ColliderConfig>();
        BoxCollider[] boxArr = transEnvRoot.GetComponentsInChildren<BoxCollider>();
        for(int i = 0; i < boxArr.Length; i++) {
            Transform trans = boxArr[i].transform;
            var cfg = new ColliderConfig {
                mPos = new PEVector3(trans.position)
            };
            cfg.mSize = new PEVector3(trans.localScale / 2);
            cfg.mType = ColliderType.Box;
            cfg.mAxis = new PEVector3[3];
            cfg.mAxis[0] = new PEVector3(trans.right);
            cfg.mAxis[1] = new PEVector3(trans.up);
            cfg.mAxis[2] = new PEVector3(trans.forward);

            envColliCfgLst.Add(cfg);
        }

        CapsuleCollider[] cylindderArr = transEnvRoot.GetComponentsInChildren<CapsuleCollider>();
        for(int i = 0; i < cylindderArr.Length; i++) {
            Transform trans = cylindderArr[i].transform;
            var cfg = new ColliderConfig {
                mPos = new PEVector3(trans.position)
            };
            cfg.mType = ColliderType.Cylinder;
            cfg.mRadius = (PEInt)(trans.localScale.x / 2);

            envColliCfgLst.Add(cfg);
        }
        return envColliCfgLst;
    }

    private void Update() {
        if(transFollow != null) {
            mapRoot.transCameraRoot.position = transFollow.position;
        }
    }

    public void Tick() {
        //bullet tick
        for(int i = bulletLst.Count - 1; i >= 0; --i) {
            if(bulletLst[i].unitState == SubUnitState.None) {
                bulletLst[i].LogicUnInit();
                bulletLst.RemoveAt(i);
            }
            else {
                bulletLst[i].LogicTick();
            }
        }

        //hero tick
        for(int i = 0; i < heroLst.Count; i++) {
            heroLst[i].LogicTick();
        }

        //tower tick
        for(int i = towerLst.Count - 1; i >= 0; --i) {
            Tower tower = towerLst[i];
            if(tower.unitState != UnitStateEnum.Dead) {
                tower.LogicTick();
            }
            else {
                towerLst[i].LogicUnInit();
                towerLst.RemoveAt(i);
            }
        }

        for(int i = soldierLst.Count - 1; i >= 0; --i) {
            Soldier soldier = soldierLst[i];
            if(soldier.unitState != UnitStateEnum.Dead) {
                soldier.LogicTick();
            }
            else {
                if(soldier.IsTeam(TeamEnum.Blue)) {
                    int index = CalcRule.blueTeamSoldier.IndexOf(soldier);
                    CalcRule.blueTeamSoldier.RemoveAt(index);
                }
                else {
                    int index = CalcRule.redTeamSoldier.IndexOf(soldier);
                    CalcRule.redTeamSoldier.RemoveAt(index);
                }
                soldierLst[i].LogicUnInit();
                soldierLst.RemoveAt(i);
            }
        }

        //global timer
        //timer tick
        for(int i = timerLst.Count - 1; i >= 0; --i) {
            LogicTimer timer = timerLst[i];
            if(timer.IsActive) {
                timer.TickTimer();
            }
            else {
                timerLst.RemoveAt(i);
            }
        }
    }

    public void InitCamFollowTrans(int posIndex) {
        transFollow = heroLst[posIndex].mainViewUnit.transform;
    }

    public void AddBullet(Bullet bullet) {
        bulletLst.Add(bullet);
    }

    public void InputKey(List<OpKey> keyLst) {
        for(int i = 0; i < keyLst.Count; i++) {
            OpKey key = keyLst[i];
            MainLogicUnit hero = heroLst[key.opIndex];
            hero.InputKey(key);
        }
    }

    public MainLogicUnit GetSelfHero(int posIndex) {
        return heroLst[posIndex];
    }

    public List<PEColliderBase> GetEnvColliders() {
        return logicEnv.GetEnvColliders();
    }
    public bool CanReleaseSkill(int posIndex, int skillID) {
        return heroLst[posIndex].CanReleaseSkill(skillID);
    }
    public bool IsForbidReleaseSkill(int posIndex) {
        return heroLst[posIndex].IsForbidReleaseSkill();
    }
    public bool CanMove(int posIndex) {
        return heroLst[posIndex].CanMove();
    }
}
