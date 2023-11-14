/*************************************************
	作者: Plane
	邮箱: 1785275942@qq.com
	日期: 2021/02/09 21:27
	功能: 登录系统

    //=================*=================\\
           关注微信公众号: PlaneZhong
           关注微信服务号: qiqikertuts
               ~~获取更多教学资讯~~
    \\=================*=================//
*************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginSys : SysRoot {
    public static LoginSys Instance;

    public LoginWnd loginWnd;

    public override void InitSys() {
        base.InitSys();

        Instance = this;
        this.Log("Init LoginSys Done.");
    }

    public void EnterLogin() {
        loginWnd.SetWndState();
        audioSvc.PlayBGMusic(NameDefine.MainCityBGMusic);
    }
}
