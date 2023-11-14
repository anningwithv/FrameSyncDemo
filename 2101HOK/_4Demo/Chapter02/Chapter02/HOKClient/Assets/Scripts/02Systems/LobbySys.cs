/*************************************************
	作者: Plane
	邮箱: 1785275942@qq.com
	日期: 2021/02/09 21:29
	功能: 大厅系统 

    //=================*=================\\
           关注微信公众号: PlaneZhong
           关注微信服务号: qiqikertuts
               ~~获取更多教学资讯~~
    \\=================*=================//
*************************************************/

using HOKProtocol;

public class LobbySys : SysRoot {
    public static LobbySys Instance;
    public LobbyWnd lobbyWnd;

    public override void InitSys() {
        base.InitSys();

        Instance = this;
        this.Log("Init LobbySys Done.");
    }

    public void EnterLobby() {
        lobbyWnd.SetWndState();
    }

    public void RspMatch(HOKMsg msg) {
        int predictTime = msg.rspMatch.predictTime;
        lobbyWnd.ShowMatchInfo(true, predictTime);
    }
}
