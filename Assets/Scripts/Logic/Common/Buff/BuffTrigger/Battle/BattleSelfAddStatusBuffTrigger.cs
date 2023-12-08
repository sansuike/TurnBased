using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    /// <summary>
    /// 自己获得状态
    /// </summary>
    public class BattleSelfAddStatusBuffTrigger : BaseBuffTrigger
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args">{status}</param>
        /// <returns></returns>
        protected override bool Trigger(object[] args)
        {
            int status = Convert.ToInt32(args[0]);
            return _triggerParams[0] == status;
        }
    }
