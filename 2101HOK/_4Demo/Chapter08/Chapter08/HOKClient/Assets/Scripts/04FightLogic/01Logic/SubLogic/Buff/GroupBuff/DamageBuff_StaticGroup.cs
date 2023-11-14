﻿/*************************************************
	作者: Plane
	邮箱: 1785275942@qq.com	
	功能: 静态位置群体伤害

    //=================*=================\\
           教学官网：www.qiqiker.com
           官方微信服务号: qiqikertuts
           Plane老师微信: PlaneZhong
               ~~获取更多教学资讯~~
    \\=================*=================//
*************************************************/
using PEMath;
using UnityEngine;
using System.Collections.Generic;


public class DamageBuffCfg_StaticGroup : BuffCfg {
    public int damage;//每次ticK伤害
}

public class DamageBuff_StaticGroup : Buff {
    PEInt damage;
    public DamageBuff_StaticGroup(MainLogicUnit source, MainLogicUnit owner, Skill skill, int buffID, object[] args = null)
        : base(source, owner, skill, buffID, args) {
    }

    public override void LogicInit() {
        base.LogicInit();
        targetLst = new List<MainLogicUnit>();
        DamageBuffCfg_StaticGroup gdbc = cfg as DamageBuffCfg_StaticGroup;
        damage = gdbc.damage;

        switch(gdbc.staticPosType) {
            case StaticPosTypeEnum.SkillCasterPos:
                LogicPos = source.LogicPos;
                break;
            case StaticPosTypeEnum.SkillLockTargetPos:
                LogicPos = skill.lockTarget.LogicPos;
                break;
            case StaticPosTypeEnum.BulletHitTargetPos:
                LogicPos = (PEVector3)args[1];
                break;
            case StaticPosTypeEnum.UIInputPos:
                LogicPos = source.LogicPos + skill.skillArgs;
                break;
            case StaticPosTypeEnum.None:
            default:
                this.Error("static buff pos error.");
                break;
        }
        #region debug 测试
        //GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        //go.transform.position = LogicPos.ConvertViewVector3();
        //go.transform.localScale = new Vector3(cfg.impacter.selectRange * 2, cfg.impacter.selectRange * 2, cfg.impacter.selectRange * 2);
        #endregion
    }

    protected override void Start() {
        base.Start();
        CalcGroupDamage();
    }

    protected override void Tick() {
        base.Tick();
        CalcGroupDamage();
    }

    void CalcGroupDamage() {
        targetLst.Clear();
        targetLst.AddRange(CalcRule.FindMulipleTargetByRule(owner, cfg.impacter, LogicPos));
        for(int i = 0; i < targetLst.Count; i++) {
            targetLst[i].GetDamageByBuff(damage, this);
        }
    }
}
