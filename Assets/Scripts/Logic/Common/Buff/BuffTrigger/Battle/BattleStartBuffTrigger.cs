using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
    /// 战斗开始触发
    /// </summary>
    public class BattleStartBuffTrigger : BaseBuffTrigger
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args">{}</param>
        /// <returns></returns>
        protected override bool Trigger(object[] args)
        {
            return true;
        }
    }
