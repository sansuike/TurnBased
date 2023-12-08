using IQIGame.Onigao.Logic.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    /// <summary>
    /// 自己放技能结束,目标特殊状态
    /// </summary>
    public class BattleCastSkillEndTargetSPStatusBuffTrigger : BaseBuffTrigger
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args">{skill, target(BattleUnitVO), spStatus}</param>
        /// <returns></returns>
        protected override bool Trigger(object[] args)
        {
            BattleSkillVO skill = args[0] as BattleSkillVO;
            int dmgType = skill.CfgSkillFunctionData.DamageType;
            bool isSubSkill = skill.IsSubSkill;
            bool isGroup = skill.SkillData.GetTargetTypeData().IsGroup;
            int skillType = skill.CfgSkillData.SkillType;
            int elementType = skill.GetElement();

            BattleUnitVO target = args[1] as BattleUnitVO;
            int spStatus = Convert.ToInt32(args[2]);
            if (_triggerParams[0] != -1 && _triggerParams[0] != dmgType)
            {
                if (_triggerParams[0] == 100 && SkillFunction.IsDamage(dmgType))
                {

                }
                else
                {
                    return false;
                }
            }

            if (_triggerParams[1] != -1)
            {
                if (!isSubSkill && _triggerParams[1] == 1)
                {
                    return false;
                }
                if (isSubSkill && _triggerParams[1] == 2)
                {
                    return false;
                }
            }
            if (_triggerParams[2] != -1)
            {
                if (!isGroup && _triggerParams[2] == 1)
                {
                    return false;
                }
                if (isGroup && _triggerParams[2] == 2)
                {
                    return false;
                }
            }

            if (_triggerParams[3] != -1 && _triggerParams[3] != skillType)
            {
                return false;
            }
            if (_triggerParams[4] != -1)
            {
                if (_triggerParams[4] == 999)
                {
                    if (elementType == 5)
                    {
                        return false;
                    }
                }
                else
                {
                    if (_triggerParams[4] != elementType)
                    {
                        return false;
                    }
                }
            }
            if (_triggerParams[5] != -1 && _triggerParams[5] != spStatus){
                return false;
            }
            if (_triggerParams[6] != -1)
            {
                bool hasBuff = false;
                foreach (Buff buff in target.GetBuffManager().GetAllBuffs(true))
                {
                    if (buff.BuffCfg.Id == _triggerParams[6])
                    {
                        hasBuff = true;
                        break;
                    }
                }
                if (!hasBuff)
                {
                    return false;
                }
            }
            if (_triggerParams[7] != -1)
            {
                bool hasBuff = false;
                foreach (Buff buff in target.GetBuffManager().GetAllBuffs(true))
                {
                    if (buff.BuffCfg.Type == _triggerParams[7])
                    {
                        hasBuff = true;
                        break;
                    }
                }
                if (!hasBuff)
                {
                    return false;
                }
            }
            return true;
        }
    }
