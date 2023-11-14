﻿/*************************************************
	作者: Plane
	邮箱: 1785275942@qq.com	
	功能: 技能配置数据

    //=================*=================\\
           教学官网：www.qiqiker.com
           官方微信服务号: qiqikertuts
           Plane老师微信: PlaneZhong
               ~~获取更多教学资讯~~
    \\=================*=================//
*************************************************/

public class ResSkillConfigs {
    #region Arthur技能配置
    /// <summary>
    /// Arthur普攻
    /// </summary>
    public static SkillCfg sk_1010 = new SkillCfg {
        skillID = 1010,
        iconName = null,
        aniName = "atk",
        releaseMode = ReleaseModeEnum.None,
        //最近的敌方目标
        targetCfg = new TargetCfg {
            targetTeam = TargetTeamEnum.Enemy,
            selectRule = SelectRuleEnum.TargetClosestSingle,
            targetTypeArr = new UnitTypeEnum[] {
                UnitTypeEnum.Hero,
                UnitTypeEnum.Soldier,
                UnitTypeEnum.Tower
            },
            selectRange = 2f,
            searchDis = 10f,
        },
        bulletCfg = null,
        cdTime = 0,
        spellTime = 800,
        isNormalAttack = true,
        skillTime = 1400,
        damage = 45,

        buffIDArr = null,
        audio_start = "arthur_ska_rls",
        audio_work = null,
        audio_hit = "arthur_ska_hit"
    };

    /// <summary>
    /// Arthur技能1:誓约这盾
    /// 1.在接下来的3秒内提升30%的移速
    /// 2.强化下一次普通攻击,增加其伤害
    /// 3.沉默命中目标1.5秒
    /// 4.同时标记目标，持续5秒。技能和普攻会对标记目标可额外造成目标最大生命1%的伤害，
    /// 5.标记附近的友军会增加10%的移速
    /// </summary>
    public static SkillCfg sk_1011 = new SkillCfg {
        skillID = 1011,
        iconName = "arthur_sk1",
        aniName = null,
        releaseMode = ReleaseModeEnum.Click,
        targetCfg = null,
        cdTime = 5000,
        spellTime = 0,
        isNormalAttack = false,
        skillTime = 0,
        damage = 0,

        //1.加速buff；2.普攻强化Buff（技能修改）
        buffIDArr = new int[] { 10110, 10111 },
        audio_start = "arthur_sk1_rls",
    };
    /// <summary>
    /// Arthur技能2:回旋打击
    /// 召唤持续5秒的圣盾对周围目标每秒造成50点伤害
    /// </summary>
    public static SkillCfg sk_1012 = new SkillCfg {
        skillID = 1012,
        iconName = "arthur_sk2",
        aniName = null,
        releaseMode = ReleaseModeEnum.Click,
        targetCfg = null,
        spellTime = 0,
        cdTime = 5000,
        isNormalAttack = false,
        skillTime = 0,
        damage = 0,

        //1.范围伤害buff
        buffIDArr = new int[] { 10120 },

        audio_start = "arthur_sk2_rls",
    };
    /// <summary>
    /// Arthur技能3：圣剑裁决
    /// 跃向目标英雄
    /// 造成其最大生命12%的伤害
    /// 并会将范围内的敌人击飞0.5秒
    /// 在目标区域留下的圣印将持续5秒对敌人造成伤害。
    /// </summary>
    public static SkillCfg sk_1013 = new SkillCfg {
        skillID = 1013,
        iconName = "arthur_sk3",
        aniName = "sk3",
        releaseMode = ReleaseModeEnum.Click,
        targetCfg = new TargetCfg {
            targetTeam = TargetTeamEnum.Enemy,
            selectRule = SelectRuleEnum.TargetClosestSingle,
            targetTypeArr = new UnitTypeEnum[] {
                UnitTypeEnum.Hero
            },
            selectRange = 4f,
            searchDis = 10f,
        },
        cdTime = 10000,
        spellTime = 250,
        isNormalAttack = false,
        skillTime = 1300,
        damage = 0,

        //10130：目标移动buff
        //10131：百分比血量斩杀buff
        //10132：击飞buff
        //10133：范围伤害buff（静态位置）
        buffIDArr = new int[] { 10130, 10131, 10132, 10133 },

        audio_start = "arthur_sk3_rls",
    };
    /// <summary>
    /// Arthur强化版普攻，用于替换技能
    /// 3.沉默命中目标1.5秒
    /// 4.同时标记目标，持续5秒。技能和普攻会对标记目标可额外造成目标最大生命1%的伤害，
    /// 5.标记附近的友军会增加10%的移速
    /// </summary>
    public static SkillCfg sk_1014 = new SkillCfg {
        skillID = 1010,
        aniName = "sk1_atk",
        releaseMode = ReleaseModeEnum.None,
        //最近的敌方目标
        targetCfg = new TargetCfg {
            targetTeam = TargetTeamEnum.Enemy,
            selectRule = SelectRuleEnum.TargetClosestSingle,
            targetTypeArr = new UnitTypeEnum[] {
                UnitTypeEnum.Hero,
                UnitTypeEnum.Soldier,
                UnitTypeEnum.Tower
            },
            selectRange = 2f,
            searchDis = 10f,
        },
        bulletCfg = null,
        cdTime = 0,
        spellTime = 800,//施法时间（技能前摇）
        isNormalAttack = true,
        skillTime = 1400,
        damage = 90,
        //沉默buff
        buffIDArr = new int[] { 10140, 10141, 10142 },

        //audio
        audio_start = null,
        audio_work = null,
        audio_hit = "arthur_sk1_hit",
    };
    #endregion

    #region Houyi技能
    /// <summary>
    /// Houyi普攻技能
    /// </summary>
    public static SkillCfg sk_1020 = new SkillCfg {
        skillID = 1020,
        iconName = null,
        aniName = "atk",
        releaseMode = ReleaseModeEnum.None,
        //最近的敌方目标
        targetCfg = new TargetCfg {
            targetTeam = TargetTeamEnum.Enemy,
            selectRule = SelectRuleEnum.TargetClosestSingle,
            targetTypeArr = new UnitTypeEnum[] {
                UnitTypeEnum.Hero,
                UnitTypeEnum.Tower,
                UnitTypeEnum.Soldier,
            },
            selectRange = 5f,
            searchDis = 15f,
        },
        bulletCfg = new BulletCfg {
            bulletType = BulletTypeEnum.SkillTarget,
            bulletName = "后羿普攻子弹",
            resPath = "houyi_ska_bullet",
            bulletSpeed = 1f,
            bulletSize = 0.1f,
            bulletHeight = 1.5f,
            bulletOffset = 0.5f,
            bulletDelay = 0,
        },
        cdTime = 0,
        spellTime = 550,//施法时间（技能前摇）
        isNormalAttack = true,
        skillTime = 1400,
        damage = 50,

        buffIDArr = null,
        audio_start = null,
        audio_work = "houyi_ska_rls",
        audio_hit = "com_hit2"
    };
    /// <summary>
    /// Houyi1技能：多重箭矢
    /// 5秒内强化普攻，造成高额伤害，同时对面前区域内另外两个敌人也发射箭矢，造成50%伤害
    /// </summary>
    public static SkillCfg sk_1021 = new SkillCfg {
        skillID = 1021,
        iconName = "houyi_sk1",
        releaseMode = ReleaseModeEnum.Click,
        aniName = null,
        targetCfg = null,
        bulletCfg = null,
        cdTime = 5000,//ms
        spellTime = 0,
        isNormalAttack = false,
        skillTime = 0,
        damage = 0,
        //普攻强化buff（技能修改）
        buffIDArr = new int[] { 10210 },

        audio_start = "houyi_sk1_rls"
    };
    /// <summary>
    /// Houyi2技能：落日余晖
    /// 在指定区域召唤激光，造成范围伤害和30%减速，持续2秒，
    /// 对范围中心敌人造成50%减速和额外50%伤害
    /// </summary>
    public static SkillCfg sk_1022 = new SkillCfg {
        skillID = 1022,
        iconName = "houyi_sk2",
        aniName = "sk2",
        isNormalAttack = false,
        releaseMode = ReleaseModeEnum.Postion,
        targetCfg = new TargetCfg {
            targetTeam = TargetTeamEnum.Dynamic,//动态目标
            selectRange = 6,//动态施法范围，纯自身buff技能这个数值为0
        },

        cdTime = 5000,
        spellTime = 630,//施法时间（技能前摇）
        skillTime = 1200,
        damage = 0,

        buffIDArr = new int[] { 10220, 10221, 10222, 10223 },

        audio_start = "houyi_sk2_rls",
        audio_work = null,
        audio_hit = null
    };
    /// <summary>
    /// Houyi3技能：灼日之矢
    /// 向指定方向释放火焰箭，命中敌方英雄时将造成眩晕效果和范围伤害，
    /// 眩晕时长取决于技能飞行距离，最多眩晕3.5秒
    /// </summary>
    public static SkillCfg sk_1023 = new SkillCfg {
        skillID = 1023,
        iconName = "houyi_sk3",
        aniName = "sk3",
        releaseMode = ReleaseModeEnum.Direction,
        targetCfg = null,
        bulletCfg = new BulletCfg {
            bulletType = BulletTypeEnum.UIDirection,//技能锁定的目标
            bulletName = "后羿大招-灼日之矢",
            resPath = "houyi_sk3_bullet",
            bulletSpeed = 1f,
            bulletSize = 0.5f,
            bulletHeight = 1.5f,
            bulletOffset = 1f,
            bulletDelay = 0,

            canBlock = true,
            //受影响的目标
            impacter = new TargetCfg {
                targetTeam = TargetTeamEnum.Enemy,
                selectRule = SelectRuleEnum.Hero,
                targetTypeArr = new UnitTypeEnum[] { UnitTypeEnum.Hero },
            },
            bulletDuration = 5000,//确保不击中目标的情况下能飞出地图
        },
        cdTime = 8000,
        spellTime = 230,//施法时间（技能前摇）
        isNormalAttack = false,
        skillTime = 800,
        damage = 0,
        buffIDArr = new int[] { 10230, 10231 },

        audio_start = "houyi_sk3_rls",
        audio_work = null,
        audio_hit = "houyi_sk3_hit",//技能命中后，命中目标播放音效
    };
    //1技能强化普攻为散射射击
    public static SkillCfg sk_1024 = new SkillCfg {
        skillID = 1024,
        iconName = null,
        aniName = "atk",
        releaseMode = ReleaseModeEnum.None,
        //最近的敌方目标
        targetCfg = new TargetCfg {
            targetTeam = TargetTeamEnum.Enemy,
            selectRule = SelectRuleEnum.TargetClosestSingle,
            targetTypeArr = new UnitTypeEnum[] {
                //散射普攻目标可以是所有，散射子弹的目标通过buff里的目标来配置
                UnitTypeEnum.Hero,
                UnitTypeEnum.Soldier,
                UnitTypeEnum.Tower,
            },
            selectRange = 5f,
            searchDis = 15f,
        },
        bulletCfg = new BulletCfg {
            bulletType = BulletTypeEnum.SkillTarget,//技能锁定的目标
            bulletName = "后羿1技能强化普攻子弹",
            resPath = "houyi_ska_bullet_skenhance",
            bulletSpeed = 1f,
            bulletSize = 0.1f,
            bulletHeight = 1.5f,
            bulletOffset = 0.5f,
            bulletDelay = 0,
        },
        cdTime = 0,
        spellTime = 550,//施法时间（技能前摇）
        isNormalAttack = true,
        skillTime = 1400,
        damage = 40,
        buffIDArr = new int[] { 10240 },

        audio_start = null,
        audio_work = "houyi_ska_rls",
        audio_hit = "com_hit2"
    };
    //被动强化普攻为多重射击
    public static SkillCfg sk_1025 = new SkillCfg {
        skillID = 1025,
        iconName = null,
        aniName = "atk",
        releaseMode = ReleaseModeEnum.None,
        //最近的敌方目标
        targetCfg = new TargetCfg {
            targetTeam = TargetTeamEnum.Enemy,
            selectRule = SelectRuleEnum.TargetClosestSingle,
            targetTypeArr = new UnitTypeEnum[] {
                //散射普攻目标可以是所有，散射子弹的目标通过buff里的目标来配置
                UnitTypeEnum.Hero,
                UnitTypeEnum.Soldier,
                UnitTypeEnum.Tower,
            },
            selectRange = 5f,
            searchDis = 15f,
        },
        bulletCfg = new BulletCfg {
            bulletType = BulletTypeEnum.SkillTarget,//技能锁定的目标
            bulletName = "后羿被动强化普攻子弹",
            resPath = "houyi_ska_bullet_edenhance",
            bulletSpeed = 1f,
            bulletSize = 0.1f,
            bulletHeight = 1.5f,
            bulletOffset = 0.5f,
            bulletDelay = 0,
        },
        cdTime = 0,
        spellTime = 550,//施法时间（技能前摇）
        isNormalAttack = true,
        skillTime = 1400,
        damage = 20,
        buffIDArr = new int[] { 10250 },

        audio_start = null,
        audio_work = "houyi_ska_multiarrow",
        audio_hit = "houyi_multi_hit"
    };

    //技能强化与被动强化混合
    public static SkillCfg sk_1026 = new SkillCfg {
        skillID = 1024,
        iconName = null,
        aniName = "atk",
        releaseMode = ReleaseModeEnum.None,
        //最近的敌方目标
        targetCfg = new TargetCfg {
            targetTeam = TargetTeamEnum.Enemy,
            selectRule = SelectRuleEnum.TargetClosestSingle,
            targetTypeArr = new UnitTypeEnum[] {
                //散射普攻目标可以是所有，散射子弹的目标通过buff里的目标来配置
                UnitTypeEnum.Hero,
                UnitTypeEnum.Soldier,
                UnitTypeEnum.Tower,
            },
            selectRange = 5f,
            searchDis = 15f,
        },
        bulletCfg = new BulletCfg {
            bulletType = BulletTypeEnum.SkillTarget,//技能锁定的目标
            bulletName = "后羿技能强化普攻子弹",
            resPath = "houyi_ska_bullet_edskmixed",
            bulletSpeed = 1f,
            bulletSize = 0.1f,
            bulletHeight = 1.5f,
            bulletOffset = 0.5f,
            bulletDelay = 0,
        },
        cdTime = 0,
        spellTime = 550,//施法时间（技能前摇）
        isNormalAttack = true,
        skillTime = 1400,
        damage = 100,
        buffIDArr = new int[] { 10260 },

        audio_start = null,
        audio_work = "houyi_ska_multiarrow",
        audio_hit = "houyi_multi_hit"
    };
    #endregion
}
