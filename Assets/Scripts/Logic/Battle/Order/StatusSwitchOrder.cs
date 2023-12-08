using System.Collections;
using System;
using System.Collections.Generic;


/// <summary>
/// AI大招状态切换
/// </summary>
public sealed class StatusSwitchOrder : BaseBattleOrder
{
    /// <summary>
    /// 施放者id
    /// </summary>
    public int UnitID;

    /// <summary>
    /// 0 关闭 1 开启
    /// </summary>
    public int Status;

    /// <summary>
    /// 回合数
    /// </summary>
    public int RoundNumber;
    //**********************************************************

    public StatusSwitchOrder() : base()
    {
    }

    public override void Parse(IDictionary pod)
    {
        UnitID = Convert.ToInt32(pod["UnitID"]);
        Status = Convert.ToInt32(pod["Status"]);
        RoundNumber = Convert.ToInt32(pod["RoundNumber"]);
    }
}