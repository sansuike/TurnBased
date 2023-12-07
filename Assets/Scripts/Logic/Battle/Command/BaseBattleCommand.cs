using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 战斗指令基类
/// </summary>
public abstract class BaseBattleCommand
{
    /// <summary>
    /// 当前帧
    /// </summary>
    public int Frame;

    //*************************************************************

    public BaseBattleCommand()
    {

    }

    /// <summary>
    /// 转为IDictionary对象，提供给lua使用
    /// </summary>
    /// <returns></returns>
    public abstract IDictionary ToDic();
}
