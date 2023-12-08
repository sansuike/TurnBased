

using System;
using System.Collections.Generic;
using FixMath.NET;
using IQIGame.Onigao.Config;
using IQIGame.Onigao.Logic.Utility;

  /// <summary>
    /// 召唤物
    /// </summary>
    public class BattleCallVO : BattleUnitVO
    {
        public BattleUnitVO Summoner { get; }

        public static LogicLogger Logger = new LogicLogger("BattleCallVO", true);
        private static FightUnitPOD CreateCallUnitPOD(BattleUnitVO summoner,int troopType, int battlePos,int monsterId)
        {
            // 取得召唤物的配置
            CfgMonsterData cfgMonsterData = CfgMonsterTable.Instance.GetDataByID(monsterId);

            // if (cfgMonsterData.MonsterType != 10)
            // {
            //     Logger.Error("BattleCallVO unit is Type Error");
            // }
            
            
            FightUnitPOD fightUnitPOD = new FightUnitPOD();
            
            fightUnitPOD.Skills = new List<int>();
            fightUnitPOD.SkillLvs = new List<int>();
            fightUnitPOD.SkillPurifyLvs = new List<int>();
            fightUnitPOD.SkillStrengLvs = new List<int>();
            fightUnitPOD.SkillStrengthens = new List<int>();
            fightUnitPOD.InitBuff = new List<int>();
            fightUnitPOD.TroopType = troopType;
            fightUnitPOD.BattlePos = battlePos;
            fightUnitPOD.AIType = 1;
            // 设置属性
            fightUnitPOD.Attributes = new Fix64[BattleConstant.Attribute.ATTRIBUTE_NUM];
            for (int j = 0; j < cfgMonsterData.Attribute.Length; j+=2) {
                Fix64 attType = cfgMonsterData.Attribute[j];
                if (attType > 0) {
                    switch (cfgMonsterData.CreateType)
                    {
                        case 0:
                            // 直接读表
                            fightUnitPOD.Attributes[(int)attType] = cfgMonsterData.Attribute[j+1];
                            break;
                        case 1:
                            // 基本属性
                            fightUnitPOD.Attributes[(int)attType] = summoner.GetFightAttribute()[(int)attType] * cfgMonsterData.Attribute[j+1];
                            break;
                        case 2:
                            // 战斗属性
                            fightUnitPOD.Attributes[(int)attType] = summoner.GetBattleAttribute((int)attType) * cfgMonsterData.Attribute[j+1];
                            break;
                    }
                }
            }
            fightUnitPOD.Attributes[BattleConstant.Attribute.TYPE_SPEED] = cfgMonsterData.Speed;
            // 设置怪物最大血量
            fightUnitPOD.Attributes[BattleConstant.Attribute.TYPE_HP_MAX] = Fix64.Min(fightUnitPOD.Attributes[BattleConstant.Attribute.TYPE_HP_MAX],fightUnitPOD.Attributes[BattleConstant.Attribute.TYPE_HP]);
            fightUnitPOD.BaseAttrs = new Fix64[0];
            fightUnitPOD.MonsterCfgId = monsterId;
            fightUnitPOD.Level = cfgMonsterData.Level == 0 ? summoner.Level : cfgMonsterData.Level;
            
            for (int i = 0; i < cfgMonsterData.Skill.Length; i++)
            {
                CfgSkillData skillData = CfgSkillTable.Instance.GetDataByID(cfgMonsterData.Skill[i]);
                fightUnitPOD.Skills.Add(skillData.Id);
                fightUnitPOD.SkillLvs.Add(skillData.Level);
                fightUnitPOD.SkillPurifyLvs.Add(0);
                fightUnitPOD.SkillStrengLvs.Add(0);
            }
            // fightUnitPOD.InitBuff.AddRange(cfgMonsterData.InitialBuff);
            fightUnitPOD.IsHelper = false;
            fightUnitPOD.Power = 0;
            fightUnitPOD.SkinId = cfgMonsterData.EntityID;
            return fightUnitPOD;
        }

        public BattleCallVO(BattleUnitVO summoner, int battlePos,int monsterId,int createType) : base(
            BattleConstant.ScopeType.CALL, summoner.Fight, CreateCallUnitPOD(summoner,summoner.TroopType, battlePos,monsterId))
        {
            CallStatus = 0;
            CreateType = createType;
            Summoner = summoner;
        }
        
        override public bool IsDead()
        {
            return GetSpStatus(BattleConstant.SPStatus.DEAD) || (CallStatus == 2 || CallStatus == 3);
        }
    }
