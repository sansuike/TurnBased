using System;
using System.Collections;
using System.Collections.Generic;


    /// <summary>
    /// 初始化命令
    /// </summary>
    public sealed class BattleInitCommand : BaseBattleCommand
    {
        /// <summary>
        /// 战场地图ID
        /// </summary>
        public int MapID;
        /// <summary>
        /// 战斗id
        /// </summary>
        public string FightID;
        /// <summary>
        /// 最大回合数
        /// </summary>
        public int MaxRound;
        /// <summary>
        ///战斗类型
        /// </summary>
        public int BattleType;
        /// <summary>
        /// 攻击方单位数据列表
        /// </summary>
        public List<BattleUnitPOD> AttackUnitPODs;
        /// <summary>
        /// 防守方单位数据列表
        /// </summary>
        public List<BattleUnitPOD> DefendUnitPODs;
        /// <summary>
        /// 怪物组
        /// </summary>
        public int MonsterTeamID;
        /// <summary>
        /// 攻击方队伍
        /// </summary>
        public BattleTroopPOD AttackTroopPOD;
        /// <summary>
        /// 防御方队伍
        /// </summary>
        public BattleTroopPOD DefendTroopPOD;
        /// <summary>
        /// 瓦片地图
        /// </summary>
        public List<BattleTilePOD> BattleTilePODs;

        /// <summary>
        /// 出手顺序
        /// </summary>
        public List<int> TurnOrders;
        
        /// <summary>
        /// 替补怪物
        /// </summary>
        public List<int> Tubstitute;

        /// <summary>
        /// 当前怪物波次
        /// </summary>
        public int CurrentBigRound;
        
        /// <summary>
        /// 最大怪物波次
        /// </summary>
        public int MaxBigRound;
        
        //*****************************************************************************************

        public BattleInitCommand(int mapId, int maxRound, string fightId, int battleType, int monsterTeamID, List<BattleUnitPOD> attackUnitPODs, List<BattleUnitPOD> defendUnitPODs, BattleTroopPOD attackTroopPOD, BattleTroopPOD defendTroopPOD, List<BattleTilePOD> battleTilePODs, List<int> turnOrders,List<int> tubstitute,int currentBigRound,int maxBigRound)
        {
            MapID = mapId;
            MaxRound = maxRound;
            FightID = fightId;
            BattleType = battleType;
            AttackUnitPODs = attackUnitPODs;
            DefendUnitPODs = defendUnitPODs;
            MonsterTeamID = monsterTeamID;
            AttackTroopPOD = attackTroopPOD;
            DefendTroopPOD = defendTroopPOD;
            BattleTilePODs = battleTilePODs;
            TurnOrders = turnOrders;
            Tubstitute = tubstitute;
            CurrentBigRound = currentBigRound;
            MaxBigRound = maxBigRound;
        }


        public override IDictionary ToDic()
        {
            IDictionary dic = new Dictionary<string, object>();
            dic["MapID"] = MapID;
            dic["MaxRound"] = MaxRound;
            dic["FightID"] = FightID;
            dic["BattleType"] = BattleType;
            dic["AttackUnitPODs"] = AttackUnitPODs;
            dic["DefendUnitPODs"] = DefendUnitPODs;
            dic["MonsterTeamID"] = MonsterTeamID;
            dic["AttackTroopPOD"] = AttackTroopPOD;
            dic["DefendTroopPOD"] = DefendTroopPOD;
            dic["BattleTilePODs"] = BattleTilePODs;
            dic["TurnOrders"] = TurnOrders;
            dic["Tubstitute"] = Tubstitute;
            dic["CurrentBigRound"] = CurrentBigRound;
            dic["MaxBigRound"] = MaxBigRound;
            return dic;
        }

    }
