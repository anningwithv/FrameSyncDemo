using HOKProtocol;
using UnityEngine;

public partial class PlayWnd {
    public SkillItem skaItem;
    public SkillItem sk1Item;
    public SkillItem sk2Item;
    public SkillItem sk3Item;

    public Transform imgInfoRoot;
    private bool isForbidReleaseSkill;

    public void InitSkillInfo() {
        BattleHeroData self = root.HeroLst[root.SelfIndex];
        UnitCfg heroCfg = resSvc.GetUnitConfigByID(self.heroID);
        int[] skillArr = heroCfg.skillArr;

        skaItem.InitSkillItem(resSvc.GetSkillConfigByID(skillArr[0]), 0);
        sk1Item.InitSkillItem(resSvc.GetSkillConfigByID(skillArr[1]), 1);
        sk2Item.InitSkillItem(resSvc.GetSkillConfigByID(skillArr[2]), 2);
        sk3Item.InitSkillItem(resSvc.GetSkillConfigByID(skillArr[3]), 3);

        SetForbidState(false);
        SetActive(imgInfoRoot, false);
    }

    public void SetForbidState() {
        SetForbidState(true);
        isForbidReleaseSkill = true;
    }

    void UpdateSkillWnd() {
        if(isForbidReleaseSkill) {
            if(BattleSys.Instance.IsForbidSelfPlayerReleaseSkill() == false) {
                SetForbidState(false);
                isForbidReleaseSkill = false;
            }
        }

        if(Input.GetKeyDown(KeyCode.Space)) {
            skaItem.ClickSkillItem();
        }
        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            sk1Item.ClickSkillItem();
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)) {
            sk2Item.ClickSkillItem();
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)) {
            sk3Item.ClickSkillItem();
        }
    }

    void SetForbidState(bool state) {
        sk1Item.SetForbidState(state);
        sk2Item.SetForbidState(state);
        sk3Item.SetForbidState(state);
    }
}
