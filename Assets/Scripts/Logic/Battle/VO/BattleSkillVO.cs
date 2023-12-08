using FixMath.NET;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 战斗单位数据
/// </summary>
public sealed class BattleSkillVO : BaseBattleVO
{
    public SkillData SkillData { get; private set; }

    //private List<CfgSkillStrengthenData> _cfgSkillStrengthenDatas;

    // /// <summary>
    // /// 进攻方表现配置
    // /// </summary>
    // public CfgSkillActionData CfgAttackerActionData { get; private set; }
    // /// <summary>
    // /// 防守方表现配置
    // /// </summary>
    // public CfgSkillActionData CfgDefenderActionData { get; private set; }
    /// <summary>
    /// 公式配置
    /// </summary>
    //public CfgSkillFunctionData CfgSkillFunctionData { get; private set; }

    /// <summary>
    /// 指定目标
    /// 1反击技能使用
    /// 2子技能使用
    /// </summary>
    public List<BattleUnitVO> SpecifyTargets { get; set; }

    /// <summary>
    /// 冷却回合
    /// </summary>
    public int CoolDown { get; set; }

    /// <summary>
    /// 是否是子技能
    /// </summary>
    public bool IsSubSkill { get; private set; }

    /// <summary>
    /// 所属主技能
    /// </summary>
    public BattleSkillVO MainSkill { get; private set; }

    /// <summary>
    /// 子技能列表
    /// </summary>
    public List<BattleSkillVO> SubSkills { get; private set; }

    /// <summary>
    /// 临时子技能列表
    /// </summary>
    public List<BattleSkillVO> TmpSubSkills { get; private set; }

    /// <summary>
    /// 表现延迟时间
    /// </summary>
    public Fix64 DelayTime { get; set; }

    /// <summary>
    /// ai的冷却回合
    /// </summary>
    public int AICoolDown { get; set; }

    /// <summary>
    /// 是否封印
    /// </summary>
    public bool IsSeal { get; set; }


    public BattleSkillVO(BaseLogicFight fight, int skillCid, int skillLv, int purifyLv, int strengLv) : base(fight)
    {
        //_cfgSkillStrengthenDatas = cfgSkillStrengthenDatas;
        //SkillData = new SkillData(skillCid,skillLv,purifyLv,strengLv, _cfgSkillStrengthenDatas);
        SkillData = new SkillData(skillCid, skillLv, purifyLv, strengLv);

        // // 取得表现配置
        // if (CfgSkillData.AttackerAction != 0)
        // {
        //     CfgAttackerActionData = CfgSkillActionTable.Instance.GetDataByID(CfgSkillData.AttackerAction);
        // }
        // if (CfgSkillData.DefenderAction != 0)
        // {
        //     CfgDefenderActionData = CfgSkillActionTable.Instance.GetDataByID(CfgSkillData.DefenderAction);
        // }
        // 取得公式配置
        //CfgSkillFunctionData = CfgSkillFunctionTable.Instance.GetDataByID(SkillData.FunctionID);

        // 子技能
        TmpSubSkills = new List<BattleSkillVO>();
        SubSkills = new List<BattleSkillVO>();
        for (int i = 0; i < SkillData.SubSkills.Length; i++)
        {
            BattleSkillVO subSkill =
                new BattleSkillVO(fight, SkillData.SubSkills[i], 0, 0, 0);
            subSkill.IsSubSkill = true;
            subSkill.MainSkill = this;
            // 暂时没用，用的Timeline
            // subSkill.DelayTime = CfgSkillData.SubSkillsDelay[i];
            SubSkills.Add(subSkill);
        }
    }

    /// <summary>
    /// 添加临时子技能
    /// </summary>
    /// <param name="skill"></param>
    /// <param name="delayTimeOffset"></param>
    public BattleSkillVO AddTmpSubSkill(int cid, Fix64 delayTimeOffset)
    {
        BattleSkillVO subSkill = new BattleSkillVO(Fight, cid, 0, 0, 0);
        subSkill.IsSubSkill = true;
        subSkill.MainSkill = this;

        if (TmpSubSkills.Count > 0)
        {
            subSkill.DelayTime = TmpSubSkills[TmpSubSkills.Count - 1].DelayTime + delayTimeOffset;
        }
        else
        {
            if (SubSkills.Count > 0)
            {
                subSkill.DelayTime = SubSkills[SubSkills.Count - 1].DelayTime + delayTimeOffset;
            }
            else
            {
                subSkill.DelayTime = delayTimeOffset;
            }
        }

        TmpSubSkills.Add(subSkill);
        return subSkill;
    }

    /// <summary>
    /// 检查释放次数限制
    /// </summary>
    /// <returns></returns>
    public bool ReleaseCountLimit()
    {
        return (SkillData.ReleaseLimit <= 0 || SkillData.ReleaseCount < SkillData.ReleaseLimit);
    }

    // public CfgSkillData CfgSkillData
    // {
    //     get
    //     {
    //         return SkillData.CfgSkillData;
    //     }
    // }


    // 附魔
    public void EnchantElement(int type, int element)
    {
        SkillData.enchantType = type;
        SkillData.enchantElement = element;
    }

    // 取得元素类型
    public int GetElement()
    {
        if (IsSubSkill)
        {
            return MainSkill.GetElement();
        }

        if (SkillData.originalElement == SkillConstant.ELEMENT_TYPE_PHYSICS && SkillData.enchantType == 0)
        {
            return SkillData.enchantElement != 0 ? SkillData.enchantElement : SkillData.originalElement;
        }

        return SkillData.originalElement;
    }
}