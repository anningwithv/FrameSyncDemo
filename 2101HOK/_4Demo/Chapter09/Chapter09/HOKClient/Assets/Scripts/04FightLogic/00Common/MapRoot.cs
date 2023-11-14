/*************************************************
	作者: Plane
	邮箱: 1785275942@qq.com	
	功能: 地图数据根节点

    //=================*=================\\
           教学官网：www.qiqiker.com
           官方微信服务号: qiqikertuts
           Plane老师微信: PlaneZhong
               ~~获取更多教学资讯~~
    \\=================*=================//
*************************************************/

using UnityEngine;

public class MapRoot : MonoBehaviour {
    public Transform transCameraRoot;
    public Transform transEnvCollider;

    public Transform blueTower;
    public Transform redTower;
    public Transform blueCrystal;
    public Transform redCrystal;
    public Transform desBlueTower;
    public Transform desRedTower;
    public Transform desBlueCrystal;
    public Transform desRedCrystal;


    public void DestroyBlueTower() {
        blueTower.gameObject.SetActive(false);
        desBlueTower.gameObject.SetActive(true);
    }

    public void DestroyRedTower() {
        redTower.gameObject.SetActive(false);
        desRedTower.gameObject.SetActive(true);
    }
    public void DestroyBlueCrystal() {
        //blueCrystal.gameObject.SetActive(false);
        desBlueCrystal.gameObject.SetActive(true);
    }
    public void DestroyRedCrystal() {
        //redCrystal.gameObject.SetActive(false);
        desRedCrystal.gameObject.SetActive(true);
    }
}
