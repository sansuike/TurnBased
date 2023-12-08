using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    /// <summary>
    /// 战斗单位行动开始
    /// </summary>
    public class BattleActionStartBuffTrigger : BaseBuffTrigger
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args">{round}</param>
        /// <returns></returns>
        protected override bool Trigger(object[] args)
        {
            int round = Convert.ToInt32(args[0]);
            return round % _triggerParams[0] == 0;
        }
    }
