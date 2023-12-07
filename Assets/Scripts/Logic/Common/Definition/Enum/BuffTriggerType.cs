
    /// <summary>
    /// BUFF触发器类型
    /// </summary>
    public enum BuffTriggerType
    {
        /// <summary>
        /// 时间触发
        /// </summary>
        TimeTrigger = 101,
        /// <summary>
        /// 指定BUFF移除时触发
        /// </summary>
        BeRemoved = 102,
        /// <summary>
        /// 指定BUFF获得时触发
        /// </summary>
        AddBuff = 103,
        /// <summary>
        /// 指定BUFF叠加时触发
        /// </summary>
        StackBuff = 104,
        

       

        /// <summary>
        /// 回合开始事件
        /// </summary>
        BattleRoundStart = 301,
        /// <summary>
        /// 回合结束事件
        /// </summary>
        BattleRoundEnd = 302,
        /// <summary>
        /// 行动开始事件
        /// </summary>
        BattleActionStart = 303,
        /// <summary>
        /// 行动结束事件
        /// </summary>
        BattleActionEnd = 304,
        /// <summary>
        /// 施放技能前
        /// </summary>
        BattleCastSkillStart = 305,
        /// <summary>
        /// 施放技能后伤害效果
        /// </summary>
        BattleCastSkillEndResult = 306,
        /// <summary>
        /// 施放技能后目标状态
        /// </summary>
        BattleCastSkillEndTargetStatus = 307,
        /// <summary>
        /// 施放技能后目标特殊状态
        /// </summary>
        BattleCastSkillEndTargetSPStatus = 308,
        /// <summary>
        /// 受攻击后伤害效果
        /// </summary>
        BattleBeAtkResult = 309,
        /// <summary>
        /// 自己获得状态
        /// </summary>
        BattleSelfAddStatus = 310,
        /// <summary>
        /// 单位获得特殊状态
        /// </summary>
        BattleUnitAddSPStatus = 311,
        /// <summary>
        /// 战斗开始触发
        /// </summary>
        BattleStart = 312,
        /// <summary>
        /// 战斗单位受伤
        /// </summary>
        BattleUnitBeHurt = 313,
        /// <summary>
        /// 战斗技能后
        /// </summary>
        BattleCastSkillEnd = 314,
        /// <summary>
        /// 单位属性变化
        /// </summary>
        BattleUnitAttrChange = 315,
        /// <summary>
        /// 战斗单位受治疗
        /// </summary>
        BattleUnitBeHeal = 316,
        /// <summary>
        /// 攻击前目标buff
        /// </summary>
        BattlePreAtkTargetBuff = 317,
        /// <summary>
        /// 攻击前击中效果
        /// </summary>
        BattlePreAtkHit = 318,
        /// <summary>
        /// 死亡前触发
        /// </summary>
        BattleUnitBeforeDead = 319,
        /// <summary>
        /// Dot生效后
        /// </summary>
        BattleUnitDotEffect = 320,
        /// <summary>
        /// Buff被免疫后
        /// </summary>
        BattleUnitBuffImmune = 321,
        /// <summary>
        /// 被攻击前击中效果
        /// </summary>
        BePreAtkHit = 322,
        /// <summary>
        /// 攻击后伤害效果
        /// </summary>
        BattleAfterAtkHit= 323,
        /// <summary>
        /// 被攻击前
        /// </summary>
        BattlePreBeAtkBuff = 324,
        /// <summary>
        /// 死亡后触发
        /// </summary>
        BattleUnitAfterDead = 325,
        /// <summary>
        /// 战斗初始化完成触发
        /// </summary>
        BattleInitComplete = 326,
        /// <summary>
        /// 弱点被击破触发
        /// </summary>
        BattleUnitWeakBeBreak = 327,
        /// <summary>
        /// 弱点恢复触发
        /// </summary>
        BattleUnitWeakRecover = 328,
        /// <summary>
        /// 技能组释放完毕触发
        /// </summary>
        BattleUnitSkillGroupVoer = 329,
        /// <summary>
        /// 战斗单位死亡触发
        /// </summary>
        BattleUnitDeadBuffTrigger = 330,
        /// <summary>
        /// 驱散BUFF触发
        /// </summary>
        BattleUnitDispelBuffTrigger = 331,
        
        /// <summary>
        /// 受技能组释放完毕触发
        /// </summary>
        BattleBeUnitSkillGroupVoer = 332,
        
        /// <summary>
        /// 休息时触发
        /// </summary>
        BattleUnitWaitBuffTrigger = 333,
        
        /// <summary>
        /// 护盾移除触发
        /// </summary>
        BattleUnitShieldRemoveTrigger = 334,
        
        /// <summary>
        /// 元素反应触发
        /// </summary>
        BattleUnitElementReactionBuffTrigger = 335,
        
        /// <summary>
        /// 护盾改变触发
        /// </summary>
        BattleUnitShieldChangeBuffTrigger = 336,
       
        /// <summary>
        /// 地形改变触发,参数：类型(1：获得 0：失去),地形ID
        /// </summary>
        BattleUnitTerrainChangeTrigger = 337,
        
        /// <summary>
        /// 弱点层数改变触发
        /// </summary>
        BattleUnitWeakChangeTrigger = 338,
        
        /// <summary>
        /// 指定BUFF触发时触发
        /// </summary>
        BattleUnitTriggerBuffTrigger = 339,
        
        /// <summary>
        /// 行动开始事件(全体)
        /// </summary>
        BattleActionStart2 = 340,
        /// <summary>
        /// 行动结束事件(全体)
        /// </summary>
        BattleActionEnd2 = 341,
        
        /// <summary>
        /// 战斗结束触发
        /// </summary>
        BattleFightOverBuffTrigger = 342,
        
        /// <summary>
        /// 单位移动触发
        /// </summary>
        BattleUnitMoveBuffTrigger = 343,
    }

