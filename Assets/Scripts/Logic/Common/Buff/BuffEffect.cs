using System.Collections;
using System.Collections.Generic;
using FixMath.NET;
using UnityEngine;

public class BuffEffect
{
    /// <summary>
    /// 效果类型
    /// </summary>
    public int EffectType;
    /// <summary>
    /// 效果参数
    /// </summary>
    public string[] EffectParams;
    /// <summary>
    /// 每层的叠加者,每个元素代表一个叠加
    /// </summary>
    public Queue<int> StackCasters;
    /// <summary>
    /// 效果值
    /// </summary>
    public Fix64 EffectValue;

    public BuffEffect(int effectType, string[] effectParams)
    {
        EffectType = effectType;
        EffectParams = effectParams;
    }
}
