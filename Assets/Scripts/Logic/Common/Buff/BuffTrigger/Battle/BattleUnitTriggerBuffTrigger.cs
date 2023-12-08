using System;


    /// <summary>
    /// 触发指定BUFF时触发
    /// </summary>
    public class BattleUnitTriggerBuffTrigger : BaseBuffTrigger
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args">{}</param>
        /// <returns></returns>
        protected override bool Trigger(object[] args)
        {
            // CfgBuffData buffData = args[0] as CfgBuffData;
            // if (_buff.BuffCfg.Id == buffData.Id)
            // {
                return false;
            //}

            //return BuffUtil.IsMatch(buffData, _triggerParams[0], _triggerParams[1]);
        }
    }
