using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff 
{
    /// <summary>
    /// 施放者
    /// </summary>
    internal IBuffHolder Caster { get; set; }
    /// <summary>
    /// 原始技能
    /// </summary>
    private SkillData _skillData { get; set; }
    /// <summary>
    /// 子Buff
    /// </summary>
    internal List<Buff> Children { get; private set; }
    /// <summary>
    /// buff配置
    /// </summary>
    //public CfgBuffData BuffCfg { get; private set; }
    /// <summary>
    /// buff管理器
    /// </summary>
    internal BuffManager BuffManager { get; private set; }
    /// <summary>
    /// 触发器
    /// </summary>
    //private BaseBuffTrigger _trigger { get; set; }
    /// <summary>
    /// 剩余时间
    /// </summary>
    public int LeftTime { get; set; }
    /// <summary>
    /// 叠加次数
    /// </summary>
    public int StackCount { get; private set; }
    /// <summary>
    /// 最大叠加次数
    /// </summary>
    public int MaxStackCount { get; set; }
    /// <summary>
    /// 效果列表
    /// </summary>
    public List<BuffEffect> Effects { get; private set; }
    /// <summary>
    /// 是否被移除
    /// </summary>
    public bool IsRemoved { get; set; }
    /// <summary>
    /// buff初始持续时间
    /// </summary>
    public int InitBuffTime { get; private set; }

    /// <summary>
    /// 触发几率失败次数
    /// </summary>
    public int TriggerProFailCount { get; set; }
    
    public SkillData SkillData
    {
        get
        {
            return _skillData;
        }
    }

    /// <summary>
    /// buff持有者
    /// </summary>
    public IBuffHolder Holder
    {
        get
        {
            return BuffManager.Holder;
        }
    }
}
