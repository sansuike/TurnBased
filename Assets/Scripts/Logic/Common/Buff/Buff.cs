using FixMath.NET;
using IQIGame.Onigao.Config;
using IQIGame.Onigao.Logic.Battle;
using System;
using System.Collections.Generic;


    /// <summary>
    /// 不能用于dic的key，因为没有重写hashcode和equals，所以逻辑还原的时候不稳定
    /// </summary>
    public class Buff
    {
        /// <summary>
        /// 施放者
        /// </summary>
        internal IBuffHolder Caster { get; set; }
        /// <summary>
        /// 原始技能
        /// </summary>
        private SkillData _skillData { get; set; }
        /// <summary>
        /// 子Buff
        /// </summary>
        internal List<Buff> Children { get; private set; }
        /// <summary>
        /// buff配置
        /// </summary>
        public CfgBuffData BuffCfg { get; private set; }
        /// <summary>
        /// buff管理器
        /// </summary>
        internal BuffManager BuffManager { get; private set; }
        /// <summary>
        /// 触发器
        /// </summary>
        private BaseBuffTrigger _trigger { get; set; }
        /// <summary>
        /// 剩余时间
        /// </summary>
        public int LeftTime { get; set; }
        /// <summary>
        /// 叠加次数
        /// </summary>
        public int StackCount { get; private set; }
        /// <summary>
        /// 最大叠加次数
        /// </summary>
        public int MaxStackCount { get; set; }
        /// <summary>
        /// 效果列表
        /// </summary>
        public List<BuffEffect> Effects { get; private set; }
        /// <summary>
        /// 是否被移除
        /// </summary>
        public bool IsRemoved { get; set; }
        /// <summary>
        /// buff初始持续时间
        /// </summary>
        public int InitBuffTime { get; private set; }

        /// <summary>
        /// 触发几率失败次数
        /// </summary>
        public int TriggerProFailCount { get; set; }
        
        internal Buff(BuffManager buffManager, IBuffHolder caster, SkillData skill, CfgBuffData buffCfg, int buffTime = 0, int stackNum = 1)
        {
            BuffManager = buffManager;
            Caster = caster;
            BuffCfg = buffCfg;
            _skillData = skill;
            Children = new List<Buff>();
            MaxStackCount = buffCfg.StackMaxNumber;
            InitBuffTime = buffTime != 0 ? buffTime : BuffCfg.BuffTime;

            if (InitBuffTime == -1)
            {
                LeftTime = -1;
            }
            else
            {
                LeftTime = InitBuffTime * BuffManager.FrameRate / 1000;
            }

            // 初始化BUFF效果
            Effects = new List<BuffEffect>();
            for (int i = 0; i < BuffCfg.EffectType.Length; i++)
            {
                if (BuffCfg.EffectType[i] > 0)
                {
                    Effects.Add(new BuffEffect(BuffCfg.EffectType[i], BuffCfg.EffectParam[i]));
                }
            }
            StackCount = Math.Min(MaxStackCount, stackNum);

            // 初始化触发器
            InitTrigger();
        }

        /// <summary>
        /// 初始化触发器
        /// </summary>
        private void InitTrigger()
        {
            if (BuffCfg.TriggerType != 0)
            {
                // 创建BUFF触发器
                _trigger = BuffTriggerFactory.CreateBuffTrigger(BuffCfg.TriggerType);
                if (_trigger != null)
                {
                    // 初始化触发器
                    _trigger.Init(this);
                }
            }
        }

        /// <summary>
        /// 处理叠加,目前只重置持续时间
        /// </summary>
        /// <param name="stacker">buff叠加</param>
        /// <param name="stackCount">叠加增量，可能为负</param>
        internal void DoStack(IBuffHolder stacker, int stackCount = 1)
        {
            if (IsRemoved)
            {
                return;
            }
            if (StackCount + stackCount < 1)
            {
                StackCount = 0;
                BuffManager.TriggerBuff(typeof(StackBuffBuffTrigger), BuffCfg);
                BuffManager.RemoveBuff(this, BuffRemoveReason.CLEAN);
                return;
            }
            //先刷新buff
            _Refresh();
            int oldStackCount = StackCount;
            //处理叠加
            StackCount += stackCount;
            if (StackCount > MaxStackCount)
            {
                StackCount = MaxStackCount;
            }

            int addStackCount = StackCount - oldStackCount;//实际叠加的层数，可能为负
            if (addStackCount == 0)
            {
                BuffManager.TriggerBuff(typeof(StackBuffBuffTrigger), BuffCfg);
                return;
            }
            if (addStackCount > 0)
            {
                //叠加，更新buff施放者
                Caster = stacker;
            }

            //修改buff抵抗率
            foreach (BuffEffect buffEffect in Effects)
            {
                if (buffEffect.EffectType == BuffConstant.BUFF_EFFECT_TYPE_BUFF_RESISTANCE)
                {
                    if (BuffCfg.StackType == BuffConstant.BUFF_STACK_TYPE_EFFECT)
                    {
                        int type = Convert.ToInt32(buffEffect.EffectParams[0]);
                        int keyID = Convert.ToInt32(buffEffect.EffectParams[1]);
                        Fix64 value = Fix64.Parse(buffEffect.EffectParams[2]);
                        buffEffect.EffectValue += value * addStackCount;
                        BuffManager.UpdateBuffResistanceChange(type, keyID, value * addStackCount);
                    }
                }
                else if (buffEffect.EffectType == BuffConstant.BUFF_EFFECT_TYPE_BUFF_TIME)
                {
                    if (BuffCfg.StackType == BuffConstant.BUFF_STACK_TYPE_EFFECT)
                    {
                        int type = Convert.ToInt32(buffEffect.EffectParams[0]);
                        int keyID = Convert.ToInt32(buffEffect.EffectParams[1]);
                        int value = Convert.ToInt32(buffEffect.EffectParams[2]);
                        buffEffect.EffectValue += value * addStackCount;
                        BuffManager.UpdateBuffTimeChange(type, keyID, value * addStackCount);
                    }
                }
            }

            BuffManager.Holder.OnBuffStack((BattleUnitVO)BuffManager.Holder, this, addStackCount);
            BuffManager.TriggerBuff(typeof(StackBuffBuffTrigger), BuffCfg);
        }

        /// <summary>
        /// 刷新
        /// </summary>
        private void _Refresh()
        {
            if (IsRemoved)
            {
                return;
            }
            if (InitBuffTime != -1)
            {
                LeftTime = InitBuffTime * BuffManager.FrameRate / 1000;
            }
            if (_trigger != null)
            {
                _trigger.ResetTriggerCount();
            }
            Holder.OnBuffUpdate(this, 2);
        }

        public void RoundReset()
        {
            if (_trigger != null)
            {
                _trigger.RoundResetTriggerCount();
            }
        }
        // /// <summary>
        // /// 剩余触发次数
        // /// </summary>
        // public int LeftTriggerCount
        // {
        //     get
        //     {
        //         if(_trigger == null)
        //         {
        //             return -1;
        //         }
        //         return _trigger.LeftTriggerCount;
        //     }
        // }

        /// <summary>
        /// 触发监听
        /// </summary>
        internal void OnTrigger(Type triggerType, object[] args)
        {
            if (IsRemoved)
            {
                return;
            }
            // 匹配触发标记 && 是否是新创建BUFF
            if (triggerType == BuffManager.Fight.triggerStepType && BuffManager.Fight.triggerStepBuffs.Contains(this))
            {
                return;
            }
            if (Holder.GetScope() == 1)
            {
                BattleTileVO tile = Holder as BattleTileVO;
                if (tile == null || tile.BattleUnitVO == null || tile.BattleUnitVO.IsDead())
                {
                    return;
                }
            }
            else
            {
                if (!BuffCfg.DeathEffective && Holder.IsDead())
                {
                    return;
                }
            }

            if (_trigger != null && _trigger.GetType() == triggerType)
            {
                // 检查触发条件
                if (Holder.CheckTriggerCondition(this))
                {
                    if (_trigger.OnTrigger(args))
                    {
                        // BUFF触发处理
                        DoTrigger(triggerType, args);
                        if (_trigger.IsTriggerLimit(0))
                        {
                            //触发次数达到上限，移除
                            BuffManager.RemoveBuff(this, BuffRemoveReason.TRIGGER_LIMIT);
                        }
                        BuffManager.TriggerBuff(typeof(BattleUnitTriggerBuffTrigger), this.BuffCfg);
                    }
                }
            }
        }

        /// <summary>
        /// buff触发处理
        /// </summary>
        private void DoTrigger(Type triggerType, object[] triggerArgs)
        {
            // 通知消息
            BuffManager.Holder.OnBuffTrigger(this);

            foreach (BuffEffect buffEffect in Effects)
            {
                switch (buffEffect.EffectType)
                {
                    case BuffConstant.BUFF_EFFECT_TYPE_ADD_BUFF:
                    {
                        for (int j = 0; j < buffEffect.EffectParams.Length; j += 5)
                        {
                            int inheritCaster = Convert.ToInt32(buffEffect.EffectParams[j]);
                            int targetCid = Convert.ToInt32(buffEffect.EffectParams[j + 1]);
                            int buffCid = Convert.ToInt32(buffEffect.EffectParams[j + 2]);
                            int buffTime = Convert.ToInt32(buffEffect.EffectParams[j + 3]);
                            int stackNum = Convert.ToInt32(buffEffect.EffectParams[j + 4]);


                            // 提取被攻击者元素BUFF
                            if (buffCid == -1)
                            {
                                BattleUnitVO battleUnitVO = (BattleUnitVO)Caster;
                                if (battleUnitVO.Fight.CurrAnchorPoint != null)
                                {
                                    List<Buff> allBuffs = battleUnitVO.Fight.CurrAnchorPoint.GetBuffManager().GetAllBuffs();
                                    Buff elementBuff = allBuffs.Find(eb => System.Array.IndexOf(eb.BuffCfg.BuffTag, 100) > -1);
                                    if (elementBuff != null)
                                    {
                                        buffCid = elementBuff.BuffCfg.BasicBuffId == 0 ? elementBuff.BuffCfg.Id : elementBuff.BuffCfg.BasicBuffId;
                                    }
                                }
                            }
                            if (buffCid < 0)
                            {
                                return;
                            }

                            foreach (IBuffHolder holder in BuffManager.Holder.GetBuffTarget(this, targetCid))
                            {
                                holder.GetBuffManager().AddBuffInner(inheritCaster == 1 ? Caster : Holder, _skillData, buffCid, buffTime, stackNum);
                            }
                        }
                        break;
                    }

                    case BuffConstant.BUFF_EFFECT_TYPE_ADD_SUB_BUFF:
                    {
                        for (int j = 0; j < buffEffect.EffectParams.Length; j += 5)
                        {
                            int inheritCaster = Convert.ToInt32(buffEffect.EffectParams[j]);
                            int targetCid = Convert.ToInt32(buffEffect.EffectParams[j + 1]);
                            int buffCid = Convert.ToInt32(buffEffect.EffectParams[j + 2]);
                            int buffTime = Convert.ToInt32(buffEffect.EffectParams[j + 3]);
                            int stackNum = Convert.ToInt32(buffEffect.EffectParams[j + 4]);

                            foreach (IBuffHolder holder in BuffManager.Holder.GetBuffTarget(this, targetCid))
                            {
                                Buff childBuff = holder.GetBuffManager().AddBuffInner(inheritCaster == 1 ? Caster : Holder, _skillData, buffCid, buffTime, stackNum);
                                if (childBuff != null && !childBuff.IsRemoved)
                                {
                                    Children.Add(childBuff);
                                }
                            }
                        }
                        break;
                    }

                    case BuffConstant.BUFF_EFFECT_TYPE_RANDOM_ADD_BUFF:
                    {
                        int totalWeight = 0;
                        for (int j = 0; j < buffEffect.EffectParams.Length; j += 6)
                        {
                            totalWeight += Convert.ToInt32(buffEffect.EffectParams[j + 5]);
                        }
                        if (totalWeight > 0)
                        {
                            int cursor = BuffManager.Random.randomInt(totalWeight) + 1;
                            for (int j = 0; j < buffEffect.EffectParams.Length; j += 6)
                            {
                                int weight = Convert.ToInt32(buffEffect.EffectParams[j + 5]);
                                if (weight >= cursor)
                                {
                                    int inheritCaster = Convert.ToInt32(buffEffect.EffectParams[j]);
                                    int targetCid = Convert.ToInt32(buffEffect.EffectParams[j + 1]);
                                    int buffCid = Convert.ToInt32(buffEffect.EffectParams[j + 2]);
                                    int buffTime = Convert.ToInt32(buffEffect.EffectParams[j + 3]);
                                    int stackNum = Convert.ToInt32(buffEffect.EffectParams[j + 4]);

                                    foreach (IBuffHolder holder in BuffManager.Holder.GetBuffTarget(this, targetCid))
                                    {
                                        holder.GetBuffManager().AddBuffInner(inheritCaster == 1 ? Caster : Holder, _skillData, buffCid, buffTime, stackNum);
                                    }
                                    break;
                                }
                                else
                                {
                                    cursor -= weight;
                                }
                            }
                        }
                        break;
                    }

                    case BuffConstant.BUFF_EFFECT_TYPE_RANDOM_ADD_SUB_BUFF:
                    {
                        int totalWeight = 0;
                        for (int j = 0; j < buffEffect.EffectParams.Length; j += 6)
                        {
                            totalWeight += Convert.ToInt32(buffEffect.EffectParams[j + 5]);
                        }
                        if (totalWeight > 0)
                        {
                            int cursor = BuffManager.Random.randomInt(totalWeight) + 1;
                            for (int j = 0; j < buffEffect.EffectParams.Length; j += 6)
                            {
                                int weight = Convert.ToInt32(buffEffect.EffectParams[j + 5]);
                                if (weight >= cursor)
                                {
                                    int inheritCaster = Convert.ToInt32(buffEffect.EffectParams[j]);
                                    int targetCid = Convert.ToInt32(buffEffect.EffectParams[j + 1]);
                                    int buffCid = Convert.ToInt32(buffEffect.EffectParams[j + 2]);
                                    int buffTime = Convert.ToInt32(buffEffect.EffectParams[j + 3]);
                                    int stackNum = Convert.ToInt32(buffEffect.EffectParams[j + 4]);

                                    foreach (IBuffHolder holder in BuffManager.Holder.GetBuffTarget(this, targetCid))
                                    {
                                        Buff childBuff = holder.GetBuffManager().AddBuffInner(inheritCaster == 1 ? Caster : Holder, _skillData, buffCid, buffTime, stackNum);
                                        if (childBuff != null && !childBuff.IsRemoved)
                                        {
                                            Children.Add(childBuff);
                                        }
                                    }
                                    break;
                                }
                                else
                                {
                                    cursor -= weight;
                                }
                            }
                        }
                        break;
                    }

                    case BuffConstant.BUFF_EFFECT_TYPE_DISPEL_BUFF:
                    {
                        int targetId = Convert.ToInt32(buffEffect.EffectParams[0]);
                        for (int j = 1; j < buffEffect.EffectParams.Length; j += 3)
                        {
                            int paramType = Convert.ToInt32(buffEffect.EffectParams[j]);
                            int keyId = Convert.ToInt32(buffEffect.EffectParams[j + 1]);
                            int num = Convert.ToInt32(buffEffect.EffectParams[j + 2]);

                            foreach (IBuffHolder target in Holder.GetBuffTarget(this, targetId))
                            {
                                List<Buff> removeBuffs = new List<Buff>();
                                foreach (Buff buff in target.GetBuffManager().GetAllBuffs(true))
                                {
                                    if (Array.IndexOf(buff.BuffCfg.Properties, BuffConstant.BUFF_PROPERTIES_IMMUNE_DISPEL) == -1)
                                    {
                                        if (BuffUtil.IsMatch(buff.BuffCfg, paramType, keyId))
                                        {
                                            removeBuffs.Add(buff);
                                        }
                                    }
                                }
                                if (removeBuffs.Count > 0)
                                {
                                    removeBuffs = target.GetBuffManager().Random.randomList(removeBuffs);
                                    for (int i = 0; i < num && i < removeBuffs.Count; i++)
                                    {
                                        target.GetBuffManager().RemoveBuff(removeBuffs[i], BuffRemoveReason.DISPEL);
                                        // BUFF释放者
                                        Caster.GetBuffManager().TriggerBuff(typeof(BattleUnitDispelBuffTrigger), removeBuffs[i].BuffCfg);
                                        // BUFF持有者
                                        Holder.GetBuffManager().TriggerBuff(typeof(BattleUnitDispelBuffTrigger), removeBuffs[i].BuffCfg);
                                    }
                                }
                            }
                        }
                        break;
                    }
                    case BuffConstant.BUFF_EFFECT_TYPE_CHANGE_TIME:
                    {
                        for (int j = 0; j < buffEffect.EffectParams.Length; j += 3)
                        {
                            int type = Convert.ToInt32(buffEffect.EffectParams[j]);
                            int typeValue = Convert.ToInt32(buffEffect.EffectParams[j + 1]);
                            int time = Convert.ToInt32(buffEffect.EffectParams[j + 2]);
                            foreach (Buff _buff in BuffManager.GetAllBuffs())
                            {
                                if (_buff.LeftTime != -1)
                                {
                                    if ((type == 1 && _buff.BuffCfg.Id == typeValue) ||
                                    (type == 2 && _buff.BuffCfg.Type == typeValue) ||
                                    (type == 3 && _buff.BuffCfg.GroupID == typeValue))
                                    {
                                        _buff.LeftTime += time * BuffManager.FrameRate / 1000;
                                        if (_buff.LeftTime < 1)
                                        {
                                            _buff.LeftTime = 0;
                                            BuffManager.RemoveBuff(_buff, BuffRemoveReason.TIME_OUT);
                                        }
                                        else
                                        {
                                            _buff.Holder.OnBuffUpdate(_buff, 3);
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    }
                    case BuffConstant.BUFF_EFFECT_TYPE_CHANGE_STACK:
                    {
                        for (int j = 0; j < buffEffect.EffectParams.Length; j += 3)
                        {
                            int buffParamType = Convert.ToInt32(buffEffect.EffectParams[j]);
                            int keyID = Convert.ToInt32(buffEffect.EffectParams[j + 1]);
                            int stackNum = Convert.ToInt32(buffEffect.EffectParams[j + 2]);
                            foreach (Buff buff in BuffManager.GetAllBuffs())
                            {
                                if (BuffUtil.IsMatch(buff.BuffCfg, buffParamType, keyID))
                                {
                                    buff.DoStack(Caster, stackNum);
                                }
                            }
                        }
                        break;
                    }
                    case BuffConstant.BUFF_EFFECT_TYPE_CHANGE_MAX_STACK:
                    {
                        for (int j = 0; j < buffEffect.EffectParams.Length; j += 2)
                        {
                            int buffCid = Convert.ToInt32(buffEffect.EffectParams[j]);
                            int stackMaxNum = Convert.ToInt32(buffEffect.EffectParams[j + 1]);
                            foreach (Buff buff in BuffManager.GetAllBuffs())
                            {
                                if (buff.BuffCfg.Id == buffCid)
                                {
                                    if (buff.MaxStackCount <= stackMaxNum)
                                    {
                                        buff.MaxStackCount = stackMaxNum;
                                    }
                                    else
                                    {
                                        buff.DoStack(Caster, -(buff.MaxStackCount - stackMaxNum));
                                        buff.MaxStackCount = stackMaxNum;
                                    }
                                }
                            }
                        }
                        break;
                    }
                    default:
                    {
                        //if (BuffManager.Holder.GetType() == typeof(BattleTileVO))
                        //{
                        //    // 作用于地板上的英雄
                        //    BattleTileVO battleTileVO = (BattleTileVO)BuffManager.Holder;
                        //    if (battleTileVO.BattleUnitVO != null)
                        //    {
                        //        IBuffHolder tmpHolder = battleTileVO.BattleUnitVO;
                        //        tmpHolder.OnBuffTriggerEffect(this, buffEffect, triggerType, triggerArgs);
                        //    }
                        //}
                        //else
                        //{
                        BuffManager.Holder.OnBuffTriggerEffect(this, buffEffect, triggerType, triggerArgs);
                        //}
                        break;
                    }
                }
            }
        }

        public SkillData SkillData
        {
            get
            {
                return _skillData;
            }
        }

        /// <summary>
        /// buff持有者
        /// </summary>
        public IBuffHolder Holder
        {
            get
            {
                return BuffManager.Holder;
            }
        }


    }
