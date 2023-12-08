using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    /// <summary>
    /// 单位获得特殊状态
    /// </summary>
    public class BattleUnitAddSPStatusBuffTrigger : BaseBuffTrigger
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args">{unitType, spStatus}</param>
        /// <returns></returns>
        protected override bool Trigger(object[] args)
        {
            int unitType = Convert.ToInt32(args[0]);
            int spStatus = Convert.ToInt32(args[1]);
            return _triggerParams[0] == unitType && _triggerParams[1] == spStatus;
        }
    }
