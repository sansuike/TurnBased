using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager
{
    /// <summary>
    /// 帧率
    /// </summary>
    public int FrameRate { get; private set; }
    /// <summary>
    /// 随机类
    /// </summary>
    public LogicStableRandom Random { get; private set; }
    /// <summary>
    /// 当前帧
    /// </summary>
    public int CurrFrame { get; private set; }
    /// <summary>
    /// 所有Buff
    /// </summary>
    private List<Buff> _allBuffs = new List<Buff>();
    /// <summary>
    /// 持有者
    /// </summary>
    private IBuffHolder _holder;

    // 战斗
    public TurnBaseLogicFight Fight;
    
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="frameRate">帧率</param>
    /// <param name="randomSeed">随机种子</param>
    /// <param name="holder">持有者</param>

    public void Init(TurnBaseLogicFight fight,int frameRate, int randomSeed, IBuffHolder holder)
    {
        FrameRate = frameRate;
        Random = new LogicStableRandom(randomSeed);
        _holder = holder;
        Fight = fight;
    }
    
    /// <summary>
    ///  持有者
    /// </summary>
    internal IBuffHolder Holder
    {
        get
        {
            return _holder;
        }
    }
    
    /// <summary>
    /// 获取buff
    /// </summary>
    /// <returns></returns>
    public List<Buff> GetAllBuffs(bool safely = false)
    {
        if (safely)
        {
            return _allBuffs;
        }
        else
        {
            return new List<Buff>(_allBuffs);
        }
    }
    
    /// <summary>
    /// 触发移除
    /// </summary>
    /// <param name="triggerType"></param>
    public void TriggerRemove(int triggerType)
    {
        // foreach (Buff buff in GetAllBuffs())
        // {
        //     if (Array.IndexOf(buff.BuffCfg.RemoveTrigger, triggerType) != -1)
        //     {
        //         RemoveBuff(buff, BuffRemoveReason.CLEAN);
        //     }
        // }
                
    }
    
    /// <summary>
    /// buff触发
    /// </summary>
    /// <param name="buff"></param>
    /// <param name="triggerType"></param>
    /// <param name="args"></param>
    public void TriggerBuff(Buff buff, Type triggerType, params object[] args)
    {
        //buff.OnTrigger(triggerType, args);
    }
    
    /// <summary>
    /// buff触发
    /// </summary>
    /// <param name="triggerType"></param>
    /// <param name="args"></param>
    public void TriggerBuff(Type triggerType, params object[] args)
    {
        // 代表是人触发的，对应触发他的地板BUFF
        // if (_holder.GetScope() == 0)
        // {
        //     BattleUnitVO battleUnitVo = _holder as BattleUnitVO;
        //     if (battleUnitVo.Tile != null && (triggerType != typeof(BattleUnitTerrainChangeTrigger)))
        //     {
        //         battleUnitVo.Tile.GetBuffManager().TriggerBuff(triggerType, args);
        //     }
        // }
        //     
        // if (_allBuffs.Count > 0)
        // {
        //     foreach (Buff buff in GetAllBuffs())
        //     {
        //         try
        //         {
        //             buff.OnTrigger(triggerType, args);
        //         }
        //         catch(Exception e)
        //         {
        //             _logger.Error("TriggerBuff error buffCid = {0}, \n source = {1}", buff.BuffCfg.Id, e.StackTrace);
        //             throw e;
        //         }
        //     }
        // }
    }
    
    /// <summary>
    /// 移除buff
    /// </summary>
    /// <param name="buffCid"></param>
    /// <param name="targetCid"></param>
    /// <param name="removeReason"></param>
    // public void RemoveBuff(int buffCid, int targetCid, BuffRemoveReason removeReason = BuffRemoveReason.CLEAN)
    // {
    //     if (buffCid == 0)
    //     {
    //         return;
    //     }
    //     foreach (IBuffHolder holder in _holder.GetBuffTarget(null, targetCid))
    //     {
    //         holder.GetBuffManager().RemoveBuff(buffCid, removeReason);
    //     }
    // }
}
