using System.Collections;
using System.Collections.Generic;


    /// <summary>
    /// 战斗瓦片
    /// </summary>
    public class BattleTilePOD : BaseBattlePOD
    {

        /// <summary>
        /// 战斗单位唯一ID
        /// </summary>
        public int ID;

        /// <summary>
        /// 瓦片位置
        /// </summary>
        public int BattlePos;

        /// <summary>
        /// 所属队伍类型
        /// </summary>
        public int TroopType;


        /// <summary>
        /// 所有buff
        /// </summary>
        public List<BattleBuffPOD> Buffs;
        /// <summary>
        /// 单位状态
        /// </summary>
        public bool[] Status;
        /// <summary>
        /// 单位特殊状态
        /// </summary>
        public bool[] SPStatus;

        /// <summary>
        /// 地形配置ID
        /// </summary>
        public int TerrainCid;

        //********************************************************************

        public BattleTilePOD()
        {
        }

        public override IDictionary ToDic()
        {
            IDictionary dic = new Dictionary<string, object>();
            dic["ID"] = ID;
            dic["BattlePos"] = BattlePos;
            dic["TroopType"] = TroopType;
            dic["Buffs"] = Buffs;
            dic["Status"] = Status;
            dic["SPStatus"] = SPStatus;
            dic["TerrainCid"] = TerrainCid;
            return dic;
        }

    }

