using FixMath.NET;
using IQIGame.Onigao.Config;
using System;
using System.Collections.Generic;


/// <summary>
/// 队伍辅助单位
/// 用于队伍层面的buff施放
/// 不算入实际作战单位
/// </summary>
public sealed class BattleTroopUnitVO : BattleUnitVO
{
    private static FightUnitPOD CreateTroopUnitPOD(TurnBaseLogicFight fight, int troopType, FightTroopPOD troopPod)
    {
        // CfgMonsterData cfgMonsterData = null;
        //
        // if (troopType == BattleConstant.TroopType.DEFEND && fight.CfgMonsterTeamData != null)
        // {
        //     cfgMonsterData = CfgMonsterTable.Instance.GetDataByID(fight.CfgMonsterTeamData.ReleaseOrder);    
        // }
        //
        // if (cfgMonsterData == null)
        // {
        //     cfgMonsterData = CfgMonsterTable.Instance.GetDataByID(1);
        // }

        FightUnitPOD fightUnitPOD = new FightUnitPOD();
        fightUnitPOD.Pid = troopPod.Pid;

        fightUnitPOD.InitBuff = new List<int>();
        fightUnitPOD.Skills = troopPod.Skills;
        fightUnitPOD.SkillLvs = troopPod.SkillLvs;
        fightUnitPOD.SkillPurifyLvs = troopPod.SkillPurifyLvs;
        fightUnitPOD.SkillStrengLvs = troopPod.SkillStrengLvs;
        fightUnitPOD.SkillStrengthens = troopPod.SkillStrengthens;

        // 处理代理人技能
        // for (int i = 0; i < troopPod.Skills.Count; i++)
        // {
        //      CfgSkillData skillCfg = CfgSkillTable.Instance.GetDataByID(troopPod.Skills[i]);
        //      
        //      if (skillCfg.SkillType == 5 && fight.CfgMonsterTeamData != null)
        //      {
        //          // 常识战斗
        //          if (fight.CfgMonsterTeamData.Type == 1)
        //          {
        //              fightUnitPOD.Skills[i] = skillCfg.SubSkills[0];
        //          }
        //          else if (fight.CfgMonsterTeamData.Type == 2){
        //              fightUnitPOD.Skills[i] = skillCfg.SubSkills[1];
        //          }
        //      }
        // }

        fightUnitPOD.Attributes = new Fix64[BattleConstant.Attribute.ATTRIBUTE_NUM];
        fightUnitPOD.Attributes[BattleConstant.Attribute.TYPE_HP] = 1;
        fightUnitPOD.Attributes[BattleConstant.Attribute.TYPE_HP_MAX] = 1;
        fightUnitPOD.BaseAttrs = new Fix64[0];
        // 初始化AP
        for (int i = 0; i < cfgMonsterData.Attribute.Length; i += 2)
        {
            if (cfgMonsterData.Attribute[i] == BattleConstant.Attribute.TYPE_AP)
            {
                fightUnitPOD.Attributes[BattleConstant.Attribute.TYPE_AP] = cfgMonsterData.Attribute[i + 1];
                break;
            }
        }

        fightUnitPOD.Attributes[BattleConstant.Attribute.TYPE_SPEED] = cfgMonsterData.Speed;


        fightUnitPOD.TroopType = troopType;
        fightUnitPOD.MonsterCfgId = cfgMonsterData.Id;
        fightUnitPOD.SkinId = cfgMonsterData.EntityID;
        return fightUnitPOD;
    }

    public BattleTroopUnitVO(TurnBaseLogicFight fight, int troopType, FightTroopPOD troopPod) : base(
        BattleConstant.ScopeType.TEAM, fight, CreateTroopUnitPOD(fight, troopType, troopPod))
    {
    }

    public Fix64 GetSelfBattleAttribute(int attType)
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
        return _fightAttributes[attType] + _buffAttributes[attType] + skillTemporaryatt;
    }

    override public Fix64 GetBattleAttribute(int attType)
    {
        Fix64 sumValue = 0;
        int count = 0;
        foreach (BattleUnitVO unit in Fight.GetAllBattleUnits().Values)
        {
            if (unit.TroopType == TroopType && !unit.IsHelper)
            {
                sumValue += unit.GetBattleAttribute(attType);
                count++;
            }
        }

        if (count == 0)
        {
            return 0;
        }

        return sumValue / count;
    }

    override public bool IsDead()
    {
        return false;
    }


    public BattleTroopPOD ToData()
    {
        BattleTroopPOD data = new BattleTroopPOD();
        data.ID = ID;
        data.Pid = Pid;
        data.Level = 0;
        data.Power = 0;
        data.HP = HP;
        data.MaxHP = Fix64.ToInt32(GetBattleAttribute(BattleConstant.Attribute.TYPE_HP_MAX));

        data.AP = AP;
        data.MaxAP = Fight.GetMaxAP();

        data.TroopType = TroopType;
        data.MonsterCfgId = 0;

        List<BattleBuffPOD> buffs = new List<BattleBuffPOD>();
        foreach (Buff buff in GetBuffManager().GetAllBuffs(true))
        {
            BattleBuffPOD pod = new BattleBuffPOD();
            pod.Cid = buff.BuffCfg.Id;
            pod.Stack = buff.StackCount;
            pod.LeftTime = buff.LeftTime;
            buffs.Add(pod);
        }

        data.Buffs = buffs;

        data.Skills = new List<int>();
        foreach (var entry in _skills)
        {
            data.Skills.Add(entry.Key);
        }

        data.CommonSkillCD = CommonSkillCD;
        data.SkillCD = new Dictionary<int, int>();
        data.SkillCostAP = new Dictionary<int, int>();
        data.SkillsLevel = new Dictionary<int, int>(AllSkills.Count);
        data.SkillPurifyLv = new Dictionary<int, int>(AllSkills.Count);
        data.SkillStrengLv = new Dictionary<int, int>(AllSkills.Count);
        data.SkillReleaseLimit = new Dictionary<int, int>();
        foreach (BattleSkillVO skill in AllSkills)
        {
            data.SkillCD.Add(skill.CfgSkillData.Id, skill.CoolDown);
            data.SkillCostAP.Add(skill.CfgSkillData.Id, skill.SkillData.NeedAp);
            data.SkillsLevel.Add(skill.CfgSkillData.Id, skill.SkillData.Lv);
            data.SkillPurifyLv.Add(skill.CfgSkillData.Id, skill.SkillData.PurifyLv);
            data.SkillStrengLv.Add(skill.CfgSkillData.Id, skill.SkillData.StrengLv);

            // 技能TimeLine
            data.SkillReleaseLimit.Add(skill.CfgSkillData.Id, skill.SkillData.ReleaseLimit);
        }

        return data;
    }
}