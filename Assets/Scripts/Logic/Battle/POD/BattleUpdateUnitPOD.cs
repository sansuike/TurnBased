
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// 战斗单位更新通信数据
    /// </summary>
    [Serializable]
    public class BattleUpdateUnitPOD : BaseBattlePOD
    {
        /// <summary>
        /// 战斗单位唯一ID
        /// </summary>
        public int ID;
        /// <summary>
        /// 作用域
        /// </summary>
        public int Scope;
        /// <summary>
        /// 类型
        /// #See @BattleConstant.UpdateType
        /// </summary>
        public int Type;
        /// <summary>
        /// 参数
        /// </summary>
        public int[] Param;
        /// <summary>
        /// 召唤物
        /// </summary>
        public BattleUnitPOD Call;
        //*********************************************************

        public BattleUpdateUnitPOD()
        {

        }

        public BattleUpdateUnitPOD(int id,int scope, int type, int[] param,BattleUnitPOD call)
        {
            ID = id;
            Scope = scope;
            Type = type;
            Param = param;
            Call = call;
        }

        public override IDictionary ToDic()
        {
            IDictionary dic = new Dictionary<string, object>();
            dic["ID"] = ID;
            dic["Scope"] = Scope;
            dic["Type"] = Type;
            dic["Param"] = Param;
            if (Call != null)
            {
                dic["Call"] = Call;
            }
            return dic;
        }
    }


