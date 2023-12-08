using IQIGame.Onigao.Logic.Battle;
using System;


    /// <summary>
    /// 攻击前目标buff
    /// </summary>
    public class BattlePreAtkTargetBuffBuffTrigger : BaseBuffTrigger
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args">{BattleUnitVO}</param>
        /// <returns></returns>
        protected override bool Trigger(object[] args)
        {
            BattleSkillVO skill = args[0] as BattleSkillVO;
            BattleUnitVO unit = args[1] as BattleUnitVO;

            int dmgType = skill.CfgSkillFunctionData.DamageType;
            bool isSubSkill = skill.IsSubSkill;
            bool isGroup = skill.SkillData.GetTargetTypeData().IsGroup;
            int skillType = skill.CfgSkillData.SkillType;
            int elementType = skill.GetElement();
            if (elementType == SkillConstant.ELEMENT_TYPE_NONE_1 ||
                elementType == SkillConstant.ELEMENT_TYPE_NONE_2 ||
                elementType == SkillConstant.ELEMENT_TYPE_NONE_3 ||
                elementType == SkillConstant.ELEMENT_TYPE_NONE_4)
            {
                //return false;
            }

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

            foreach(Buff buff in unit.GetBuffManager().GetAllBuffs(true))
            {
                if(BuffUtil.IsMatch(buff.BuffCfg, _triggerParams[5], _triggerParams[6]))
                {
                    return true;
                }
            }
            return false;
        }
    }
