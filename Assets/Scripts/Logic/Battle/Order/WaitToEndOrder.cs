using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    /// <summary>
    /// 休息
    /// </summary>
    public sealed class WaitToEndOrder : BaseBattleOrder
    {

        /// <summary>
        /// 施放者id
        /// </summary>
        public int UnitID;

        /// <summary>
        /// 回合数
        /// </summary>
        public int RoundNumber;
        
        public WaitToEndOrder() : base()
        {

        }

        public override void Parse(IDictionary pod)
        {
            UnitID = Convert.ToInt32(pod["UnitID"]);
            RoundNumber = Convert.ToInt32(pod["RoundNumber"]);
        }
        
    }
