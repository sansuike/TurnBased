using System;
using System.Collections;
using System.Collections.Generic;


    /// <summary>
    /// 战斗出手中数据
    /// </summary>
    [Serializable]
    public class BattleInActionPOD : BaseBattlePOD
    {

        /// <summary>
        /// 一次施放技能
        /// </summary>
        public List<BattleCastSkillPOD> CastSkills;

        //*********************************************************

        public BattleInActionPOD()
        {
        }

        public override IDictionary ToDic()
        {
            IDictionary dic = new Dictionary<string, object>();
            dic["CastSkills"] = CastSkills;
            return dic;
        }
    }


