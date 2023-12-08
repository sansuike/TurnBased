using FixMath.NET;
using System;
using System.Collections.Generic;
using IQIGame.Onigao.Logic.Battle;


/// <summary>
/// 不能用于dic的key，因为没有重写hashcode和equals，所以逻辑还原的时候不稳定
/// </summary>
public class SkillData
{

    private int _skillCid;
    private int _skillLv;
    private int _purifyLv;
    private int _strengLv;

    /// <summary>
    /// 技能强化
    /// </summary>
   // private List<CfgSkillStrengthenData> _cfgSkillStrengthens;

    //自建数据
    //public CfgSkillData CfgSkillData { get; private set; }

    /// <summary>
    /// 优先级
    /// </summary>
    public int Priority { get; private set; }

    /// <summary>
    /// 目标ID
    /// </summary>
    //public CfgSearchTargetData TargetTypeData { get; private set; }

    /// <summary>
    /// 目标ID
    /// </summary>
    //public List<CfgSkillAIData> SkillAIData { get; private set; }

    /// <summary>
    /// 计算公式ID
    /// </summary>
    public int FunctionID { get; private set; }

    /// <summary>
    /// 技能系数
    /// </summary>
    public Fix64 SkillRatio { get; private set; }

    /// <summary>
    /// BUFF释放顺序：0战斗开始，1技能前，2技能后，3技能结束，4伤害后 5技能准备后
    /// </summary>
    public List<int> BuffOrder { get; private set; }

    /// <summary>
    /// BUFF的几率
    /// </summary>
    public List<Fix64> BuffProbability { get; private set; }

    /// <summary>
    /// BUFF目标ID
    /// </summary>
    public List<int> BuffTarget { get; private set; }

    /// <summary>
    /// BUFFID
    /// </summary>
    public List<int> BuffID { get; private set; }

    /// <summary>
    /// BUFF回合
    /// </summary>
    public List<int> BuffTime { get; private set; }

    /// <summary>
    /// BUFF层数
    /// </summary>
    public List<int> BuffStackNum { get; private set; }

    /// <summary>
    /// 技能元素系0无 1冰 2火 3雷 4毒 5物理 6魔法
    /// </summary>
    public int originalElement = 0;


    /// <summary>
    /// 附魔类型
    /// </summary>
    public int enchantType = 0;

    /// <summary>
    /// 附魔元素
    /// </summary>
    public int enchantElement = 0;

    /// <summary>
    /// 临时属性ID
    /// </summary>
    public List<int> TemporaryAttType { get; private set; }

    /// <summary>
    /// 临时属性值
    /// </summary>
    public List<Fix64> TemporaryAttValue { get; private set; }

    /// <summary>
    /// 初始CD
    /// </summary>
    public int InitCD { get; private set; }

    /// <summary>
    /// 公共CD
    /// </summary>
    public int CommonCD { get; private set; }

    /// <summary>
    /// 持续CD
    /// </summary>
    public int CoolDown { get; private set; }

    /// <summary>
    /// AI持续CD
    /// </summary>
    public int AICoolDown { get; private set; }

    /// <summary>
    /// 大招消耗怒气
    /// </summary>
    public int[] HeroCostEnergy { get; private set; }

    /// <summary>
    /// 大招消耗怒气
    /// </summary>
    public int[] MonsterCostEnergy { get; private set; }


    /// <summary>
    /// 增加大招怒气值
    /// </summary>
    public int AddEnergy { get; private set; }


    /// <summary>
    /// 已释放次数
    /// </summary>
    public int ReleaseCount { get; set; }

    /// <summary>
    /// 释放次数
    /// </summary>
    public int ReleaseLimit { get; private set; }

    /// <summary>
    /// 技能消耗AP值
    /// </summary>
    public int NeedAp { get; private set; }

    /// <summary>
    /// 技能释放前移除BUFF标签
    /// </summary>
    public int[] CastRemoveBuffTag { get; private set; }

    /// <summary>
    /// 击穿比率
    /// </summary>
    public Fix64 BreakThrough { get; private set; }

    /// <summary>
    /// 溅射比率
    /// </summary>
    public Fix64 Sputter { get; private set; }


    /// <summary>
    /// 等级
    /// </summary>
    public int Lv { get; private set; }

    /// <summary>
    /// 精炼等级
    /// </summary>
    public int PurifyLv { get; private set; }

    /// <summary>
    /// 突破等级
    /// </summary>
    public int StrengLv { get; private set; }

    /// <summary>
    /// 子技能
    /// </summary>
    public int[] SubSkills { get; private set; }

    // /// <summary>
    // /// 之技能表现延迟
    // /// </summary>
    // public Fix64[] SubSkillsDelay{ get; private set; }

    /// <summary>
    /// 技能TimeLine类型
    /// </summary>
    public int TimelineID { get; private set; }

    /// <summary>
    /// 怒气修正
    /// </summary>
    public int ChangeEnergy { get; set; }

    /// <summary>
    /// 怒气修正
    /// </summary>
    public int ChangeADDEnergy { get; set; }

    /// <summary>
    /// 临时技能目标
    /// </summary>
    //public CfgSearchTargetData TempTargetTypeData { get; set; }
    ///// <summary>
    ///// 锚点AI
    ///// </summary>
    //public Dictionary<CfgAnchorPointAIData, int> AnchorPointAI { get; private set; }

    public SkillData(int skillCid, int skillLv, int purifyLv, int strengLv
        )
    {
        _skillCid = skillCid;
        _skillLv = skillLv;
        _purifyLv = purifyLv;
        _strengLv = strengLv;
        // 初始化技能
        _Init();
        // 计算技能强化项
        _Strengthen();
    }
    // public SkillData(int skillCid, int skillLv, int purifyLv, int strengLv,
    //     List<CfgSkillStrengthenData> cfgSkillStrengthens)
    // {
    //     _skillCid = skillCid;
    //     _skillLv = skillLv;
    //     _purifyLv = purifyLv;
    //     _strengLv = strengLv;
    //     _cfgSkillStrengthens = cfgSkillStrengthens;
    //     // 初始化技能
    //     _Init();
    //     // 计算技能强化项
    //     _Strengthen();
    // }

    /// <summary>
    /// 把skillinfo的字段缓存下来，方便被强化属性修改
    /// </summary>
    private void _Init()
    {
       /* // 取得技能配置
        CfgSkillData = CfgSkillTable.Instance.GetDataByID(_skillCid);

        if (CfgSkillData == null)
        {
            _logger.Error("_skillCid == null cid = " + _skillCid);
        }


        // 取得技能数据配置
        CfgSkillDetailData cfgSkillDetailData = CfgSkillDetailTable.Instance.GetDataByID(CfgSkillData.SkillDetail);

        Priority = cfgSkillDetailData.Priority;

        // 取得筛选目标配置
        TargetTypeData = CfgSearchTargetTable.Instance.GetDataByID(cfgSkillDetailData.TargetTypeID);

        //// 锚点AI
        //AnchorPointAI = new Dictionary<CfgAnchorPointAIData, int>();
        //int[] pointAi = cfgSkillDetailData.PointAI;
        //int[] pointScore = cfgSkillDetailData.PointScore;
        //for (int i = 0; i < pointAi.Length; i++ ) {
        //    var k = CfgAnchorPointAITable.Instance.GetDataByID(pointAi[i]);
        //    var v = pointScore[i];
        //    AnchorPointAI.Add(k, v);
        //}

        // 取得技能AI
        SkillAIData = new List<CfgSkillAIData>();
        for (int i = 0; i < cfgSkillDetailData.SkillAI.Length; i++)
        {
            SkillAIData.Add(CfgSkillAITable.Instance.GetDataByID(cfgSkillDetailData.SkillAI[i]));
        }

        FunctionID = cfgSkillDetailData.FunctionID;
        Lv = _skillLv <= 0 ? CfgSkillData.Level : _skillLv;
        PurifyLv = _purifyLv;
        StrengLv = _strengLv;
        // 根据技能等级取得技能系数
        SkillRatio = cfgSkillDetailData.SkillRatio.Length == 0
            ? 0
            : cfgSkillDetailData.SkillRatio[Math.Min(cfgSkillDetailData.SkillRatio.Length - 1, Lv - 1)];

        BuffOrder = new List<int>(cfgSkillDetailData.BuffOrder);
        BuffProbability = new List<Fix64>(cfgSkillDetailData.BuffProbability);
        BuffTarget = new List<int>(cfgSkillDetailData.BuffTarget);
        BuffID = new List<int>(cfgSkillDetailData.BuffID);
        BuffTime = new List<int>(cfgSkillDetailData.BuffTime);
        BuffStackNum = new List<int>(cfgSkillDetailData.BuffStackNum);


        originalElement = cfgSkillDetailData.Element;

        TemporaryAttType = new List<int>(cfgSkillDetailData.TemporaryAttType);
        TemporaryAttValue = new List<Fix64>(cfgSkillDetailData.TemporaryAttValue);
        InitCD = cfgSkillDetailData.InitCD;
        CoolDown = cfgSkillDetailData.CoolDown;
        CommonCD = cfgSkillDetailData.CommonCD;
        AICoolDown = cfgSkillDetailData.AICD;
        HeroCostEnergy = (int[])cfgSkillDetailData.HeroEnergyCost.Clone();
        MonsterCostEnergy = (int[])cfgSkillDetailData.MonsterEnergyCost.Clone();
        AddEnergy = cfgSkillDetailData.AddEnergy;
        ReleaseLimit = cfgSkillDetailData.ReleaseLimti;
        NeedAp = cfgSkillDetailData.NeedAp;
        CastRemoveBuffTag = (int[])cfgSkillDetailData.CastRemoveBuffTag.Clone();
        BreakThrough = cfgSkillDetailData.BreakThrough;
        Sputter = cfgSkillDetailData.Sputter;

        SubSkills = (int[])CfgSkillData.SubSkills.Clone();
        // SubSkillsDelay = CfgSkillData.SubSkillsDelay;
        TimelineID = CfgSkillData.TimelineID;
        */
    }

    /// <summary>
    /// 强化技能数据，对缓存下来的字段进行修改
    /// </summary>
    private void _Strengthen()
    {
       /* if (_cfgSkillStrengthens != null)
        {
            foreach (CfgSkillStrengthenData cfgSkillStrengthenData in _cfgSkillStrengthens)
            {
                for (int i = 0; i < cfgSkillStrengthenData.SkillSlotEffect.Length; i++)
                {
                    //if (Array.IndexOf(cfgSkillStrengthenData.EffectSkill[i], _skillCid) < 0)
                    //{
                    //    continue;
                    //}
                    int type = cfgSkillStrengthenData.SkillSlotEffect[i];
                    if (type == 0 || type == SkillStrengthenConstant.TYPE_ADD_PABLITY ||
                        type == SkillStrengthenConstant.TYPE_ADD_SKILL ||
                        type == SkillStrengthenConstant.TYPE_CHANGE_COST ||
                        type == SkillStrengthenConstant.TYPE_CHANGE_SKILL_LV)
                    {
                        // 已由服务器处理
                        continue;
                    }

                    string[] param = cfgSkillStrengthenData.SkillSlotEffectParam[i];

                    if (_skillCid != Convert.ToInt32(param[0]))
                    {
                        continue;
                    }

                    switch (type)
                    {
                        case SkillStrengthenConstant.TYPE_REPLACE_FUNCTION:
                            FunctionID = Convert.ToInt32(param[1]);
                            break;
                        case SkillStrengthenConstant.TYPE_CHANGE_SKILL_RATIO:
                            SkillRatio += Fix64.Parse(param[1]);
                            break;
                        case SkillStrengthenConstant.TYPE_REPLACE_TARGET:
                            TargetTypeData = CfgSearchTargetTable.Instance.GetDataByID(Convert.ToInt32(param[1]));
                            break;
                        case SkillStrengthenConstant.TYPE_REPLACE_ELEMENT:
                            originalElement = Convert.ToInt32(param[1]);
                            break;
                        case SkillStrengthenConstant.TYPE_ADD_BUFF:
                            BuffOrder.Add(Convert.ToInt32(param[1]));
                            BuffProbability.Add(Fix64.Parse(param[2]));
                            BuffTarget.Add(Convert.ToInt32(param[3]));
                            BuffID.Add(Convert.ToInt32(param[4]));
                            BuffTime.Add(Convert.ToInt32(param[5]));
                            BuffStackNum.Add(Convert.ToInt32(param[6]));
                            break;
                        case SkillStrengthenConstant.TYPE_REPLACE_BUFF:
                        {
                            int buffID = Convert.ToInt32(param[1]);
                            int newID = Convert.ToInt32(param[2]);
                            for (int j = 0; j < BuffID.Count; j++)
                            {
                                if (BuffID[j] == buffID)
                                {
                                    BuffID[j] = newID;
                                }
                            }

                            break;
                        }
                        case SkillStrengthenConstant.TYPE_CHANGE_BUFF_PROBABILITY:
                        {
                            int buffID = Convert.ToInt32(param[1]);
                            for (int j = 0; j < BuffID.Count; j++)
                            {
                                if (BuffID[j] == buffID)
                                {
                                    BuffProbability[j] += Fix64.Parse(param[2]);
                                }
                            }

                            break;
                        }
                        case SkillStrengthenConstant.TYPE_CHANGE_BUFF_TARGET:
                        {
                            int buffID = Convert.ToInt32(param[1]);
                            for (int j = 0; j < BuffID.Count; j++)
                            {
                                if (BuffID[j] == buffID)
                                {
                                    BuffTarget[j] = Convert.ToInt32(param[2]);
                                }
                            }

                            break;
                        }
                        case SkillStrengthenConstant.TYPE_CHANGE_BUFF_TIME:
                        {
                            int buffID = Convert.ToInt32(param[1]);
                            for (int j = 0; j < BuffID.Count; j++)
                            {
                                if (BuffID[j] == buffID)
                                {
                                    BuffTime[j] += Convert.ToInt32(param[2]);
                                }
                            }

                            break;
                        }
                        case SkillStrengthenConstant.TYPE_CHANGE_BUFF_STACK_NUM:
                        {
                            int buffID = Convert.ToInt32(param[1]);
                            for (int j = 0; j < BuffID.Count; j++)
                            {
                                if (BuffID[j] == buffID)
                                {
                                    BuffStackNum[j] += Convert.ToInt32(param[2]);
                                }
                            }

                            break;
                        }
                        case SkillStrengthenConstant.TYPE_CHANGE_TEMPORARY_ATT:
                        {
                            TemporaryAttType.Add(Convert.ToInt32(param[1]));
                            TemporaryAttValue.Add(Fix64.Parse(param[2]));
                            break;
                        }
                        case SkillStrengthenConstant.TYPE_CHANGE_COST_ENERGY:
                        {
                            for (int j = 0; j < HeroCostEnergy.Length; j++)
                            {
                                HeroCostEnergy[j] += Convert.ToInt32(param[1]);
                            }

                            for (int j = 0; j < MonsterCostEnergy.Length; j++)
                            {
                                MonsterCostEnergy[j] += Convert.ToInt32(param[1]);
                            }

                            break;
                        }
                        case SkillStrengthenConstant.TYPE_CHANGE_AP:
                        {
                            NeedAp += Convert.ToInt32(param[1]);
                            NeedAp = Math.Max(0, NeedAp);
                            break;
                        }
                        case SkillStrengthenConstant.TYPE_CHANGE_ADD_ENERGY:
                        {
                            AddEnergy += Convert.ToInt32(param[1]);
                            break;
                        }
                        case SkillStrengthenConstant.TYPE_CHANGE_SPUTTER:
                        {
                            Sputter += Fix64.Parse(param[1]);
                            break;
                        }
                        case SkillStrengthenConstant.TYPE_CHANGE_BREAK_THROUGH:
                        {
                            BreakThrough += Fix64.Parse(param[1]);
                            break;
                        }
                        case SkillStrengthenConstant.TYPE_CHANGE_SKILL_CD:
                        {
                            CoolDown += Convert.ToInt32(param[1]);
                            break;
                        }
                        // case SkillStrengthenConstant.TYPE_CHANGE_SKILL_LV:
                        // {
                        //     Lv = Convert.ToInt32(param[1]);
                        //     break;
                        // }
                        case SkillStrengthenConstant.TYPE_CHANGE_SUB_SKILL:
                        {
                            // 子技能
                            string[] subSkills = param[1].Split(',');
                            SubSkills = new int[subSkills.Length];
                            for (int j = 0; j < subSkills.Length; j++)
                            {
                                SubSkills[j] = Convert.ToInt32(subSkills[j]);
                            }

                            // // 子技能延迟
                            // string[] subSkillsDelay = param[2].Split(',');
                            // SubSkillsDelay = new Fix64[subSkillsDelay.Length];
                            // for (int j = 0; j < subSkillsDelay.Length; j++)
                            // {
                            //     SubSkillsDelay[j] = Fix64.Parse(subSkillsDelay[j]);
                            // }
                            break;
                        }
                        case SkillStrengthenConstant.TYPE_CHANGE_SKILL_TIMELINE:
                        {
                            TimelineID = Convert.ToInt32(param[1]);
                            break;
                        }
                        case SkillStrengthenConstant.TYPE_CHANGE_SKILL_RELEASE_LIMIT:
                        {
                            ReleaseLimit = Convert.ToInt32(param[1]);
                            break;
                        }
                        default:
                            _logger.Error("not find skill strengthen type : {0}", type);
                            break;
                    }
                }
            }
        }
        */
    }


    // 取得怒气消耗
    public int GetCostEnergy(int troopType)
    {
        int[] CostEnergy = troopType == BattleConstant.TroopType.ATTACK ? HeroCostEnergy : MonsterCostEnergy;
        if (CostEnergy.Length < 1)
        {
            return ChangeEnergy;
        }

        if (ReleaseLimit > CostEnergy.Length)
        {
            return CostEnergy[CostEnergy.Length - 1];
        }

        int costEnergy = ReleaseLimit > 0 ? CostEnergy[ReleaseLimit - 1] : CostEnergy[0];
        // 怒气修正
        return Math.Max(costEnergy + ChangeEnergy, 0);
    }


    /*public CfgSearchTargetData GetTargetTypeData()
    {
        if (TempTargetTypeData == null)
        {
            return TargetTypeData;
        }
        else
        {
            return TempTargetTypeData;
        }
    }
    */
}