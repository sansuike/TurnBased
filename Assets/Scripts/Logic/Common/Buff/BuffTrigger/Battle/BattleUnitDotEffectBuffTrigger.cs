using IQIGame.Onigao.Config;
using System;


    /// <summary>
    /// dot生效后
    /// </summary>
    public class BattleUnitDotEffectBuffTrigger : BaseBuffTrigger
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args">{buffCfg}</param>
        /// <returns></returns>
        protected override bool Trigger(object[] args)
        {
            CfgBuffData buffCfg = args[0] as CfgBuffData;
            if (_triggerParams[0] != -1&& _triggerParams[0] != buffCfg.Id)
            {
                return false;
            }
            if (_triggerParams[1] != -1 && _triggerParams[1] != buffCfg.GroupID)
            {
                return false;
            }
            if (_triggerParams[2] != -1 && _triggerParams[2] != buffCfg.Type)
            {
                return false;
            }
            if (_triggerParams[3] != -1 && _triggerParams[3] != buffCfg.DebuffType)
            {
                return false;
            }
            return true;
        }
    }
