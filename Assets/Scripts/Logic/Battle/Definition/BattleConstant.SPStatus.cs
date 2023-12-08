using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    public static partial class BattleConstant
    {
        /// <summary>
        /// 特殊状态
        /// </summary>
        public static class SPStatus
        {
            /// <summary>
            /// 无
            /// </summary>
            public const int NONE = 0;
            /// <summary>
            /// 大破
            /// </summary>
            public const int BIG_BROKEN = 1;
            /// <summary>
            /// 小破
            /// </summary>
            public const int SMALL_BROKEN = 2;
            /// <summary>
            /// 死亡
            /// </summary>
            public const int DEAD = 3;
            /// <summary>
            /// 行动
            /// </summary>
            public const int ACTION = 4;
            /// <summary>
            /// 特殊状态总数量
            /// </summary>
            public const int SPSTATUS_NUM = 5;
        }
    }
