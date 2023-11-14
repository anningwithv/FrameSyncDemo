/*************************************************
	作者: Plane
	邮箱: 1785275942@qq.com	
	功能: 战斗结算界面

    //=================*=================\\
           教学官网：www.qiqiker.com
           官方微信服务号: qiqikertuts
           Plane老师微信: PlaneZhong
               ~~获取更多教学资讯~~
    \\=================*=================//
*************************************************/

using HOKProtocol;
using UnityEngine.UI;

public class ResultWnd : WindowRoot {
    public Image imgResult;
    public Image imgPrg;
    public Text txtTime;

    protected override void InitWnd() {
        base.InitWnd();
    }

    public void SetBattleResult(bool isSucc) {
        if(isSucc) {
            audioSvc.PlayUIAudio("victory");
            SetSprite(imgResult, "ResImages/ResultWnd/win");
        }
        else {
            audioSvc.PlayUIAudio("defeat");
            SetSprite(imgResult, "ResImages/ResultWnd/lose");
        }
        imgResult.SetNativeSize();

        CreateMonoTimer(
            (loopCount) => {
                SetText(txtTime, 5 - loopCount);
            },
            1000,
            5,
            (isDelay, loopPrg, allPrg) => {
                imgPrg.fillAmount = allPrg;
            },
            ClickContinueBtn,
            1000);
    }

    public void ClickContinueBtn() {
        if(gameObject.activeSelf) {
            HOKMsg msg = new HOKMsg {
                cmd = CMD.ReqBattleEnd,
                reqBattleEnd = new ReqBattleEnd {
                    roomID = root.RoomID
                }
            };
            netSvc.SendMsg(msg);
        }
    }
}
