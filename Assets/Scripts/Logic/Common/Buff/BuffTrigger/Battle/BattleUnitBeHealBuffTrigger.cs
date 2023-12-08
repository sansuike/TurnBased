using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    /// <summary>
    /// 战斗单位受治疗
    /// </summary>
    public class BattleUnitBeHealBuffTrigger : BaseBuffTrigger
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args">{}</param>
        /// <returns></returns>
        protected override bool Trigger(object[] args)
        {
            bool isSkill = (bool) args[0];
            int type = isSkill ? 1 : 0;
            return _triggerParams[0] == -1 || _triggerParams[0] == type;
        }
    }
