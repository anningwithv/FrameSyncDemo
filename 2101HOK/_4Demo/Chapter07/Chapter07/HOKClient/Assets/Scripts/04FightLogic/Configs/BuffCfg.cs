/*************************************************
	作者: Plane
	邮箱: 1785275942@qq.com	
	功能: Buff配置

    //=================*=================\\
           教学官网：www.qiqiker.com
           官方微信服务号: qiqikertuts
           Plane老师微信: PlaneZhong
               ~~获取更多教学资讯~~
    \\=================*=================//
*************************************************/

public class BuffCfg {
    public int buffID;
    public string buffName;
    /// <summary>
    /// buff类型，用来创建不同类型的buff
    /// </summary>
    public BuffTypeEnum buffType;
    /// <summary>
    /// buff附着目标
    /// </summary>
    public AttachTypeEnum attacher;
    /// <summary>
    /// buff作用目标，为null默认影响附着对象
    /// </summary>
    public TargetCfg impacter;

    public int buffDelay;
    public int buffInterval;
    public int buffDuration;//（不包含delay）0：生效1次，-1：永久生效
    public StaticPosTypeEnum staticPosType;

    public string buffAudio;
    public string buffEffect;
    public string hitTickAudio;
}

public enum AttachTypeEnum {
    None,
    Caster,//Arthur的1技能加速buff
    Target,//Arthur的1技能沉默buff

    Indie,//Arthur大招产生的持续范围伤害

    Bullet,//Houyi大招命中目标时产生的范围伤害
}

public enum StaticPosTypeEnum {
    None,
    SkillCasterPos,//Buff所属技能施放者的位置
    SkillLockTargetPos,//Buff所属技能锁定目标的位置
    BulletHitTargetPos,//子弹命中目标的位置
    UIInputPos,//UI输入位置信息
}

public enum BuffTypeEnum {
    None,
    ModifySkill,
    MoveSpeed_Single,//单体加速buff
    Silense,//沉默

    ArthurMark,//Arthur1技能的标记伤害Buff

    HPCure,//治疗


    MoveSpeed_DynamicGroup,//动态群体移速Buff

    MoveAttack,//移动攻击
}
