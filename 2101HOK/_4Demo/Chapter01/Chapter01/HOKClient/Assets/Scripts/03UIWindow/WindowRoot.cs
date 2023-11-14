﻿/*************************************************
	作者: Plane
	邮箱: 1785275942@qq.com
	日期: 2021/02/09 21:46
	功能: UI窗口基类

    //=================*=================\\
           关注微信公众号: PlaneZhong
           关注微信服务号: qiqikertuts
               ~~获取更多教学资讯~~
    \\=================*=================//
*************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowRoot : MonoBehaviour {
    protected GameRoot root;
    protected NetSvc netSvc;
    protected ResSvc resSvc;
    protected AudioSvc audioSvc;

    public virtual void SetWndState(bool isActive = true) {
        if(gameObject.activeSelf != isActive) {
            gameObject.SetActive(isActive);
        }
        if(isActive) {
            InitWnd();
        }
        else {
            UnInitWnd();
        }
    }

    protected virtual void InitWnd() {
        root = GameRoot.Instance;
        netSvc = NetSvc.Instance;
        resSvc = ResSvc.Instance;
        audioSvc = AudioSvc.Instance;
    }

    protected virtual void UnInitWnd() {
        root = null;
        netSvc = null;
        resSvc = null;
        audioSvc = null;
    }

    protected void SetActive(GameObject go, bool state = true) {
        go.SetActive(state);
    }
    protected void SetActive(Transform trans, bool state = true) {
        trans.gameObject.SetActive(state);
    }
    protected void SetActive(RectTransform rectTrans, bool state = true) {
        rectTrans.gameObject.SetActive(state);
    }
    protected void SetActive(Image img, bool state = true) {
        img.gameObject.SetActive(state);
    }
    protected void SetActive(Text txt, bool state = true) {
        txt.gameObject.SetActive(state);
    }
    protected void SetActive(InputField ipt, bool state = true) {
        ipt.gameObject.SetActive(state);
    }
}
