
    public static partial class BattleConstant
    {
        public static class UserData
        {
            /// <summary>
            /// 战斗积分
            /// </summary>
            public const string FIGHT_INTEGRAL = "FIGHT_INTEGRAL";

            /// <summary>
            /// 回合数
            /// </summary>
            public const string FIGHT_ROUND = "FIGHT_ROUND";
            
            /// <summary>
            /// 战斗单位元素反应次数
            /// </summary>
            public const string FIGHT_UNIT_ELEMENT_REACTION_NUM = "FIGHT_UNIT_ELEMENT_REACTION_NUM";
        }
        
        /// <summary>
        /// 逻辑帧相关定义
        /// </summary>
        public static class FightResult
        {
            /// <summary>
            /// 未结束
            /// </summary>
            public const int NOT_END = 0;

            /// <summary>
            /// 进攻方胜利
            /// </summary>
            public const int ATTACKER_WIN = 1;

            /// <summary>
            /// 防守方胜利
            /// </summary>
            public const int DEFENDER_WIN = 2;

            /// <summary>
            /// 超时
            /// </summary>
            public const int TIME_OUT = 3;

            /// <summary>
            /// 双方失败
            /// </summary>
            public const int ALL_FAIL = 4;

            /// <summary>
            /// 放弃
            /// </summary>
            public const int GIVE_UP = 5;
        }
    }
