/*************************************************
	作者: Plane
	邮箱: 1785275942@qq.com
	日期: 2021/03/06 2:09
	功能: 对战进行

    //=================*=================\\
           教学官网：www.qiqiker.com
           关注微信服务号: qiqikertuts
           关注微信公众号: PlaneZhong
               ~~获取更多教学资讯~~
    \\=================*=================//
*************************************************/

using HOKProtocol;
using System;
using System.Collections.Generic;
using System.Text;

namespace HOKServer {
    public class RoomStateFight : RoomStateBase {
        uint frameID = 0;
        List<OpKey> opkeyLst = new List<OpKey>();
        int checkTaskID;

        private bool[] endArr;
        public RoomStateFight(PVPRoom room) : base(room) {
            int len = room.sessionArr.Length;
            endArr = new bool[len];
        }

        public override void Enter() {
            opkeyLst.Clear();
            checkTaskID = TimerSvc.Instance.AddTask(ServerConfig.ServerLogicFrameIntervelMs, SyncLogicFrame, null, 0);
        }

        void SyncLogicFrame(int tid) {
            ++frameID;
            HOKMsg msg = new HOKMsg {
                cmd = CMD.NtfOpKey,
                isEmpty = true,
                ntfOpKey = new NtfOpKey {
                    frameID = frameID,
                    keyList = new List<OpKey>()
                }
            };

            int count = opkeyLst.Count;
            if(count > 0) {
                msg.isEmpty = false;
                msg.ntfOpKey.keyList.AddRange(opkeyLst);
            }
            opkeyLst.Clear();
            room.BroadcastMsg(msg);
        }

        public override void Exit() {
            checkTaskID = 0;
            opkeyLst.Clear();
            endArr = null;
        }

        public override void Update() { }

        public void UpdateOpKey(OpKey key) {
            opkeyLst.Add(key);
        }

        public void UpdateEndState(int posIndex) {
            endArr[posIndex] = true;

            if(TimerSvc.Instance.DeleteTask(checkTaskID)) {
                this.ColorLog(PEUtils.LogColor.Green, "Delete Sync Task Success.");
            }
            else {
                this.Warn("Delete Sync Task Failed.");
            }
            room.ChangeRoomState(RoomStateEnum.End);
        }
    }
}
