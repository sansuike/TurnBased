using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  static partial class BattleConstant
{
    /// <summary>
    /// 逻辑帧相关定义
    /// </summary>
    public static class TroopType
    {
        /// <summary>
        /// 攻击队伍
        /// </summary>
        public const int ATTACK = 1;

        /// <summary>
        /// 防御部队
        /// </summary>
        public const int DEFEND = 2;

    }
        
    /// <summary>
    /// 作用域
    /// </summary>
    public static class ScopeType
    {
        /// <summary>
        /// 英雄 | 怪物
        /// </summary>
        public const int MONSTER = 1;
            
        /// <summary>
        /// 地板
        /// </summary>
        public const int TILE = 2;
            
        /// <summary>
        /// 队伍
        /// </summary>
        public const int TEAM = 3;
            
        /// <summary>
        /// 召唤物
        /// </summary>
        public const int CALL = 4;
    }
}
