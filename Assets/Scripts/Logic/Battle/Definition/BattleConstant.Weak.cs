using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQIGame.Onigao.Logic.Battle
{
    public static partial class BattleConstant
    {
        /// <summary>
        /// 战斗状态
        /// </summary>
        public static class WeakStatus
        {
            /** 显示 */
            public const int SHOW = 1;
            
            /** 显示 */
            public const int HIDE = 2;
            
            /** 锁定 */
            public const int LOCK = 3;
        }
        
        
        /// <summary>
        /// 战斗状态
        /// </summary>
        public static class WeakType
        {
            /** 元素伤害 */
            public const int ELEMENT_ATTACK = 1;
            
            /** 元素反应 */
            public const int ELEMENT_REACTION = 2;
            
            /** 武器类型 */
            public const int ARMS_TYPE = 3;
        }
    }
}
