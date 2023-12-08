using System.Collections;
using System.Collections.Generic;


    /// <summary>
	/// 每回合命令
	/// </summary>
	public class BattleRoundCommand : BaseBattleCommand
    {
        /// <summary>
        /// 回合数
        /// </summary>
        public int Round;
        /// <summary>
        /// 出手顺序
        /// </summary>
        public List<int> TurnOrders;

        /// <summary>
        ///  先手队伍
        /// </summary>
        public int FirstTroopType;
        
        /// <summary>
        /// 战斗单位更新
        /// </summary>
        public List<BattleUpdateUnitPOD> UpdateUnits;


        //*******************************************************
        public BattleRoundCommand() { }

        public override IDictionary ToDic()
        {
            IDictionary dic = new Dictionary<string, object>();
            dic["Round"] = Round;
            dic["TurnOrders"] = TurnOrders;
            dic["UpdateUnits"] = UpdateUnits;
            dic["FirstTroopType"] = FirstTroopType;
            return dic;
        }
    }
