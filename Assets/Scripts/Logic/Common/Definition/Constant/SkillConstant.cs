/// <summary>
/// 技能
/// </summary>
public static class SkillConstant
{
    //*****************技能类型******************
    /// <summary>
    /// 类型：普通
    /// </summary>
    public const int SKILL_TYPE_NORMAL = 1;

    /// <summary>
    /// 类型：被动
    /// </summary>
    public const int SKILL_TYPE_PASSIVE = 2;

    /// <summary>
    /// 类型：必杀
    /// </summary>
    public const int SKILL_TYPE_MUSTKILL = 3;

    /// <summary>
    /// 类型：绝杀
    /// </summary>
    public const int SKILL_TYPE_LORE = 4;

    /// <summary>
    /// 类型：职业
    /// </summary>
    public const int SKILL_TYPE_PROFESSION = 5;

    //*****************技能类型******************
    /// <summary>
    /// 类型：普攻
    /// </summary>
    public const int TYPE_NORMAL = 1;

    /// <summary>
    /// 类型：大招
    /// </summary>
    public const int TYPE_ULTIMATE = 2;

    /// <summary>
    /// 类型：被动
    /// </summary>
    public const int TYPE_MASTERY = 3;


    /// <summary>
    /// 类型：追击
    /// </summary>
    public const int TYPE_BATTER = 11;

    /// <summary>
    /// 类型：反击
    /// </summary>
    public const int TYPE_REVOLT = 12;

    ///// <summary>
    ///// 类型：核心
    ///// </summary>
    //public const int TYPE_CORE = 3;
    ///// <summary>
    ///// 类型：核心2
    ///// </summary>
    //public const int TYPE_CORE_2 = 4;

    /// <summary>
    /// 类型：亡语
    /// </summary>
    public const int TYPE_DEADTH_RATTLE = 7;

    /// <summary>
    /// 类型：反击
    /// </summary>
    // public const int TYPE_BEAT_BACK = 8;
    /// <summary>
    /// 类型：衍生技能
    /// </summary>
    public const int TYPE_DERIVATION = 9;

    //*****************技能元素******************

    /// <summary>
    /// 元素：物理
    /// </summary>
    public const int ELEMENT_TYPE_PHYSICS = 0;

    /// <summary>
    /// 元素：风
    /// </summary>
    public const int ELEMENT_TYPE_WIND = 1;

    /// <summary>
    /// 元素：火
    /// </summary>
    public const int ELEMENT_TYPE_FIRE = 2;

    /// <summary>
    /// 元素：雷
    /// </summary>
    public const int ELEMENT_TYPE_THUNDER = 3;

    /// <summary>
    /// 元素：水
    /// </summary>
    public const int ELEMENT_TYPE_WATER = 4;

    /// <summary>
    /// 元素：无
    /// </summary>
    public const int ELEMENT_TYPE_NONE_1 = 11;

    public const int ELEMENT_TYPE_NONE_2 = 12;
    public const int ELEMENT_TYPE_NONE_3 = 13;
    public const int ELEMENT_TYPE_NONE_4 = 14;


    //*****************技能系******************
    //灵魂专属系是递增的id，配置在Soul表里，这里没有列出
    /// <summary>
    /// 1 肉盾系——生存/控制  
    /// </summary>
    public const int SYSTEM_DEFENSE = 1;

    /// <summary>
    /// 2 近战输出——近战/输出系  
    /// </summary>
    public const int SYSTEM_CLOSE_ATTACK = 2;

    /// <summary>
    /// 3 远程物理系——远程/物理输出  
    /// </summary>
    public const int SYSTEM_LONG_DIS_PHYSIC_ATK = 3;

    /// <summary>
    /// 4 远程魔法系——远程/魔法输出  
    /// </summary>
    public const int SYSTEM_LONG_DIS_MAGIC_ATK = 4;

    /// <summary>
    /// 5 诅咒系——虚弱/异常状态  
    /// </summary>
    public const int SYSTEM_CURSE = 5;

    /// <summary>
    /// 6 辅助系——治疗/BUFF  
    /// </summary>
    public const int SYSTEM_SUPPORT = 6;

    /// <summary>
    /// 7 探索1系——探索某方向加成  
    /// </summary>
    public const int SYSTEM_EXPLORE_1 = 7;

    /// <summary>
    /// 8 探索2系——探索某方向加成  
    /// </summary>
    public const int SYSTEM_EXPLORE_2 = 8;

    /// <summary>
    /// 9 探索3系——探索某方向加成  
    /// </summary>
    public const int SYSTEM_EXPLORE_3 = 9;

    /// <summary>
    /// 10 探索4系——探索某方向加成  
    /// </summary>
    public const int SYSTEM_EXPLORE_4 = 10;

    /// <summary>
    /// 普通技能系结束值
    /// </summary>
    public const int COMMON_SKILL_SYSTEM_END = 10;

    //*****************类型：近战还是远程******************
    /// <summary>
    /// 近战技能
    /// </summary>
    public const int RANGE_TYPE_CLOSE = 1;

    /// <summary>
    /// 远程技能
    /// </summary>
    public const int RANGE_TYPE_LONG_DISTANCE = 2;

    //*****************类型：主动还是被动******************
    /// <summary>
    /// 主动技能
    /// </summary>
    public const int RELEASE_TYPE_ACTIVE = 1;

    /// <summary>
    /// 被动技能
    /// </summary>
    public const int RELEASE_TYPE_PASSIVE = 2;

    //*****************技能生效区域******************
    /// <summary>
    /// 战斗中生效
    /// </summary>
    public const int AREA_BATTLE = 1;
}