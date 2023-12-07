
    public class BattlePlayerVO:BaseBattleVO
    {
        /// <summary>
        /// 玩家ID
        /// </summary>
        public string Pid { get; set; }
        /// <summary>
        /// 准备完毕
        /// </summary>
        public bool Ready { get; set; }
        internal BattlePlayerVO(BaseLogicFight fight, string pid) : base(fight)
        {
            
        }
    }
