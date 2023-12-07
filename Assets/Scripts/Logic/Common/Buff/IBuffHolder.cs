using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Buff持有者接口
/// </summary>
public interface IBuffHolder
{
    /// <summary>
    /// 获取buff管理器
    /// </summary>
    /// <returns></returns>
    BuffManager GetBuffManager();

    /// <summary>
    /// 获取施放buff的目标
    /// </summary>
    /// <param name="caster">buff</param>
    /// <param name="searchTargetCid">SearchTarget配置id</param>
    /// <returns></returns>
    //IEnumerable<IBuffHolder> GetBuffTarget(Buff buff, int searchTargetCid);

    /// <summary>
    /// 新增buff监听
    /// </summary>
    /// <param name="buff"></param>
    void OnBuffAdd(BattleUnitVO notifyUnit, Buff buff);

    /// <summary>
    /// buff持续时间变化
    /// </summary>
    /// <param name="buff"></param>
    /// <param name="reason">原因1=Manager.Update,2=Refresh,3=EffectChange</param>
    void OnBuffUpdate(Buff buff, int reason);

    /// <summary>
    /// buff被免疫
    /// </summary>
    /// <param name="buffCid"></param>
    void OnBuffImmune(int buffCid);

    /// <summary>
    /// buff叠加监听
    /// </summary>
    /// <param name="buff"></param>
    /// <param name="oldStackCount">增加的叠加次数</param>
    void OnBuffStack(BattleUnitVO notifyUnit, Buff buff, int oldStackCount);

    /// <summary>
    /// buff移除监听
    /// </summary>
    /// <param name="buff"></param>
    /// <param name="removeReason"></param>
    void OnBuffRemove(BattleUnitVO notifyUnit, Buff buff, BuffRemoveReason removeReason);

    /// <summary>
    /// buff触发监听
    /// </summary>
    /// <param name="buff"></param>
    /// <returns></returns>
    void OnBuffTrigger(Buff buff);

    /// <summary>
    /// buff触发效果
    /// </summary>
    /// <param name="buff"></param>
    /// <param name="buffEffect"></param>
    /// <param name="triggerType"></param>
    /// <param name="triggerArgs"></param>
    /// <returns></returns>
    void OnBuffTriggerEffect(Buff buff, BuffEffect buffEffect, Type triggerType, object[] triggerArgs);

    /// <summary>
    /// 检查触发条件
    /// </summary>
    /// <param name="buff"></param>
    /// <returns></returns>
    bool CheckTriggerCondition(Buff buff);

    /// <summary>
    ///  是否死亡
    /// </summary>
    /// <returns></returns>
    bool IsDead();

    /// <summary>
    /// 取得阵营
    /// </summary>
    /// <returns></returns>
    int getTroopType();

    /// <summary>
    /// 取得作用域
    /// </summary>
    /// <returns></returns>
    int GetScope();

    TurnBaseLogicFight GetFight();
}