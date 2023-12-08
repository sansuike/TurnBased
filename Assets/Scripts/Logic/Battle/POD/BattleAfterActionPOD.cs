using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
    /// 战斗出手后数据
    /// </summary>
    [Serializable]
    public class BattleAfterActionPOD : BaseBattlePOD
    {
        /// <summary>
        /// 战斗单位更新
        /// </summary>
        public List<BattleUpdateUnitPOD> UpdateUnits;
        //*********************************************************

        public BattleAfterActionPOD()
        {

        }

        public override IDictionary ToDic()
        {
            IDictionary dic = new Dictionary<string, object>();
            dic["UpdateUnits"] = UpdateUnits;
            return dic;
        }
    }

