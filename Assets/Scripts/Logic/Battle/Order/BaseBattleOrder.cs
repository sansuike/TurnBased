using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBattleOrder
{
    /// <summary>
    /// 所在帧
    /// </summary>
    public int Frame;
    
    /// <summary>
    /// 玩家ID
    /// </summary>
    public string Pid;

    //***************************************************

    public BaseBattleOrder()
    {
            
    }

    /// <summary>
    /// 解析POD对象
    /// </summary>
    /// <param name="pod"></param>
    public abstract void Parse(IDictionary pod);
}
