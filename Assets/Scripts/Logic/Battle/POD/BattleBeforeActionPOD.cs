using System;
using System.Collections;
using System.Collections.Generic;


    /// <summary>
    /// 战斗出手前数据
    /// </summary>
    [Serializable]
    public class BattleBeforeActionPOD : BaseBattlePOD
    {

        /// <summary>
        /// 战斗单位更新
        /// </summary>
        public List<BattleUpdateUnitPOD> UpdateUnits;

        //*********************************************************

        public BattleBeforeActionPOD()
        {

        }

        public override IDictionary ToDic()
        {
            IDictionary dic = new Dictionary<string, object>();
            dic["UpdateUnits"] = UpdateUnits;
            return dic;
        }
    }


