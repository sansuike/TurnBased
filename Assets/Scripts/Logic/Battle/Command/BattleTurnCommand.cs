using System.Collections;
using System.Collections.Generic;


    /// <summary>
	/// 每次出手命令
	/// </summary>
	public class BattleTurnCommand : BaseBattleCommand
    {
        /// <summary>
        /// 行动者
        /// </summary>
        public int MoverID;
        /// <summary>
        /// 出手前数据
        /// </summary>
        public BattleBeforeActionPOD BeforeActionPOD;
        /// <summary>
        /// 出手中数据
        /// </summary>
        public BattleInActionPOD InActionPOD;
        /// <summary>
        /// 出手后数据
        /// </summary>
        public BattleAfterActionPOD AfterActionPOD;



        //*******************************************************
        public BattleTurnCommand() { }

        public override IDictionary ToDic()
        {
            IDictionary dic = new Dictionary<string, object>();
            dic["MoverID"] = MoverID;
            dic["BeforeActionPOD"] = BeforeActionPOD;
            dic["InActionPOD"] = InActionPOD;
            dic["AfterActionPOD"] = AfterActionPOD;
            return dic;
        }
    }
