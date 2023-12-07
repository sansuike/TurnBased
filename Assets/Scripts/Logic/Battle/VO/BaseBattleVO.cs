using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBattleVO 
{
    /// <summary>
    /// 唯一id
    /// </summary>
    public int ID { get; private set; }
    /// <summary>
    /// 当前战斗
    /// </summary>
    public BaseLogicFight Fight { get; private set; }
    //**************************************************

    internal BaseBattleVO(BaseLogicFight fight)
    {
        Fight = fight;
        //ID = Fight.AllocID();
    }

    public override int GetHashCode()
    {
        return ID.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }
        if (!GetType().Equals(obj.GetType()))
        {
            return false;
        }
        return ID.Equals(((BaseBattleVO)obj).ID);
    }
}
