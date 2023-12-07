    /// <summary>
    /// BUFF移除原因类型
    /// </summary>
    public enum BuffRemoveReason
    {
        /// <summary>
        /// buff移除原因-清理（死亡、离开区域、叠加层数为0等）
        /// </summary>
        CLEAN = 0,
        /// <summary>
        /// buff移除原因-持续时间结束
        /// </summary>
        TIME_OUT = 1,
        /// <summary>
        /// buff移除原因-触发次数达到上限
        /// </summary>
        TRIGGER_LIMIT = 2,
        /// <summary>
        /// buff移除原因-覆盖
        /// </summary>
        OVERRIDE = 3,
        /// <summary>
        /// buff移除原因-驱散
        /// </summary>
        DISPEL = 4,
        /// <summary>
        /// buff移除原因-失效，护盾
        /// </summary>
        NONEFFECTIVE = 5,
        /// <summary>
        /// 移动移除BUFF效果
        /// </summary>
        MOVE = 6,
    }

