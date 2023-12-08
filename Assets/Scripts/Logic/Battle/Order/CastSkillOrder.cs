using System.Collections;
using System;
using System.Collections.Generic;


    /// <summary>
    /// 施放技能的指令
    /// </summary>
    public sealed class CastSkillOrder : BaseBattleOrder
    {
        /// <summary>
        /// 施放者id
        /// </summary>
        public int UnitID;
        /// <summary>
        /// 技能id,-1待机0自动
        /// </summary>
        public int SkillCid;
        /// <summary>
        /// 目标
        /// </summary>
        public List<int> Targets;

        /// <summary>
        /// 回合数
        /// </summary>
        public int RoundNumber;
        //**********************************************************

        public CastSkillOrder() : base()
        {

        }

        public override void Parse(IDictionary pod)
        {
            UnitID = Convert.ToInt32(pod["UnitID"]);
            SkillCid = Convert.ToInt32(pod["SkillCid"]);
            RoundNumber = Convert.ToInt32(pod["RoundNumber"]);
            object[] tempList = DictionaryUtil.Dic2Arr(pod["Targets"] as IDictionary);
            if (tempList != null)
            {
                Targets = new List<int>(Array.ConvertAll(tempList, s => Convert.ToInt32(s)));
            }
            else
            {
                Targets = new List<int>();
            }
        }


    }
