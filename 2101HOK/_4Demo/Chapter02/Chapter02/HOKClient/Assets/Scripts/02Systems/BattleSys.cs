/*************************************************
	作者: Plane
	邮箱: 1785275942@qq.com
	日期: 2021/02/09 21:29
	功能: 战斗系统

    //=================*=================\\
           关注微信公众号: PlaneZhong
           关注微信服务号: qiqikertuts
               ~~获取更多教学资讯~~
    \\=================*=================//
*************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSys : SysRoot {
    public static BattleSys Instance;

    public override void InitSys() {
        base.InitSys();

        Instance = this;
        this.Log("Init BattleSys Done.");
    }
}
