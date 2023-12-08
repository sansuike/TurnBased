using System;


    /// <summary>
    /// 地形改变触发
    /// </summary>
    public class BattleUnitTerrainChangeTrigger : BaseBuffTrigger
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args">{}</param>
        /// <returns></returns>
        protected override bool Trigger(object[] args)
        {
            int type = Convert.ToInt32(args[0]);
            int cid = Convert.ToInt32(args[1]);
            if (_triggerParams[0] != -1 && _triggerParams[0] != type)
            {
                return false;
            }
            if (_triggerParams[1] != -1 && _triggerParams[1] != cid)
            {
                return false;
            }
            return true;
        }
    }
