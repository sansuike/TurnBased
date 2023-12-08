using System.Collections;
using System.Collections.Generic;


    /// <summary>
	/// 战斗结束指令
	/// </summary>
	public class BattleOverCommand : BaseBattleCommand
    {
        /// <summary>
        /// 持有战斗的模块id
        /// </summary>
        public long HolderID;
        /// <summary>
        /// 战斗id
        /// </summary>
        public string FightID;
        /// <summary>
        /// 战斗结果
        /// </summary>
        public int FightResult;
        /// <summary>
        /// 回合数
        /// </summary>
        public int Round;
        /// <summary>
        /// 战斗类型
        /// </summary>
        public int BattleType;
        /// <summary>
        /// 进攻部队数据
        /// </summary>
        public FightTroopPOD Attacker;
        /// <summary>
        /// 防御部队数据
        /// </summary>
        public FightTroopPOD Defender;
        /// <summary>
        /// 所有的指令
        /// </summary>
        public List<BaseBattleOrder> Orders;
        /// <summary>
        /// 是否跳过战斗
        /// </summary>
        public bool SkipBattle;
        /// <summary>
        /// 伤害统计
        /// </summary>
        public Dictionary<int, int> DmgRecords;
        /// <summary>
        /// 自定义数据
        /// </summary>
        public string UserData;
        /// <summary>
        /// order的json格式
        /// </summary>
        public string JsonOrder;

        /// <summary>
        /// 战斗单位更新
        /// </summary>
        public List<BattleUpdateUnitPOD> UpdateUnits;
        
        public BattleOverCommand()
        {
        }

        public override IDictionary ToDic()
        {
            IDictionary dic = new Dictionary<string, object>();
            dic["HolderID"] = HolderID;
            dic["FightID"] = FightID;
            dic["Attacker"] = Attacker;
            dic["Defender"] = Defender;
            dic["FightResult"] = FightResult;
            dic["Round"] = Round;
            dic["BattleType"] = BattleType;
            dic["SkipBattle"] = SkipBattle;
            dic["DmgRecords"] = DmgRecords;
            dic["UserData"] = UserData;
            dic["JsonOrder"] = JsonOrder;
            dic["UpdateUnits"] = UpdateUnits;
            return dic;
        }
    }
