using System;
using System.Collections;
using System.Collections.Generic;


    /// <summary>
    /// 一次技能命中的信息
    /// </summary>
    [Serializable]
    public class BattleSkillHitPOD : BaseBattlePOD
    {
        /// <summary>
        /// 战斗单位id
        /// </summary>
        public int UnitID;
        /// <summary>
        /// 作用域
        /// </summary>
        public int Scope;
        /// <summary>
        /// 伤害类型
        /// </summary>
        public int DamageType;
        /// <summary>
        /// 元素类型
        /// </summary>
        public int ElementType;
        /// <summary>
        /// 是否免疫
        /// </summary>
        public bool Immunity;
        /// <summary>
        /// 暴击
        /// </summary>
        public bool Crit;
        /// <summary>
        /// 格挡
        /// </summary>
        public bool Block;
        /// <summary>
        /// 命中
        /// </summary>
        public bool Hit;
        /// <summary>
        /// 技能实际伤害
        /// </summary>
        public int ActualDmg;
        /// <summary>
        /// 护盾吸收
        /// </summary>
        public int ShieldReduce;
        /// <summary>
        /// 出击者吸血值
        /// </summary>
        public int BloodSuck;
        /// <summary>
        /// 出击者反伤
        /// </summary>
        public int ThornsDmg;
        /// <summary>
        /// 战斗单位更新
        /// </summary>
        public List<BattleUpdateUnitPOD> UpdateUnits;




        public BattleSkillHitPOD() { }

        public override IDictionary ToDic()
        {
            IDictionary dic = new Dictionary<string, object>();
            dic["UnitID"] = UnitID;
            dic["Scope"] = Scope;
            dic["DamageType"] = DamageType;
            dic["ElementType"] = ElementType;
            dic["Crit"] = Crit;
            dic["Block"] = Block;
            dic["Hit"] = Hit;
            dic["ActualDmg"] = ActualDmg;
            dic["Immunity"] = Immunity;
            dic["ShieldReduce"] = ShieldReduce;
            dic["BloodSuck"] = BloodSuck;
            dic["ThornsDmg"] = ThornsDmg;
            dic["UpdateUnits"] = UpdateUnits;
            return dic;
        }
    }
