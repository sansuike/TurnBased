using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// 战斗单位行动结束
/// </summary>
public class BattleActionEndBuffTrigger : BaseBuffTrigger
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