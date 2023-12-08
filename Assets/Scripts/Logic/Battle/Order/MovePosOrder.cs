using System.Collections;
using System;
using System.Collections.Generic;


    /// <summary>
    /// 角色移动的指令
    /// </summary>
    public sealed class MovePosOrder : BaseBattleOrder
    {
        /// <summary>
        /// 施放者id
        /// </summary>
        public int UnitID;
        /// <summary>
        /// 新的站位
        /// </summary>
        public int BattlePos;

        /// <summary>
        /// 回合数
        /// </summary>
        public int RoundNumber;
        //**********************************************************

        public MovePosOrder() : base()
        {

        }

        public override void Parse(IDictionary pod)
        {
            UnitID = Convert.ToInt32(pod["UnitID"]);
            BattlePos = Convert.ToInt32(pod["BattlePos"]);
            RoundNumber = Convert.ToInt32(pod["RoundNumber"]);
        }

        
    }
