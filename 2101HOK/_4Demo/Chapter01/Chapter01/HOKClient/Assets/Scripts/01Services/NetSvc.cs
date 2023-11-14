/*************************************************
	作者: Plane
	邮箱: 1785275942@qq.com
	日期: 2021/02/09 21:18
	功能: 网络服务

    //=================*=================\\
           关注微信公众号: PlaneZhong
           关注微信服务号: qiqikertuts
               ~~获取更多教学资讯~~
    \\=================*=================//
*************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetSvc : MonoBehaviour {
    public static NetSvc Instance;

    public void InitSvc() {
        Instance = this;
        this.Log("Init NetSvc Done.");
    }

}
