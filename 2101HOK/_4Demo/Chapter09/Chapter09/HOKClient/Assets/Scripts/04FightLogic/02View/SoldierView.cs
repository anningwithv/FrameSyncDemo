/*************************************************
	作者: Plane
	邮箱: 1785275942@qq.com	
	功能: 小兵视图显示控制 

    //=================*=================\\
           教学官网：www.qiqiker.com
           官方微信服务号: qiqikertuts
           Plane老师微信: PlaneZhong
               ~~获取更多教学资讯~~
    \\=================*=================//
*************************************************/

public class SoldierView : MainViewUnit {
    Soldier soldier;
    public override void Init(LogicUnit logicUnit) {
        base.Init(logicUnit);
        soldier = logicUnit as Soldier;
    }

    protected override void Update() {
        base.Update();

        if(soldier.unitState == UnitStateEnum.Dead) {
            DestroySoldier();
            RmvUIItemInfo();
        }
    }

    void DestroySoldier() {
        Destroy(gameObject, 3f);
    }
}
