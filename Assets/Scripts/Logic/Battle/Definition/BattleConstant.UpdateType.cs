public static partial class BattleConstant
{
    /// <summary>
    /// 单位更新类型
    /// </summary>
    public static class UpdateType
    {
        /// <summary>
        /// 改变生命，参数{生命值全量，生命值增量（用于展示）,元素类型}
        /// </summary>
        public const int CHANGE_HP = 1;

        /// <summary>
        /// 改变最大生命，参数{最大生命}
        /// </summary>
        public const int UPDATE_MAX_HP = 2;

        /// <summary>
        /// 改变怒气，参数{怒气全量，怒气增量（用于展示）}
        /// </summary>
        public const int CHANGE_ENERGY = 3;

        /// <summary>
        /// 更新buff包括新增，参数{buffCid, 层数, 剩余时间, 是否是挂载}
        /// </summary>
        public const int UPDATE_BUFF = 4;

        /// <summary>
        /// 移除buff，参数{buffCid,移除原因} //BuffRemoveReason
        /// </summary>
        public const int REMOVE_BUFF = 5;

        /// <summary>
        /// 修改技能cd，参数{skillCid,cooldown}
        /// </summary>
        public const int CHANGE_SKILL_CD = 6;

        /// <summary>
        /// 获得状态，参数{状态id}
        /// </summary>
        public const int ADD_STATUS = 7;

        /// <summary>
        /// 取消状态，参数{状态id}
        /// </summary>
        public const int REMOVE_STATUS = 8;

        /// <summary>
        /// 获得特殊状态，参数{特殊状态类型} //SPStatus(大破、小破、死亡)
        /// </summary>
        public const int ADD_SP_STATUS = 9;

        /// <summary>
        /// 更新护盾值，参数{护盾全量,是否是消耗}
        /// </summary>
        public const int UPDATE_SHIELD = 10;

        /// <summary>
        /// 触发buff，参数{buffCid}
        /// </summary>
        public const int TRIGGER_BUFF = 12;

        /// <summary>
        /// 更新单位伤害统计,参数｛伤害统计量｝
        /// </summary>
        public const int UPDATE_DMG_RECORDS = 14;

        /// <summary>
        /// 免疫buff，参数{buffCid}
        /// </summary>
        public const int BUFF_IMMUNE = 15;

        /// <summary>
        /// 免疫buff效果，参数{buffCid}
        /// </summary>
        public const int BUFF_EFFECT_IMMUNE = 16;

        /// <summary>
        /// 改变速度，参数{速度}
        /// </summary>
        public const int UPDATE_SPEED = 17;
        // /// <summary>
        // /// 更新弱点类型，参数{类型1,类型2...}
        // /// </summary>
        // public const int UPDATE_WEAK_TYPE = 18;
        // /// <summary>
        // /// 更新弱点最大层数，参数{层数}
        // /// </summary>
        // public const int UPDATE_WEAK_MAX_NUM = 19;
        // /// <summary>
        // /// 更新弱点层数，参数{层数}
        // /// </summary>
        // public const int UPDATE_WEAK_NUM = 20;

        /// <summary>
        /// 改变AP，参数{AP全量，AP增量（用于展示）}
        /// </summary>
        public const int CHANGE_AP = 21;

        /// <summary>
        /// 改变最大AP，参数{AP全量，AP增量（用于展示）}
        /// </summary>
        public const int CHANGE_MAX_AP = 22;

        /// <summary>
        /// 改变最大怒气，参数{怒气全量，怒气增量（用于展示）}
        /// </summary>
        public const int CHANGE_MAX_ENERGY = 23;

        /// <summary>
        /// 改变英雄位置，参数{坐标}
        /// </summary>
        public const int CHANGE_POS = 24;

        /// <summary>
        /// 修改技能次数，参数{skillCid,已经释放的次数}
        /// </summary>
        public const int CHANGE_SKILL_COUNT = 25;

        /// <summary>
        /// 技能消耗更新，参数{skillCid,需要消耗的怒气}
        /// </summary>
        public const int CHANGE_SKILL_ENERGY = 26;

        /// <summary>
        /// 召唤
        /// </summary>
        public const int SUMMON_ADD = 27;

        /// <summary>
        /// 移除
        /// </summary>
        public const int SUMMON_REMOVE = 28;

        /// <summary>
        /// 封印技能，参数{skillCid,是否封印(1：封印 0不封印)}
        /// </summary>
        public const int SEAL_SKILL = 29;

        /// <summary>
        /// 修改技能目标，参数{skillCid,targetCid}
        /// </summary>
        public const int CHANGE_SKILL_TARGET = 30;

        /// <summary>
        /// 修改地格地形，参数{terrainCid}
        /// </summary>
        public const int CHANGE_TILE_TERRAIN = 31;

        /// <summary>
        /// 修改公共CD,参数{cd}
        /// </summary>
        public const int CHANGE_COMMON_CD = 32;

        /// <summary>
        /// 修改角色大小,参数{changeSize,maxSize}
        /// </summary>
        public const int CHANGE_SIZE = 33;

        /// <summary>
        /// 触发对话,参数{对话id}
        /// </summary>
        public const int TRIGGER_DIALOG = 34;


        /// <summary>
        /// 新增弱点，参数：弱点ID,状态
        /// </summary>
        public const int ADD_WEAK_TYPE = 35;

        /// <summary>
        /// 更新弱点最大层数，参数{层数}
        /// </summary>
        public const int UPDATE_WEAK_MAX_NUM = 36;

        /// <summary>
        /// 更新弱点层数，参数{层数}
        /// </summary>
        public const int UPDATE_WEAK_NUM = 37;

        /// <summary>
        /// 更新弱点状态，参数：弱点ID,状态
        /// </summary>
        public const int UPDATE_WEAK_STATUS = 38;

        // <summary>
        /// 删除弱点，参数：弱点ID
        /// </summary>
        public const int REMOVE_WEAK_TYPE = 39;

        // <summary>
        /// 更改AI类型，参数：类型
        /// </summary>
        public const int UPDATE_AI_TYPE = 40;
    }
}