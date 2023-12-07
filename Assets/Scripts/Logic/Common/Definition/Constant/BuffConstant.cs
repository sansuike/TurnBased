using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// buff常量
/// </summary>
public static class BuffConstant
{
    /// <summary>
    /// buff组关系-无
    /// </summary>
    public const int BUFF_GROUP_RELATION_NONE = 0;

    /// <summary>
    /// buff组关系-覆盖
    /// </summary>
    public const int BUFF_GROUP_RELATION_OVERRIDE = 1;

    /// <summary>
    /// buff组关系-免疫
    /// </summary>
    public const int BUFF_GROUP_RELATION_IMMUNE = 2;

    /************************************************************************************************/

    /// <summary>
    /// 叠加类型：无
    /// </summary>
    public const int BUFF_STACK_TYPE_NONE = 0;

    /// <summary>
    /// 叠加类型：叠加效果
    /// </summary>
    public const int BUFF_STACK_TYPE_EFFECT = 3;

    /// <summary>
    /// 叠加类型：覆盖
    /// </summary>
    public const int BUFF_STACK_TYPE_OVRERIDE = 4;

    /// <summary>
    /// 叠加类型：按回合大小覆盖
    /// </summary>
    public const int BUFF_STACK_TYPE_ROUND_OVRERIDE = 5;

    /************************************************************************************************/
    /// <summary>
    /// buff特性-免疫驱散
    /// </summary>
    public const int BUFF_PROPERTIES_IMMUNE_DISPEL = 1;

    /************************************************************************************************/

    /// <summary>
    /// buff移除触发类型-单位死亡
    /// </summary>
    public const int BUFF_REMOVE_TRIGGER_TYPE_DEAD = 101;

    /// <summary>
    /// buff移除触发类型-离开区域
    /// </summary>
    public const int BUFF_REMOVE_TRIGGER_TYPE_LEAVE_AREA = 201;

    /// <summary>
    /// buff移除触发类型-施放技能结束
    /// </summary>
    public const int BUFF_REMOVE_TRIGGER_TYPE_CAST_SKILL_OVER = 301;

    /// <summary>
    /// buff移除触发类型-单位技能组施放结束
    /// </summary>
    public const int BUFF_REMOVE_TRIGGER_TYPE_CAST_ALL_SKILL_OVER = 302;

    /// <summary>
    /// buff移除触发类型-攻击目标后
    /// </summary>
    public const int BUFF_REMOVE_TRIGGER_TYPE_AFTER_ATK_TARGET = 303;

    /// <summary>
    /// buff移除触发类型-元素反应移除
    /// </summary>
    public const int BUFF_REMOVE_TRIGGER_TYPE_ELEMENT_REACTION = 304;

    /// <summary>
    /// buff移除触发类型-离开地块
    /// </summary>
    public const int BUFF_REMOVE_TRIGGER_TYPE_MOVE = 305;
    /************************************************************************************************/


    /// <summary>
    /// 触发效果：添加buff
    /// 参数：{是否继承施放者,目标表id, buffid, buffTime, stackNum, 是否继承施放者,目标表id, buffid, buffTime, stackNum...}
    /// </summary>
    public const int BUFF_EFFECT_TYPE_ADD_BUFF = 101;

    /// <summary>
    /// 触发效果：添加子buff
    /// 参数：{是否继承施放者,目标表id, buffid, buffTime, stackNum, 是否继承施放者,目标表id, buffid, buffTime, stackNum...}
    /// </summary>
    public const int BUFF_EFFECT_TYPE_ADD_SUB_BUFF = 102;

    /// <summary>
    /// 触发效果：驱散buff
    /// 参数：{目标表id,buff参数类型,keyid,数量}
    /// </summary>
    public const int BUFF_EFFECT_TYPE_DISPEL_BUFF = 103;

    /// <summary>
    /// 触发效果：随机添加buff
    /// 参数：{是否继承施放者,目标表id, buffid, buffTime, stackNum, 权重, 是否继承施放者,目标表id, buffid, buffTime, stackNum, 权重...}
    /// </summary>
    public const int BUFF_EFFECT_TYPE_RANDOM_ADD_BUFF = 106;

    /// <summary>
    /// 触发效果：随机添加子buff
    /// 参数：{是否继承施放者,目标表id, buffid, stackNum, buffTime, 权重, 是否继承施放者,目标表id, buffid, buffTime, stackNum, 权重...}
    /// </summary>
    public const int BUFF_EFFECT_TYPE_RANDOM_ADD_SUB_BUFF = 107;

    /// <summary>
    /// 持续效果：指定Buff的抵抗率
    /// 参数：{buff参数类型，keyid，几率}
    /// </summary>
    public const int BUFF_EFFECT_TYPE_BUFF_RESISTANCE = 108;

    /// <summary>
    /// 触发效果：修改BUFF持续时间
    /// 参数：{类型，类型值，持续时间增量}
    /// </summary>
    public const int BUFF_EFFECT_TYPE_CHANGE_TIME = 109;

    /// <summary>
    /// 触发效果：修改BUFF的层数
    /// 参数：{BUFF_id，层数}
    /// </summary>
    public const int BUFF_EFFECT_TYPE_CHANGE_STACK = 110;

    /// <summary>
    /// 触发效果：修改BUFF的叠加上限
    /// 参数：{BUFF_id，上限}
    /// </summary>
    public const int BUFF_EFFECT_TYPE_CHANGE_MAX_STACK = 111;

    /// <summary>
    /// 持续效果：指定Buff的时间
    /// 参数：{buff参数类型，keyid，时间}
    /// </summary>
    public const int BUFF_EFFECT_TYPE_BUFF_TIME = 112;


    ///// <summary>
    ///// 触发效果：执行某个服务
    ///// 参数：{serviceCid, serviceCid...}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_SERVICE = 201;
    ///// <summary>
    ///// 触发效果：执行某个掉落
    ///// 参数：{dropCid, dropCid...}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_DROP = 202;
    ///// <summary>
    ///// 持续效果：额外增加掉落
    ///// 参数：{random, dropCid}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_EX_DROP = 203;
    ///// <summary>
    ///// 持续效果：改变属性
    ///// 参数：{attId, 修改方式（1公式id2比率3值），值}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_CHANGE_ATTRIBUTES = 204;
    ///// <summary>
    ///// 触发效果：翻牌游戏重选次数
    ///// 参数：{次数增量}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_CARD_GAME_REDRAW_COUNT = 206;

    ///// <summary>
    ///// 触发效果：翻牌游戏复制
    ///// 参数：{复制牌的类型}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_CARD_GAME_COPY = 207;

    ///// <summary>
    ///// 触发效果：翻牌游戏改变洗牌时间
    ///// 参数：{百分比增量}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_CARD_GAME_SHUFFLE_DURATION = 208;

    ///// <summary>
    ///// 触发效果：翻牌游戏改变切牌次数
    ///// 参数：{次数增量}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_CARD_GAME_SHUFFLE_TIMES = 209;

    ///// <summary>
    ///// 触发效果：翻牌游戏标记卡牌
    ///// 参数：{标记牌的类型，数量}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_CARD_GAME_MARK_CARD = 210;

    ///// <summary>
    ///// 触发效果：翻牌游戏改变卡牌类型
    ///// 参数：{目标类型，改变类型}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_CARD_GAME_CHANGE_TYPE = 211;

    ///// <summary>
    ///// 持续效果：道具获得数量增加
    ///// 参数：{道具id,数量}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_ITEM_GET_NUM_ADD = 212;

    ///// <summary>
    ///// 持续效果：道具获得数量百分比增加
    ///// 参数：{道具id，百分比值,叠加值}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_ITEM_GET_NUM_PERCENT_ADD = 213;

    ///// <summary>
    ///// 触发效果：迷宫虚弱复活
    ///// 参数：{}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_MAZE_REVIVE = 214;

    ///// <summary>
    ///// 触发效果：判定增加重判次数
    ///// 参数：次数
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_MAZE_JUDGE_ADD_RECOUNT = 215;

    ///// <summary>
    ///// 触发效果：转盘改变速度
    ///// 参数：速度
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_MAZE_TURN_TABLE_CHANGE_SPEED = 217;

    ///// <summary>
    ///// 触发效果：转盘增加重转次数
    ///// 参数：次数
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_MAZE_TURN_TABLE_ADD_REDRAW = 218;

    ///// <summary>
    ///// 触发效果：转盘复制卡牌
    ///// 参数：{复制牌的类型}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_MAZE_TURN_TABLE_COPY_CARD = 219;

    ///// <summary>
    ///// 触发效果：转盘删除卡牌
    ///// 参数：{删除牌的类型}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_MAZE_TURN_TABLE_DELETE_CARD = 220;

    ///// <summary>
    ///// 触发效果：增加判定单个骰子评分
    ///// 参数：{值}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_MAZE_JUDGE_ADD_DICE_SCORE = 226;
    ///// <summary>
    ///// 触发效果：获得物品
    ///// 参数：{物品id，数量}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_ADD_ITEM = 229;
    ///// <summary>
    ///// 触发效果：转化物品
    ///// 参数：{物品id，目标物品id，转换百分比}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_CONVERT_ITEM = 230;
    ///// <summary>
    ///// 持续效果：耐久消耗改变
    ///// 参数：{百分比}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_DURABLE_COST_CHANGE = 233;
    ///// <summary>
    ///// 触发效果：提升\降低本次采集过程产物的产量
    ///// 参数：{采集类型|采集等级（包含该等级及以下的）|产物序号|变化百分比}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_GATHER_NUM_CHANGE = 234;
    ///// <summary>
    ///// 触发效果：提升\降低本次采集过程产物的获得几率
    ///// 参数：{采集类型|采集等级（包含该等级及以下的）|产物序号|变化百分比}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_GATHER_PROBABILITY_CHANGE = 235;
    ///// <summary>
    ///// 触发效果：提升\降低本次采集过程产物的暴击率
    ///// 参数：{采集类型|采集等级（包含该等级及以下的）|产物序号|变化百分比}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_GATHER_CRIT_CHANGE = 236;
    ///// <summary>
    ///// 持续效果：提升\降低本次指定id鱼的出现权重
    ///// 参数：{鱼的id|变化的百分比}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_FISH_ID_WEIGHTS = 237;
    ///// <summary>
    ///// 触发效果：提升\降低本次指定重量序号鱼的出现权重
    ///// 参数：{鱼的重量序号|变化的百分比}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_FISH_SIZE_WEIGHTS = 238;
    ///// <summary>
    ///// 持续效果：刻印强化消耗改变
    ///// 参数：{百分比|增加值}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_RUNE_STRENGTH_COST_CHANGE = 239;
    ///// <summary>
    ///// 持续效果：大迷宫评分改变
    ///// 参数：{百分比|增加值}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_ABYSS_SCORE_CHANGE = 240;
    ///// <summary>
    ///// 持续效果：修改生命改变率
    ///// 参数：{类型，百分比}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_CHANGE_HP_RATE = 241;
    ///// <summary>
    ///// 持续效果：改变战斗进入类型
    ///// 参数：{类型}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_FORCE_FIGHT_MODE = 242;
    ///// <summary>
    ///// 持续效果：选择刻印的选项数量改变
    ///// 参数：{改变值}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_CHOOSE_RUNE_NUM_CHANGE = 243;
    ///// <summary>
    ///// 持续效果：刻印转化为评分
    ///// 参数：{普通刻印转化评分|精良刻印转化评分|史诗刻印转化评分}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_RUNE_CONVET_TO_SCORE = 244;
    ///// <summary>
    ///// 触发效果：给乙方人偶加战斗buff
    ///// 参数：{buffid...}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_ADD_DOLL_BATTLE_BUFF = 245;
    ///// <summary>
    ///// 触发效果：给敌方怪物加战斗buff
    ///// 参数：{buffid...}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_ADD_MONSTER_BATTLE_BUFF = 246;
    ///// <summary>
    ///// 触发效果：人偶复活
    ///// 参数：{}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_DOLL_REVIVE = 247;
    ///// <summary>
    ///// 触发效果：给己方队伍加战斗队伍buff
    ///// 参数：{buffid...}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_ADD_SELF_TROOP_BATTLE_BUFF = 248;
    ///// <summary>
    ///// 触发效果：给敌方队伍加战斗队伍buff
    ///// 参数：{buffid...}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_ADD_ENEMY_TROOP_BATTLE_BUFF = 249;
    ///// <summary>
    ///// 持续效果：刻印商店价格改变
    ///// 参数：{百分比|增加值}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_RUNE_SHOP_PRICE_CHANGE = 250;
    ///// <summary>
    ///// 触发效果：获得次元深渊评分
    ///// 参数：{值}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_ADD_ABYSS_SCORE = 251;
    ///// <summary>
    ///// 持续效果：修改体力改变率
    ///// 参数：{类型，百分比}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_CHANGE_ENERGY_RATE = 252;

    ///// <summary>
    ///// 持续效果：体力上限修改
    ///// 参数：{值}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_CHANGE_ENERGY_MAX = 254;
    ///// <summary>
    ///// 触发效果：改变QTE按钮数量
    ///// 参数：{值}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_CHANGE_QTE_BUTTON_COUNT = 255;
    ///// <summary>
    ///// 触发效果：改变QTE重试次数
    ///// 参数：{值}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_CHANGE_QTE_RETRY_COUNT = 256;
    ///// <summary>
    ///// 触发效果：改变QTE时间
    ///// 参数：{类型，值}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_CHANGE_QTE_TIME = 257;
    ///// <summary>
    ///// 持续效果：修改元素状态时间
    ///// 参数：{元素类型|百分比}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_CHANGE_ELEMENT_STATUS_TIME = 259;
    ///// <summary>
    ///// 持续效果：修改单次生命值改变量上限
    ///// 参数：{类型,百分比}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_CHANGE_HP_CHANGE_MAX_LIMIT = 260;
    ///// <summary>
    ///// 触发效果：修改本次人偶提交需求属性
    ///// 参数：{类型,模式，值}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_CHANGE_SUBMIT_DOLL_ATTR = 261;
    ///// <summary>
    ///// 触发效果：修改本次人偶提交道具数量
    ///// 参数：{值}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_CHANGE_SUBMIT_DOLL_TOOL_NUM = 262;
    ///// <summary>
    ///// 触发效果：修改单个人偶HP
    ///// 参数：{修改类型，修改模式（1生命上限百分比,2绝对值)，值}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_CHANGE_DOLL_HP = 263;
    ///// <summary>
    ///// 触发效果：修改钟摆重试次数
    ///// 参数：{值}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_CHANGE_CLOCK_RETRY_COUNT = 264;
    ///// <summary>
    ///// 触发效果：修改钟摆速度
    ///// 参数：{百分比}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_CHANGE_CLOCK_SPEED = 265;
    ///// <summary>
    ///// 触发效果：删除钟摆方块
    ///// 参数：{类型，个数}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_DELETE_CLOCK_ITEM = 266;
    ///// <summary>
    ///// 触发效果：修改钟摆方块宽度
    ///// 参数：{类型，百分比}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_CHANGE_CLOCK_ITEM_AREA = 267;
    ///// <summary>
    ///// 触发效果：给乙方随机人偶加战斗buff
    ///// 参数：{buffid...}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_ADD_RANDOM_DOLL_BATTLE_BUFF = 268;
    ///// <summary>
    ///// 触发效果：给敌方随机怪物加战斗buff
    ///// 参数：{buffid...}
    ///// </summary>
    //public const int BUFF_EFFECT_TYPE_ADD_RANDOM_MONSTER_BATTLE_BUFF = 269;


    /// <summary>
    /// 持续性效果：改变战斗属性
    /// 参数：{属性id，修改方式（1公式id2比率基础属性3值4比率当前属性），值}
    /// </summary>
    public const int BUFF_EFFECT_TYPE_CHANGE_BATTLE_ATTRIBUTES = 301;

    /// <summary>
    /// 持续性效果：设置战斗状态
    /// 参数： 状态id
    /// </summary>
    public const int BUFF_EFFECT_TYPE_CHANGE_BATTLE_STATUS = 302;

    /// <summary>
    /// 触发效果：改变生命
    /// 参数：无视护盾，改变方式，值，
    /// </summary>
    public const int BUFF_EFFECT_TYPE_BATTLE_CHANGE_HP = 303;

    /// <summary>
    /// 触发效果：改变怒气
    /// 参数：增加或减少或设置,改变方式,值,是否计算加成
    /// </summary>
    public const int BUFF_EFFECT_TYPE_BATTLE_CHANGE_SKILL_ENERGY = 304;

    /// <summary>
    /// 触发效果：触发施放技能
    /// 参数：{技能施放者（1buff施放者/2buff拥有者/3技能施放者）,技能类型或技能id（0代表当前主技能）}
    /// </summary>
    public const int BUFF_EFFECT_TYPE_BATTLE_CAST_SKILL = 305;

    /// <summary>
    /// 持续性效果：护盾
    /// 参数：护盾类型,改变类型（1公式2值），值
    /// </summary>
    public const int BUFF_EFFECT_TYPE_SHIELD = 306;

    /// <summary>
    /// 触发效果：即死
    /// 参数：
    /// </summary>
    public const int BUFF_EFFECT_TYPE_IMMEDIATELY_DEAD = 307;

    /// <summary>
    /// 持续性效果：无视状态
    /// 参数：状态id
    /// </summary>
    public const int BUFF_EFFECT_TYPE_IMMUNE_STATUS = 308;

    /// <summary>
    /// 触发效果：积累伤害改变生命
    /// 参数：dot类型,改变方式，值
    /// </summary>
    public const int BUFF_EFFECT_TYPE_BATTLE_ACCUMULATE_CHANGE_HP = 309;

    /// <summary>
    /// 触发效果：通过公式添加buff
    /// 参数：{是否继承施放者,目标表id, buffid, buffTime, stackFunction, 是否继承施放者,目标表id, buffid, buffTime, stackFunction...}
    /// </summary>
    public const int BUFF_EFFECT_TYPE_ADD_BUFF_BY_FUNCTION = 310;

    /// <summary>
    /// 触发效果：通过公式添加子buff
    /// 参数：{是否继承施放者,目标表id, buffid, buffTime, stackFunction, 是否继承施放者,目标表id, buffid, buffTime, stackFunction...}
    /// </summary>
    public const int BUFF_EFFECT_TYPE_ADD_SUB_BUFF_BY_FUNCTION = 311;

    /// <summary>
    /// 触发效果：添加临时子技能
    /// 参数：{技能id,延迟时间...}
    /// </summary>
    public const int BUFF_EFFECT_TYPE_ADD_TMP_SUB_SKILL = 312;

    /// <summary>
    /// 触发效果：修改技能cd
    /// 参数：{技能类型，增减cd}
    /// </summary>
    public const int BUFF_EFFECT_TYPE_CHANGE_SKILL_CD = 313;


    /// <summary>
    /// 持续性效果：修改技能释放添加怒气
    /// 参数：1、类型(1技能类型 2技能ID) 2、类型值 3、改变值
    /// </summary>
    public const int BUFF_CHANGE_SKILL_ADD_ENERGY = 314;

    /// <summary>
    /// 触发效果：改变怒气队伍AP
    /// 参数：增加或减少或设置,改变方式,值
    /// </summary>
    public const int BUFF_EFFECT_TYPE_BATTLE_CHANGE_SKILL_AP = 316;

    /// <summary>
    /// 触发效果：累计受伤
    /// 参数：系数
    /// </summary>
    public const int BUFF_EFFECT_TYPE_BATTLE_CUMULATIVE_HURT = 317;

    /// <summary>
    /// 持续性效果：伤害限制
    /// 参数：限制类型(1上限 2破防),伤害类型,值
    /// </summary>
    public const int BUFF_EFFECT_TYPE_BATTLE_DAMAGE_LIMIT = 318;


    /// <summary>
    /// 触发效果：变幻位置
    /// 参数：移动值
    /// </summary>
    public const int BUFF_EFFECT_TYPE_BATTLE_MOVE = 319;


    /// <summary>
    /// 触发效果：附魔
    /// 参数：1、元素类型 2、附魔类型
    /// </summary>
    public const int BUFF_EFFECT_TYPE_ENCHANT = 320;


    /// <summary>
    /// 持续性效果：伤害均摊
    /// 参数：均摊类型：1:伤害&治疗 2：单伤害 3：单治疗
    /// </summary>
    public const int BUFF_EFFECT_TYPE_AVERAGE = 321;

    /// <summary>
    /// 触发效果：帮助(支援)
    /// 参数：
    /// </summary>
    public const int BUFF_EFFECT_TYPE_HELP = 322;


    /// <summary>
    /// 持续性效果：命中修正
    /// 参数：1、攻击类型 2、伤害类型 3、元素类型 4、技能类型 5、值
    /// </summary>
    public const int BUFF_EFFECT_TYPE_HIT_FIX = 323;


    /// <summary>
    /// 持续性效果：技能伤害修正
    /// 参数：1：(伤害or减免) 2、检查类型(1:技能ID 2:技能类型) 3、检查值 4、修正值
    /// </summary>
    public const int BUFF_EFFECT_TYPE_SKILL_DMG_FIX = 324;


    /// <summary>
    /// 持续性效果：反伤修正
    /// 参数：1、攻击类型 2、伤害类型 3、元素类型 4、技能类型 5、值
    /// </summary>
    public const int BUFF_EFFECT_TYPE_REBOUND_FIX = 325;

    /// <summary>
    /// 持续性效果：攻击免疫
    /// 参数：1、攻击类型 2、伤害类型 3、元素类型 4、技能类型
    /// </summary>
    public const int BUFF_EFFECT_TYPE_ATTACK_IMMUNITY = 326;


    /// <summary>
    /// 触发效果：召唤
    /// 参数：1.怪物ID 2
    /// </summary>
    public const int BUFF_EFFECT_TYPE_SUMMON = 327;


    /// <summary>
    /// 持续性效果：修改技能怒气消耗
    /// 参数：1、类型(1技能类型 2技能ID) 2、类型值 3、改变值
    /// </summary>
    public const int BUFF_CHANGE_SKILL_ENERGY = 328;

    /// <summary>
    /// 持续性效果：对怪物类型伤害修正
    /// 参数：1：(伤害or减免) 2、怪物类型 3、子类型 4、修正值
    /// </summary>
    public const int BUFF_EFFECT_MONSTER_TYPE_DMG_FIX = 329;

    /// <summary>
    /// 持续性效果：对职业类型伤害修正
    /// 参数：1：(伤害or减免) 2、职业类型 3、修正值
    /// </summary>
    public const int BUFF_EFFECT_PROFESSION_TYPE_DMG_FIX = 330;

    /// <summary>
    /// 持续性效果：封印技能
    /// 参数：技能类型
    /// </summary>
    public const int BUFF_EFFECT_SEAL_SKILL = 331;


    /// <summary>
    /// 持续性效果：修改技能目标
    /// 参数：1、类型(1技能类型 2技能ID) 2、类型值 3、TARGET_ID
    /// </summary>
    public const int BUFF_CHANGE_SKILL_TARGET = 332;


    /// <summary>
    /// 触发效果：改变地块地形
    /// 参数：1、TerrainId 2、回合数
    /// </summary>
    public const int BUFF_CHANGE_TILE_TERRAIN = 333;


    /// <summary>
    /// 触发效果：改变角色大小
    /// 参数：改变大小，参照大小
    /// </summary>
    public const int BUFF_EFFECT_CAHNEG_SIZE = 334;


    /// <summary>
    /// 触发效果：触发对话
    /// 参数：对话ID
    /// </summary>
    public const int BUFF_EFFECT_DIALOG = 335;


    /// <summary>
    /// 触发效果：添加弱点类型
    /// 参数：{弱点ID,弱点状态}
    /// </summary>
    public const int BUFF_EFFECT_TYPE_ADD_WEAK_TYPE = 336;

    /// <summary>
    /// 触发弱点：删除弱点类型
    /// 参数：{弱点ID}
    /// </summary>
    public const int BUFF_EFFECT_TYPE_REMOVE_WEAK_TYPE = 337;

    /// <summary>
    /// 触发效果：修改弱点层数
    /// 参数：{层数}
    /// </summary>
    public const int BUFF_EFFECT_TYPE_CHANGE_WEAK_NUM = 338;

    /// <summary>
    /// 触发效果：修改弱点状态
    /// 参数：{弱点ID,状态}
    /// </summary>
    public const int BUFF_EFFECT_TYPE_CHANGE_WEAK_STATUS = 339;

    /// <summary>
    /// 持续性效果：守护
    /// 参数：{元素类型 -1任意,比例}
    /// </summary>
    public const int BUFF_EFFECT_TYPE_PROTECT = 340;

    /// <summary>
    /// 触发性效果：设置当前回合行动状态
    /// 参数：状态 1：已出手 其他：未出手
    /// </summary>
    public const int BUFF_EFFECT_TYPE_RESET_ROUND_ACTION = 341;


    /// <summary>
    /// 触发性效果：修改战斗积分
    /// 参数：增加或减少或设置,改变方式,值
    /// </summary>
    public const int BUFF_EFFECT_TYPE_CHANGE_FIGHT_INTEGRAL = 342;
}