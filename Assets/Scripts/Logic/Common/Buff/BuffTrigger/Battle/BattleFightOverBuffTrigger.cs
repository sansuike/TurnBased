using System;
using IQIGame.Onigao.Logic.Battle;


    /// <summary>
    /// 战斗结束完成触发
    /// </summary>
    public class BattleFightOverBuffTrigger : BaseBuffTrigger
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args">{}</param>
        /// <returns></returns>
        protected override bool Trigger(object[] args)
        {
            int fightResult = Convert.ToInt32(args[0]);
            return _triggerParams[0] == fightResult;
        }
    }
