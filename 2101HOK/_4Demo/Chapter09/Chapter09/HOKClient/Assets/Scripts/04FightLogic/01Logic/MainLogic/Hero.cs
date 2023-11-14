/*************************************************
	作者: Plane
	邮箱: 1785275942@qq.com	
	功能: 英雄单位

    //=================*=================\\
           教学官网：www.qiqiker.com
           官方微信服务号: qiqikertuts
           Plane老师微信: PlaneZhong
               ~~获取更多教学资讯~~
    \\=================*=================//
*************************************************/

public class Hero : MainLogicUnit {
    public int heroID;
    public int posIndex;
    public string userName;//玩家名字

    public Hero(HeroData hd) : base(hd) {
        heroID = hd.heroID;
        posIndex = hd.posIndex;
        userName = hd.userName;

        unitType = UnitTypeEnum.Hero;
        unitName = ud.unitCfg.unitName + "_" + userName;

        pathPrefix = "ResChars";
    }

    bool setRevive;
    public override void LogicTick() {
        base.LogicTick();

        if(unitState == UnitStateEnum.Dead && setRevive == false) {
            setRevive = true;
            //更新击杀数据
            if(IsTeam(TeamEnum.Blue)) {
                BattleSys.Instance.SetKillData(TeamEnum.Red);
            }
            else {
                BattleSys.Instance.SetKillData(TeamEnum.Blue);
            }

            if(IsPlayerSelf()) {
                BattleSys.Instance.SetReviveState(true, 5);
            }

            CreateLogicTimer(() => {
                setRevive = false;

                if(IsPlayerSelf()) {
                    BattleSys.Instance.SetReviveState(false);
                }
                unitState = UnitStateEnum.Alive;

                isDirChanged = true;
                LogicPos = ud.bornPos;
                mainViewUnit.ForcePosSync();
                ResetHP();
            }, 5000);
        }
    }

    #region API Functions
    public override bool IsPlayerSelf() {
        return posIndex == GameRoot.Instance.SelfIndex;
    }

    public override bool Equals(MainLogicUnit mainLogicUnit) {
        if(mainLogicUnit.unitType == unitType) {
            Hero hero = mainLogicUnit as Hero;
            return posIndex == hero.posIndex;
        }
        else {
            return false;
        }
    }
    #endregion
}
