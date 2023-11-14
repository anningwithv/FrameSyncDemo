/*************************************************
	作者: Plane
	邮箱: 1785275942@qq.com
	日期: 2021/02/25 16:03
	功能: 网络服务

    //=================*=================\\
           教学官网：www.qiqiker.com
           关注微信公众号: PlaneZhong
           关注微信服务号: qiqikertuts           
               ~~获取更多教学资讯~~
    \\=================*=================//
*************************************************/

using PENet;
using PEUtils;
using HOKProtocol;
using System.Collections.Generic;

namespace HOKServer {
    public class MsgPack {
        public ServerSession session;
        public HOKMsg msg;
        public MsgPack(ServerSession session, HOKMsg msg) {
            this.session = session;
            this.msg = msg;
        }
    }

    public class NetSvc : Singleton<NetSvc> {
        public static readonly string pkgque_lock = "pkgque_lock";
        private KCPNet<ServerSession, HOKMsg> server = new KCPNet<ServerSession, HOKMsg>();
        private Queue<MsgPack> msgPackQue = new Queue<MsgPack>();

        public override void Init() {
            base.Init();
            msgPackQue.Clear();

            KCPTool.LogFunc = this.Log;
            KCPTool.WarnFunc = this.Warn;
            KCPTool.ErrorFunc = this.Error;
            KCPTool.ColorLogFunc = (color, msg) => {
                this.ColorLog((LogColor)color, msg);
            };
#if DEBUG
            server.StartAsServer(ServerConfig.LocalDevInnerIP, ServerConfig.UdpPort);
#else
            server.StartAsServer(ServerConfig.RemoteServerIP, ServerConfig.UdpPort);
#endif
            this.Log("NetSvc Init Done.");
        }

        public void AddMsgQue(ServerSession session, HOKMsg msg) {
            lock(pkgque_lock) {
                msgPackQue.Enqueue(new MsgPack(session, msg));
            }
        }

        public override void Update() {
            base.Update();

            if(msgPackQue.Count > 0) {
                lock(pkgque_lock) {
                    MsgPack msg = msgPackQue.Dequeue();
                    HandoutMsg(msg);
                }
            }
        }

        //消息分发
        private void HandoutMsg(MsgPack pack) {
            switch(pack.msg.cmd) {
                case CMD.ReqLogin:
                    LoginSys.Instance.ReqLogin(pack);
                    break;
                case CMD.ReqMatch:
                    MatchSys.Instance.ReqMatch(pack);
                    break;
                case CMD.SndConfirm:
                    RoomSys.Instance.SndConfirm(pack);
                    break;
                case CMD.SndSelect:
                    RoomSys.Instance.SndSelect(pack);
                    break;
                case CMD.SndLoadPrg:
                    RoomSys.Instance.SndLoadPrg(pack);
                    break;
                case CMD.ReqBattleStart:
                    RoomSys.Instance.ReqBattleStart(pack);
                    break;
                case CMD.SndOpKey:
                    RoomSys.Instance.SndOpKey(pack);
                    break;
                case CMD.SndChat:
                    RoomSys.Instance.SndChat(pack);
                    break;
                case CMD.ReqBattleEnd:
                    RoomSys.Instance.ReqBattleEnd(pack);
                    break;
                case CMD.ReqPing:
                    SyncPingCMD(pack);
                    break;
                case CMD.None:
                default:
                    break;
            }
        }

        private void SyncPingCMD(MsgPack pack) {
            ReqPing req = pack.msg.reqPing;
            HOKMsg msg = new HOKMsg {
                cmd = CMD.RspPing,
                rspPing = new RspPing {
                    pingID = req.pingID
                }
            };
            pack.session.SendMsg(msg);
        }
    }
}
