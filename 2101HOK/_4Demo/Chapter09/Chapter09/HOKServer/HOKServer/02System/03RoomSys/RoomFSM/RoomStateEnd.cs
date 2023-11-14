/*************************************************
	作者: Plane
	邮箱: 1785275942@qq.com
	日期: 2021/03/06 2:10
	功能: 战斗结束

    //=================*=================\\
           教学官网：www.qiqiker.com
           关注微信服务号: qiqikertuts
           关注微信公众号: PlaneZhong
               ~~获取更多教学资讯~~
    \\=================*=================//
*************************************************/

using HOKProtocol;

namespace HOKServer {
    public class RoomStateEnd : RoomStateBase {
        public RoomStateEnd(PVPRoom room) : base(room) {
        }

        public override void Enter() {
            HOKMsg msg = new HOKMsg {
                cmd = CMD.RspBattleEnd,
                rspBattleEnd = new RspBattleEnd {
                    //TOADD
                }
            };

            room.BroadcastMsg(msg);
            Exit();
        }

        public override void Exit() {
            RoomSys.Instance.DestroyRoom(room.roomID);
        }

        public override void Update() {
        }
    }
}
