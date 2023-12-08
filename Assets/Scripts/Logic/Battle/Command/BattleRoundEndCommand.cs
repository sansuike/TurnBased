using System.Collections;
using System.Collections.Generic;


    /// <summary>
	/// 每回合结束命令
	/// </summary>
	public class BattleRoundEndCommand : BaseBattleCommand
    {

        /// <summary>
        /// 回合数
        /// </summary>
        public int Round;
        /// <summary>
        /// 战斗单位更新
        /// </summary>
        public List<BattleUpdateUnitPOD> UpdateUnits;


        //*******************************************************
        public BattleRoundEndCommand() { }

        public override IDictionary ToDic()
        {
            IDictionary dic = new Dictionary<string, object>();
            dic["Round"] = Round;
            dic["UpdateUnits"] = UpdateUnits;
            return dic;
        }
    }
