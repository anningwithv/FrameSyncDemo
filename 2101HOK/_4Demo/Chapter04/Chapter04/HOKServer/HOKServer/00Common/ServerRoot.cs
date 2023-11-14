﻿/*************************************************
	作者: Plane
	邮箱: 1785275942@qq.com
	日期: 2021/02/25 16:02
	功能: 服务器根节点

    //=================*=================\\
           教学官网：www.qiqiker.com
           关注微信公众号: PlaneZhong
           关注微信服务号: qiqikertuts           
               ~~获取更多教学资讯~~
    \\=================*=================//
*************************************************/

using PEUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace HOKServer {
    public class ServerRoot : Singleton<ServerRoot> {
        public override void Init() {
            base.Init();

            //日志
            PELog.InitSettings();

            //服务
            CacheSvc.Instance.Init();
            TimerSvc.Instance.Init();
            NetSvc.Instance.Init();

            //业务
            LoginSys.Instance.Init();
            MatchSys.Instance.Init();
            RoomSys.Instance.Init();

            this.ColorLog(LogColor.Green, "ServerRoot Init Done.");
        }

        public override void Update() {
            base.Update();

            //服务
            CacheSvc.Instance.Update();
            TimerSvc.Instance.Update();
            NetSvc.Instance.Update();

            //业务
            LoginSys.Instance.Update();
            MatchSys.Instance.Update();
            RoomSys.Instance.Update();
        }
    }
}
