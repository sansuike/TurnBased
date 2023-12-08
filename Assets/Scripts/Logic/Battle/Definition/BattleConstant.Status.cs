using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static partial class BattleConstant
{
    /// <summary>
    /// 战斗状态
    /// </summary>
    public static class Status
    {
        /** 无 */
        public const int NONE = 0;

        /** 封印普攻 */
        public const int SEAL_NORMAL = 1;

        /** 封印核心1 */
        public const int SEAL_CORE = 2;

        /** 封印核心2 */
        public const int SEAL_CORE_2 = 3;

        /** 封印大招 */
        public const int SEAL_ULTIMATE = 4;

        /** 封印亡语 */
        public const int SEAL_DEAD_RATTLE = 5;

        /** 封印反击 */
        public const int SEAL_BEAT_BACK = 6;

        /** 封印待机 */
        public const int SEAL_STAND_BY = 7;

        /** 封印衍生 */
        public const int SEAL_DERIVATION = 8;

        /** 隐身 */
        public const int STEALTH = 9;

        /** CD停止 */
        public const int CD_STOP = 10;

        /** 链接 */
        public const int DMG_LINK = 11;

        /** 制怒 */
        public const int ENERGY_LIMIT = 12;

        /** 必定躲闪 */
        public const int MUST_DODGE = 13;

        /** 必定格挡 */
        public const int MUST_BLOCK = 14;

        /** 必定命中 */
        public const int MUST_HIT = 15;

        /** 必定暴击 */
        public const int MUST_CRIT = 16;

        /** 无法躲闪 */
        public const int CAN_NOT_DODGE = 17;

        /** 无法格挡 */
        public const int CAN_NOT_BLOCK = 18;

        /** 无法命中 */
        public const int CAN_NOT_HIT = 19;

        /** 无法暴击 */
        public const int CAN_NOT_CRIT = 20;


        ///** 火元素免疫 */
        //public const int IMMUNITY_FIRE = 22;
        ///** 雷元素免疫 */
        //public const int IMMUNITY_THUNDER = 23;
        ///** 水元素免疫 */
        //public const int IMMUNITY_WATER = 24;
        ///** 风元素免疫 */
        //public const int IMMUNITY_WIND = 25;


        /** 嘲讽 */
        public const int TAUNT = 27;

        /** 假死 */
        public const int PLAY_DEAD = 28;

        /** AP消耗限制 */
        public const int AP_LIMIT = 29;

        /** 混乱 */
        public const int CHAOS = 30;

        /** 魅惑 */
        public const int CHARM = 31;

        /** 禁止治疗 */
        public const int SEAL_HEAL = 32;

        /** 晕眩 */
        public const int DIZZY = 33;

        /** 睡眠 */
        public const int SLEEP = 34;

        /** 霸体 */
        public const int TYRANTS = 35;

        /** 禁止移动 */
        public const int MOVE = 36;

        /// <summary>
        /// 状态总数量
        /// </summary>
        public const int STATUS_NUM = 37;
    }
}