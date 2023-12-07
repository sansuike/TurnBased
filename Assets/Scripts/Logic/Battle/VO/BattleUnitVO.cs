using System;
using System.Collections;
using System.Collections.Generic;
using FixMath.NET;
using UnityEngine;

public class BattleUnitVO : BaseBattleVO,IBuffHolder
{
    /// <summary>
        /// AI类型
        /// </summary>
        public int AIType { get; set; }
        /// <summary>
        /// 0待新增 1战场中 2待移除 3已移除
        /// </summary>
        public int CallStatus { get; set; }
        /// <summary>
        /// 所属玩家id
        /// </summary>
        public string Pid { get; private set; }
        /// <summary>
        /// 等级
        /// </summary>
        public int Level { get; private set; }
        /// <summary>
        /// 所有技能
        /// </summary>
        public Dictionary<int,int> _skills { get; set; }
        /// <summary>
        /// 技能精炼等级
        /// </summary>
        public Dictionary<int, int> skillPurifyLvs;
        /// <summary>
        /// 技能突破等级
        /// </summary>
        public Dictionary<int, int> skillStrengLvs;
        /// <summary>
        /// 技能强化配置
        /// </summary>
        //public List<CfgSkillStrengthenData> CfgSkillStrengthens;
        
        /// <summary>
        /// 所属队伍类型
        /// </summary>
        public int TroopType { get; private set; }
        /// <summary>
        /// 站位ID
        /// </summary>
        public int BattlePos { get; set; }
        
        /// <summary>
        /// 站位顺序
        /// </summary>
        public int BattleOrder { get; set; }
        
        
        /// <summary>
        /// 创建类型
        /// </summary>
        public int CreateType { get; set; }
        /// <summary>
        /// 皮肤
        /// </summary>
        public int SkinId { get; set; }
        
        /// <summary>
        /// 头像
        /// </summary>
        public int IconId { get; set; }
        
        public bool HasWaitToEnd { get; set; }
        /// <summary>
        /// 瓦片ID
        /// </summary>
        public BattleTileVO Tile { get; set; }
        /// <summary>
        /// 怪物配置Id(怪物)
        /// </summary>
        private int _monsterCfgId;
        /// <summary>
        /// 是否是援手
        /// </summary>
        public bool IsHelper;
        /// <summary>
        /// 战斗力
        /// </summary>
        private int _power;
        /// <summary>
        /// 初始化buff
        /// </summary>
        private List<int> _initBuff;
        /// <summary>
        /// 怪物配置
        /// </summary>
        //public CfgMonsterData CfgMonsterData { get; private set; }
        
        /// <summary>
        /// 职业
        /// </summary>
        //public CfgProfessionData CfgProfessionData { get; private set; }

        /*****************************************以下为战斗字段*********************************************/

        /// <summary>
        /// 当前战斗
        /// </summary>
        public new TurnBaseLogicFight Fight { get; private set; }
        /// <summary>
        /// 玩家
        /// </summary>
        public BattlePlayerVO Player { get; private set; }
        /// <summary>
        /// 战斗属性，与LogicConstant.Attribute保持前段部分序号对齐，方便初始化和管理
        /// </summary>
        protected Fix64[] _fightAttributes;
        /// <summary>
        /// 技能临时属性
        /// </summary>
        public Dictionary<int, Fix64> SkillTemporaryAttrs { get; private set; }
        /// <summary>
        /// buff带来的属性
        /// </summary>
        public Fix64[] _buffAttributes { get; set; }
        
        /// <summary>
        /// 基础属性
        /// </summary>
        public Fix64[] _baseAttributes { get; set; }
        /// <summary>
        /// 特殊状态
        /// </summary>
        public bool[] _SPStatus { get; private set; }
        /// <summary>
        /// 战斗状态
        /// </summary>
        public bool[] FightStatus { get; private set; }
        /// <summary>
        /// 无视战斗状态
        /// </summary>
        public bool[] ImmuneFightStatus { get; private set; }
        /// <summary>
        /// 当前生命
        /// </summary>
        public int HP { get; set; }
        /// <summary>
        /// 技能怒气,控制大招
        /// </summary>
        public int SkillEnergy { get; private set; }
        /// <summary>
        /// 技能需要消耗的AP值。只用在整个队伍中。
        /// </summary>
        public int AP { get; set; }
        /// <summary>
        /// 普攻技能
        /// </summary>
        public BattleSkillVO NormalSkill { get; private set; }
        /// <summary>
        /// 大招技能
        /// </summary>
        public BattleSkillVO UltimateSkill { get; private set; }
        /// <summary>
        /// 反击技能
        /// </summary>
        public BattleSkillVO RevoltSkill { get; private set; }
        /// <summary>
        /// 追击技能
        /// </summary>
        public BattleSkillVO BatterSkill { get; private set; }
        /// <summary>
        /// 所有技能
        /// </summary>
        public List<BattleSkillVO> AllSkills { get; private set; }
        /// <summary>
        /// 亡语技能
        /// </summary>
        public BattleSkillVO DeadthRattleSkill { get; private set; }
        /// <summary>
        /// buff管理器
        /// </summary>
        private BuffManager _BuffManager;
        /// <summary>
        /// 伤害统计
        /// </summary>
        public int DmgRecord { get; private set; }
        
        /// <summary>
        /// 回血统计
        /// </summary>
        public int RecoverRecord { get; private set; }
        
        
        /// <summary>
        /// 受伤统计
        /// </summary>
        public int BearRecord { get; private set; }
        
        /// <summary>
        /// 统计
        /// </summary>
        public Dictionary<int,int> Statistics { get; private set; }
        
        
        ///// <summary>
        ///// 当前行动回合增加的buff
        ///// 记录下来，当update的时候回合数+1,防止行动中自己加buff与别人加buff回合数不一致的问题
        ///// </summary>
        //private HashSet<Buff> _currActionAddBuffs;
        /// <summary>
        /// 行动次数
        /// </summary>
        public int ActionCount;
        /// <summary>
        /// 待机
        /// </summary>
        public int StandByIndex { get; set; }
        /// <summary>
        /// 弱点类型
        /// </summary>
        public Dictionary<int,int> WeakStatus {get; set; }
        /// <summary>
        /// 弱点最大层数
        /// </summary>
        public int WeakMaxNum { get; set; }
        /// <summary>
        /// 当前弱点层数
        /// </summary>
        public int WeakNum { get; set; }
        /// <summary>
        /// 弱点击破回合记录
        /// </summary>
        public int WeakBreakRound { get; set; }
        /// <summary>
        /// skillai按优先级的顺序
        /// value=aiid
        /// </summary>
       
        // public List<KeyValuePair<BattleSkillVO, CfgSkillAIData>> SkillAIOrder;
    
         // /// <summary>
        // /// 当前回合是否已行动
        // /// </summary>
        // public bool CurrTurnRoundAction { get; set; }
        /// <summary>
        /// 存活回合数
        /// </summary>
        public int AliveRound { get; set; }
        /// <summary>
        /// 物件类型
        /// </summary>
        public int ScopeType { get;}
        
        
        /// <summary>
        /// 公用技能CD
        /// </summary>
        public int CommonSkillCD;
    
    public BattleUnitVO ( int scopeType,TurnBaseLogicFight fight , FightUnitPOD unitPOD):base(fight)
    {
        
    }
    
    /// <summary>
    /// 获取护盾
    /// </summary>
    /// <param name="shieldType"></param>
    /// <returns></returns>
    public int GetShield()
    {
        int shield = 0;
        foreach (Buff buff in _BuffManager.GetAllBuffs(true))
        {
            foreach (BuffEffect effect in buff.Effects)
            {
                if (effect.EffectType == BuffConstant.BUFF_EFFECT_TYPE_SHIELD)
                {
                    shield += Fix64.ToInt32(effect.EffectValue);
                    break;//护盾只有一个效果
                }
            }
        }
        return shield;
    }

    public BuffManager GetBuffManager()
    {
        throw new NotImplementedException();
    }

    IEnumerable<IBuffHolder> IBuffHolder.GetBuffTarget(Buff buff, int searchTargetCid)
    {
        throw new NotImplementedException();
    }

    public void OnBuffAdd(BattleUnitVO notifyUnit, Buff buff)
    {
        throw new NotImplementedException();
    }

    public void OnBuffUpdate(Buff buff, int reason)
    {
        throw new NotImplementedException();
    }

    public void OnBuffImmune(int buffCid)
    {
        throw new NotImplementedException();
    }

    public void OnBuffStack(BattleUnitVO notifyUnit, Buff buff, int oldStackCount)
    {
        throw new NotImplementedException();
    }

    public void OnBuffRemove(BattleUnitVO notifyUnit, Buff buff, BuffRemoveReason removeReason)
    {
        throw new NotImplementedException();
    }

    public void OnBuffTrigger(Buff buff)
    {
        throw new NotImplementedException();
    }

    public void OnBuffTriggerEffect(Buff buff, BuffEffect buffEffect, Type triggerType, object[] triggerArgs)
    {
        throw new NotImplementedException();
    }

    public bool CheckTriggerCondition(Buff buff)
    {
        throw new NotImplementedException();
    }

    public bool IsDead()
    {
        throw new NotImplementedException();
    }

    public int getTroopType()
    {
        throw new NotImplementedException();
    }

    public int GetScope()
    {
        throw new NotImplementedException();
    }

    public TurnBaseLogicFight GetFight()
    {
        throw new NotImplementedException();
    }

    IEnumerable<IBuffHolder> IBuffHolder.GetBuffTarget(Buff buff, int searchTargetCid)
    {
        List<BattleUnitVO> targetRange = new List<BattleUnitVO>();
        return targetRange.ToArray();
    }
    
    /// <summary>
    /// 是否可以选择
    /// </summary>
    /// <returns></returns>
    public bool IsSelect()
    {
        /**
         * 1.死亡
         * 2.假死
         * 3.隐身
         * 4.嘲讽
         * 5.混乱
         * 6.魅惑
         */
            
        return  !IsDead() && !FightStatus[BattleConstant.Status.STEALTH] 
                          && !FightStatus[BattleConstant.Status.PLAY_DEAD];
    }
        
    /// <summary>
    /// 是否可以选择
    /// </summary>
    /// <returns></returns>
    public bool IsAction()
    {
        return !IsDead() && !FightStatus[BattleConstant.Status.DIZZY]
                         && !FightStatus[BattleConstant.Status.SLEEP]
                         && !FightStatus[BattleConstant.Status.PLAY_DEAD];
    }
}
