﻿/*************************************************
	作者: Plane
	邮箱: 1785275942@qq.com
	日期: 2021/03/08 4:23
	功能: 战斗界面

    //=================*=================\\
           教学官网：www.qiqiker.com
           关注微信服务号: qiqikertuts
           Plane老师微信: PlaneZhong
               ~~获取更多教学资讯~~
    \\=================*=================//
*************************************************/

using PEMath;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using HOKProtocol;

public partial class PlayWnd : WindowRoot {
    public Image imgCancelSkill;
    public Image imgTouch;
    public Image imgDirBg;
    public Image imgDirPoint;
    public Transform ArrowRoot;

    public Text txtTime;
    public Text txtBlueKill;
    public Text txtRedKill;
    public Text txtIncome;
    public Text txtFPS;
    public Text txtDelay;
    public Text txtChat;
    public InputField iptChat;
    public Image imgCover;
    public Text txtRevieTime;
    public int maxChatLen;
    public int maxChatCount;
    public float chatShowTime;

    float pointDis = 135;
    Vector2 startPos = Vector2.zero;
    Vector2 defaultPos = Vector2.zero;
    List<ChatItem> chatLst;
    int blueTeamKillCount;
    int redTeamKillCount;

    protected override void InitWnd() {
        base.InitWnd();

        SetActive(imgCancelSkill, false);
        chatLst = new List<ChatItem>();
        txtChat.text = "";
        iptChat.text = "";
        SetActive(txtChat, false);
        SetActive(iptChat, false);
        SetActive(imgCover, false);
        blueTeamKillCount = 0;
        redTeamKillCount = 0;
        SetText(txtBlueKill, blueTeamKillCount);
        SetText(txtRedKill, redTeamKillCount);

        SetActive(ArrowRoot, false);
        pointDis = Screen.height * 1.0f / ClientConfig.ScreenStandardHeight * ClientConfig.ScreenOPDis;
        defaultPos = imgDirBg.transform.position;

        RegisterMoveEvts();

        InitMiniMap();
    }

    protected override void UnInitWnd() {
        base.UnInitWnd();
        UnInitMiniMap();
        if(chatLst != null) {
            chatLst.Clear();
            chatLst = null;
        }
    }

    void RegisterMoveEvts() {
        SetActive(ArrowRoot, false);

        OnClickDown(imgTouch.gameObject, (PointerEventData evt, object[] args) => {
            startPos = evt.position;
            //Debug.Log($"evt.pos:{evt.position}");
            imgDirPoint.color = new Color(1, 1, 1, 1f);
            imgDirBg.transform.position = evt.position;
        });
        OnClickUp(imgTouch.gameObject, (PointerEventData evt, object[] args) => {
            imgDirBg.transform.position = defaultPos;
            imgDirPoint.color = new Color(1, 1, 1, 0.5f);
            imgDirPoint.transform.localPosition = Vector2.zero;
            SetActive(ArrowRoot, false);

            InputMoveKey(Vector2.zero);
        });
        OnDrag(imgTouch.gameObject, (PointerEventData evt, object[] args) => {
            Vector2 dir = evt.position - startPos;
            float len = dir.magnitude;
            if(len > pointDis) {
                Vector2 clampDir = Vector2.ClampMagnitude(dir, pointDis);
                imgDirPoint.transform.position = startPos + clampDir;
            }
            else {
                imgDirPoint.transform.position = evt.position;
            }

            if(dir != Vector2.zero) {
                SetActive(ArrowRoot);
                float angle = Vector2.SignedAngle(new Vector2(1, 0), dir);
                ArrowRoot.localEulerAngles = new Vector3(0, 0, angle);
            }

            InputMoveKey(dir.normalized);
        });
    }

    private Vector2 lastKeyDir = Vector2.zero;
    private void Update() {
        /*
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector2 keyDir = new Vector2(h, v);
        if(keyDir != lastKeyDir) {
            if(h != 0 || v != 0) {
                keyDir = keyDir.normalized;
            }
            InputMoveKey(keyDir);
            lastKeyDir = keyDir;
        }
        */

        UpdateSkillWnd();
        float delta = Time.deltaTime;
        UpdateChatLst(delta);
        UpdateFPS(delta);
        UpdateTime(delta);
        UpdateImgInfo(delta);
        UpdatePingMS(root.NetDelay);
    }

    private Vector2 lastStickDir = Vector2.zero;
    private void InputMoveKey(Vector2 dir) {
        if(!dir.Equals(lastStickDir)) {
            Vector3 dirVector3 = new Vector3(dir.x, 0, dir.y);
            dirVector3 = Quaternion.Euler(0, 45, 0) * dirVector3;
            PEVector3 logicDir = PEVector3.zero;
            if(dir != Vector2.zero) {
                isUIInput = true;
                logicDir.x = (PEInt)dirVector3.x;
                logicDir.y = (PEInt)dirVector3.y;
                logicDir.z = (PEInt)dirVector3.z;
            }
            else {
                isUIInput = false;
            }

            bool isSend = BattleSys.Instance.SendMoveKey(logicDir);
            if(isSend) {
                lastStickDir = dir;
            }
        }
    }

    bool isUIInput = false;
    public bool IsUIInput() {
        return isUIInput;
    }


    public void EnterCDState(int skillID, int cdTime) {
        if(skaItem.CheckSkillID(skillID)) {
            skaItem.EnterCDState(cdTime);
        }
        else if(sk1Item.CheckSkillID(skillID)) {
            sk1Item.EnterCDState(cdTime);
        }
        else if(sk2Item.CheckSkillID(skillID)) {
            sk2Item.EnterCDState(cdTime);
        }
        else if(sk3Item.CheckSkillID(skillID)) {
            sk3Item.EnterCDState(cdTime);
        }
        else {
            this.Error($"skill id{skillID} enter cd error.");
        }
    }


    #region Battle Info
    public void ClickChatBtn() {
        if(iptChat.gameObject.activeSelf) {
            iptChat.gameObject.SetActive(false);

            string chatStr = iptChat.text;
            MainLogicUnit selfHero = BattleSys.Instance.GetSelfHero();
            string userName = (selfHero as Hero).userName;
            int unitID = selfHero.ud.unitCfg.unitID;
            string heroName = ResSvc.Instance.GetUnitConfigByID(unitID).unitName;

            if(chatStr != null && chatStr != "") {
                if(chatStr.Length >= maxChatLen) {
                    root.ShowTips("最多发送20个字");
                    return;
                }
                if(selfHero.IsTeam(TeamEnum.Blue)) {
                    chatStr = string.Format("<color=#2B91D1>[全部]{0}({1}):</color>{2}", userName, heroName, chatStr);
                }
                else {
                    chatStr = string.Format("<color=#C83535>[全部]{0}({1}):</color>{2}", userName, heroName, chatStr);
                }

                HOKMsg msg = new HOKMsg {
                    cmd = CMD.SndChat,
                    sndChat = new SndChat {
                        roomID = root.RoomID,
                        chatMsg = chatStr
                    }
                };
                netSvc.SendMsg(msg);
                iptChat.text = "";
            }
        }
        else {
            iptChat.gameObject.SetActive(true);
        }
    }

    class ChatItem {
        public float timeCounter;
        public string chatMsg;
    }
    public void AddChatMsg(string msg) {
        while(chatLst.Count > maxChatCount - 1) {
            chatLst.RemoveAt(0);
        }
        chatLst.Add(new ChatItem {
            timeCounter = chatShowTime,
            chatMsg = msg
        });

        RefreshChatUI();
    }

    private void UpdateChatLst(float delta) {
        for(int i = chatLst.Count - 1; i >= 0; --i) {
            float timerCounter = chatLst[i].timeCounter - delta;
            if(timerCounter <= 0) {
                chatLst.RemoveAt(i);
                RefreshChatUI();
            }
            else {
                chatLst[i].timeCounter = timerCounter;
            }
        }
    }

    void RefreshChatUI() {
        if(chatLst.Count > 0) {
            SetActive(txtChat);
            string chatStr = "";
            for(int i = 0; i < chatLst.Count; i++) {
                chatStr += (chatLst[i].chatMsg + "\n");
            }
            txtChat.text = chatStr;
        }
        else {
            SetActive(txtChat, false);
        }
    }

    //Time
    int battleSecondCount;
    float battleTimeCounter;
    void UpdateTime(float delta) {
        battleTimeCounter += delta;
        if(battleTimeCounter >= 1) {
            ++battleSecondCount;
            battleTimeCounter -= 1;
            RefreshBattleTime();
        }
    }
    void RefreshBattleTime() {
        int min = battleSecondCount / 60;
        int sec = battleSecondCount % 60;
        string minStr = min.ToString();
        string secStr = sec.ToString();
        if(min < 10) {
            minStr = "0" + minStr;
        }
        if(sec < 10) {
            secStr = "0" + secStr;
        }
        txtTime.text = minStr + ":" + secStr;
    }

    float frameTimeCount;
    int frameCounter;
    void UpdateFPS(float delta) {
        frameTimeCount += delta;
        ++frameCounter;
        if(frameTimeCount >= 2) {
            txtFPS.text = "FPS " + frameCounter / 2;
            frameTimeCount -= 2;
            frameCounter = 0;
        }
    }

    //coin
    public void RefreshIncome(int income) {
        txtIncome.text = income.ToString();
    }

    //ping
    public void UpdatePingMS(int ms) {
        txtDelay.text = ms + "ms";
    }

    //设置复活
    public void SetReviveTime(bool isRevive, int reviveSec) {
        if(isRevive) {
            if(blueTeamKillCount + redTeamKillCount != 1) {
                BattleSys.Instance.PlayBattleFieldAudio("selfDeath");
            }
            SetActive(imgCover);
            CreateMonoTimer((loopCount) => {
                SetText(txtRevieTime, "复活时间 " + (reviveSec - loopCount) + "秒");
            }, 1000, reviveSec);
        }
        else {
            SetActive(imgCover, false);
        }
    }
    //设置击杀
    public void SetKillData(TeamEnum killHeroTeam) {
        if(killHeroTeam == TeamEnum.Blue) {
            ++blueTeamKillCount;
            SetText(txtBlueKill, blueTeamKillCount);
        }
        else {
            ++redTeamKillCount;
            SetText(txtRedKill, redTeamKillCount);
        }

        if(blueTeamKillCount + redTeamKillCount == 1) {
            BattleSys.Instance.PlayBattleFieldAudio("firstblood");
        }
    }
    #endregion
}
