using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// 单位行动结束
/// </summary>
public sealed class FightUnitActionOverOrder : BaseBattleOrder
{
    /// <summary>
    /// 施放者id
    /// </summary>
    public int UnitID;

    public FightUnitActionOverOrder() : base()
    {
    }

    public override void Parse(IDictionary pod)
    {
        UnitID = Convert.ToInt32(pod["UnitID"]);
    }
}