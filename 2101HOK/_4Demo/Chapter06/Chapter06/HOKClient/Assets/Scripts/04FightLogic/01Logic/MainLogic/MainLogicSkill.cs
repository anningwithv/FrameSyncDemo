/*************************************************
	作者: Plane
	邮箱: 1785275942@qq.com	
	功能: 主要逻辑单位技能处理

    //=================*=================\\
           教学官网：www.qiqiker.com
           官方微信服务号: qiqikertuts
           Plane老师微信: PlaneZhong
               ~~获取更多教学资讯~~
    \\=================*=================//
*************************************************/

using HOKProtocol;
using PEMath;

public partial class MainLogicUnit {
    protected Skill[] skillArr;

    void InitSkill() {
        int len = ud.unitCfg.skillArr.Length;
        skillArr = new Skill[len];
        for(int i = 0; i < len; i++) {
            skillArr[i] = new Skill(ud.unitCfg.skillArr[i], this);
        }
    }

    void TickSkill() {
        //TODO
    }

    void InputSkillKey(SkillKey key) {
        for(int i = 0; i < skillArr.Length; i++) {
            if(skillArr[i].skillID == key.skillID) {
                PEInt x = PEInt.zero;
                PEInt z = PEInt.zero;
                x.ScaledValue = key.x_value;
                z.ScaledValue = key.z_value;
                PEVector3 skillArgs = new PEVector3(x, 0, z);
                skillArr[i].ReleaseSkill(skillArgs);
                return;
            }
        }
        this.Error($"skillID:{key.skillID} is not exist.");
    }
    #region API Functions
    public Skill GetNormalSkill() {
        if(skillArr != null && skillArr[0] != null) {
            return skillArr[0];
        }
        return null;
    }
    #endregion
}
