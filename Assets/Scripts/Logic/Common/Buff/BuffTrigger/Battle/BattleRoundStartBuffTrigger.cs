using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    /// <summary>
    /// 战斗回合开始
    /// </summary>
    public class BattleRoundStartBuffTrigger : BaseBuffTrigger
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args">{round}</param>
        /// <returns></returns>
        protected override bool Trigger(object[] args)
        {
            int round = Convert.ToInt32(args[0]);
            // -1：任意 0：大回合 1：我方回合 2：敌方回合
            int type = Convert.ToInt32(args[1]);
            bool match = round % _triggerParams[0] == 0;

            if (match && _triggerParams.Length > 1)
            {
                match = _triggerParams[1] == -1 || _triggerParams[1] == type;
            }
            return match;
        }
    }
