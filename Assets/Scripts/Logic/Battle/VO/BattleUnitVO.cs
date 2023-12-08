using FixMath.NET;
using IQIGame.Onigao.Config;
using IQIGame.Onigao.Logic.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CxExtension;
using Unity.Mathematics;


    /// <summary>
    /// 战斗单位数据
    /// </summary>
    public class BattleUnitVO : BaseBattleVO, IBuffHolder
    {
        private LogicLogger _logger = BaseLogicFight.Logger;
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
        public List<CfgSkillStrengthenData> CfgSkillStrengthens;
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
        public CfgMonsterData CfgMonsterData { get; private set; }
        /// <summary>
        /// 职业
        /// </summary>
        public CfgProfessionData CfgProfessionData { get; private set; }

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
        public List<KeyValuePair<BattleSkillVO, CfgSkillAIData>> SkillAIOrder;
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
        
        
        public BattleUnitVO(int scopeType,TurnBaseLogicFight fight, FightUnitPOD unitPOD) : base(fight)
        {
            if (unitPOD.TroopType == BattleConstant.TroopType.ATTACK)
            {
                AIType = unitPOD.AIType;
            }
            else
            {
                AIType = 1;
            }

            ScopeType = scopeType;
            CallStatus = 1;
            Statistics = new Dictionary<int, int>();
            Fight = fight;
            Pid = unitPOD.Pid;
            AllSkills = new List<BattleSkillVO>();
            Level = unitPOD.Level;
            _power = unitPOD.Power;
            _monsterCfgId = unitPOD.MonsterCfgId;
            CfgMonsterData = CfgMonsterTable.Instance.GetDataByID(_monsterCfgId);
            CfgProfessionData = CfgProfessionTable.Instance.GetDataByID(CfgMonsterData.Profession);
            if (!string.IsNullOrEmpty(Pid))
            {
                Player = Fight.Players[Pid];
                Level = unitPOD.Level;
            }
            else {
               
                if (scopeType == BattleConstant.ScopeType.CALL)
                {
                    
                    Level = unitPOD.Level;
                }
                else
                {
                    Level = CfgMonsterData.Level;
                }
            }
            TroopType = unitPOD.TroopType;
            BattlePos = unitPOD.BattlePos;
            BattleOrder = unitPOD.Order;
            SkinId = unitPOD.SkinId;
            IconId = unitPOD.IconId;
            IsHelper = unitPOD.IsHelper;
            _skills = new Dictionary<int, int>();
            skillPurifyLvs = new Dictionary<int, int>();
            skillStrengLvs = new Dictionary<int, int>();
            for (int i = 0; i < unitPOD.Skills.Count; i++)
            {
                _skills[unitPOD.Skills[i]] = unitPOD.SkillLvs[i];
                skillPurifyLvs[unitPOD.Skills[i]] = unitPOD.SkillPurifyLvs[i];
                skillStrengLvs[unitPOD.Skills[i]] = unitPOD.SkillStrengLvs[i];
            }
            
            // 职业技能
            // CfgProfessionData.Skills
            if (CfgProfessionData != null && TroopType == BattleConstant.TroopType.ATTACK)
            {
                foreach (int skillCid in CfgProfessionData.Skills)
                {
                    if (_skills.ContainsKey(skillCid))
                    {
                        continue;
                    }
                    _skills[skillCid] = 1;
                    skillPurifyLvs[skillCid] = 0;
                    skillStrengLvs[skillCid] = 0;
                }
            }
            
            // monster技能
            for (int i = 0; i < CfgMonsterData.Skill.Length; i++)
            {
                if (_skills.ContainsKey(CfgMonsterData.Skill[i]))
                {
                    continue;
                }
                CfgSkillData skillData = CfgSkillTable.Instance.GetDataByID(CfgMonsterData.Skill[i]);
                _skills[skillData.Id] = skillData.Level;
                skillPurifyLvs[skillData.Id] = 0;
                skillStrengLvs[skillData.Id] = 0;
            }
            
            _initBuff = unitPOD.InitBuff;
            //if (this.GetType() != typeof(BattleTileVO)) { }
            // 弱点
            WeakStatus = new Dictionary<int, int>();
            for (int i = 0; i < CfgMonsterData.WeakType.Length; i++)
            {
                WeakStatus.Add(CfgMonsterData.WeakType[i],CfgMonsterData.WeakStatus[i]);
            }
            WeakMaxNum = CfgMonsterData.WeakMaxNum;
            WeakNum = WeakMaxNum;
            WeakBreakRound = -1;
            CreateType = 0;
            // 技能强化项
            CfgSkillStrengthens = new List<CfgSkillStrengthenData>();
            for (int i = 0; i < unitPOD.SkillStrengthens.Count; i++)
            {
                CfgSkillStrengthens.Add(CfgSkillStrengthenTable.Instance.GetDataByID(unitPOD.SkillStrengthens[i]));
            }

            // 初始化战斗状态
            FightStatus = new bool[BattleConstant.Status.STATUS_NUM];
            // 无视战斗状态
            ImmuneFightStatus = new bool[BattleConstant.Status.STATUS_NUM];
            // 特殊战斗状态
            _SPStatus = new bool[BattleConstant.SPStatus.SPSTATUS_NUM];

            //初始化战斗属性
            SkillTemporaryAttrs = new Dictionary<int, Fix64>();
            _buffAttributes = new Fix64[BattleConstant.Attribute.ATTRIBUTE_NUM];
            _fightAttributes = new Fix64[BattleConstant.Attribute.ATTRIBUTE_NUM];
            _baseAttributes = new Fix64[BattleConstant.Attribute.ATTRIBUTE_NUM];
            for (int i = 0; i < unitPOD.Attributes.Length; i++)
            {
                _fightAttributes[i] = unitPOD.Attributes[i];
            }
            for (int i = 0; i < unitPOD.BaseAttrs.Length; i++)
            {
                _baseAttributes[i] = unitPOD.BaseAttrs[i];
            }

            // 怒气  
            SkillEnergy = Fix64.ToInt32(_fightAttributes[BattleConstant.Attribute.TYPE_ENERGY]);
            // 生命
            HP = Fix64.ToInt32(_fightAttributes[BattleConstant.Attribute.TYPE_HP]);
            // AP
            AP = Fix64.ToInt32(_fightAttributes[BattleConstant.Attribute.TYPE_AP]);

            //战斗属性默认值
            for (int i = BattleConstant.Attribute.TYPE_CURR_BE_CURR_HURT; i < BattleConstant.Attribute.ATTRIBUTE_NUM; i++)
            {
                _fightAttributes[i] = 0;
            }

            if (HP <= 0)
            {
                HP = 0;
                if (!FightStatus[BattleConstant.Status.PLAY_DEAD])
                {
                    _SPStatus[BattleConstant.SPStatus.DEAD] = true;
                }

            }

            //寻找各种特殊技能并赋值
            foreach (var entry in _skills)
            {
                BattleSkillVO skill = new BattleSkillVO(Fight, entry.Key,entry.Value,skillPurifyLvs[entry.Key],skillStrengLvs[entry.Key], CfgSkillStrengthens);
                //if (skill.CfgSkillData.Area == SkillConstant.AREA_BATTLE)
                {
                    skill.CoolDown = skill.SkillData.InitCD;
                    switch (skill.CfgSkillData.SkillType)
                    {
                        case SkillConstant.TYPE_NORMAL:
                            NormalSkill = skill;
                            // skill.CoolDown = skill.SkillData.InitCD;
                            // AllSkills.Add(skill);
                            break;
                        case SkillConstant.TYPE_ULTIMATE:
                            UltimateSkill = skill;
                            // skill.CoolDown = skill.SkillData.InitCD;
                            // AllSkills.Add(skill);
                            break;
                        case SkillConstant.TYPE_REVOLT:
                            RevoltSkill = skill;
                        //    skill.CoolDown = skill.SkillData.InitCD;
                        //    AllSkills.Add(skill);
                            break;
                        case SkillConstant.TYPE_BATTER:
                            BatterSkill = skill;
                        //    CoreSkill2 = skill;
                        //    skill.CoolDown = skill.SkillData.InitCD;
                        //    AllSkills.Add(skill);
                            break;
                        case SkillConstant.TYPE_DEADTH_RATTLE:
                            DeadthRattleSkill = skill;
                            break;
                        default:
                            break;
                    }

                    if (skill.CfgSkillData.ReleaseType == 1)
                    {
                        AllSkills.Add(skill);    
                    }
                }
            }

            if (NormalSkill == null && scopeType != BattleConstant.ScopeType.TEAM)
            {
                // 取消默认普攻
                // NormalSkill = new BattleSkillVO(Fight, CfgMonsterData.NormalSkill,0, CfgSkillStrengthens);
                // NormalSkill.CoolDown = NormalSkill.SkillData.InitCD;
                // AllSkills.Add(NormalSkill);
            }


            // 技能AI
            SkillAIOrder = new List<KeyValuePair<BattleSkillVO, CfgSkillAIData>>();
            foreach (BattleSkillVO skill in AllSkills)
            {
                foreach (CfgSkillAIData skillAIData in skill.SkillData.SkillAIData)
                {
                    if (skillAIData == null)
                    {
                        continue;
                    }
                    SkillAIOrder.Add(new KeyValuePair<BattleSkillVO, CfgSkillAIData>(skill, skillAIData));
                }
            }

            // 技能排序
            SkillAIOrder.Sort((o1, o2) =>
            {
                return o2.Value.Priority.CompareTo(o1.Value.Priority);
            });

            //初始化buff管理器
            _BuffManager = new BuffManager();
            _BuffManager.Init(Fight,1000, Fight.Random.randomInt(10000), this);// 由于本身是按毫秒为单位，现在按回合算，所以帧率写死1000。

            // // 统计日志
            // if (scopeType == BattleConstant.ScopeType.MONSTER)
            // {
            //     Hashtable fightStart_info = StatisticsUtil.GetHashTable(Fight.Statistics, "fightStart");
            //     ArrayList units_info = StatisticsUtil.GetArrayList(fightStart_info, TroopType == BattleConstant.TroopType.ATTACK ? "attackers" : "defenders");
            //
            //     Hashtable unit_info = new Hashtable();
            //     unit_info.Add("id",CfgMonsterData.Id);
            //     unit_info.Add("pos",unitPOD.BattlePos);
            //     unit_info.Add("isHelper",unitPOD.IsHelper);
            //     units_info.Add(unit_info);
            //
            //     ArrayList skills_info = StatisticsUtil.GetArrayList(unit_info, "skills");
            //     foreach (BattleSkillVO skillVo in AllSkills)
            //     {
            //         Hashtable skill_info = new Hashtable();
            //         skill_info.Add("id",skillVo.SkillData.CfgSkillData.Id);
            //         skill_info.Add("type",skillVo.CfgSkillData.Type);
            //         skill_info.Add("name",CfgI18NTable.Instance.GetDataByID(Convert.ToInt32(skillVo.CfgSkillData.Name)).Str);
            //         skill_info.Add("isSubSkill",skillVo.IsSubSkill);
            //         skills_info.Add(skill_info);
            //     }
            // }
            PrintLog();
        }

        /// <summary>
        /// 统计伤害
        /// </summary>
        /// <param name="dmg"></param>
        public void AddDmgRecord(int dmg)
        {
            if (dmg <= 0)
            {
                return;
            }
            DmgRecord += dmg;
            if (TroopType == BattleConstant.TroopType.ATTACK)
            {
                Fight.AddUpdateUnit(this, BattleConstant.UpdateType.UPDATE_DMG_RECORDS, dmg);
            }
        }
        
        
        /// <summary>
        /// 统计恢复
        /// </summary>
        /// <param name="dmg"></param>
        public void AddDmgRecover(int dmg)
        {
            if (dmg <= 0)
            {
                return;
            }
            RecoverRecord += dmg;
            // if (TroopType == BattleConstant.TroopType.ATTACK)
            // {
            //     Fight.AddUpdateUnit(this, BattleConstant.UpdateType.UPDATE_DMG_RECORDS, dmg);
            // }
        }
        
        
        /// <summary>
        /// 统计受伤
        /// </summary>
        /// <param name="dmg"></param>
        public void AddBearRecover(int dmg)
        {
            if (dmg > 0)
            {
                return;
            }
            BearRecord += dmg;
            // if (TroopType == BattleConstant.TroopType.ATTACK)
            // {
            //     Fight.AddUpdateUnit(this, BattleConstant.UpdateType.UPDATE_DMG_RECORDS, dmg);
            // }
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

        /// <summary>
        /// 扣除护盾
        /// </summary>
        /// <param name="elementType"></param>
        /// <param name="value"></param>
        private void LoseShield(int value)
        {
            if (value <= 0)
            {
                return;
            }

            
            foreach (Buff buff in _BuffManager.GetAllBuffs())
            {
                foreach (BuffEffect effect in buff.Effects)
                {
                    if (effect.EffectType == BuffConstant.BUFF_EFFECT_TYPE_SHIELD)
                    {
                        if (value >= Fix64.ToInt32(effect.EffectValue))
                        {
                            value -= Fix64.ToInt32(effect.EffectValue);
                            effect.EffectValue = 0;
                            //护盾值用完，移除
                            _BuffManager.RemoveBuff(buff, BuffRemoveReason.NONEFFECTIVE);
                        }
                        else
                        {
                            effect.EffectValue = Fix64.ToInt32(effect.EffectValue) - value;
                            value = 0;
                            _BuffManager.TriggerBuff(typeof(BattleUnitShieldChangeBuffTrigger));
                            Fight.AddUpdateUnit(this, BattleConstant.UpdateType.UPDATE_SHIELD, GetShield(), 1);
                            return;
                        }
                        break;//护盾只有一个效果
                    }
                }
            }
        }

        /// <summary>
        /// 伤害限制
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int DamageLimit(int damage)
        {
            foreach (Buff buff in _BuffManager.GetAllBuffs(true))
            {
                foreach (BuffEffect effect in buff.Effects)
                {
                    if (effect.EffectType == BuffConstant.BUFF_EFFECT_TYPE_BATTLE_DAMAGE_LIMIT)
                    {

                        int limitType = Convert.ToInt32(effect.EffectParams[0]);
                        int DamageType = Convert.ToInt32(effect.EffectParams[1]);
                        int limitValue = Convert.ToInt32(effect.EffectParams[2]);
                        switch (limitType)
                        {
                            // 上限
                            case 1:
                                damage = Math.Min(damage, limitValue);
                                break;
                            // 破防
                            case 2:
                                damage = damage > limitValue ? damage : 1;
                                break;
                        }
                    }
                }
            }
            return damage;
        }

        /// <summary>
        /// 累计伤害
        /// </summary>
        /// <param name="elementType"></param>
        /// <param name="value"></param>
        private void CumulativeHurt(int value)
        {
            if (value >= 0)
            {
                return;
            }
            int _v = Math.Abs(value);
            
            // 当前主技能受伤
            if (Fight.CurrSkill!=null) {
                // if (!Fight.CurrSkill.IsSubSkill)
                // {
                _fightAttributes[BattleConstant.Attribute.TYPE_CURR_BE_MAIN_SKILL_HURT] += _v;    
                // }
                // 当前技能受伤
                _fightAttributes[BattleConstant.Attribute.TYPE_CURR_BE_SKILL_HURT] += _v;
            }
            
            // 当前回合受伤
            _fightAttributes[BattleConstant.Attribute.TYPE_CURR_BE_CURR_HURT] += _v;

            // 当前战斗受伤
            _fightAttributes[BattleConstant.Attribute.TYPE_CURR_BE_FIGHT_HURT] += _v;
            
            // 累计受伤
            foreach (Buff buff in _BuffManager.GetAllBuffs())
            {
                foreach (BuffEffect effect in buff.Effects)
                {
                    if (effect.EffectType == BuffConstant.BUFF_EFFECT_TYPE_BATTLE_CUMULATIVE_HURT)
                    {
                        effect.EffectValue += _v;
                        break;
                    }
                }
            }
            
            // 最后一次行动技能伤害
            if (Fight.CurrSkill!=null && Fight.CurrMover != null)
            {
                Fight.CurrMover._fightAttributes[BattleConstant.Attribute.TYPE_LAST_HURT] += _v;    
            }
        }

        /// <summary>
        /// 初始化buff
        /// </summary>
        public virtual void InitBuff()
        {
            // 配置表BUFF
            for (int i = 0; i < CfgMonsterData.InitialBuff.Length; i += 3)
            {
                int buffCid = CfgMonsterData.InitialBuff[i];
                int buffTime = CfgMonsterData.InitialBuff[i + 1];
                int stackNum = CfgMonsterData.InitialBuff[i + 2];
                _BuffManager.AddBuff(this, null, buffCid, buffTime, stackNum);
            }

            //初始化技能buff
            foreach (var entry in _skills)
            {
                // ？？？ 此处为什么不用初始化创建的技能对象
                BattleSkillVO skill = new BattleSkillVO(Fight, entry.Key,entry.Value,skillPurifyLvs[entry.Key],skillStrengLvs[entry.Key], CfgSkillStrengthens);
                //if (skill.CfgSkillData.Area == SkillConstant.AREA_BATTLE)
                {
                    for (int j = 0; j < skill.SkillData.BuffTarget.Count; j++)
                    {
                        // 战斗开始前BUFF
                        if (skill.SkillData.BuffOrder[j] == 0 && Fight.Random.randomFix64() < skill.SkillData.BuffProbability[j])
                        {
                            GetBuffManager().AddBuff(skill.SkillData.BuffTarget[j], this, skill.SkillData, skill.SkillData.BuffID[j], skill.SkillData.BuffTime[j], skill.SkillData.BuffStackNum[j]);
                        }
                    }
                }
            }

            // 服务器带入的BUFF
            foreach (int buffCid in _initBuff)
            {
                _BuffManager.AddBuff(this, null, buffCid);
            }
        }

        /// <summary>
        /// 选择技能
        /// </summary>
        /// <returns></returns>
        public BattleSkillVO ChooseSkill()
        {
            foreach (KeyValuePair<BattleSkillVO, CfgSkillAIData> kv in SkillAIOrder)
            {
                if (SkillAI.CheckSkillAI(this, kv.Key, kv.Value))
                {
                    return kv.Key;
                }
            }
            return null;
        }

        public void ActionStart()
        {
            ActionCount++;
        }

        public void ActionOver()
        {
            //_currActionAddBuffs = null;
        }

        /// <summary>
        /// 是否死亡
        /// </summary>
        /// <returns></returns>
        virtual public bool IsDead()
        {
            return GetSpStatus(BattleConstant.SPStatus.DEAD);
        }

        /// <summary>
        /// 判定特殊状态
        /// </summary>
        public void CheckSpStatus()
        {
            if (GetSpStatus(BattleConstant.SPStatus.DEAD))
            {
                if (HP > 0)
                {
                    //复活
                    SetSpStatus(BattleConstant.SPStatus.DEAD, false);
                }
                return;
            }
            if (HP <= 0)
            {
                HP = 0;
                GetBuffManager().TriggerBuff(typeof(BattleUnitBeforeDeadBuffTrigger));
                if (HP <= 0)
                {
                    HP = 0;

                    if (!FightStatus[BattleConstant.Status.PLAY_DEAD])
                    {
                        SetSpStatus(BattleConstant.SPStatus.DEAD, true);
                        AliveRound = Fight._round;
                        if (DeadthRattleSkill != null)
                        {
                            Fight.TriggerSkillUnits.Enqueue(new KeyValuePair<BattleUnitVO, BattleSkillVO>(this, DeadthRattleSkill));
                        }
                        _BuffManager.TriggerRemove(BuffConstant.BUFF_REMOVE_TRIGGER_TYPE_DEAD);
                        GetBuffManager().TriggerBuff(typeof(BattleUnitAfterDeadBuffTrigger));

                        if (Fight.CfgMonsterTeamData != null && Fight.CfgMonsterTeamData.BackTeam.Length > Fight.tubstituteIndex && TroopType == BattleConstant.TroopType.DEFEND)
                        {
                            // 死亡后补怪
                            int monsterId = Fight.CfgMonsterTeamData.BackTeam[Fight.tubstituteIndex];
                            BattleCallVO battleCallVO = new BattleCallVO(this,BattlePos,monsterId,2);
                            battleCallVO.InitBuff();
                            Fight.AllBattleCalls().Add(battleCallVO.ID,battleCallVO);
                            // Fight.AddUpdateUnit(battleCallVO.Summoner, BattleConstant.UpdateType.SUMMON_ADD, battleCallVO.ToData());
                            Fight.tubstituteIndex++;
                        }
                    }
                    
                    
                    // 单位死亡触发
                    foreach (KeyValuePair<int, BattleUnitVO> kv in Fight.GetAllBattleUnits())
                    {
                        kv.Value.GetBuffManager().TriggerBuff(typeof(BattleUnitDeadBuffTrigger));
                    }

                    // 死亡后离开地板
                    if (this.ScopeType == BattleConstant.ScopeType.MONSTER || this.ScopeType == BattleConstant.ScopeType.CALL)
                    {
                        var oldTile = this.Tile;
                        if (oldTile != null)
                        {
                            oldTile.Leave();
                        }
                    
                    }
                }
            }
        }

        /// <summary>
        /// 获取特殊状态
        /// </summary>
        /// <param name="spType"></param>
        /// <returns></returns>
        public bool GetSpStatus(int spType)
        {
            return _SPStatus[spType];
        }

        /// <summary>
        /// 设置特殊状态
        /// </summary>
        /// <param name="spType"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public void SetSpStatus(int spType, bool status)
        {
            Fight.AddUpdateUnit(this, BattleConstant.UpdateType.ADD_SP_STATUS, spType,status ? 1 : 0);
            _SPStatus[spType] = status;
            _TriggerBattleUnitAddSPStatus(spType);
            if (spType == BattleConstant.SPStatus.DEAD)
            { 
                SkillEnergy = 0;
                Fight.AddUpdateUnit(this, BattleConstant.UpdateType.CHANGE_ENERGY, SkillEnergy, 0);
            }
        }

        /// <summary>
        /// 修改生命
        /// </summary>
        /// <param name="hp"></param>
        public void ChangeHP(int hp, bool show,bool isSkill, BattleUnitVO source, int elementType, bool shieldCost = false)
        {
            // 禁止治疗
            if (hp > 0 && FightStatus[BattleConstant.Status.SEAL_HEAL])
            {
                return;
            }
            show = true;

            if (hp < 0)
            {
                //守护
                BattleUnitVO protect = null;
                string[] protectEffect = null;
                foreach (Buff buff in GetBuffManager().GetAllBuffs(true))
                {
                    foreach (BuffEffect effect in buff.Effects)
                    {
                        if (effect.EffectType == BuffConstant.BUFF_EFFECT_TYPE_PROTECT)
                        {
                            protect = buff.Caster as BattleUnitVO;
                            protectEffect = effect.EffectParams;
                            break;
                        }
                    }
                }

                if (protect != null && protectEffect.Length == 2)
                {
                    if (Convert.ToInt32(protectEffect[0]) == -1 || Convert.ToInt32(protectEffect[0]) == elementType)
                    {
                        int protectHp = (int)(hp * Fix64.Parse(protectEffect[1]));
                        protect.ChangeHpInner(protectHp, show,isSkill, source, elementType, shieldCost);
                        hp -= protectHp;
                    }
                }
            }


            if (hp < 0 && FightStatus[BattleConstant.Status.DMG_LINK])
            {
                //连接
                foreach (BattleUnitVO unit in Fight.GetAllBattleUnits().Values)
                {
                    if (!unit.IsDead() && unit.FightStatus[BattleConstant.Status.DMG_LINK])
                    {
                        unit.ChangeHpInner(hp, true, isSkill,source, 0, false);
                        //移除一层连接buff
                        foreach (Buff buff in unit.GetBuffManager().GetAllBuffs())
                        {
                            foreach (BuffEffect effect in buff.Effects)
                            {
                                if (effect.EffectType == BuffConstant.BUFF_EFFECT_TYPE_CHANGE_BATTLE_STATUS)
                                {
                                    foreach (string param in effect.EffectParams)
                                    {
                                        int status = Convert.ToInt32(param);
                                        if (status == BattleConstant.Status.DMG_LINK)
                                        {
                                            buff.DoStack(this, -1);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                // 灵魂锁链
                if (IsAverage(hp))
                {
                    List<KeyValuePair<int, BattleUnitVO>> units = Fight.GetAllBattleUnits().Where(t => !t.Value.IsDead() && t.Value.TroopType == TroopType && t.Value.IsAverage(hp)).ToList();
                    int averageHP = (int)Math.Ceiling(hp / units.Count * 1.0f);
                    foreach (KeyValuePair<int, BattleUnitVO> unit in units)
                    {
                        unit.Value.ChangeHpInner(averageHP, show,isSkill, source, elementType, shieldCost);
                    }
                }
                else
                {
                    ChangeHpInner(hp, show,isSkill, source, elementType, shieldCost);
                }
            }
        }

        private bool IsAverage(int hp)
        {
            List<BuffEffect> buffEffects = GetBuffEffect(BuffConstant.BUFF_EFFECT_TYPE_AVERAGE);

            bool average = false;
            foreach (BuffEffect buffEffect in buffEffects)
            {
                switch (Convert.ToInt32(buffEffect.EffectParams[0]))
                {
                    case 1:
                        average = true;
                        break;
                    case 2:
                        average = hp < 0;
                        break;
                    case 3:
                        average = hp > 0;
                        break;
                }

                if (average)
                {
                    return average;
                }
            }

            return average;
        }

        private void ChangeHpInner(int hp, bool show,bool isSkill, BattleUnitVO source, int elementType, bool shieldCost)
        {
            if (hp == 0)
            {
                return;
            }

            // 查看是否睡眠状态
            if (hp < 0 && FightStatus[BattleConstant.Status.SLEEP])
            {
                foreach (Buff buff in GetBuffManager().GetAllBuffs())
                {
                    foreach (BuffEffect effect in buff.Effects)
                    {
                        if (effect.EffectType == BuffConstant.BUFF_EFFECT_TYPE_CHANGE_BATTLE_STATUS)
                        {
                            foreach (string param in effect.EffectParams)
                            {
                                int status = Convert.ToInt32(param);
                                if (status == BattleConstant.Status.SLEEP)
                                {
                                    GetBuffManager().RemoveBuff(buff,BuffRemoveReason.CLEAN);
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            // 累计伤害
            CumulativeHurt(hp);

            if (hp < 0 && shieldCost)
            {
                //伤害，检查护盾
                int shield = GetShield();
                if (shield > 0)
                {
                    int dmg = -hp;
                    if (shield >= dmg)
                    {
                        LoseShield(dmg);
                        return;//被护盾完全吸收
                    }
                    else
                    {
                        hp = -(dmg - shield);
                        LoseShield(shield);
                    }
                }
            }

            // 是否统计真实数据
            int oldHP = HP;
            
            HP += hp;
            HP = Math.Min(HP, Fix64.ToInt32(GetBattleAttribute(BattleConstant.Attribute.TYPE_HP_MAX)));
            HP = Math.Max(HP,0);
            Fight.AddUpdateUnit(this, BattleConstant.UpdateType.CHANGE_HP, Math.Max(0, HP), show ? hp : 0, elementType);

            if (hp > 0)
            {
                _fightAttributes[BattleConstant.Attribute.TYPE_LAST_HEAL_2] = hp;
            }
            // 真实改变数据
            hp = HP - oldHP;
            
            
            
            if (hp < 0)
            {
                //掉血，记录伤害
                if (source != null)
                {
                    source.AddDmgRecord(-hp);
                }
                // 受伤统计
                AddBearRecover(hp);
                _BuffManager.TriggerBuff(typeof(BattleUnitBeHurtBuffTrigger));
            }
            else
            {
                //加血，记录恢复
                AddDmgRecover(hp);
                // 记录最后一次治疗
                _fightAttributes[BattleConstant.Attribute.TYPE_LAST_HEAL] = hp;
                
                _BuffManager.TriggerBuff(typeof(BattleUnitBeHealBuffTrigger),isSkill);
            }

            _BuffManager.TriggerBuff(typeof(BattleUnitAttrChangeBuffTrigger), BattleConstant.Attribute.TYPE_HP);
            
            CheckSpStatus();
        }

        /// <summary>
        /// 触发单位获得特殊状态
        /// </summary>
        /// <param name="spStatus"></param>
        private void _TriggerBattleUnitAddSPStatus(int spStatus)
        {
            Fight.StartTriggerStep(typeof(BattleUnitAddSPStatusBuffTrigger));

            if (TroopType == BattleConstant.TroopType.ATTACK)
            {
                Fight.AttackTroopUnitVO.GetBuffManager().TriggerBuff(typeof(BattleUnitAddSPStatusBuffTrigger), 3, spStatus);
                Fight.DefendTroopUnitVO.GetBuffManager().TriggerBuff(typeof(BattleUnitAddSPStatusBuffTrigger), 4, spStatus);
            }
            else
            {
                Fight.AttackTroopUnitVO.GetBuffManager().TriggerBuff(typeof(BattleUnitAddSPStatusBuffTrigger), 4, spStatus);
                Fight.DefendTroopUnitVO.GetBuffManager().TriggerBuff(typeof(BattleUnitAddSPStatusBuffTrigger), 3, spStatus);
            }

            foreach (KeyValuePair<int, BattleUnitVO> kv in Fight.GetAllBattleUnits())
            {
                if (kv.Value.ID == ID)
                {
                    //自己
                    kv.Value.GetBuffManager().TriggerBuff(typeof(BattleUnitAddSPStatusBuffTrigger), 1, spStatus);
                }
                else
                {
                    kv.Value.GetBuffManager().TriggerBuff(typeof(BattleUnitAddSPStatusBuffTrigger), 2, spStatus);
                }
                if (kv.Value.TroopType == TroopType)
                {
                    kv.Value.GetBuffManager().TriggerBuff(typeof(BattleUnitAddSPStatusBuffTrigger), 3, spStatus);
                }
                else
                {
                    kv.Value.GetBuffManager().TriggerBuff(typeof(BattleUnitAddSPStatusBuffTrigger), 4, spStatus);
                }
            }

            Fight.EndTriggerStep();
        }

        /// <summary>
        /// 改变技能怒气
        /// </summary>
        /// <param name="addEnergy"></param>
        public void ChangeSkillEnergy(int energy,bool canAddition = true, bool show = false)
        {
            
            if (canAddition && energy > 0)
            {
                CfgDiscreteDataData discreteData = CfgDiscreteDataTable.Instance.GetDataByID(101);
                Fix64 max = (Fix64)(discreteData.Data[0] / 10000.0f);
                Fix64 add = Fix64.Min(max, (1 + GetBattleAttribute(BattleConstant.Attribute.TYPE_ENERGY_PERCENT)));
                if (add > (Fix64)1.0)
                {
                    energy = Fix64.ToInt32(energy * add);    
                }
            }
            if (energy == 0)
            {
                return;
            }
            if (energy > 0 && FightStatus[BattleConstant.Status.ENERGY_LIMIT])
            {
                return;
            }

            int oldSkillEnergy = SkillEnergy;
            SkillEnergy += energy;

            SkillEnergy = Math.Min(SkillEnergy, Fix64.ToInt32(GetBattleAttribute(BattleConstant.Attribute.TYPE_ENERGY_MAX)));
            SkillEnergy = Math.Max(0, SkillEnergy);
            Fight.AddUpdateUnit(this, BattleConstant.UpdateType.CHANGE_ENERGY, SkillEnergy, show ? energy : 0);

            _BuffManager.TriggerBuff(typeof(BattleUnitAttrChangeBuffTrigger), BattleConstant.Attribute.TYPE_ENERGY);

            // 记录真实怒气
            int change = SkillEnergy - oldSkillEnergy;
            // 当前回合怒气改变值
            _fightAttributes[BattleConstant.Attribute.TYPE_CURRENT_ROUND_CHANGE_SKILL_ENERGY] += change;
            
            int type = change > 0
                ? BattleConstant.UNIT_STATISTICS.ADD_ENERGY
                : BattleConstant.UNIT_STATISTICS.USE_ENERGY;
            int totalEnergy = 0;
            Statistics.TryGetValue(type,out totalEnergy);
            totalEnergy += Math.Abs(change);
            Statistics[type] = totalEnergy;
        }


        /// <summary>
        /// 改变AP
        /// </summary>
        /// <param name="ChangeAP"></param>
        public void ChangeAP(int ap, bool show = false)
        {
            if (ap == 0)
            {
                return;
            }
            if (ap > 0 && FightStatus[BattleConstant.Status.AP_LIMIT])
            {
                return;
            }

            AP += ap;

            AP = Math.Min(AP, Fight.GetMaxAP());
            AP = Math.Max(0, AP);
            Fight.AddUpdateUnit(this, BattleConstant.UpdateType.CHANGE_AP, AP, show ? ap : 0);

            _BuffManager.TriggerBuff(typeof(BattleUnitAttrChangeBuffTrigger), BattleConstant.Attribute.TYPE_AP);
        }

        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="targetPos"></param>
        public void MovePos(int targetPos)
        {
            // 取得地板
            var oldTile = this.Tile;
            oldTile.Leave();

            this.BattlePos = targetPos;
            BattleTileVO newTile = Fight.GetAllBattleTiles().Where(t => (t.Value.BattlePos == targetPos && t.Value.TroopType == this.TroopType)).First().Value;
            newTile.Enter(this);

            Fight.AddUpdateUnit(this, BattleConstant.UpdateType.CHANGE_POS, targetPos);
            // 移动触发
            GetBuffManager().TriggerBuff(typeof(BattleUnitMoveBuffTrigger));
        }

        virtual public Fix64 GetFightAttribute(int attType)
        {
            if (attType == BattleConstant.Attribute.TYPE_HP)
            {
                return HP;
            }
            if (attType == BattleConstant.Attribute.TYPE_ENERGY)
            {
                return SkillEnergy;
            }
            Fix64 v = _fightAttributes[attType];
            
            // 属性上限
            if (attType == BattleConstant.Attribute.TYPE_DEFENSE_PENETRATION && v > 1)
            {
                return 1;
            }
            else
            {
                return v;
            }
        }

        
        public Fix64 GetBaseAttribute(int attType)
        {
            // if (attType == BattleConstant.Attribute.TYPE_HP)
            // {
            //     return HP;
            // }
            // if (attType == BattleConstant.Attribute.TYPE_ENERGY)
            // {
            //     return SkillEnergy;
            // }
            Fix64 v = _baseAttributes[attType];
            
            // 属性上限
            if (attType == BattleConstant.Attribute.TYPE_DEFENSE_PENETRATION && v > 1)
            {
                return 1;
            }
            else
            {
                return v;
            }
        }

        public Fix64[] GetFightAttribute()
        {
            return _fightAttributes;
        }

        public Fix64 GetAttribute(int type,int attType)
        {
            switch (type)
            {
                case 1:
                    return GetBattleAttribute(attType);
                case 2:
                    return GetFightAttribute(attType);
                case 3:
                    return GetBaseAttribute(attType);
            }

            return 0;
        }

        /// <summary>
        /// 获取战斗属性
        /// </summary>
        /// <param name="attType"></param>
        /// <returns></returns>
        virtual public Fix64 GetBattleAttribute(int attType)
        {
            if (attType == BattleConstant.Attribute.TYPE_HP)
            {
                return HP;
            }
            if (attType == BattleConstant.Attribute.TYPE_ENERGY)
            {
                return SkillEnergy;
            }
            Fix64 skillTemporaryatt = 0;
            SkillTemporaryAttrs.TryGetValue(attType, out skillTemporaryatt);
            
            // 战斗属性 + BUFF属性 + 技能临时属性
            Fix64 v = _fightAttributes[attType] + _buffAttributes[attType] + skillTemporaryatt;
            
            
            // 属性上限
            if (attType == BattleConstant.Attribute.TYPE_DEFENSE_PENETRATION && v > 1)
            {
                return 1;
            }
            else
            {
                return v;
            }
        }

        IEnumerable<IBuffHolder> IBuffHolder.GetBuffTarget(Buff buff, int searchTargetCid)
        {
            CfgSearchTargetData cfgSearchTargetData = CfgSearchTargetTable.Instance.GetDataByID(searchTargetCid);

            List<BattleUnitVO> targetRange = new List<BattleUnitVO>();
            if (cfgSearchTargetData.SearchScope == 0)
            {
                targetRange.AddRange(Fight.GetAllBattleUnits().Values);
            }
            else
            {
                targetRange.AddRange(Fight.GetAllBattleTiles().Values);
            }


            List<BattleUnitVO> targets = SearchTarget.SearchBuffTargets(this, buff, cfgSearchTargetData, targetRange);

            //去掉死亡单位
            for (int i = targets.Count - 1; i >= 0; i--)
            {
                if (targets[i].IsDead())
                {
                    targets.RemoveAt(i);
                }
            }

            return targets.ToArray();
        }


        /// <summary>
        /// 计算属性改变
        /// </summary>
        /// <param name="caster">此次的叠加者或者施放者</param>
        /// <param name="effect"></param>
        /// <param name="buff"></param>
        /// <returns></returns>
        private Fix64 CalcAttribute(BattleUnitVO caster, BuffEffect effect, Buff buff)
        {
            int attType = Convert.ToInt32(effect.EffectParams[0]);
            int changeType = Convert.ToInt32(effect.EffectParams[1]);

            Fix64 effectValue = 0;//效果值
            if (changeType == 1)
            {
                //走公式表
                int functionCid = Convert.ToInt32(effect.EffectParams[2]);
                CfgSkillFunctionData cfgSkillFunctionData = CfgSkillFunctionTable.Instance.GetDataByID(functionCid);
                effectValue = SkillFunction.CalcChangeAttribute(caster as BattleUnitVO, this, buff.SkillData, cfgSkillFunctionData);
            }
            else if (changeType == 2)
            {
                Fix64 percentage = Fix64.Parse(effect.EffectParams[2]);
                effectValue = GetFightAttribute(attType) * percentage; //比率以初始属性为基础
            }
            else if (changeType == 3)
            {
                //直接读值
                effectValue = Fix64.Parse(effect.EffectParams[2]);
            }
            else if (changeType == 4)
            {
                Fix64 percentage = Fix64.Parse(effect.EffectParams[2]);
                effectValue = GetBattleAttribute(attType) * percentage; //比率当前属性为基础
            }

            
            // 上限 or 下限 修正
            if (effect.EffectParams.Length >= 4 && effect.EffectParams[3] != "0")
            {
                if(effectValue >= 0)
                {
                    effectValue = Fix64.Min(effectValue, Fix64.Parse(effect.EffectParams[3]));
                }
                else
                {
                    effectValue = Fix64.Max(effectValue, Fix64.Parse(effect.EffectParams[3]));
                }
            }
            return effectValue;
        }


        /// <summary>
        /// 计算dot伤害
        /// </summary>
        /// <param name="caster"></param>
        /// <param name="buff"></param>
        /// <param name="effect"></param>
        /// <returns></returns>
        private Fix64 CalcDotDmg(BattleUnitVO caster, Buff buff, BuffEffect effect)
        {
            // int dotType = Convert.ToInt32(effect.EffectParams[0]);//dot类型
            int changeType = Convert.ToInt32(effect.EffectParams[1]);//改变方式
            Fix64 value = 0;
            if (changeType == 1)
            {
                //伤害公式表
                int functionID = Convert.ToInt32(effect.EffectParams[2]);
                Fix64 rate = Fix64.Parse(effect.EffectParams[3]);
                CfgSkillFunctionData cfgSkillFunctionData = CfgSkillFunctionTable.Instance.GetDataByID(functionID);
                bool isCrit = SkillFunction.IsCrit(caster, this, cfgSkillFunctionData);
                value = -SkillFunction.CalcFunctionDamage(caster, this, buff.BuffCfg.Element, buff.SkillData, cfgSkillFunctionData, isCrit, false, rate);
            }
            else if (changeType == 2)
            {
                //值
                value = Fix64.Parse(effect.EffectParams[2]);
            }
            else if (changeType == 3)
            {
                //属性公式
                int functionID = Convert.ToInt32(effect.EffectParams[2]);
                Fix64 rate = Fix64.Parse(effect.EffectParams[3]);
                CfgSkillFunctionData cfgSkillFunctionData = CfgSkillFunctionTable.Instance.GetDataByID(functionID);
                value = SkillFunction.CalcChangeAttribute(caster, this, buff.SkillData, cfgSkillFunctionData) * rate;
            }

            Fix64 change = 0;
            //switch (dotType)
            //{
            //    case 1:
            //        //流血
            //        change = caster.GetBattleAttribute(BattleConstant.Attribute.TYPE_DOT_BLEED_CHANGE) - GetBattleAttribute(BattleConstant.Attribute.TYPE_BE_DOT_BLEED_CHANGE);
            //        break;
            //    case 2:
            //        //燃烧
            //        change = caster.GetBattleAttribute(BattleConstant.Attribute.TYPE_DOT_BURN_CHANGE) - GetBattleAttribute(BattleConstant.Attribute.TYPE_BE_DOT_BURN_CHANGE);
            //        break;
            //    case 3:
            //        //电击
            //        change = caster.GetBattleAttribute(BattleConstant.Attribute.TYPE_DOT_SHOCK_CHANGE) - GetBattleAttribute(BattleConstant.Attribute.TYPE_BE_DOT_SHOCK_CHANGE);
            //        break;
            //    case 4:
            //        //反击
            //        change = caster.GetBattleAttribute(BattleConstant.Attribute.TYPE_DOT_STRIKE_BACK_CHANGE) - GetBattleAttribute(BattleConstant.Attribute.TYPE_BE_DOT_STRIKE_BACK_CHANGE);
            //        break;

            //    default:
            //        break;
            //}
            value *= 1 + change;
            return value;
        }

        public virtual void 
            OnBuffAdd(BattleUnitVO notifyUnit, Buff buff)
        {
            _logger.Debug("UnitPos={0}, Buff Add id = {1}", TroopType == BattleConstant.TroopType.DEFEND ? 10 + BattlePos : BattlePos, buff.BuffCfg.Id);
            //if (Fight.CurrMover == this)
            //{
            //    _currActionAddBuffs.Add(buff);
            //}
            if (notifyUnit != null && notifyUnit.CallStatus !=0) { 
                Fight.AddUpdateUnit(notifyUnit, BattleConstant.UpdateType.UPDATE_BUFF, buff.BuffCfg.Id, buff.StackCount, buff.LeftTime, 1);
            }
            foreach (BuffEffect effect in buff.Effects)
            {
                switch (effect.EffectType)
                {
                    case BuffConstant.BUFF_EFFECT_TYPE_SHIELD:
                        {
                            int changeType = Convert.ToInt32(effect.EffectParams[1]);
                            Fix64 value = 0;
                            if (changeType == 1)
                            {
                                //公式
                                CfgSkillFunctionData cfgSkillFunctionData = CfgSkillFunctionTable.Instance.GetDataByID(Convert.ToInt32(effect.EffectParams[2]));
                                value = SkillFunction.CalcChangeAttribute(buff.Caster as BattleUnitVO, this, buff.SkillData, cfgSkillFunctionData);
                            }
                            else if (changeType == 2)
                            {
                                //值
                                value = Fix64.Parse(effect.EffectParams[2]);
                            }

                            value *= 1 + GetBattleAttribute(BattleConstant.Attribute.TYPE_POSITIVE_SHIELD) + (buff.Caster as BattleUnitVO).GetBattleAttribute(BattleConstant.Attribute.TYPE_NEGATIVE_SHIELD);
                            effect.EffectValue = Fix64.Max(1, value);
                            _BuffManager.TriggerBuff(typeof(BattleUnitShieldChangeBuffTrigger));
                            Fight.AddUpdateUnit(this, BattleConstant.UpdateType.UPDATE_SHIELD, GetShield(), 0);
                            break;
                        }
                    case BuffConstant.BUFF_EFFECT_TYPE_BATTLE_ACCUMULATE_CHANGE_HP:
                        {
                            effect.StackCasters = new Queue<int>();
                            int casterID = (buff.Caster as BattleUnitVO).ID;
                            if (buff.BuffCfg.StackType == BuffConstant.BUFF_STACK_TYPE_EFFECT)
                            {
                                for (int i = 0; i < buff.StackCount; i++)
                                {
                                    effect.StackCasters.Enqueue(casterID);
                                }
                            }
                            else
                            {
                                effect.StackCasters.Enqueue(casterID);
                            }
                            break;
                        }
                    case BuffConstant.BUFF_EFFECT_TYPE_CHANGE_BATTLE_ATTRIBUTES:
                        {
                            int attType = Convert.ToInt32(effect.EffectParams[0]);
                            Fix64 effectValue = CalcAttribute(buff.Caster as BattleUnitVO, effect, buff);
                            //处理叠加效果
                            if (buff.BuffCfg.StackType == BuffConstant.BUFF_STACK_TYPE_EFFECT)
                            {
                                effectValue *= buff.StackCount;
                            }
                            effect.EffectValue = effectValue;
                            _buffAttributes[attType] += effectValue;
                            //最大生命改变单独处理生命
                            if (attType == BattleConstant.Attribute.TYPE_HP_MAX)
                            {
                                Fight.AddUpdateUnit(this, BattleConstant.UpdateType.UPDATE_MAX_HP, Fix64.ToInt32(GetBattleAttribute(BattleConstant.Attribute.TYPE_HP_MAX)));
                                if (effectValue > 0)
                                {
                                    //当最大生命[Att23]增加时，同步增加相应的当前生命[Att27]
                                    ChangeHP(Fix64.ToInt32(effectValue), true,false, null, buff.BuffCfg.Element);
                                }
                                else
                                {
                                    //当最大生命[Att23]减少时，如果当前生命[Att23]高于最大生命[Att27]，则当前生命[Att23]=最大生命[Att27]
                                    HP = Math.Min(HP, Fix64.ToInt32(GetBattleAttribute(BattleConstant.Attribute.TYPE_HP_MAX)));
                                    Fight.AddUpdateUnit(this, BattleConstant.UpdateType.CHANGE_HP, HP, 0, buff.BuffCfg.Element);
                                }
                            }
                            else if (attType == BattleConstant.Attribute.TYPE_SPEED)
                            {
                                Fight.AddUpdateUnit(this, BattleConstant.UpdateType.UPDATE_SPEED, Fix64.ToInt32(GetBattleAttribute(BattleConstant.Attribute.TYPE_SPEED)));
                            }
                            PrintLog();
                            GetBuffManager().TriggerBuff(typeof(BattleUnitAttrChangeBuffTrigger), attType);
                        }
                        break;
                    case BuffConstant.BUFF_EFFECT_TYPE_CHANGE_BATTLE_STATUS:
                        _ResetFightStatus();
                        break;
                    case BuffConstant.BUFF_EFFECT_TYPE_IMMUNE_STATUS:
                        _ResetImmuneFightStatus();
                        break;
                    case BuffConstant.BUFF_EFFECT_TYPE_SUMMON:
                        // 召唤
                        int pos = Fight.GetIdlePos(this.getTroopType());
                        if (pos >= 1 && pos <= 9)
                        {
                            BattleCallVO battleCallVO = new BattleCallVO(buff.Holder as BattleUnitVO,pos,Convert.ToInt32(effect.EffectParams[0]),1);
                            battleCallVO.InitBuff();
                            Fight.AllBattleCalls().Add(battleCallVO.ID,battleCallVO);
                            effect.EffectValue = battleCallVO.ID;
                            Fight.AddUpdateUnit(battleCallVO.Summoner, BattleConstant.UpdateType.SUMMON_ADD, battleCallVO.ToData());
                        }
                        break;
                    case BuffConstant.BUFF_EFFECT_SEAL_SKILL:
                        foreach (BattleSkillVO skillVo in AllSkills)
                        {
                            if (!skillVo.IsSeal && (skillVo.CfgSkillData.SkillType == Convert.ToInt32(effect.EffectParams[0]) || Convert.ToInt32(effect.EffectParams[0]) == -1))
                            {
                                skillVo.IsSeal = true;
                                Fight.AddUpdateUnit(this, BattleConstant.UpdateType.SEAL_SKILL, skillVo.CfgSkillData.Id,1);
                            }
                        }
                        break;
                    case BuffConstant.BUFF_CHANGE_SKILL_ENERGY:
                    {
                        
                        int type = Convert.ToInt32(effect.EffectParams[0]);
                        int type_val = Convert.ToInt32(effect.EffectParams[1]);
                        int effectValue = Convert.ToInt32(effect.EffectParams[2]);
                        //处理叠加效果
                        if (buff.BuffCfg.StackType == BuffConstant.BUFF_STACK_TYPE_EFFECT)
                        {
                            effectValue *= buff.StackCount;
                        }

                        effect.EffectValue = effectValue;
                        
                        foreach (BattleSkillVO skillVo in AllSkills)
                        {
                            if (skillVo.IsSubSkill)
                            {
                                continue;
                            }
                            switch (type)
                            {
                                case 1:
                                    if (skillVo.CfgSkillData.SkillType == type_val)
                                    {
                                        skillVo.SkillData.ChangeEnergy += effectValue;
                                        Fight.AddUpdateUnit(this, BattleConstant.UpdateType.CHANGE_SKILL_ENERGY, skillVo.CfgSkillData.Id, skillVo.SkillData.GetCostEnergy(this.TroopType));
                                    }
                                    break;
                                case 2:
                                    if (skillVo.CfgSkillData.Id == type_val)
                                    {
                                        skillVo.SkillData.ChangeEnergy += effectValue;
                                        Fight.AddUpdateUnit(this, BattleConstant.UpdateType.CHANGE_SKILL_ENERGY, skillVo.CfgSkillData.Id, skillVo.SkillData.GetCostEnergy(this.TroopType));
                                    }
                                    break;
                            }
                            // 修改技能怒气
                            
                        }
                        break;
                    }
                    case BuffConstant.BUFF_CHANGE_SKILL_ADD_ENERGY:
                    {
                        
                        int type = Convert.ToInt32(effect.EffectParams[0]);
                        int type_val = Convert.ToInt32(effect.EffectParams[1]);
                        int effectValue = Convert.ToInt32(effect.EffectParams[2]);
                        //处理叠加效果
                        if (buff.BuffCfg.StackType == BuffConstant.BUFF_STACK_TYPE_EFFECT)
                        {
                            effectValue *= buff.StackCount;
                        }

                        effect.EffectValue = effectValue;
                        
                        foreach (BattleSkillVO skillVo in AllSkills)
                        {
                            // if (skillVo.IsSubSkill)
                            // {
                            //     continue;
                            // }
                            switch (type)
                            {
                                case 1:
                                    if (skillVo.CfgSkillData.SkillType == type_val)
                                    {
                                        skillVo.SkillData.ChangeADDEnergy += effectValue;
                                    }
                                    break;
                                case 2:
                                    if (skillVo.CfgSkillData.Id == type_val)
                                    {
                                        skillVo.SkillData.ChangeADDEnergy += effectValue;
                                    }
                                    break;
                            }
                            // 修改技能怒气
                            
                        }
                        break;
                    }
                    case BuffConstant.BUFF_CHANGE_SKILL_TARGET:
                    {
                        
                        int type = Convert.ToInt32(effect.EffectParams[0]);
                        int type_val = Convert.ToInt32(effect.EffectParams[1]);
                        int effectValue = Convert.ToInt32(effect.EffectParams[2]);
                        CfgSearchTargetData cfgSearchTarget = CfgSearchTargetTable.Instance.GetDataByID(effectValue);
                        
                        effect.EffectValue = effectValue;
                        foreach (BattleSkillVO skillVo in AllSkills)
                        {
                            
                            switch (type)
                            {
                                case 1:
                                    if (skillVo.CfgSkillData.SkillType == type_val)
                                    {
                                        skillVo.SkillData.TempTargetTypeData = cfgSearchTarget;
                                        Fight.AddUpdateUnit(this, BattleConstant.UpdateType.CHANGE_SKILL_TARGET, skillVo.CfgSkillData.Id, effectValue);
                                    }
                                    break;
                                case 2:
                                    if (skillVo.CfgSkillData.Id == type_val)
                                    {
                                        skillVo.SkillData.TempTargetTypeData = cfgSearchTarget;
                                        Fight.AddUpdateUnit(this, BattleConstant.UpdateType.CHANGE_SKILL_TARGET, skillVo.CfgSkillData.Id, effectValue);
                                    }
                                    break;
                            }
                        }
                        break;
                    }
                    default:
                        break;
                }
            }
        }

        void IBuffHolder.OnBuffUpdate(Buff buff, int reason)
        {
            //if (reason == 1 && _currActionAddBuffs != null && _currActionAddBuffs.Contains(buff))
            //{
            //    buff.LeftTime++;
            //}
            //else
            {
                Fight.AddUpdateUnit(this, BattleConstant.UpdateType.UPDATE_BUFF, buff.BuffCfg.Id, buff.StackCount, buff.LeftTime, 0);
            }
        }

        public virtual void OnBuffStack(BattleUnitVO notifyUnit,Buff buff, int addStackCount)
        {
            _logger.Debug("UnitPos={0}, Buff Stack id = {1}", TroopType == BattleConstant.TroopType.DEFEND ? 10 + BattlePos : BattlePos, buff.BuffCfg.Id);
            if (notifyUnit != null)
            {
                Fight.AddUpdateUnit(this, BattleConstant.UpdateType.UPDATE_BUFF, buff.BuffCfg.Id, buff.StackCount,
                    buff.LeftTime, 0);
            }

            if (buff.BuffCfg.StackType == BuffConstant.BUFF_STACK_TYPE_EFFECT)
            {
                foreach (BuffEffect effect in buff.Effects)
                {
                    switch (effect.EffectType)
                    {
                        case BuffConstant.BUFF_EFFECT_TYPE_SHIELD:
                        {
                            int changeType = Convert.ToInt32(effect.EffectParams[1]);
                            Fix64 value = 0;
                            if (changeType == 1)
                            {
                                //公式
                                CfgSkillFunctionData cfgSkillFunctionData = CfgSkillFunctionTable.Instance.GetDataByID(Convert.ToInt32(effect.EffectParams[2]));
                                value = SkillFunction.CalcChangeAttribute(buff.Caster as BattleUnitVO, this, buff.SkillData, cfgSkillFunctionData);
                            }
                            else if (changeType == 2)
                            {
                                //值
                                value = Fix64.Parse(effect.EffectParams[2]);
                            }

                            value *= 1 + GetBattleAttribute(BattleConstant.Attribute.TYPE_POSITIVE_SHIELD) + (buff.Caster as BattleUnitVO).GetBattleAttribute(BattleConstant.Attribute.TYPE_NEGATIVE_SHIELD);
                            effect.EffectValue += Fix64.Max(1, value) * addStackCount;
                            _BuffManager.TriggerBuff(typeof(BattleUnitShieldChangeBuffTrigger));
                            Fight.AddUpdateUnit(this, BattleConstant.UpdateType.UPDATE_SHIELD, GetShield(), 0);
                            break;
                        }
                        
                        case BuffConstant.BUFF_EFFECT_TYPE_BATTLE_ACCUMULATE_CHANGE_HP:
                            {
                                if (addStackCount > 0)
                                {
                                    int casterID = (buff.Caster as BattleUnitVO).ID;
                                    for (int i = 0; i < addStackCount; i++)
                                    {
                                        effect.StackCasters.Enqueue(casterID);
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < -addStackCount; i++)
                                    {
                                        effect.StackCasters.Dequeue();
                                    }
                                }
                                break;
                            }

                        case BuffConstant.BUFF_EFFECT_TYPE_CHANGE_BATTLE_ATTRIBUTES:
                            {
                                int attType = Convert.ToInt32(effect.EffectParams[0]);
                                Fix64 effectValue = CalcAttribute(buff.Caster as BattleUnitVO, effect, buff) * addStackCount;

                                effect.EffectValue += effectValue;
                                _buffAttributes[attType] += effectValue;

                                //最大生命改变单独处理生命
                                if (attType == BattleConstant.Attribute.TYPE_HP_MAX)
                                {
                                    Fight.AddUpdateUnit(this, BattleConstant.UpdateType.UPDATE_MAX_HP, Fix64.ToInt32(GetBattleAttribute(BattleConstant.Attribute.TYPE_HP_MAX)));
                                    if (effectValue > 0)
                                    {
                                        //当最大生命[Att23]增加时，同步增加相应的当前生命[Att27]
                                        ChangeHP(Fix64.ToInt32(effectValue), true,false, null, buff.BuffCfg.Element);
                                    }
                                    else
                                    {
                                        //当最大生命[Att23]减少时，如果当前生命[Att23]高于最大生命[Att27]，则当前生命[Att23]=最大生命[Att27]
                                        HP = Math.Min(HP, Fix64.ToInt32(GetBattleAttribute(BattleConstant.Attribute.TYPE_HP_MAX)));
                                        Fight.AddUpdateUnit(this, BattleConstant.UpdateType.CHANGE_HP, HP, 0, buff.BuffCfg.Element);
                                    }
                                }
                                else if (attType == BattleConstant.Attribute.TYPE_SPEED)
                                {
                                    Fight.AddUpdateUnit(this, BattleConstant.UpdateType.UPDATE_SPEED, Fix64.ToInt32(GetBattleAttribute(BattleConstant.Attribute.TYPE_SPEED)));
                                }
                                PrintLog();
                                GetBuffManager().TriggerBuff(typeof(BattleUnitAttrChangeBuffTrigger), attType);
                            }
                            break;
                        case BuffConstant.BUFF_CHANGE_SKILL_ENERGY:
                        {
                        
                            int type = Convert.ToInt32(effect.EffectParams[0]);
                            int type_val = Convert.ToInt32(effect.EffectParams[1]);
                            int effectValue = Convert.ToInt32(effect.EffectParams[2]) * addStackCount;
                            
                            effect.EffectValue += effectValue;
                            foreach (BattleSkillVO skillVo in AllSkills)
                            {
                                if (skillVo.IsSubSkill)
                                {
                                    continue;
                                }
                                switch (type)
                                {
                                    case 1:
                                        if (skillVo.CfgSkillData.SkillType == type_val)
                                        {
                                            skillVo.SkillData.ChangeEnergy += effectValue;
                                            // 修改技能怒气
                                            Fight.AddUpdateUnit(this, BattleConstant.UpdateType.CHANGE_SKILL_ENERGY, skillVo.CfgSkillData.Id, skillVo.SkillData.GetCostEnergy(this.TroopType));
                                        }
                                        break;
                                    case 2:
                                        if (skillVo.CfgSkillData.Id == type_val)
                                        {
                                            skillVo.SkillData.ChangeEnergy += effectValue;
                                            // 修改技能怒气
                                            Fight.AddUpdateUnit(this, BattleConstant.UpdateType.CHANGE_SKILL_ENERGY, skillVo.CfgSkillData.Id, skillVo.SkillData.GetCostEnergy(this.TroopType));
                                        }
                                        break;
                                }
                                
                            }
                            break;
                        }
                        case BuffConstant.BUFF_CHANGE_SKILL_ADD_ENERGY:
                        {

                            int type = Convert.ToInt32(effect.EffectParams[0]);
                            int type_val = Convert.ToInt32(effect.EffectParams[1]);
                            int effectValue = Convert.ToInt32(effect.EffectParams[2]) * addStackCount;

                            effect.EffectValue += effectValue;
                            foreach (BattleSkillVO skillVo in AllSkills)
                            {
                                // if (skillVo.IsSubSkill)
                                // {
                                //     continue;
                                // }
                                switch (type)
                                {
                                    case 1:
                                        if (skillVo.CfgSkillData.SkillType == type_val)
                                        {
                                            skillVo.SkillData.ChangeADDEnergy += effectValue;
                                        }
                                        break;
                                    case 2:
                                        if (skillVo.CfgSkillData.Id == type_val)
                                        {
                                            skillVo.SkillData.ChangeADDEnergy += effectValue;
                                        }
                                        break;
                                }

                            }
                            break;
                        }
                        default:
                            break;
                    }
                }
            }
        }

        public virtual void OnBuffRemove(BattleUnitVO notifyUnit,Buff buff, BuffRemoveReason removeReason)
        {
            _logger.Debug("UnitPos={0}, Buff Remove id = {1} removeReason = {2}", TroopType == BattleConstant.TroopType.DEFEND ? 10 + BattlePos : BattlePos, buff.BuffCfg.Id,removeReason);
            if (notifyUnit != null) { 
                Fight.AddUpdateUnit(notifyUnit, BattleConstant.UpdateType.REMOVE_BUFF, buff.BuffCfg.Id, (int)removeReason);
            }
            foreach (BuffEffect effect in buff.Effects)
            {
                switch (effect.EffectType)
                {
                    case BuffConstant.BUFF_EFFECT_TYPE_CHANGE_BATTLE_ATTRIBUTES:
                        {
                            int attType = Convert.ToInt32(effect.EffectParams[0]);
                            _buffAttributes[attType] -= effect.EffectValue;
                            if (attType == BattleConstant.Attribute.TYPE_HP_MAX)
                            {
                                Fight.AddUpdateUnit(this, BattleConstant.UpdateType.UPDATE_MAX_HP, Fix64.ToInt32(GetBattleAttribute(BattleConstant.Attribute.TYPE_HP_MAX)));
                                HP = Math.Min(HP, Fix64.ToInt32(GetBattleAttribute(BattleConstant.Attribute.TYPE_HP_MAX)));
                                Fight.AddUpdateUnit(this, BattleConstant.UpdateType.CHANGE_HP, HP, 0, buff.BuffCfg.Element);
                            }
                            else if (attType == BattleConstant.Attribute.TYPE_SPEED)
                            {
                                Fight.AddUpdateUnit(this, BattleConstant.UpdateType.UPDATE_SPEED, Fix64.ToInt32(GetBattleAttribute(BattleConstant.Attribute.TYPE_SPEED)));
                            }
                            PrintLog();
                            GetBuffManager().TriggerBuff(typeof(BattleUnitAttrChangeBuffTrigger), attType);
                            break;
                        }
                    case BuffConstant.BUFF_EFFECT_TYPE_CHANGE_BATTLE_STATUS:
                        _ResetFightStatus();
                        break;
                    case BuffConstant.BUFF_EFFECT_TYPE_IMMUNE_STATUS:
                        _ResetImmuneFightStatus();
                        break;
                    case BuffConstant.BUFF_EFFECT_TYPE_SHIELD:
                        _BuffManager.TriggerBuff(typeof(BattleUnitShieldChangeBuffTrigger));
                        _BuffManager.TriggerBuff(typeof(BattleUnitShieldRemoveTrigger));
                        Fight.AddUpdateUnit(this, BattleConstant.UpdateType.UPDATE_SHIELD, GetShield(), 0);
                        break;
                    case BuffConstant.BUFF_EFFECT_TYPE_ENCHANT:
                        foreach (BattleSkillVO skill in AllSkills)
                        {
                            // 给技能附魔元素
                            skill.EnchantElement(0,0);
                        }
                        break;
                    case BuffConstant.BUFF_EFFECT_TYPE_SUMMON:
                        // 召唤
                        if (effect.EffectValue > 0)
                        {
                            BattleCallVO battleCallVO = Fight.AllBattleCalls()[Fix64.ToInt32(effect.EffectValue)];
                            battleCallVO.CallStatus = 2;
                            // Fight.RemoveUnit(battleCallVO);
                            Fight.AddUpdateUnit(battleCallVO.Summoner, BattleConstant.UpdateType.SUMMON_REMOVE, battleCallVO.ID);
                        }
                        break;
                    case BuffConstant.BUFF_EFFECT_SEAL_SKILL:
                        foreach (BattleSkillVO skillVo in AllSkills)
                        {
                            if (skillVo.IsSeal && (skillVo.CfgSkillData.SkillType == Convert.ToInt32(effect.EffectParams[0]) || Convert.ToInt32(effect.EffectParams[0]) == -1))
                            {
                                skillVo.IsSeal = false;
                                Fight.AddUpdateUnit(this, BattleConstant.UpdateType.SEAL_SKILL, skillVo.CfgSkillData.Id,0);
                            }
                        }
                        break;
                    case BuffConstant.BUFF_CHANGE_SKILL_ENERGY:
                    {
                        
                        int type = Convert.ToInt32(effect.EffectParams[0]);
                        int type_val = Convert.ToInt32(effect.EffectParams[1]);
                        // int effectValue = Convert.ToInt32(effect.EffectParams[3]) * addStackCount;
                            
                        // effect.EffectValue += effectValue;
                        foreach (BattleSkillVO skillVo in AllSkills)
                        {
                            if (skillVo.IsSubSkill)
                            {
                                continue;
                            }
                            switch (type)
                            {
                                case 1:
                                    if (skillVo.CfgSkillData.SkillType == type_val)
                                    {
                                        skillVo.SkillData.ChangeEnergy -= Fix64.ToInt32(effect.EffectValue);
                                        // 修改技能怒气
                                        Fight.AddUpdateUnit(this, BattleConstant.UpdateType.CHANGE_SKILL_ENERGY, skillVo.CfgSkillData.Id, skillVo.SkillData.GetCostEnergy(this.TroopType));
                                    }
                                    break;
                                case 2:
                                    if (skillVo.CfgSkillData.Id == type_val)
                                    {
                                        skillVo.SkillData.ChangeEnergy -= Fix64.ToInt32(effect.EffectValue);;
                                        // 修改技能怒气
                                        Fight.AddUpdateUnit(this, BattleConstant.UpdateType.CHANGE_SKILL_ENERGY, skillVo.CfgSkillData.Id, skillVo.SkillData.GetCostEnergy(this.TroopType));
                                    }
                                    break;
                            }
                            
                        }
                        break;
                    }
                    case BuffConstant.BUFF_CHANGE_SKILL_ADD_ENERGY:
                    {
                        
                        int type = Convert.ToInt32(effect.EffectParams[0]);
                        int type_val = Convert.ToInt32(effect.EffectParams[1]);
                        // int effectValue = Convert.ToInt32(effect.EffectParams[3]) * addStackCount;
                            
                        // effect.EffectValue += effectValue;
                        foreach (BattleSkillVO skillVo in AllSkills)
                        {
                            // if (skillVo.IsSubSkill)
                            // {
                            //     continue;
                            // }
                            switch (type)
                            {
                                case 1:
                                    if (skillVo.CfgSkillData.SkillType == type_val)
                                    {
                                        skillVo.SkillData.ChangeADDEnergy -= Fix64.ToInt32(effect.EffectValue);
                                    }
                                    break;
                                case 2:
                                    if (skillVo.CfgSkillData.Id == type_val)
                                    {
                                        skillVo.SkillData.ChangeADDEnergy -= Fix64.ToInt32(effect.EffectValue);;
                                    }
                                    break;
                            }
                            
                        }
                        break;
                    }
                    case BuffConstant.BUFF_CHANGE_SKILL_TARGET:
                    {
                        
                        int type = Convert.ToInt32(effect.EffectParams[0]);
                        int type_val = Convert.ToInt32(effect.EffectParams[1]);
                        int effectValue = Convert.ToInt32(effect.EffectParams[2]);
                        effect.EffectValue = effectValue;
                        foreach (BattleSkillVO skillVo in AllSkills)
                        {
                            
                            switch (type)
                            {
                                case 1:
                                    if (skillVo.CfgSkillData.SkillType == type_val)
                                    {
                                        if (skillVo.SkillData.TempTargetTypeData != null && skillVo.SkillData.TempTargetTypeData.Id == effectValue)
                                        {
                                            skillVo.SkillData.TempTargetTypeData = null;
                                            Fight.AddUpdateUnit(this, BattleConstant.UpdateType.CHANGE_SKILL_TARGET, skillVo.CfgSkillData.Id, skillVo.SkillData.TargetTypeData.Id);    
                                        }
                                    }
                                    break;
                                case 2:
                                    if (skillVo.CfgSkillData.Id == type_val)
                                    {
                                        if (skillVo.SkillData.TempTargetTypeData != null && skillVo.SkillData.TempTargetTypeData.Id == effectValue)
                                        {
                                            skillVo.SkillData.TempTargetTypeData = null;
                                            Fight.AddUpdateUnit(this, BattleConstant.UpdateType.CHANGE_SKILL_TARGET, skillVo.CfgSkillData.Id, skillVo.SkillData.TargetTypeData.Id);    
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                    }
                    case BuffConstant.BUFF_EFFECT_CAHNEG_SIZE:
                    {
                        Fight.AddUpdateUnit(this, BattleConstant.UpdateType.CHANGE_SIZE, 1, 1);
                        break;
                    }
                    case BuffConstant.BUFF_EFFECT_TYPE_ADD_WEAK_TYPE:
                    {
                        int weakId = Convert.ToInt32(effect.EffectParams[0]);
                        int weakStatus = Convert.ToInt32(effect.EffectParams[1]);
                        if (WeakStatus.ContainsKey(weakId))
                        {
                            WeakStatus.Remove(weakId);
                            Fight.AddUpdateUnit(this, BattleConstant.UpdateType.REMOVE_WEAK_TYPE, weakId,weakStatus);
                        }
                        break;
                    }
                    case BuffConstant.BUFF_EFFECT_TYPE_CHANGE_WEAK_STATUS:
                    {
                        int weakId = Convert.ToInt32(effect.EffectParams[0]);
                        int weakStatus = Convert.ToInt32(effect.EffectParams[1]);
                        int oldStatus = Fix64.ToInt32(effect.EffectValue);
                        if (WeakStatus.ContainsKey(weakId) && WeakStatus[weakId] == weakStatus)
                        {
                            WeakStatus[weakId] = oldStatus;
                            Fight.AddUpdateUnit(this, BattleConstant.UpdateType.UPDATE_WEAK_STATUS, weakId,oldStatus);
                        }
                        break;
                    }
                    case BuffConstant.BUFF_EFFECT_TYPE_REMOVE_WEAK_TYPE:
                    {
                        int weakId = Convert.ToInt32(effect.EffectParams[0]);
                        int oldStatus = Fix64.ToInt32(effect.EffectValue);
                        if (!WeakStatus.ContainsKey(weakId))
                        {
                            WeakStatus.Add(weakId,oldStatus);
                            Fight.AddUpdateUnit(this, BattleConstant.UpdateType.ADD_WEAK_TYPE, weakId,oldStatus);
                        }
                        break;
                    }
                    // case BuffConstant.BUFF_EFFECT_DIALOG:
                    // {
                    //     Fight.AddUpdateUnit(this, BattleConstant.UpdateType.TRIGGER_DIALOG, 1, 1);
                    //     break;
                    // }
                    default:
                        break;
                }
            }
        }

        void IBuffHolder.OnBuffImmune(int buffCid)
        {
            Fight.AddUpdateUnit(this, BattleConstant.UpdateType.BUFF_IMMUNE, buffCid);
            _BuffManager.TriggerBuff(typeof(BattleUnitBuffImmuneBuffTrigger), buffCid);
        }

        void IBuffHolder.OnBuffTrigger(Buff buff)
        {
            _logger.Info(" -> Trigger Buff = " + buff.BuffCfg.Id + " Round = " + Fight._round);
            Fight.AddUpdateUnit(this, BattleConstant.UpdateType.TRIGGER_BUFF, buff.BuffCfg.Id);
        }

        public virtual void OnBuffTriggerEffect(Buff buff, BuffEffect buffEffect, Type triggerType, object[] triggerArgs)
        {
            switch (buffEffect.EffectType)
            {
                case BuffConstant.BUFF_EFFECT_TYPE_ADD_TMP_SUB_SKILL:
                    {
                        if (this != Fight.CurrSkillCaster)
                        {
                            break;
                        }
                        for (int i = 0; i < buffEffect.EffectParams.Length; i += 2)
                        {
                            int skillCid = Convert.ToInt32(buffEffect.EffectParams[i]);
                            Fix64 delayTimeOffset = Fix64.Parse(buffEffect.EffectParams[i + 1]);
                            BattleSkillVO skill = Fight.CurrMainSkill.AddTmpSubSkill(skillCid, delayTimeOffset);
                        }
                        break;
                    }
                case BuffConstant.BUFF_EFFECT_TYPE_BATTLE_CAST_SKILL:
                    {
                        int casterType = Convert.ToInt32(buffEffect.EffectParams[0]);
                        int skillType = Convert.ToInt32(buffEffect.EffectParams[1]);

                        BattleUnitVO caster = null;
                        switch (casterType)
                        {
                            case 1:
                                caster = buff.Caster as BattleUnitVO;
                                break;
                            case 2:
                                caster = buff.Holder as BattleUnitVO;
                                break;
                            case 3:
                                caster = Fight.CurrSkillCaster;
                                break;
                            default:
                                break;
                        }


                        BattleSkillVO triggerCastSkill = null;
                        //技能类型触发
                        switch (skillType)
                        {
                            case 0:
                                //当前主技能
                                if (caster == Fight.CurrSkillCaster)
                                {
                                    triggerCastSkill = Fight.CurrMainSkill;
                                }
                                break;
                            case SkillConstant.TYPE_REVOLT:
                                triggerCastSkill = caster.RevoltSkill;
                                break;
                            case SkillConstant.TYPE_BATTER:
                                triggerCastSkill = caster.BatterSkill;
                                break;
                            case SkillConstant.TYPE_ULTIMATE:
                                triggerCastSkill = caster.UltimateSkill;
                                break;
                            case SkillConstant.TYPE_NORMAL:
                                triggerCastSkill = caster.NormalSkill;
                                break;
                            case SkillConstant.TYPE_DEADTH_RATTLE:
                                triggerCastSkill = caster.DeadthRattleSkill;
                                break;
                            case SkillConstant.TYPE_MASTERY:
                            // case SkillConstant.TYPE_BEAT_BACK:
                            case SkillConstant.TYPE_DERIVATION:
                                _logger.Error("can not support type : {0}", skillType);
                                break;
                            default:
                                //技能cid触发
                                triggerCastSkill = new BattleSkillVO(Fight, skillType,0,0,0, CfgSkillStrengthens);
                                if (!Fight.Moving && triggerCastSkill.CfgSkillData.SkillType == SkillConstant.TYPE_REVOLT)
                                {
                                    if (caster != this)
                                    {
                                        //只能触发自己的反击技能
                                        triggerCastSkill = null;
                                    }
                                    //子技能不触发反击，反击技能不触发反击
                                    else if (Fight.CurrSkillCaster == null || Fight.CurrSkill == null || Fight.CurrSkill.IsSubSkill || Fight.CurrSkill.CfgSkillData.SkillType == SkillConstant.TYPE_REVOLT)
                                    {
                                        triggerCastSkill = null;
                                    }
                                    else
                                    {
                                        triggerCastSkill.SpecifyTargets = new List<BattleUnitVO>();
                                        triggerCastSkill.SpecifyTargets.Add(Fight.CurrSkillCaster);
                                    }
                                }
                                break;
                        }

                        if (triggerCastSkill != null)
                        {
                            Fight.TriggerSkillUnits.Enqueue(new KeyValuePair<BattleUnitVO, BattleSkillVO>(caster, triggerCastSkill));
                        }
                        return;
                    }
                case BuffConstant.BUFF_EFFECT_TYPE_BATTLE_CHANGE_HP:
                    {
                        int shieldCost = Convert.ToInt32(buffEffect.EffectParams[0]);
                        bool immunity = SkillFunction.IsImmunity(buff.Caster as BattleUnitVO, buff.Holder as BattleUnitVO, buff.BuffCfg.Element,false);
                        if (!immunity)
                        {
                            Fix64 value = CalcDotDmg(buff.Caster as BattleUnitVO, buff, buffEffect);
                            if (buff.BuffCfg.StackType == BuffConstant.BUFF_STACK_TYPE_EFFECT)
                            {
                                value *= buff.StackCount;
                            }
                            ChangeHP(Fix64.ToInt32(value), true,false, buff.Caster as BattleUnitVO, buff.BuffCfg.Element,shieldCost == 0);
                        }
                        else
                        {
                            Fight.AddUpdateUnit(this, BattleConstant.UpdateType.BUFF_EFFECT_IMMUNE, buff.BuffCfg.Id);
                        }

                        GetBuffManager().TriggerBuff(typeof(BattleUnitDotEffectBuffTrigger), buff.BuffCfg);
                        return;
                    }
                case BuffConstant.BUFF_EFFECT_TYPE_BATTLE_ACCUMULATE_CHANGE_HP:
                    {
                        int shieldCost = Convert.ToInt32(buffEffect.EffectParams[0]);
                        bool immunity = SkillFunction.IsImmunity(buff.Caster as BattleUnitVO, buff.Holder as BattleUnitVO, buff.BuffCfg.Element, false);
                        if (!immunity)
                        {
                            Fix64 value = 0;
                            foreach (int casterID in buffEffect.StackCasters)
                            {
                                BattleUnitVO caster = null;
                                if (casterID == Fight.AttackTroopUnitVO.ID)
                                {
                                    caster = Fight.AttackTroopUnitVO;
                                }
                                else if (casterID == Fight.DefendTroopUnitVO.ID)
                                {
                                    caster = Fight.DefendTroopUnitVO;
                                }
                                else
                                {
                                    caster = Fight.GetAllBattleUnits()[casterID];
                                }

                                Fix64 dmg = CalcDotDmg(caster, buff, buffEffect);
                                value += dmg;
                                //单独统计伤害
                                if (dmg < 0)
                                {
                                    caster.AddDmgRecord(Fix64.ToInt32(-dmg));
                                }
                            }
                            ChangeHP(Fix64.ToInt32(value), true,false, null, buff.BuffCfg.Element,shieldCost == 0);
                        }
                        else
                        {
                            Fight.AddUpdateUnit(this, BattleConstant.UpdateType.BUFF_EFFECT_IMMUNE, buff.BuffCfg.Id);
                        }
                        GetBuffManager().TriggerBuff(typeof(BattleUnitDotEffectBuffTrigger), buff.BuffCfg);
                        return;
                    }
                case BuffConstant.BUFF_EFFECT_TYPE_BATTLE_CHANGE_SKILL_ENERGY:
                    {
                        int addOrDec = Convert.ToInt32(buffEffect.EffectParams[0]);//增加或减少
                        int type = Convert.ToInt32(buffEffect.EffectParams[1]);//类型
                        int value = Convert.ToInt32(buffEffect.EffectParams[2]);//值
                        bool can = false;
                        if (buffEffect.EffectParams.Length > 3)
                        {
                            can = Convert.ToInt32(buffEffect.EffectParams[3]) == 1;//是否计算加成
                        }

                        if (type == 3)
                        {
                            value = Fix64.ToInt32(SkillFunction.CalcChangeAttribute(buff.Caster as BattleUnitVO, this, buff.SkillData, CfgSkillFunctionTable.Instance.GetDataByID(value)));
                        }

                        if (addOrDec == 1)
                        {
                            //增加
                            ChangeSkillEnergy(value,can, true);
                        }
                        else if (addOrDec == 2)
                        {
                            //减少
                            ChangeSkillEnergy(-value,can, true);
                        }
                        else if (addOrDec == 3)
                        {
                            //设置
                            ChangeSkillEnergy(value - SkillEnergy,can, true);
                        }
                        return;
                    }
                case BuffConstant.BUFF_EFFECT_TYPE_IMMEDIATELY_DEAD:
                    {
                        if (!IsDead())
                        {
                            HP = 0;
                            Fight.AddUpdateUnit(this, BattleConstant.UpdateType.CHANGE_HP, HP, 0, 0);
                            CheckSpStatus();
                        }
                        return;
                    }

                case BuffConstant.BUFF_EFFECT_TYPE_ADD_BUFF_BY_FUNCTION:
                    {
                        for (int j = 0; j < buffEffect.EffectParams.Length; j += 5)
                        {
                            int inheritCaster = Convert.ToInt32(buffEffect.EffectParams[j]);
                            int targetCid = Convert.ToInt32(buffEffect.EffectParams[j + 1]);
                            int buffCid = Convert.ToInt32(buffEffect.EffectParams[j + 2]);
                            int buffTime = Convert.ToInt32(buffEffect.EffectParams[j + 3]);
                            int stackFunction = Convert.ToInt32(buffEffect.EffectParams[j + 4]);
                            int stackNum = Math.Max(1, Fix64.ToInt32(SkillFunction.CalcChangeAttribute(buff.Caster as BattleUnitVO, buff.Holder as BattleUnitVO, buff.SkillData, CfgSkillFunctionTable.Instance.GetDataByID(stackFunction))));

                            foreach (IBuffHolder holder in (this as IBuffHolder).GetBuffTarget(buff, targetCid))
                            {
                                holder.GetBuffManager().AddBuffInner(inheritCaster == 1 ? buff.Caster : this, buff.SkillData, buffCid, buffTime, stackNum);
                            }
                        }
                        return;
                    }
                case BuffConstant.BUFF_EFFECT_TYPE_BATTLE_CHANGE_SKILL_AP:
                    {
                        int addOrDec = Convert.ToInt32(buffEffect.EffectParams[0]);//增加或减少
                        int type = Convert.ToInt32(buffEffect.EffectParams[1]);//类型
                        int value = Convert.ToInt32(buffEffect.EffectParams[2]);//值

                        if (type == 3)
                        {
                            value = Fix64.ToInt32(SkillFunction.CalcChangeAttribute(buff.Caster as BattleUnitVO, this, buff.SkillData, CfgSkillFunctionTable.Instance.GetDataByID(value)));
                        }

                        var troopVO = this.TroopType == BattleConstant.TroopType.ATTACK ? Fight.AttackTroopUnitVO : Fight.DefendTroopUnitVO;
                        if (addOrDec == 1)
                        {
                            //增加
                            troopVO.ChangeAP(value, true);
                        }
                        else if (addOrDec == 2)
                        {
                            //减少
                            troopVO.ChangeAP(-value, true);
                        }
                        else if (addOrDec == 3)
                        {
                            //设置
                            troopVO.ChangeAP(value - SkillEnergy, true);
                        }
                        return;
                    }
                case BuffConstant.BUFF_EFFECT_TYPE_ADD_SUB_BUFF_BY_FUNCTION:
                    {
                        for (int j = 0; j < buffEffect.EffectParams.Length; j += 5)
                        {
                            int inheritCaster = Convert.ToInt32(buffEffect.EffectParams[j]);
                            int targetCid = Convert.ToInt32(buffEffect.EffectParams[j + 1]);
                            int buffCid = Convert.ToInt32(buffEffect.EffectParams[j + 2]);
                            int buffTime = Convert.ToInt32(buffEffect.EffectParams[j + 3]);
                            int stackFunction = Convert.ToInt32(buffEffect.EffectParams[j + 4]);
                            int stackNum = Math.Max(1, Fix64.ToInt32(SkillFunction.CalcChangeAttribute(buff.Caster as BattleUnitVO, buff.Holder as BattleUnitVO, buff.SkillData, CfgSkillFunctionTable.Instance.GetDataByID(stackFunction))));

                            foreach (IBuffHolder holder in (this as IBuffHolder).GetBuffTarget(buff, targetCid))
                            {
                                Buff childBuff = holder.GetBuffManager().AddBuffInner(inheritCaster == 1 ? buff.Caster : this, buff.SkillData, buffCid, buffTime, stackNum);
                                if (childBuff != null && !childBuff.IsRemoved)
                                {
                                    buff.Children.Add(childBuff);
                                }
                            }
                        }
                        return;
                    }
                case BuffConstant.BUFF_EFFECT_TYPE_CHANGE_SKILL_CD:
                    {
                        int skillType = Convert.ToInt32(buffEffect.EffectParams[0]);
                        int cd = Convert.ToInt32(buffEffect.EffectParams[1]);

                        BattleSkillVO skill = null;
                        switch (skillType)
                        {
                            case SkillConstant.TYPE_REVOLT:
                                skill = RevoltSkill;
                                break;
                            case SkillConstant.TYPE_BATTER:
                                skill = BatterSkill;
                                break;
                            case SkillConstant.TYPE_ULTIMATE:
                                skill = UltimateSkill;
                                break;
                            case SkillConstant.TYPE_NORMAL:
                                skill = NormalSkill;
                                break;
                            default:
                                _logger.Error("can not support type : {0}", skillType);
                                break;
                        }

                        if (skill != null)
                        {
                            skill.CoolDown += cd;
                            if (skill.CoolDown < 0)
                            {
                                skill.CoolDown = 0;
                            }
                            Fight.AddUpdateUnit(this, BattleConstant.UpdateType.CHANGE_SKILL_CD, skill.CfgSkillData.Id, skill.CoolDown);
                        }
                        break;
                    }
                case BuffConstant.BUFF_EFFECT_TYPE_ADD_WEAK_TYPE:
                {
                    int weakId = Convert.ToInt32(buffEffect.EffectParams[0]);
                    int weakStatus = Convert.ToInt32(buffEffect.EffectParams[1]);
                    if (!WeakStatus.ContainsKey(weakId))
                    {
                        WeakStatus.Add(weakId,weakStatus);
                        Fight.AddUpdateUnit(this, BattleConstant.UpdateType.ADD_WEAK_TYPE, weakId,weakStatus);
                    }
                    break;
                }
                case BuffConstant.BUFF_EFFECT_TYPE_CHANGE_WEAK_STATUS:
                    {
                        int weakId = Convert.ToInt32(buffEffect.EffectParams[0]);
                        int weakStatus = Convert.ToInt32(buffEffect.EffectParams[1]);
                        
                        if (WeakStatus.ContainsKey(weakId))
                        {
                            int status = WeakStatus[weakId];
                            buffEffect.EffectValue = status;
                            WeakStatus[weakId] = weakStatus;
                            Fight.AddUpdateUnit(this, BattleConstant.UpdateType.UPDATE_WEAK_STATUS, weakId,weakStatus);
                        }
                        break;
                    }
                case BuffConstant.BUFF_EFFECT_TYPE_REMOVE_WEAK_TYPE:
                {
                    int weakId = Convert.ToInt32(buffEffect.EffectParams[0]);
                    if (WeakStatus.ContainsKey(weakId))
                    {
                        int status = WeakStatus[weakId];
                        buffEffect.EffectValue = status;
                        WeakStatus.Remove(weakId);
                        Fight.AddUpdateUnit(this, BattleConstant.UpdateType.REMOVE_WEAK_TYPE, weakId);                        
                    }
                    break;
                }
                case BuffConstant.BUFF_EFFECT_TYPE_CHANGE_WEAK_NUM:
                    int changeType =  Convert.ToInt32(buffEffect.EffectParams[0]);
                    int changeVal =  Convert.ToInt32(buffEffect.EffectParams[1]);
                    int oldWeak = WeakNum;
                    switch (changeType)
                    {
                        case 1:
                            WeakNum += changeVal;
                            break;
                        case 2:
                            if (WeakNum <= 0)
                            {
                                return;
                            }
                            WeakNum -= changeVal;
                            break;
                        case 3:
                            WeakNum = changeVal;
                            break;
                    }

                    WeakNum = Math.Max(0, WeakNum);
                    Fight.AddUpdateUnit(this, BattleConstant.UpdateType.UPDATE_WEAK_NUM, WeakNum);
                    
                    GetBuffManager().TriggerBuff(typeof(BattleUnitWeakChangeBuffTrigger));
                    
                    if (WeakNum > 0 && oldWeak < 1)
                    {
                        GetBuffManager().TriggerBuff(typeof(BattleUnitWeakRecoverBuffTrigger));
                        // 移除弱点击破BUFF
                        GetBuffManager().RemoveBuff(CfgMonsterData.WeakDestroy);
                    }
                    if (WeakNum < 1 && oldWeak > 0)
                    {
                        WeakDestroy();
                    }
                    break;
                case BuffConstant.BUFF_EFFECT_TYPE_BATTLE_CUMULATIVE_HURT:
                    {
                        bool immunity = SkillFunction.IsImmunity(buff.Caster as BattleUnitVO, buff.Holder as BattleUnitVO, buff.BuffCfg.Element, false);
                        if (!immunity)
                        {
                            Fix64 percentage = Fix64.Parse(buffEffect.EffectParams[0]);
                            Fix64 value = buffEffect.EffectValue * percentage;
                            ChangeHP(Fix64.ToInt32(value), true, false,buff.Caster as BattleUnitVO, buff.BuffCfg.Element);
                        }
                        else
                        {
                            Fight.AddUpdateUnit(this, BattleConstant.UpdateType.BUFF_EFFECT_IMMUNE, buff.BuffCfg.Id);
                        }
                        GetBuffManager().TriggerBuff(typeof(BattleUnitDotEffectBuffTrigger), buff.BuffCfg);
                    }
                    return;
                case BuffConstant.BUFF_EFFECT_TYPE_BATTLE_MOVE:
                    {
                        int move = Convert.ToInt32(buffEffect.EffectParams[0]);
                        /*
                         * 7 4 1
                         * 8 5 2
                         * 9 6 3
                         * move 取值，中心原点为33
                         * 例：
                         * 当前坐标为5， move=32就向上移动，4
                         * 当前坐标为5， move=34就向下移动，6
                         * 当前坐标为5， move=23就向前移动，2
                         * 当前坐标为5， move=43就向后移动，8
                         */
                        if (move == 0)
                        {
                            // 寻找空地
                            List<int> clearing = new List<int>();
                            for (int i = 1; i < 9; i++)
                            {
                                clearing.Add(i);
                            }
                            Dictionary<int, BattleUnitVO> units = Fight.GetAllBattleUnits();
                            foreach (KeyValuePair<int,BattleUnitVO> unit in units)
                            {
                                if (unit.Value.BattlePos == BattlePos && unit.Value.TroopType == TroopType)
                                {
                                    clearing.Remove(unit.Value.BattlePos);
                                }
                            }

                            if (clearing.Count > 0)
                            {
                                int index = Fight.Random.randomInt(clearing.Count);
                                // 执行移动
                                MovePos(clearing[index]);
                            }
                        }
                        else
                        {
                            int ax = (int)Math.Ceiling(BattlePos / 3.0f);
                            int ay = BattlePos % 3;
                            ay = ay == 0 ? 3 : ay;
                            
                            int dx = move / 10 - 3;
                            int dy = move % 10 - 3;

                            int nx = ax + dx;
                            int ny = ay + dy;
                            if (nx < 1 || nx > 3 || ny < 1 || ny > 3) //目前是九宫格
                                return;

                            int battlePos = ((nx - 1) * 3) + ny;

                            // 选取敌人
                            int count = Fight.GetAllBattleUnits().Where(t => t.Value.BattlePos == battlePos && t.Value.TroopType == TroopType).Count();
                            if (count < 1)
                            {
                                // 执行移动
                                MovePos(battlePos);
                            }
                        }
                    }
                    return;

                case BuffConstant.BUFF_EFFECT_TYPE_ENCHANT:
                    {
                        foreach (BattleSkillVO skill in AllSkills)
                        {
                            // 给技能附魔元素
                            skill.EnchantElement(Convert.ToInt32(buffEffect.EffectParams[0]), Convert.ToInt32(buffEffect.EffectParams[1]));
                        }
                    }
                    return;
                case BuffConstant.BUFF_CHANGE_TILE_TERRAIN:
                    {
                        int terrainCid = Convert.ToInt32(buffEffect.EffectParams[0]);
                        int buffTime = Convert.ToInt32(buffEffect.EffectParams[1]);
                        // 修改地形
                        if (Tile!=null)
                        {
                            Tile.ChangeTerrain(terrainCid,buffTime);    
                        }
                        
                        
                        // int terrainCid = Convert.ToInt32(buffEffect.EffectParams[0]);
                        // CfgTerrainData newCfgTerrainData = CfgTerrainTable.Instance.GetDataByID(7);
                        // if (newCfgTerrainData == null)
                        // {
                        //     return;
                        // }
                        //
                        // if (Tile.CfgTerrainData != null)
                        // {
                        //     // 移除之前的地形BUFF
                        //     for (int i = 0; i < Tile.CfgTerrainData.Buff.Length; i++)
                        //     {
                        //         GetBuffManager().RemoveBuff(Tile.CfgTerrainData.Buff[i]);
                        //     }   
                        // }
                        // Tile.CfgTerrainData = newCfgTerrainData;
                        // // 重新初始化BUFF
                        // Tile.InitBuff();
                    }
                    return;
                case BuffConstant.BUFF_EFFECT_CAHNEG_SIZE:
                {
                    int changeSize = Convert.ToInt32(buffEffect.EffectParams[0]);
                    int maxSize = Convert.ToInt32(buffEffect.EffectParams[1]);
                    Fight.AddUpdateUnit(this, BattleConstant.UpdateType.CHANGE_SIZE, changeSize,maxSize);
                }
                    return;
                case BuffConstant.BUFF_EFFECT_DIALOG:
                {
                    int dialogId = Convert.ToInt32(buffEffect.EffectParams[0]);
                    Fight.AddUpdateUnit(this, BattleConstant.UpdateType.TRIGGER_DIALOG, dialogId);
                }
                    return;
                case BuffConstant.BUFF_EFFECT_TYPE_RESET_ROUND_ACTION:
                {
                    int action = Convert.ToInt32(buffEffect.EffectParams[0]);
                    SetSpStatus(BattleConstant.SPStatus.ACTION,action == 1);
                }
                    return;
                case BuffConstant.BUFF_EFFECT_TYPE_CHANGE_FIGHT_INTEGRAL:
                {
                    int addOrDec = Convert.ToInt32(buffEffect.EffectParams[0]);//增加或减少
                    int type = Convert.ToInt32(buffEffect.EffectParams[1]);//类型
                    int value = Convert.ToInt32(buffEffect.EffectParams[2]);//值

                    if (type == 3)
                    {
                        value = Fix64.ToInt32(SkillFunction.CalcChangeAttribute(buff.Caster as BattleUnitVO, this, buff.SkillData, CfgSkillFunctionTable.Instance.GetDataByID(value)));
                    }
                    if (addOrDec == 1)
                    {
                        Fight.FightIntegral += value;
                    }
                    else if (addOrDec == 2)
                    {
                        Fight.FightIntegral -= value;
                    }
                    else if (addOrDec == 3)
                    {
                        Fight.FightIntegral = value;
                    }
                    return;
                }
                default:
                    return;
            }
        }

        private ICollection<BattleUnitVO> GetTriggerConditionTarget(int targetType)
        {
            List<BattleUnitVO> retList = new List<BattleUnitVO>();
            switch (targetType)
            {
                case 3:
                    retList.AddRange(Fight.GetAllBattleUnits().Values);
                    break;
                case 2:
                    foreach (BattleUnitVO unit in Fight.GetAllBattleUnits().Values)
                    {
                        if (!unit.IsDead() && unit.TroopType != TroopType)
                        {
                            retList.Add(unit);
                        }
                    }
                    break;
                case 1:
                    foreach (BattleUnitVO unit in Fight.GetAllBattleUnits().Values)
                    {
                        if (!unit.IsDead() && unit.TroopType == TroopType)
                        {
                            retList.Add(unit);
                        }
                    }
                    break;
                default:
                    return null;
            }
            return retList;
        }

        
        public static ICollection<BattleUnitVO> GetFunctionValueWithRPNExpTarget(BattleUnitVO self, int targetType)
        {
            List<BattleUnitVO> retList = new List<BattleUnitVO>();
            switch (targetType)
            {
                case 3:
                    retList.AddRange(self.Fight.GetAllBattleUnits().Values);
                    break;
                case 2:
                    foreach (BattleUnitVO unit in self.Fight.GetAllBattleUnits().Values)
                    {
                        if (!unit.IsDead() && unit.TroopType != self.TroopType)
                        {
                            retList.Add(unit);
                        }
                    }
                    break;
                case 1:
                    foreach (BattleUnitVO unit in self.Fight.GetAllBattleUnits().Values)
                    {
                        if (!unit.IsDead() && unit.TroopType == self.TroopType)
                        {
                            retList.Add(unit);
                        }
                    }
                    break;
                default:
                    return null;
            }
            return retList;
        }
        
        private BattleUnitVO GetTriggerConditionTarget(Buff buff, int type)
        {
            switch (type)
            {
                case 1:
                    return buff.Caster as BattleUnitVO;
                case 2:
                    return buff.Holder as BattleUnitVO;
                case 3:
                    return Fight.CurrSkillCaster;
                case 4:
                    return Fight.AttackTroopUnitVO;
                case 5:
                    return Fight.DefendTroopUnitVO;
                case 6:
                    return Fight.CurrAtkTarget;
                default:
                    //指定id的buff施放者
                    foreach (Buff _buff in buff.Holder.GetBuffManager().GetAllBuffs(true))
                    {
                        if (_buff.BuffCfg.Id == type)
                        {
                            return _buff.Caster as BattleUnitVO;
                        }
                    }
                    return null;
            }
        }

        bool IBuffHolder.CheckTriggerCondition(Buff buff)
        {
            // BUFF条件表达式
            string RPNExp = buff.BuffCfg.DynamicRPNExp;
            if (RPNExp == "")
            {
                return true;
            }
            Fix64[] dynamicArgs = new Fix64[buff.BuffCfg.DynamicArgType.Length];
            for (int i = 0; i < buff.BuffCfg.DynamicArgType.Length; i++)
            {
                int type = buff.BuffCfg.DynamicArgType[i];
                string[] param = buff.BuffCfg.DynamicArgParams[i];
                if (type != 0)
                {
                    switch (type)
                    {
                        // 返回本技能的等级
                        case 101:
                            dynamicArgs[i] = buff.SkillData == null ? 0 :  buff.SkillData.Lv;
                            break;
                        // 指定目标是否有指定的BUFF（ID，类ID，组ID）
                        case 102:
                            {
                                dynamicArgs[i] = 0;
                                IBuffHolder unit = GetTriggerConditionTarget(buff, Convert.ToInt32(param[0]));
                                if (unit == null)
                                {
                                    return false;
                                }
                                int buffParamType = Convert.ToInt32(param[1]);
                                int keyID = Convert.ToInt32(param[2]);
                                foreach (Buff _buff in unit.GetBuffManager().GetAllBuffs(true))
                                {
                                    if (BuffUtil.IsMatch(_buff.BuffCfg, buffParamType, keyID))
                                    {
                                        dynamicArgs[i] = 1;
                                        break;
                                    }
                                }
                            }
                            break;
                        // 指定目标的指定BUFF的层数
                        case 103:
                            {
                                dynamicArgs[i] = 0;
                                IBuffHolder unit = GetTriggerConditionTarget(buff, Convert.ToInt32(param[0]));
                                if (unit == null)
                                {
                                    return false;
                                }
                                int buffParamType = Convert.ToInt32(param[1]);
                                int keyID = Convert.ToInt32(param[2]);
                                foreach (Buff _buff in unit.GetBuffManager().GetAllBuffs(true))
                                {
                                    if (BuffUtil.IsMatch(_buff.BuffCfg, buffParamType, keyID))
                                    {
                                        dynamicArgs[i] += _buff.StackCount;
                                    }
                                }
                                break;
                            }
                        // 返回指定目标的指定ID属性的值
                        case 303:
                            {
                                BattleUnitVO unit = GetTriggerConditionTarget(buff, Convert.ToInt32(param[0]));
                                if (unit == null)
                                {
                                    return false;
                                }
                                dynamicArgs[i] = unit.GetAttribute(Convert.ToInt32(param[1]),Convert.ToInt32(param[2]));
                                // dynamicArgs[i] = unit.GetBattleAttribute(Convert.ToInt32(param[1]));
                            }
                            break;
                        // 指定目标是否有标签（谦卑/贞洁）
                        case 304:
                            {
                                BattleUnitVO unit = GetTriggerConditionTarget(buff, Convert.ToInt32(param[0]));
                                if (unit == null)
                                {
                                    return false;
                                }
                                int tag = Convert.ToInt32(param[1]);
                                dynamicArgs[i] = Array.IndexOf(unit.CfgMonsterData.Tag, tag) > -1 ? 1 : 0;
                                break;
                            }
                        // // 7宗罪类型
                        // case 305:
                        //     {
                        //         BattleUnitVO unit = GetTriggerConditionTarget(buff, Convert.ToInt32(param[0]));
                        //         if (unit == null)
                        //         {
                        //             return false;
                        //         }
                        //         dynamicArgs[i] = unit.CfgMonsterData.Religion;
                        //         break;
                        //     }
                        // 返回目标的状态
                        case 306:
                            {
                                BattleUnitVO unit = GetTriggerConditionTarget(buff, Convert.ToInt32(param[0]));
                                if (unit == null)
                                {
                                    return false;
                                }
                                int statusType = Convert.ToInt32(param[1]);
                                dynamicArgs[i] = unit.FightStatus[statusType] ? 1 : 0;
                                break;
                            }
                        // 返回目标的特殊状态
                        case 307:
                            {
                                BattleUnitVO unit = GetTriggerConditionTarget(buff, Convert.ToInt32(param[0]));
                                if (unit == null)
                                {
                                    return false;
                                }
                                int spType = Convert.ToInt32(param[1]);
                                dynamicArgs[i] = unit.GetSpStatus(spType) ? 1 : 0;
                                break;
                            }
                        // 返回战场参数
                        case 308:
                            {
                                int paramID = Convert.ToInt32(param[0]);
                                int value = 0;
                                Fight.BattleParams.TryGetValue(paramID, out value);
                                dynamicArgs[i] = value;
                                break;
                            }
                        // 返回站位位置
                        case 309:
                            {
                                BattleUnitVO unit = GetTriggerConditionTarget(buff, Convert.ToInt32(param[0]));
                                if (unit == null)
                                {
                                    return false;
                                }
                                dynamicArgs[i] = unit.BattlePos;
                                break;
                            }
                        // 返回战场中拥有指定buff的目标数
                        case 310:
                            {
                                int targetType = Convert.ToInt32(param[0]);
                                int buffParamType = Convert.ToInt32(param[1]);
                                int keyID = Convert.ToInt32(param[2]);

                                int targetCount = 0;
                                foreach (BattleUnitVO unit in GetTriggerConditionTarget(targetType))
                                {
                                    foreach (Buff _buff in unit.GetBuffManager().GetAllBuffs(true))
                                    {
                                        if (BuffUtil.IsMatch(_buff.BuffCfg, buffParamType, keyID))
                                        {
                                            targetCount++;
                                            break;
                                        }
                                    }
                                }
                                dynamicArgs[i] = targetCount;
                                break;
                            }
                        // 返回存活的单位数量
                        case 311:
                            {
                                int friendType = Convert.ToInt32(param[0]);
                                int troopType = 0;
                                if (friendType == 1)
                                {
                                    //友军
                                    troopType = TroopType;
                                }
                                else if (friendType == 2)
                                {
                                    troopType = TroopType == BattleConstant.TroopType.ATTACK ? BattleConstant.TroopType.DEFEND : BattleConstant.TroopType.ATTACK;
                                }
                                else
                                {
                                    troopType = -1;
                                }

                                int count = 0;
                                foreach (BattleUnitVO unit in Fight.GetAllBattleUnits().Values)
                                {
                                    if (!unit.IsDead() && (unit.TroopType == troopType || troopType == -1))
                                    {
                                        count++;
                                    }
                                }
                                dynamicArgs[i] = count;
                                break;
                            }
                        // 返回战场中目标拥有指定buff层数
                        case 312:
                            {
                                int targetType = Convert.ToInt32(param[0]);
                                int buffParamType = Convert.ToInt32(param[1]);
                                int keyID = Convert.ToInt32(param[2]);
                                int targetCount = 0;
                                foreach (BattleUnitVO unit in GetTriggerConditionTarget(targetType))
                                {
                                    foreach (Buff _buff in unit.GetBuffManager().GetAllBuffs(true))
                                    {
                                        if (BuffUtil.IsMatch(_buff.BuffCfg, buffParamType, keyID))
                                        {
                                            targetCount += _buff.StackCount;
                                        }
                                    }
                                }
                                dynamicArgs[i] = targetCount;
                                break;
                            }
                        // 返回目标buff数量
                        case 313:
                            {
                                BattleUnitVO unit = GetTriggerConditionTarget(buff, Convert.ToInt32(param[0]));
                                if (unit == null)
                                {
                                    return false;
                                }
                                int buffParamType = Convert.ToInt32(param[1]);
                                int keyID = Convert.ToInt32(param[2]);
                                int targetCount = 0;
                                foreach (Buff _buff in unit.GetBuffManager().GetAllBuffs(true))
                                {
                                    if (BuffUtil.IsMatch(_buff.BuffCfg, buffParamType, keyID))
                                    {
                                        targetCount++;
                                    }
                                }
                                dynamicArgs[i] = targetCount;
                                break;
                            }
                        // 返回目标是否为指定类型怪物
                        case 314:
                            {
                                BattleUnitVO unit = GetTriggerConditionTarget(buff, Convert.ToInt32(param[0]));
                                if (unit == null)
                                {
                                    return false;
                                }
                                int monsterType = Convert.ToInt32(param[1]);
                                int subType = Convert.ToInt32(param[2]);
                                
                                dynamicArgs[i] = 0;
                                
                                if (monsterType != -1 && unit.CfgMonsterData.MonsterType != monsterType)
                                {
                                    break;    
                                }
                                if (subType != -1 && unit.CfgMonsterData.SubType != subType)
                                {
                                    break;    
                                }
                                dynamicArgs[i] = 1;

                                break;
                            }
                        // 返回目标是否为指定种族怪物
                        case 315:
                            {
                                BattleUnitVO unit = GetTriggerConditionTarget(buff, Convert.ToInt32(param[0]));
                                if (unit == null)
                                {
                                    return false;
                                }
                                int race = Convert.ToInt32(param[1]);
                                dynamicArgs[i] = unit.CfgMonsterData.Profession == race ? 1 : 0;
                                break;
                            }
                        // 返回目标大招ID
                        case 316:
                            {
                                BattleUnitVO unit = GetTriggerConditionTarget(buff, Convert.ToInt32(param[0]));
                                if (unit == null)
                                {
                                    return false;
                                }
                                dynamicArgs[i] = unit.UltimateSkill == null ? 0 : unit.UltimateSkill.CfgSkillData.Id;
                                break;
                            }
                        // 返回目标护盾
                        case 317:
                            {
                                BattleUnitVO unit = GetTriggerConditionTarget(buff, Convert.ToInt32(param[0]));
                                if (unit == null)
                                {
                                    return false;
                                }

                                dynamicArgs[i] = unit.GetShield();
                                break;
                            }
                        
                        // 英雄等级
                        case 318:
                            {
                                BattleUnitVO unit = GetTriggerConditionTarget(buff, Convert.ToInt32(param[0]));
                                if (unit == null)
                                {
                                    return false;
                                }
                                dynamicArgs[i] = unit.Level;
                                break;
                            }
                        // 技能释放次数
                        case 319:
                            {
                                dynamicArgs[i] = buff.SkillData == null ? 0 : buff.SkillData.ReleaseCount;
                                break;
                            }
                        // 英雄元素
                        case 320:
                            {
                                BattleUnitVO unit = GetTriggerConditionTarget(buff, Convert.ToInt32(param[0]));
                                if (unit == null)
                                {
                                    return false;
                                }
                                dynamicArgs[i] = unit.CfgMonsterData.Elements;
                                break;
                            }
                        // 返回指定单位属性1 -> 属性2
                        case 321:
                        {
                            BattleUnitVO unit = GetTriggerConditionTarget(buff, Convert.ToInt32(param[0]));
                            if (unit == null)
                            {
                                return false;
                            }
                            Fix64 attr1 = unit.GetBattleAttribute(Convert.ToInt32(param[1]));
                            Fix64 attr2 = unit.GetBattleAttribute(Convert.ToInt32(param[2]));
                            switch (Convert.ToInt32(param[3]))
                            {
                                case 1:
                                    dynamicArgs[i] = attr1 + attr2;
                                    break;
                                case 2:
                                    dynamicArgs[i] = attr1 - attr2;
                                    break;
                                case 3:
                                    dynamicArgs[i] = attr1 * attr2;
                                    break;
                                case 4:
                                    dynamicArgs[i] = attr1 / attr2;
                                    break;
                            }
                            break;
                        }
                        // 返回当前回合数
                        case 322:
                        {
                            BattleUnitVO first = Fight._unitActionOrders.First();
                            switch (Convert.ToInt32(param[0]))
                            {
                                case 0:
                                    // 总回合数
                                    dynamicArgs[i] = Fight._round;
                                    break;
                                case 1:
                                    // 我方回合数
                                    if (first.TroopType == TroopType)
                                    {
                                        dynamicArgs[i] = Fight._round;
                                    }
                                    else
                                    {
                                        if (Fight._roundToggle)
                                        {
                                            dynamicArgs[i] = Fight._round;
                                        }
                                        else
                                        {
                                            dynamicArgs[i] = Fight._round - 1;
                                        }
                                    }
                                    break;
                                case 2:
                                    // 敌方回合数
                                    if (first.TroopType == TroopType)
                                    {
                                        dynamicArgs[i] = Fight._round;
                                    }
                                    else
                                    {
                                        if (Fight._roundToggle)
                                        {
                                            dynamicArgs[i] = Fight._round;
                                        }
                                        else
                                        {
                                            dynamicArgs[i] = Fight._round - 1;
                                        }
                                    }
                                    break;
                            }
                            break;
                        }
                        // 返回当前技能目标数
                        case 323:
                        {
                            if (Fight.CurrSkillTargets != null)
                            {
                                dynamicArgs[i] = Fight.CurrSkillTargets.Count;
                            }
                            else
                            {
                                dynamicArgs[i] = 0;
                            }

                            break;
                        }
                        // 返回当前技能元素
                        case 324:
                        {
                            if (Fight.CurrSkill != null)
                            {
                                dynamicArgs[i] = Fight.CurrSkill.GetElement();
                            }
                            else
                            {
                                dynamicArgs[i] = 0;
                            }
                            break;
                        }
                        // 返回目标元素反应值
                        case 325:
                        {
                            BattleUnitVO unit = GetTriggerConditionTarget(buff, Convert.ToInt32(param[0]));
                            if (unit == null)
                            {
                                return false;
                            }

                            CfgHeroLvData heroLvData = CfgHeroLvTable.Instance.GetDataByID(unit.Level);
                            if (heroLvData == null)
                            {
                                return false;
                            }
                            
                            int val = Fix64.ToInt32(unit.GetBattleAttribute(BattleConstant.Attribute.TYPE_TRIGGER_ELEMENT_REACTION));
                            if (val >= heroLvData.ElementReactionParam.Length)
                            {
                                return false;
                            }
                            
                            dynamicArgs[i] = val <= 0 ? 0 : heroLvData.ElementReactionParam[val-1];
                            break;
                        }
                        // 返回战场中拥有指定标签的目标数
                        case 326:
                        {
                            int targetType = Convert.ToInt32(param[0]); 
                            int tag = Convert.ToInt32(param[1]);
                            int targetCount = 0;
                            foreach (BattleUnitVO unit in GetFunctionValueWithRPNExpTarget(buff.Holder as BattleUnitVO, targetType))
                            {
                                if (Array.IndexOf(unit.CfgMonsterData.Tag, tag) > -1)
                                {
                                    targetCount++;
                                    break;
                                }
                            }
                            dynamicArgs[i] = targetCount;
                            break;
                        }
                        // 返回目标指定弱点状态  参数:目标类型，弱点ID
                        case 327:
                        {
                            BattleUnitVO unit = GetTriggerConditionTarget(buff, Convert.ToInt32(param[0]));
                            if (unit == null)
                            {
                                return false;
                            }

                            int value = 0;
                            unit.WeakStatus.TryGetValue(Convert.ToInt32(param[1]), out value);
                            dynamicArgs[i] = value;
                            break;
                        }
                        // 返回目标弱点层数 参数:目标类型
                        case 328:
                        {
                            BattleUnitVO unit = GetTriggerConditionTarget(buff, Convert.ToInt32(param[0]));
                            if (unit == null)
                            {
                                return false;
                            }
                            dynamicArgs[i] = unit.WeakNum;
                            break;
                        }
                        // 返回目标是否存在指定弱点 参数:目标类型，弱点ID，弱点类型
                        case 329:
                        {
                            BattleUnitVO unit = GetTriggerConditionTarget(buff, Convert.ToInt32(param[0]));
                            if (unit == null)
                            {
                                return false;
                            }

                            int value = 0;
                            unit.WeakStatus.TryGetValue(Convert.ToInt32(param[1]), out value);
                            dynamicArgs[i] = value == Convert.ToInt32(param[2]) || (value != 0 && Convert.ToInt32(param[2]) == -1) ? 1 : 0;
                            break;
                        }
                        // 返回目标弱点层数 参数:目标类型
                        case 330:
                        {
                            BattleUnitVO unit = GetTriggerConditionTarget(buff, Convert.ToInt32(param[0]));
                            if (unit == null)
                            {
                                return false;
                            }
                            dynamicArgs[i] = unit.WeakStatus.Count;
                            break;
                        }
                        // 返回目标层数是否减少  参数:目标类型
                        case 331:
                        {
                            BattleUnitVO unit = GetTriggerConditionTarget(buff, Convert.ToInt32(param[0]));
                            if (unit == null)
                            {
                                return false;
                            }
                            dynamicArgs[i] = unit.WeakNum < unit.WeakMaxNum ? 1 : 0;
                            break;
                        }
                        default:
                            _logger.Warning("not find dynamicArgs type : {0}", type);
                            break;
                    }
                }
            }
            return (bool)RPN.RPN.Instance.Evaluate(RPNExp, dynamicArgs);
        }

        bool IBuffHolder.IsDead()
        {
            return IsDead();
        }

        /// <summary>
        /// 从buff效果获取数据，设置无视战斗状态
        /// </summary>
        private void _ResetImmuneFightStatus()
        {
            //计算状态
            bool[] immuneFightStatus = new bool[BattleConstant.Status.STATUS_NUM];
            foreach (Buff buff in _BuffManager.GetAllBuffs(true))
            {
                foreach (BuffEffect effect in buff.Effects)
                {
                    if (effect.EffectType == BuffConstant.BUFF_EFFECT_TYPE_IMMUNE_STATUS)
                    {
                        foreach (string param in effect.EffectParams)
                        {
                            int status = Convert.ToInt32(param);
                            immuneFightStatus[status] = true;
                        }
                    }
                }
            }
            ImmuneFightStatus = immuneFightStatus;
            _ResetFightStatus();
        }

        /// <summary>
        /// 从buff效果获取数据，设置战斗状态
        /// </summary>
        private void _ResetFightStatus()
        {
            //计算状态
            bool[] fightStatus = new bool[BattleConstant.Status.STATUS_NUM];
            foreach (Buff buff in _BuffManager.GetAllBuffs(true))
            {
                foreach (BuffEffect effect in buff.Effects)
                {
                    if (effect.EffectType == BuffConstant.BUFF_EFFECT_TYPE_CHANGE_BATTLE_STATUS)
                    {
                        foreach (string param in effect.EffectParams)
                        {
                            int status = Convert.ToInt32(param);
                            if (!ImmuneFightStatus[status])
                            {
                                fightStatus[status] = true;
                            }
                        }
                    }
                }
            }

            //设置状态
            for (int i = 0; i < fightStatus.Length; i++)
            {
                if (FightStatus[i] != fightStatus[i])
                {
                    if (fightStatus[i] == true)
                    {
                        //获得状态
                        Fight.AddUpdateUnit(this, BattleConstant.UpdateType.ADD_STATUS, i);
                        _BuffManager.TriggerBuff(typeof(BattleSelfAddStatusBuffTrigger), i);
                    }
                    else
                    {
                        //移除状态
                        Fight.AddUpdateUnit(this, BattleConstant.UpdateType.REMOVE_STATUS, i);
                    }
                    FightStatus[i] = fightStatus[i];
                }
            }
        }

        public BattleUnitPOD ToData()
        {
            BattleUnitPOD data = new BattleUnitPOD();
            data.ID = ID;
            data.Pid = Pid;
            data.Level = Level;
            data.Power = _power;
            data.AI_TYPE = AIType;
            data.HP = HP;
            data.MaxHP = Fix64.ToInt32(GetBattleAttribute(BattleConstant.Attribute.TYPE_HP_MAX));
            data.SkillEnergy = SkillEnergy;
            data.MaxEnergy = Fix64.ToInt32(GetBattleAttribute(BattleConstant.Attribute.TYPE_ENERGY_MAX));
            data.AP = AP;
            data.Size = Convert.ToSingle(CfgMonsterData.MonsterScale.ToString());
            data.MaxAP = Fight.GetMaxAP();
            data.CommonSkillCD = CommonSkillCD;
            data.Speed = Fix64.ToInt32(GetBattleAttribute(BattleConstant.Attribute.TYPE_SPEED));
            data.TroopType = TroopType;
            data.BattlePos = BattlePos;
            data.Skills = new List<int>();
            foreach (var entry in AllSkills)
            {
                data.Skills.Add(entry.CfgSkillData.Id);
            }
            data.MonsterCfgId = _monsterCfgId;
            List<BattleBuffPOD> buffs = new List<BattleBuffPOD>();
            foreach (Buff buff in _BuffManager.GetAllBuffs(true))
            {
                BattleBuffPOD pod = new BattleBuffPOD();
                pod.Cid = buff.BuffCfg.Id;
                pod.Stack = buff.StackCount;
                pod.LeftTime = buff.LeftTime;
                buffs.Add(pod);
            }
            data.Shield = GetShield();
            data.Buffs = buffs;
            data.Status = FightStatus;
            data.SPStatus = _SPStatus;
            data.WeakStatus = new Dictionary<int, int>();
            data.SkillCD = new Dictionary<int, int>();
            data.SkillCostEnergy = new Dictionary<int, int>();
            data.SkillCostAP = new Dictionary<int, int>();
            data.SkillsLevel = new Dictionary<int, int>(AllSkills.Count);
            data.SkillPurifyLv = new Dictionary<int, int>(AllSkills.Count);
            data.SkillStrengLv = new Dictionary<int, int>(AllSkills.Count);
            data.StateTimeLine = new Dictionary<int, int>();
            data.SkillSkinId = new Dictionary<int, int>();
            data.SkillReleaseLimit = new Dictionary<int, int>();
            data.SkillTarget = new Dictionary<int, int>();
            foreach (BattleSkillVO skill in AllSkills)
            {
                data.SkillCD.Add(skill.CfgSkillData.Id, skill.CoolDown);
                data.SkillCostEnergy.Add(skill.CfgSkillData.Id, skill.SkillData.GetCostEnergy(this.TroopType));
                data.SkillCostAP.Add(skill.CfgSkillData.Id, skill.SkillData.NeedAp);
                data.SkillsLevel.Add(skill.CfgSkillData.Id, skill.SkillData.Lv);
                data.SkillPurifyLv.Add(skill.CfgSkillData.Id, skill.SkillData.PurifyLv);
                data.SkillStrengLv.Add(skill.CfgSkillData.Id, skill.SkillData.StrengLv);
                data.SkillSkinId.Add(skill.CfgSkillData.Id, skill.SkillData.TimelineID);
                data.SkillReleaseLimit.Add(skill.CfgSkillData.Id, skill.SkillData.ReleaseLimit);
                data.SkillTarget.Add(skill.CfgSkillData.Id, skill.SkillData.TargetTypeData.Id);
            }
            
            // 状态Timeline
            CfgElementEntityData cfg = CfgElementEntityTable.Instance.GetDataByID(CfgMonsterData.EntityID);
            data.StateTimeLine.Add(UnitTimelineConstant.TIMELINE_BORN, cfg.BornTimelineId);
            data.StateTimeLine.Add(UnitTimelineConstant.TIMELINE_DEATH, cfg.DieTimelineId);
            //data.StateTimeLine.Add(UnitTimelineConstant.TIMELINE_HIT, cfg.HitTimelineId);
            data.WeakMaxNum = WeakMaxNum;
            data.WeakNum = WeakNum;
            data.WeakStatus = WeakStatus;
            data.SkinId = SkinId;
            data.IconId = IconId;
            data.CreateType = CreateType;
            return data;
        }

        public FightUnitPOD ToFightData()
        {
            FightUnitPOD data = new FightUnitPOD();
            data.Pid = Pid;
            data.Attributes = new Fix64[BattleConstant.Attribute.ATTRIBUTE_NUM];
            data.Attributes[BattleConstant.Attribute.TYPE_HP] = HP;
            data.Attributes[BattleConstant.Attribute.TYPE_HP_MAX] = Fix64.ToInt32(GetBattleAttribute(BattleConstant.Attribute.TYPE_HP_MAX));
            data.Attributes[BattleConstant.Attribute.TYPE_ENERGY] = SkillEnergy;
            data.Attributes[BattleConstant.Attribute.TYPE_AP] = AP;
            data.Level = Level;
            data.Power = _power;
            data.TroopType = TroopType;
            data.BattlePos = BattlePos;
            data.MonsterCfgId = _monsterCfgId;
            data.SPStatus = _SPStatus;
            return data;
        }


        public void PrintLog()
        {
            //string attr = "{";
            //for (int i = 1; i < BattleConstant.Attribute.ATTRIBUTE_NUM; i++)
            //{
            //    attr += GetFightAttribute(i);
            //    if(i != BattleConstant.Attribute.ATTRIBUTE_NUM - 1)
            //    {
            //        attr += ", ";
            //    }
            //}
            //attr += "}";

            //_logger.Info("BattleUnit POS:{0}, Attr:{1}", TroopType == BattleConstant.TroopType.ATTACK ? BattlePos + 1 : BattlePos + 10 + 1, attr);
        }

        public BuffManager GetBuffManager()
        {
            return _BuffManager;
        }

        public int getTroopType()
        {
            return TroopType;
        }

        public virtual int GetScope()
        {
            return 0;
        }

        public virtual TurnBaseLogicFight GetFight()
        {
            return Fight;
        }

        /// <summary>
        /// 取得指定BUFF效果
        /// </summary>
        /// <param name="effectType"></param>
        /// <returns></returns>
        public List<BuffEffect> GetBuffEffect(int effectType)
        {
            List<BuffEffect> list = new List<BuffEffect>();
            foreach (Buff buff in GetBuffManager().GetAllBuffs(true))
            {
                foreach (BuffEffect effect in buff.Effects)
                {
                    if (effect.EffectType == effectType)
                    {
                        list.Add(effect);
                    }
                }
            }

            if (ScopeType == BattleConstant.ScopeType.MONSTER && Tile !=null)
            {
                foreach (Buff buff in Tile.GetBuffManager().GetAllBuffs(true))
                {
                    foreach (BuffEffect effect in buff.Effects)
                    {
                        if (effect.EffectType == effectType)
                        {
                            list.Add(effect);
                        }
                    }
                }
            }
            return list;
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

        /// <summary>
        /// 触发弱点
        /// </summary>
        /// <param name="type"></param>
        /// <param name="weakId"></param>
        /// <returns></returns>
        public void TriggerWeak(BattleUnitVO target,int type,params object[] args)
        {
            if (target.getTroopType() == getTroopType())
            {
                return;
            }
            if (WeakNum <= 0)
            {
                return;
            }
            foreach (KeyValuePair<int, int> kv in WeakStatus)
            {
                if (BattleConstant.WeakStatus.LOCK == kv.Value)
                {
                    continue;
                }

                CfgMonsterWeakData cfg = CfgMonsterWeakTable.Instance.GetDataByID(kv.Key);
                if (cfg == null || type != cfg.Type)
                {
                    continue;
                }

                
                // 检查触发参数
                switch (type)
                {
                    case BattleConstant.WeakType.ELEMENT_ATTACK:
                        if (cfg.Param[0] != -1 && cfg.Param[0] != Convert.ToInt32(args[0]))
                        {
                            continue;
                        }
                        break;
                    case BattleConstant.WeakType.ELEMENT_REACTION:
                        if (cfg.Param[0] != -1 && cfg.Param[0] != Convert.ToInt32(args[0]))
                        {
                            continue;
                        }
                        break;
                    case BattleConstant.WeakType.ARMS_TYPE:
                        if (cfg.Param[0] != -1 && cfg.Param[0] != Convert.ToInt32(args[0]))
                        {
                            continue;
                        }
                        break;
                    default:
                        continue;
                }
                
                
                _logger.Info("触发弱点 type = {0} key = {1}",kv.Key, type);
                
                
                if (BattleConstant.WeakStatus.HIDE == kv.Value)
                {
                    // 状态修改
                    WeakStatus[kv.Key] = BattleConstant.WeakStatus.SHOW;
                    Fight.AddUpdateUnit(this, BattleConstant.UpdateType.UPDATE_WEAK_STATUS,cfg.Id, BattleConstant.WeakStatus.SHOW);
                }

                // 修改弱点层数
                WeakNum = Math.Max(0, WeakNum - cfg.Weaken);
                Fight.AddUpdateUnit(this, BattleConstant.UpdateType.UPDATE_WEAK_NUM, this.WeakNum);
                GetBuffManager().TriggerBuff(typeof(BattleUnitWeakChangeBuffTrigger));
                
                // 弱点击破
                if (WeakNum <= 0)
                {
                    WeakDestroy();
                }
                
                break;
            }
        }

        /// <summary>
        /// 弱点击破
        /// </summary>
        private void WeakDestroy()
        {
            this.WeakBreakRound = Fight._round;
            this.GetBuffManager().TriggerBuff(typeof(BattleUnitWeakBeBreakBuffTrigger));

            // 击破效果
            this.GetBuffManager().AddBuff(this, null, CfgMonsterData.WeakDestroy);
        }
    }


