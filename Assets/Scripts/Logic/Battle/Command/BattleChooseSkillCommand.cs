using System.Collections;
using System.Collections.Generic;


    /// <summary>
	/// 每次出手命令
	/// </summary>
	public class BattleChooseSkillCommand : BaseBattleCommand
    {
        /// <summary>
        /// 行动者
        /// </summary>
        public int MoverID;

        /// <summary>
        /// 行动者阵营
        /// </summary>
        public int TroopType;



        //*******************************************************
        public BattleChooseSkillCommand(int moverId,int troopType) {
            MoverID = moverId;
            TroopType = troopType;
        }

        public override IDictionary ToDic()
        {
            IDictionary dic = new Dictionary<string, object>();
            dic["MoverID"] = MoverID;
            dic["TroopType"] = TroopType;
            return dic;
        }
    }
