using System;
using System.Collections;
using System.Collections.Generic;

    /// <summary>
    /// 战斗开始
    /// </summary>
    public sealed class BattleStartCommand : BaseBattleCommand
    {
        /// <summary>
        /// 战斗单位更新
        /// </summary>
        public List<BattleUpdateUnitPOD> UpdateUnits;


        //*******************************************************
        public BattleStartCommand()
        {
        }

        public override IDictionary ToDic()
        {
            IDictionary dic = new Dictionary<string, object>();
            dic["UpdateUnits"] = UpdateUnits;
            return dic;
        }

    }
