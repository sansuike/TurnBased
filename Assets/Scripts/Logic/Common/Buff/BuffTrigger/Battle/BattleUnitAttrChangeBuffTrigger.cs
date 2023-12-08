using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    /// <summary>
    /// 战斗属性变化
    /// </summary>
    public class BattleUnitAttrChangeBuffTrigger : BaseBuffTrigger
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args">{type}</param>
        /// <returns></returns>
        protected override bool Trigger(object[] args)
        {
            return _triggerParams[0] == Convert.ToInt32(args[0]);
        }
    }
