using IQIGame.Onigao.Config;
using System;


    /// <summary>
    /// 战斗单位免疫buff触发
    /// </summary>
    public class BattleUnitBuffImmuneBuffTrigger : BaseBuffTrigger
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args">{buffCid}</param>
        /// <returns></returns>
        protected override bool Trigger(object[] args)
        {
            int buffCid = Convert.ToInt32(args[0]);
            CfgBuffData cfgBuffData = CfgBuffTable.Instance.GetDataByID(buffCid);
            return BuffUtil.IsMatch(cfgBuffData, _triggerParams[0], _triggerParams[1]);
        }
    }
