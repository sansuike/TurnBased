using FixMath.NET;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    /// <summary>
    /// 施放技能
    /// </summary>
    [Serializable]
    public class BattleCastSkillPOD : BaseBattlePOD
    {
        /// <summary>
        /// 施放者
        /// </summary>
        public int CasterID;
        /// <summary>
        /// 技能id
        /// </summary>
        public int SkillID;
        /// <summary>
        /// 所属主技能id
        /// </summary>
        public int MainSkillID;
        /// <summary>
        /// 延迟时间
        /// </summary>
        public Fix64 DelayTime;
        /// <summary>
        /// 总时间
        /// </summary>
        public Fix64 TotalTime;
        /// <summary>
        /// 技能表现
        /// </summary>
        public int SkillAction;
        /// <summary>
        /// 受击者
        /// </summary>
        public List<BattleSkillHitPOD> SkillHits;
        /// <summary>
        /// 战斗单位更新
        /// </summary>
        public List<BattleUpdateUnitPOD> BeforeUpdateUnits;

        /// <summary>
        /// 战斗单位更新
        /// </summary>
        public List<BattleUpdateUnitPOD> AfterUpdateUnits;

        public BattleCastSkillPOD() { }

        public override IDictionary ToDic()
        {
            IDictionary dic = new Dictionary<string, object>();
            dic["CasterID"] = CasterID;
            dic["SkillID"] = SkillID;
            dic["MainSkillID"] = MainSkillID;
            dic["DelayTime"] = DelayTime;
            dic["SkillAction"] = SkillAction;
            dic["SkillHits"] = SkillHits;
            dic["BeforeUpdateUnits"] = BeforeUpdateUnits;
            dic["AfterUpdateUnits"] = AfterUpdateUnits;
            return dic;
        }
    }
