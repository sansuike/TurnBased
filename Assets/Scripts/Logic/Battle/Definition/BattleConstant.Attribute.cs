using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public static partial class BattleConstant
{
    /// <summary>
    /// 战斗属性
    /// </summary>
    public static class Attribute
    {
        public const int TYPE_NONE = 0;

        // ------------------------------ 1级属性(1-20) ------------------------------

        /** 生命 */
        public const int TYPE_HP = 1;

        /** 攻击 */
        public const int TYPE_ATTACK = 2;

        /** 防御 */
        public const int TYPE_DEFENSE = 3;

        /** 速度 */
        public const int TYPE_SPEED = 4;

        /** 命中 */
        public const int TYPE_HIT = 5;

        /** 闪避 */
        public const int TYPE_DODGE = 6;

        /** 怒气 */
        public const int TYPE_ENERGY = 7;

        /** 最大生命值 */
        public const int TYPE_HP_MAX = 8;

        /** COST值 */
        public const int TYPE_COST = 9;

        /** AP值 */
        public const int TYPE_AP = 10;

        /** 吸血 */
        public const int TYPE_SUCK_BLOOD = 11;

        /** 反伤 */
        public const int TYPE_THORNS = 12;

        /** 支援 */
        public const int TYPE_HELP = 13;

        /** 最大怒气 */
        public const int TYPE_ENERGY_MAX = 14;

        /** 怒气 */
        public const int TYPE_ENERGY_RECOVER = 15;

        /** 防御穿透 */
        public const int TYPE_DEFENSE_PENETRATION = 16;

        /** 生命成长 */
        public const int TYPE_LIFE_GROWING = 17;

        /** 攻击成长 */
        public const int TYPE_ATTACK_GROWING = 18;

        /** 防御成长 */
        public const int TYPE_DEFENSE_GROWING = 19;

        /** 仇恨值 */
        public const int TYPE_HATRED = 20;

        // ------------------------------ 2级属性(21-40   41-60) ------------------------------

        /** 生命百分比 */
        public const int TYPE_LIFE_PERCENT = 21;

        /** 攻击百分比 */
        public const int TYPE_ATTACK_PERCENT = 22;

        /** 防御百分比 */
        public const int TYPE_DEFENSE_PERCENT = 23;

        /** 速度百分比 */
        public const int TYPE_SPEED_PERCENT = 24;

        /** 怒气百分比(恢复) */
        public const int TYPE_ENERGY_PERCENT = 25;


        /** 额外固定生命值 */
        public const int TYPE_EXTRA_LIFE = 41;

        /** 额外固定攻击 */
        public const int TYPE_EXTRA_ATTACK = 42;

        /** 额外固定防御 */
        public const int TYPE_EXTRA_DEFENSE = 43;

        /** 额外固定速度 */
        public const int TYPE_EXTRA_SPEED = 44;


        // ------------------------------ 3级属性(61-100) ------------------------------
        /** 暴击率 */
        public const int TYPE_POSITIVE_CRIT = 61;

        /** 抗爆率 */
        public const int TYPE_NEGATIVE_CRIT = 62;

        // --------------------------------------------------------------------

        /** 元素精通 */
        public const int TYPE_POSITIVE_ELEMENT = 63;

        /** 元素抗性 */
        public const int TYPE_NEGATIVE_ELEMENT = 64;

        // --------------------------------------------------------------------

        /** 物理精通 */
        public const int TYPE_POSITIVE_PHYSICS = 65;

        /** 物理抗性 */
        public const int TYPE_NEGATIVE_PHYSICS = 66;

        // --------------------------------------------------------------------

        /** 风-精通 */
        public const int TYPE_POSITIVE_WIND = 67;

        /** 风-抗性 */
        public const int TYPE_NEGATIVE_WIND = 68;

        // --------------------------------------------------------------------

        /** 火-精通 */
        public const int TYPE_POSITIVE_FIRE = 69;

        /** 火-抗性 */
        public const int TYPE_NEGATIVE_FIRE = 70;

        // --------------------------------------------------------------------

        /** 雷-精通 */
        public const int TYPE_POSITIVE_MINE = 71;

        /** 雷-抗性 */
        public const int TYPE_NEGATIVE_MINE = 72;

        // --------------------------------------------------------------------

        /** 水-精通 */
        public const int TYPE_POSITIVE_WATER = 73;

        /** 水-抗性 */
        public const int TYPE_NEGATIVE_WATER = 74;

        // --------------------------------------------------------------------

        /** 释放护盾效果修正 */
        public const int TYPE_POSITIVE_SHIELD = 75;

        /** 受护盾效果修正 */
        public const int TYPE_NEGATIVE_SHIELD = 76;

        // --------------------------------------------------------------------

        /** 治疗效果修正 */
        public const int TYPE_HEALED_CHANGE = 77;

        /** 受治疗效果修正 */
        public const int TYPE_BE_HEALED_CHANGE = 78;

        // --------------------------------------------------------------------

        /** 最终伤害修正 */
        public const int TYPE_FINAL_HURT_CHANGE = 79;

        /** 最终受伤修正 */
        public const int TYPE_FINAL_BE_HURT_CHANGE = 80;

        // --------------------------------------------------------------------

        /** 暴击伤害 */
        public const int TYPE_POSITIVE_CRIT_HURT = 81;

        /** 暴击修正 */
        public const int TYPE_NEGATIVE_CRIT_HURT = 82;

        /** 元素精通修正 */
        public const int TYPE_POSITIVE_ALL = 83;

        /** 元素精通抗性 */
        public const int TYPE_NEGATIVE_ALL = 84;


        // ******************************** 战斗属性(101) ********************************
        /** 当前回合受伤 */
        public const int TYPE_CURR_BE_CURR_HURT = 101;

        /** 当前主技能受伤 */
        public const int TYPE_CURR_BE_MAIN_SKILL_HURT = 102;

        /** 当前技能受伤 */
        public const int TYPE_CURR_BE_SKILL_HURT = 103;

        /** 当前战斗受伤 */
        public const int TYPE_CURR_BE_FIGHT_HURT = 104;

        /** 上次治疗量 */
        public const int TYPE_LAST_HEAL = 105;


        /** 上次伤害 */
        public const int TYPE_LAST_HURT = 106;

        /** 当前回合怒气改变值 */
        public const int TYPE_CURRENT_ROUND_CHANGE_SKILL_ENERGY = 107;

        /** 触发的元素反应 */
        public const int TYPE_TRIGGER_ELEMENT_REACTION = 108;

        /** 上次治疗量(算溢出) */
        public const int TYPE_LAST_HEAL_2 = 109;

        /** 总属性条目数=最后一个属性加一 */
        public const int ATTRIBUTE_NUM = 110;
    }
}