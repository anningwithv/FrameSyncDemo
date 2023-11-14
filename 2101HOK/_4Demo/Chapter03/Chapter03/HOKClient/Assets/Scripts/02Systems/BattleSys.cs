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

using HOKProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSys : SysRoot {
    public static BattleSys Instance;
    public LoadWnd loadWnd;
    public PlayWnd playWnd;

    private int mapID;
    public override void InitSys() {
        base.InitSys();

        Instance = this;
        this.Log("Init BattleSys Done.");
    }

    public void EnterBattle() {
        audioSvc.StopBGMusic();
        loadWnd.SetWndState();

        mapID = root.MapID;

        resSvc.AsyncLoadScene("map_" + mapID, SceneLoadProgress, SceneLoadDone);
    }

    int lastPercent = 0;
    void SceneLoadProgress(float val) {
        int percent = (int)(val * 100);
        if(lastPercent != percent) {
            HOKMsg msg = new HOKMsg {
                cmd = CMD.SndLoadPrg,
                sndLoadPrg = new SndLoadPrg {
                    roomID = root.RoomID,
                    percent = percent
                }
            };
            netSvc.SendMsg(msg);
            lastPercent = percent;
        }
    }

    void SceneLoadDone() {
        //TODO
        //初始化UI
        playWnd.SetWndState();
        //加载角色及资源
        //初始化战斗

        HOKMsg msg = new HOKMsg {
            cmd = CMD.ReqBattleStart,
            reqBattleStart = new ReqBattleStart {
                roomID = root.RoomID
            }
        };
        netSvc.SendMsg(msg);
    }

    public void NtfLoadPrg(HOKMsg msg) {
        loadWnd.RefreshPrgData(msg.ntfLoadPrg.percentLst);
    }

    public void RspBattleStart(HOKMsg msg) {
        loadWnd.SetWndState(false);

        audioSvc.PlayBGMusic(NameDefine.BattleBGMusic);
    }
}
