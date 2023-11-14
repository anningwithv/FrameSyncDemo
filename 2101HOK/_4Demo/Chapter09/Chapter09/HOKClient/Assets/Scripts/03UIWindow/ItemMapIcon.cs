﻿/*************************************************
	作者: Plane
	邮箱: 1785275942@qq.com	
	功能: 小地图显示Item

    //=================*=================\\
           教学官网：www.qiqiker.com
           官方微信服务号: qiqikertuts
           Plane老师微信: PlaneZhong
               ~~获取更多教学资讯~~
    \\=================*=================//
*************************************************/

using UnityEngine;
using UnityEngine.UI;

public class ItemMapIcon : WindowRoot {
    public float scaler;
    public RectTransform rectTrans;
    public Image imgIcon;
    public Image imgFrame;

    MainLogicUnit unit;
    Vector3 refPos;

    public void InitItem(MainLogicUnit unit, Vector3 refPos) {
        this.unit = unit;
        this.refPos = refPos;

        rectTrans.localEulerAngles = new Vector3(0, 0, -45);
        switch(unit.unitType) {
            case UnitTypeEnum.Hero:
                SetSprite(imgIcon, string.Format("ResImages/PlayWnd/MiniMap/{0}_mapIcon", unit.ud.unitCfg.resName));
                if(unit.IsTeam(TeamEnum.Blue)) {
                    SetSprite(imgFrame, "ResImages/PlayWnd/MiniMap/blueHeroMapFrame");
                }
                else {
                    SetSprite(imgFrame, "ResImages/PlayWnd/MiniMap/redHeroMapFrame");
                }
                imgFrame.SetNativeSize();
                break;
            case UnitTypeEnum.Soldier:
                if(unit.IsTeam(TeamEnum.Blue)) {
                    SetSprite(imgIcon, "ResImages/PlayWnd/MiniMap/blueSoldier_mapIcon");
                }
                else {
                    SetSprite(imgIcon, "ResImages/PlayWnd/MiniMap/redSoldier_mapIcon");
                }
                imgIcon.SetNativeSize();
                break;
            case UnitTypeEnum.Tower:
                if(unit.IsTeam(TeamEnum.Blue)) {
                    switch(unit.ud.unitCfg.unitID) {
                        case 1001:
                            SetSprite(imgIcon, "ResImages/PlayWnd/MiniMap/blueTower");
                            break;
                        case 1002:
                            SetSprite(imgIcon, "ResImages/PlayWnd/MiniMap/blueCrystal");
                            break;
                    }
                }
                else {
                    switch(unit.ud.unitCfg.unitID) {
                        case 2001:
                            SetSprite(imgIcon, "ResImages/PlayWnd/MiniMap/redTower");
                            break;
                        case 2002:
                            SetSprite(imgIcon, "ResImages/PlayWnd/MiniMap/redCrystal");
                            break;
                    }
                }
                imgIcon.SetNativeSize();
                break;
            default:
                this.Error("Unknow unitType.");
                break;
        }
    }

    private void Update() {
        Vector3 offset = unit.LogicPos.ConvertViewVector3() - refPos;
        rectTrans.localPosition = new Vector3(offset.x, offset.z, 0) * scaler;
    }

    public void UnInitItem() {
        unit = null;
        refPos = Vector3.zero;
    }
}
