using FixMath.NET;
using IQIGame.Onigao.Config;
using IQIGame.Onigao.Logic.Battle;
using IQIGame.Onigao.Logic.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// BUFF管理器
/// </summary>
public partial class BuffManager
{
    /// <summary>
    /// 帧率
    /// </summary>
    public int FrameRate { get; private set; }

    /// <summary>
    /// 随机类
    /// </summary>
    public LogicStableRandom Random { get; private set; }

    /// <summary>
    /// 当前帧
    /// </summary>
    public int CurrFrame { get; private set; }

    /// <summary>
    /// 所有Buff
    /// </summary>
    private List<Buff> _allBuffs = new List<Buff>();

    /// <summary>
    /// 持有者
    /// </summary>
    private IBuffHolder _holder;

    // 战斗
    public TurnBaseLogicFight Fight;


    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="frameRate">帧率</param>
    /// <param name="randomSeed">随机种子</param>
    /// <param name="holder">持有者</param>
    public void Init(TurnBaseLogicFight fight, int frameRate, int randomSeed, IBuffHolder holder)
    {
        FrameRate = frameRate;
        Random = new LogicStableRandom(randomSeed);
        _holder = holder;
        Fight = fight;
    }


    /// <summary>
    ///  持有者
    /// </summary>
    internal IBuffHolder Holder
    {
        get { return _holder; }
    }

    /// <summary>
    /// 获取buff
    /// </summary>
    /// <returns></returns>
    public List<Buff> GetAllBuffs(bool safely = false)
    {
        if (safely)
        {
            return _allBuffs;
        }
        else
        {
            return new List<Buff>(_allBuffs);
        }
    }

    /// <summary>
    /// 触发移除
    /// </summary>
    /// <param name="triggerType"></param>
    public void TriggerRemove(int triggerType)
    {
        foreach (Buff buff in GetAllBuffs())
        {
            // if (Array.IndexOf(buff.BuffCfg.RemoveTrigger, triggerType) != -1)
            // {
            //     RemoveBuff(buff, BuffRemoveReason.CLEAN);
            // }
        }
    }

    /// <summary>
    /// buff触发
    /// </summary>
    /// <param name="buff"></param>
    /// <param name="triggerType"></param>
    /// <param name="args"></param>
    public void TriggerBuff(Buff buff, Type triggerType, params object[] args)
    {
        buff.OnTrigger(triggerType, args);
    }

    /// <summary>
    /// buff触发
    /// </summary>
    /// <param name="triggerType"></param>
    /// <param name="args"></param>
    public void TriggerBuff(Type triggerType, params object[] args)
    {
        // 代表是人触发的，对应触发他的地板BUFF
        if (_holder.GetScope() == 0)
        {
            BattleUnitVO battleUnitVo = _holder as BattleUnitVO;
            if (battleUnitVo.Tile != null && (triggerType != typeof(BattleUnitTerrainChangeTrigger)))
            {
                battleUnitVo.Tile.GetBuffManager().TriggerBuff(triggerType, args);
            }
        }

        if (_allBuffs.Count > 0)
        {
            foreach (Buff buff in GetAllBuffs())
            {
                try
                {
                    buff.OnTrigger(triggerType, args);
                }
                catch (Exception e)
                {
                    _logger.Error("TriggerBuff error buffCid = {0}, \n source = {1}", buff.BuffCfg.Id, e.StackTrace);
                    throw e;
                }
            }
        }
    }

    /// <summary>
    /// 移除buff
    /// </summary>
    /// <param name="buffCid"></param>
    /// <param name="targetCid"></param>
    /// <param name="removeReason"></param>
    public void RemoveBuff(int buffCid, int targetCid, BuffRemoveReason removeReason = BuffRemoveReason.CLEAN)
    {
        if (buffCid == 0)
        {
            return;
        }

        foreach (IBuffHolder holder in _holder.GetBuffTarget(null, targetCid))
        {
            holder.GetBuffManager().RemoveBuff(buffCid, removeReason);
        }
    }

    /// <summary>
    /// 移除BUFF
    /// </summary>
    /// <param name="buffCid"></param>
    /// <param name="removeReason"></param>
    public void RemoveBuff(int buffCid, BuffRemoveReason removeReason = BuffRemoveReason.CLEAN)
    {
        if (buffCid == 0)
        {
            return;
        }

        foreach (Buff buff in GetAllBuffs())
        {
            if (buff.BuffCfg.Id == buffCid)
            {
                RemoveBuff(buff, removeReason);
            }
        }
    }

    /// <summary>
    /// 移除BUFF
    /// </summary>
    /// <param name="buff"></param>
    /// <param name="removeReason"></param>
    public void RemoveBuff(Buff buff, BuffRemoveReason removeReason = BuffRemoveReason.CLEAN)
    {
        if (!buff.IsRemoved && _allBuffs.Contains(buff))
        {
            if (removeReason != BuffRemoveReason.CLEAN)
            {
                buff.OnTrigger(typeof(BuffBeRemoveBuffTrigger),
                    new object[] { buff.BuffCfg, (int)removeReason }); //单独处理自己的被移除触发，因为被移除后不会监听触发
            }

            if (removeReason != BuffRemoveReason.CLEAN)
            {
                TriggerBuff(typeof(BuffBeRemoveBuffTrigger), buff.BuffCfg, (int)removeReason);
            }

            buff.IsRemoved = true;
            _allBuffs.Remove(buff);

            //修改buff抵抗率
            foreach (BuffEffect buffEffect in buff.Effects)
            {
                if (buffEffect.EffectType == BuffConstant.BUFF_EFFECT_TYPE_BUFF_RESISTANCE)
                {
                    int type = Convert.ToInt32(buffEffect.EffectParams[0]);
                    int keyID = Convert.ToInt32(buffEffect.EffectParams[1]);
                    UpdateBuffResistanceChange(type, keyID, -buffEffect.EffectValue);
                }
                else if (buffEffect.EffectType == BuffConstant.BUFF_EFFECT_TYPE_BUFF_TIME)
                {
                    int type = Convert.ToInt32(buffEffect.EffectParams[0]);
                    int keyID = Convert.ToInt32(buffEffect.EffectParams[1]);
                    UpdateBuffTimeChange(type, keyID, Fix64.ToInt32(-buffEffect.EffectValue));
                }
            }

            buff.BuffManager.Holder.OnBuffRemove((BattleUnitVO)buff.BuffManager.Holder, buff, removeReason);
            foreach (Buff child in buff.Children)
            {
                child.BuffManager.RemoveBuff(child, removeReason);
            }
        }
    }

    /// <summary>
    /// 添加buff
    /// </summary>
    /// <param name="caster"></param>
    /// <param name="skillData"></param>
    /// <param name="buffCid"></param>
    public void AddBuff(IBuffHolder caster, SkillData skillData, int buffCid, int buffTime = 0, int stackNum = 1)
    {
        if (buffCid == 0)
        {
            return;
        }

        // 添加BUFF
        AddBuffInner(caster, skillData, buffCid, buffTime, stackNum);
    }

    /// <summary>
    /// 添加buff
    /// </summary>
    /// <param name="targetCid"></param>
    /// <param name="caster"></param>
    /// <param name="skillData"></param>
    /// <param name="buffCid"></param>
    public void AddBuff(int targetCid, IBuffHolder caster, SkillData skillData, int buffCid, int buffTime = 0,
        int stackNum = 1)
    {
        if (buffCid == 0)
        {
            return;
        }

        foreach (IBuffHolder holder in _holder.GetBuffTarget(null, targetCid))
        {
            holder.GetBuffManager().AddBuffInner(caster, skillData, buffCid, buffTime, stackNum);
        }
    }

    /// <summary>
    /// 添加buff
    /// </summary>
    /// <param name="caster"></param>
    /// <param name="skillData"></param>
    /// <param name="buffCid"></param>
    /// <returns>返回的buff可能已经被移除，需要判断buff.isremove</returns>
    internal Buff AddBuffInner(IBuffHolder caster, SkillData skillData, int buffCid, int buffTime = 0, int stackNum = 1)
    {
        // 取得BUFF配置
        CfgBuffData addBuffCfg = CfgBuffTable.Instance.GetDataByID(buffCid);
        if (addBuffCfg == null)
        {
            _logger.Error("buffid is not find:{0}", buffCid);
            return null;
        }


        // if (addBuffCfg.Scope != _holder.GetScope())
        // {
        //     _logger.Error("buffid is Scope Error");
        //     return null;
        // }

        // 检查抵抗率(BUFF免疫)
        if (IsBuffResistance(addBuffCfg))
        {
            // BUFF 被免疫
            _holder.OnBuffImmune(addBuffCfg.Id);
            return null;
        }


        // 检查BUFF组关系，是否免疫
        Dictionary<Buff, int> relations = GetBuffGroupRelation(addBuffCfg);
        if (relations.Values.Contains(BuffConstant.BUFF_GROUP_RELATION_IMMUNE))
        {
            _holder.OnBuffImmune(addBuffCfg.Id);
            // 组关系包含免疫，无法添加buff
            return null;
        }

        // 检查元素反应
        if (System.Array.IndexOf(addBuffCfg.BuffTag, 100) > -1)
        {
            foreach (Buff _buff in GetAllBuffs())
            {
                IDictionary<int, CfgElementReactionData> cfgs = CfgElementReactionTable.Instance.GetAllData();
                foreach (KeyValuePair<int, CfgElementReactionData> kv in cfgs)
                {
                    int monsterTeamType = 1;
                    if (Fight.CfgMonsterTeamData != null)
                    {
                        monsterTeamType = Fight.CfgMonsterTeamData.Type;
                    }

                    // 区分普通元素反应 or 常世元素反应
                    if (_buff.BuffCfg.Type == kv.Value.SourceBuffType
                        && addBuffCfg.Type == kv.Value.TargetBuffType
                        && monsterTeamType == kv.Value.ReactionSetting)
                    {
                        bool chain = false;
                        // 连锁反应
                        if (_buff.BuffCfg.Type == addBuffCfg.Type)
                        {
                            if (_buff.Caster == caster)
                            {
                                continue;
                            }
                            else
                            {
                                chain = true;
                            }
                        }


                        BattleUnitVO casterUnit = caster as BattleUnitVO;
                        BattleUnitVO _holderUnit = _holder as BattleUnitVO;

                        // 记录元素触发
                        casterUnit.GetFightAttribute()[BattleConstant.Attribute.TYPE_TRIGGER_ELEMENT_REACTION] =
                            kv.Value.ReactionType;

                        // 元素反应移除
                        TriggerRemove(BuffConstant.BUFF_REMOVE_TRIGGER_TYPE_ELEMENT_REACTION);

                        int[] buffs = _holder.getTroopType() == caster.getTroopType()
                            ? kv.Value.ReactionID
                            : kv.Value.SelfReactionID;
                        foreach (int reactionID in buffs)
                        {
                            AddBuff(caster, skillData, reactionID, 0, stackNum);
                        }

                        // 元素反应弱点触发
                        _holderUnit.TriggerWeak(casterUnit, BattleConstant.WeakType.ELEMENT_REACTION,
                            kv.Value.ReactionType);

                        // 当前元素反应目标
                        caster.GetFight().CurrAtkElementReactionTarget = _holder as BattleUnitVO;
                        // 前BUFF释放者
                        // _buff.Caster.GetBuffManager().TriggerBuff(typeof(BattleUnitElementReactionBuffTrigger),kv.Value.SourceBuffType,kv.Value.TargetBuffType);
                        // 元素反应触发
                        caster.GetBuffManager().TriggerBuff(typeof(BattleUnitElementReactionBuffTrigger),
                            kv.Value.SourceBuffType, kv.Value.TargetBuffType);

                        // 清除当前元素反应目标
                        caster.GetFight().CurrAtkElementReactionTarget = null;

                        // 记录元素反应
                        {
                            Hashtable element_reaction_num =
                                StatisticsUtil.GetHashTable(casterUnit.Fight.Statistics, "element_reaction_num");
                            int count = 0;
                            if (element_reaction_num.ContainsKey(kv.Key))
                            {
                                count = (int)element_reaction_num[kv.Key];
                            }

                            element_reaction_num[kv.Key] = count + 1;
                        }
                        // 记录任务用途元素反应
                        {
                            Hashtable unit_element_reaction_num = StatisticsUtil.GetHashTable(
                                casterUnit.Fight.userDataDict, BattleConstant.UserData.FIGHT_UNIT_ELEMENT_REACTION_NUM);

                            Hashtable unit_element_reaction = StatisticsUtil.GetHashTable(unit_element_reaction_num,
                                casterUnit.CfgMonsterData.Id + "");

                            int count = 0;
                            if (unit_element_reaction.ContainsKey(kv.Key))
                            {
                                count = (int)unit_element_reaction[kv.Key];
                            }

                            unit_element_reaction[kv.Key] = count + 1;
                        }

                        // Buff elementBuff = _allBuffs.Find(eb => System.Array.IndexOf(eb.BuffCfg.BuffTag, 100) > -1);
                        // if (!chain && elementBuff != null)
                        if (!chain)
                        {
                            // 执行了元素反应
                            return null;
                        }
                    }
                }
            }
        }


        // 检查并处理相同的BUFF
        foreach (Buff buff in GetAllBuffs(true))
        {
            if (buff.BuffCfg.Id == buffCid)
            {
                //同一个buff
                if (buff.BuffCfg.StackType == BuffConstant.BUFF_STACK_TYPE_OVRERIDE)
                {
                    //覆盖
                    RemoveBuff(buff, BuffRemoveReason.OVERRIDE);
                    break;
                }
                else if (buff.BuffCfg.StackType == BuffConstant.BUFF_STACK_TYPE_ROUND_OVRERIDE)
                {
                    int addBuffTime = buffTime != 0 ? buffTime : buff.BuffCfg.BuffTime;
                    int addBuffLeftTime = addBuffTime == -1 ? -1 : addBuffTime * FrameRate / 1000;

                    ;
                    if (addBuffLeftTime == -1)
                    {
                        //覆盖
                        RemoveBuff(buff, BuffRemoveReason.OVERRIDE);
                        break;
                    }
                    else if (buff.LeftTime == -1)
                    {
                        //无法添加
                        return null;
                    }
                    else if (addBuffLeftTime >= buff.LeftTime)
                    {
                        //覆盖
                        RemoveBuff(buff, BuffRemoveReason.OVERRIDE);
                        break;
                    }
                    else
                    {
                        //无法添加
                        return null;
                    }
                }
                else
                {
                    //叠加
                    buff.DoStack(caster, stackNum);
                    TriggerBuff(typeof(AddBuffBuffTrigger), buff.BuffCfg);
                    return null;
                }
            }
        }


        bool createBuff = true;
        foreach (Buff buff in GetAllBuffs())
        {
            // 相同类型的BUFF
            if (buff.BuffCfg.Type == addBuffCfg.Type)
            {
                //同类型，判断优先级
                if (buff.BuffCfg.Priority == addBuffCfg.Priority)
                {
                    // 优先级想相同，共存
                }
                else if (buff.BuffCfg.Priority < addBuffCfg.Priority)
                {
                    // 优先级更高，覆盖
                    RemoveBuff(buff, BuffRemoveReason.OVERRIDE);
                }
                else
                {
                    //优先级低，无法添加新buff
                    createBuff = false;
                }
            }
            else
            {
                int relation = -1;
                relations.TryGetValue(buff, out relation);
                // 不同类型，判断组关系
                switch (relation)
                {
                    case BuffConstant.BUFF_GROUP_RELATION_NONE:
                        break;
                    case BuffConstant.BUFF_GROUP_RELATION_OVERRIDE:
                        RemoveBuff(buff, BuffRemoveReason.OVERRIDE);
                        break;
                    default:
                        break;
                }
            }
        }

        // 创建一个新的BUFF
        if (createBuff)
        {
            Buff buff = new Buff(this, caster, skillData, addBuffCfg, buffTime, stackNum);

            // 是否标记触发
            if (Fight.triggerStepType != null)
            {
                Fight.triggerStepBuffs.Add(buff);
            }

            // 添加BUFF
            _allBuffs.Add(buff);

            //if (_holder.GetType() == typeof(BattleTileVO))
            //{
            //    // 作用于地板上的英雄
            //    BattleTileVO battleTileVO = (BattleTileVO)_holder;
            //    if (battleTileVO.BattleUnitVO != null)
            //    {
            //        IBuffHolder tmpHolder = battleTileVO.BattleUnitVO;
            //        tmpHolder.OnBuffAdd(buff);
            //    }
            //}
            //else
            //{
            // 持续性BUFF(添加及生效)
            _holder.OnBuffAdd((BattleUnitVO)_holder, buff);
            //}


            //如果配置的是自己获得后触发，那buff会被移除
            TriggerBuff(typeof(AddBuffBuffTrigger), buff.BuffCfg);

            //buff时间修改
            if (buff.LeftTime != -1)
            {
                int buffTimeChange = GetBuffTimeChange(buff.BuffCfg);
                if (buffTimeChange != 0)
                {
                    buff.LeftTime += buffTimeChange * FrameRate / 1000;
                    if (buff.LeftTime < 1)
                    {
                        buff.LeftTime = 0;
                        RemoveBuff(buff, BuffRemoveReason.TIME_OUT);
                    }
                    else
                    {
                        buff.Holder.OnBuffUpdate(buff, 3);
                    }
                }
            }


            //buff某些持续效果记录，如修改抵抗率，时间
            foreach (BuffEffect buffEffect in buff.Effects)
            {
                if (buffEffect.EffectType == BuffConstant.BUFF_EFFECT_TYPE_BUFF_RESISTANCE)
                {
                    int type = Convert.ToInt32(buffEffect.EffectParams[0]);
                    int keyID = Convert.ToInt32(buffEffect.EffectParams[1]);
                    Fix64 value = Fix64.Parse(buffEffect.EffectParams[2]);
                    //处理叠加效果
                    if (buff.BuffCfg.StackType == BuffConstant.BUFF_STACK_TYPE_EFFECT)
                    {
                        value = value * buff.StackCount;
                    }

                    buffEffect.EffectValue = value;
                    // 修改BUFF抵抗率
                    UpdateBuffResistanceChange(type, keyID, value);
                }
                else if (buffEffect.EffectType == BuffConstant.BUFF_EFFECT_TYPE_BUFF_TIME)
                {
                    int type = Convert.ToInt32(buffEffect.EffectParams[0]);
                    int keyID = Convert.ToInt32(buffEffect.EffectParams[1]);
                    int value = Convert.ToInt32(buffEffect.EffectParams[2]);
                    //处理叠加效果
                    if (buff.BuffCfg.StackType == BuffConstant.BUFF_STACK_TYPE_EFFECT)
                    {
                        value = value * buff.StackCount;
                    }

                    // 修改BUFF时间
                    buffEffect.EffectValue = value;
                    UpdateBuffTimeChange(type, keyID, value);
                }
            }

            return buff;
        }

        return null;
    }

    /// <summary>
    /// 获取buff组关系
    /// </summary>
    /// <param name="addBuffCfg"></param>
    /// <returns>与已有的buff对应的组关系，{key=buff, value=relation}</returns>
    private Dictionary<Buff, int> GetBuffGroupRelation(CfgBuffData addBuffCfg)
    {
        Dictionary<Buff, int> relations = new Dictionary<Buff, int>();
        if (addBuffCfg.GroupID == 0)
        {
            return relations;
        }

        // 取得BUFF组配置
        CfgBuffGroupRelationData addCfgBuffGroupRelationData =
            CfgBuffGroupRelationTable.Instance.GetDataByID(addBuffCfg.GroupID);
        foreach (Buff buff in GetAllBuffs(true))
        {
            if (buff.BuffCfg.GroupID != 0)
            {
                CfgBuffGroupRelationData cfgBuffGroupRelationData =
                    CfgBuffGroupRelationTable.Instance.GetDataByID(buff.BuffCfg.GroupID);
                int relation = addCfgBuffGroupRelationData.Group[cfgBuffGroupRelationData.Index - 1];
                relations.Add(buff, relation);
            }
        }

        return relations;
    }

    /// <summary>
    /// 帧刷
    /// </summary>
    public void Update()
    {
        CurrFrame++;
        //检查移除
        List<Buff> buffs = GetAllBuffs();
        foreach (Buff buff in buffs)
        {
            if (buff.IsRemoved)
            {
                continue;
            }

            buff.RoundReset();
            if (buff.LeftTime != -1)
            {
                if (buff.BuffCfg.Id == 100)
                {
                    int j = 0;
                }

                --buff.LeftTime;
                buff.Holder.OnBuffUpdate(buff, 1);
                if (buff.LeftTime < 1)
                {
                    buff.LeftTime = 0;
                    RemoveBuff(buff, BuffRemoveReason.TIME_OUT);
                }
            }
        }

        //buff时间触发
        TriggerBuff(typeof(TimeBuffTrigger));
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public void Destory()
    {
        _allBuffs.Clear();
        _allBuffs = null;
        Random = null;
        _holder = null;
    }
}