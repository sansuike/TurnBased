using IQIGame.Onigao.Config;
using System;


    /// <summary>
    /// 驱散他人BUFF触发
    /// </summary>
    public class BattleUnitDispelBuffTrigger : BaseBuffTrigger
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args">{}</param>
        /// <returns></returns>
        protected override bool Trigger(object[] args)
        {

            CfgBuffData buffData = args[0] as CfgBuffData;
            if (!BuffUtil.IsMatch(buffData, _triggerParams[0], _triggerParams[1]))
            {
                return false;
            }
            return true;
        }
    }
