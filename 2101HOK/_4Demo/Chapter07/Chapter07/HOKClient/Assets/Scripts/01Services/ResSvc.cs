/*************************************************
	作者: Plane
	邮箱: 1785275942@qq.com
	日期: 2021/02/09 21:22
	功能: 资源服务

    //=================*=================\\
           Plane老师微信: PlaneZhong
           关注微信服务号: qiqikertuts
               ~~获取更多教学资讯~~
    \\=================*=================//
*************************************************/

using PEMath;
using PEPhysx;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResSvc : MonoBehaviour {
    public static ResSvc Instance;

    public void InitSvc() {
        Instance = this;
        this.Log("Init ResSvc Done.");
    }

    private Action prgCB = null;
    public void AsyncLoadScene(string sceneName, Action<float> loadRate, Action loaded) {
        AsyncOperation sceneAsync = SceneManager.LoadSceneAsync(sceneName);

        prgCB = () => {
            float progress = sceneAsync.progress;
            loadRate?.Invoke(progress);
            if(progress == 1) {
                loaded?.Invoke();
                prgCB = null;
                sceneAsync = null;
            }
        };
    }

    private void Update() {
        prgCB?.Invoke();
    }

    private Dictionary<string, GameObject> goDic = new Dictionary<string, GameObject>();
    public GameObject LoadPrefab(string path, bool cache = false) {
        GameObject prefab = null;
        if(!goDic.TryGetValue(path, out prefab)) {
            prefab = Resources.Load<GameObject>(path);
            if(cache) {
                goDic.Add(path, prefab);
            }
        }

        GameObject go = null;
        if(prefab != null) {
            go = Instantiate(prefab);
        }
        return go;
    }

    private Dictionary<string, AudioClip> adDic = new Dictionary<string, AudioClip>();
    public AudioClip LoadAudio(string path, bool cache = false) {
        AudioClip au = null;
        if(!adDic.TryGetValue(path, out au)) {
            au = Resources.Load<AudioClip>(path);
            if(cache) {
                adDic.Add(path, au);
            }
        }
        return au;
    }

    private Dictionary<string, Sprite> spDic = new Dictionary<string, Sprite>();
    public Sprite LoadSprite(string path, bool cache = false) {
        Sprite sp = null;
        if(!spDic.TryGetValue(path, out sp)) {
            sp = Resources.Load<Sprite>(path);
            if(cache) {
                spDic.Add(path, sp);
            }
        }
        return sp;
    }

    public Buff CreateBuff(MainLogicUnit source, MainLogicUnit owner, Skill skill, int buffID, object[] args) {
        BuffCfg cfg = GetBuffConfigByID(buffID);
        switch(cfg.buffType) {
            case BuffTypeEnum.MoveAttack:
                return new MoveAttackBuff(source, owner, skill, buffID, args);
            case BuffTypeEnum.MoveSpeed_Single:
                return new MoveSpeedBuff_Single(source, owner, skill, buffID, args);
            case BuffTypeEnum.MoveSpeed_DynamicGroup:
                return new MoveSpeedBuff_DynamicGroup(source, owner, skill, buffID, args);
            case BuffTypeEnum.ModifySkill:
                return new CommonModifySkillBuff(source, owner, skill, buffID, args);
            case BuffTypeEnum.Silense:
                return new SilenseBuff_Single(source, owner, skill, buffID, args);
            case BuffTypeEnum.ArthurMark:
                return new ArthurMarkBuff(source, owner, skill, buffID, args);
            case BuffTypeEnum.HPCure:
                return new HPCureBuff_Single(source, owner, skill, buffID, args);
            //TOADD
            case BuffTypeEnum.None:
            default:
                this.Error("Create Buff Failed,BuffID:" + buffID);
                return null;
        }
    }

    public UnitCfg GetUnitConfigByID(int unitID) {
        switch(unitID) {
            case 101:
                return new UnitCfg {
                    unitID = 101,
                    unitName = "亚瑟",
                    resName = "arthur",

                    hp = 6500,
                    def = 0,
                    moveSpeed = 5,
                    colliCfg = new ColliderConfig {
                        mType = ColliderType.Cylinder,
                        mRadius = (PEInt)0.5f
                    },
                    pasvBuff = new int[] { 10100 },
                    skillArr = new int[] { 1010, 1011, 1012, 1013 }
                };
            case 102:
                return new UnitCfg {
                    unitID = 102,
                    unitName = "后羿",
                    resName = "houyi",

                    hp = 3500,
                    def = 10,
                    moveSpeed = 5,
                    colliCfg = new ColliderConfig {
                        mType = ColliderType.Cylinder,
                        mRadius = (PEInt)0.5f
                    },
                    skillArr = new int[] { 1020, 1021, 1022, 1023 }
                };
        }
        return null;
    }
    public BuffCfg GetBuffConfigByID(int buffID) {
        switch(buffID) {
            case 10100:
                return ResBuffConfigs.buff_10100;
            case 10110://移速加速
                return ResBuffConfigs.buff_10110;
            case 10111:
                return ResBuffConfigs.buff_10111;
            case 10140:
                return ResBuffConfigs.buff_10140;
            case 10141:
                return ResBuffConfigs.buff_10141;
            case 10142:
                return ResBuffConfigs.buff_10142;
            case 90000:
                return ResBuffConfigs.buff_90000;
            default:
                break;
        }
        this.Error("Get Buff Config Failed,buffID:" + buffID);
        return null;
    }
    public SkillCfg GetSkillConfigByID(int skillID) {
        switch(skillID) {
            case 1010:
                return ResSkillConfigs.sk_1010;
            case 1011:
                return ResSkillConfigs.sk_1011;
            case 1012:
                return ResSkillConfigs.sk_1012;
            case 1013:
                return ResSkillConfigs.sk_1013;
            case 1014:
                return ResSkillConfigs.sk_1014;
            case 1020:
                return ResSkillConfigs.sk_1020;
            case 1021:
                return ResSkillConfigs.sk_1021;
            case 1022:
                return ResSkillConfigs.sk_1022;
            case 1023:
                return ResSkillConfigs.sk_1023;
            default:
                this.Error("Get Skill Config Failed,skillID:" + skillID);
                return null;
        }
    }
    public MapCfg GetMapConfigByID(int mapID) {
        switch(mapID) {
            case 101:
                return new MapCfg {
                    mapID = 101,
                    blueBorn = new PEVector3(-27, 0, 0),
                    redBorn = new PEVector3(27, 0, 0),
                    bornDelay = 15000,
                    bornInterval = 2000,
                    waveInterval = 50000
                };
            case 102:
                return new MapCfg {
                    mapID = 102,
                    blueBorn = new PEVector3(-5, 0, 0),
                    redBorn = new PEVector3(5, 0, 0),
                    bornDelay = 15000,
                    bornInterval = 2000,
                    waveInterval = 50000
                };
            default:
                return null;
        }
    }
}
