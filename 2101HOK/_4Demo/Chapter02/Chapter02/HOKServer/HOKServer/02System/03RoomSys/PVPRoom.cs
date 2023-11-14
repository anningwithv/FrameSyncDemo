/*************************************************
	作者: Plane
	邮箱: 1785275942@qq.com
	日期: 2021/02/26 17:24
	功能: 对战房间

    //=================*=================\\
           教学官网：www.qiqiker.com
           关注微信公众号: PlaneZhong
           关注微信服务号: qiqikertuts           
               ~~获取更多教学资讯~~
    \\=================*=================//
*************************************************/

using HOKProtocol;
using System;
using System.Collections.Generic;
using System.Text;

namespace HOKServer {
    public class PVPRoom {
        public uint roomID;
        public PVPEnum pvpEnum = PVPEnum.None;
        public ServerSession[] sessionArr;

        public PVPRoom(uint roomID, PVPEnum pvpEnum, ServerSession[] sessionArr) {
            this.roomID = roomID;
            this.pvpEnum = pvpEnum;
            this.sessionArr = sessionArr;
        }
    }
}
