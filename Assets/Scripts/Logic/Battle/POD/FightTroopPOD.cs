using System;
using System.Collections;
using System.Collections.Generic;

public class FightTroopPOD : BaseBattlePOD
{
     /// <summary>
        /// 玩家ID
        /// </summary>
        public string Pid;
        
        /// <summary>
        /// 带入的buff
        /// </summary>
        public List<int> Buffs;
        
        /// <summary>
        /// 地格信息
        /// </summary>
        public int[] Terrains;
        
        /// <summary>
        /// 所有技能
        /// key=skillCid
        /// </summary>
        public List<int> Skills;
        
        /// <summary>
        /// 所有技能等级
        /// key=skillCid
        /// </summary>
        public List<int> SkillLvs;
        /// <summary>
        /// 所有技能精炼等级
        /// key=skillCid
        /// </summary>
        public List<int> SkillPurifyLvs;
        
        /// <summary>
        /// 所有技能突破等级
        /// key=skillCid
        /// </summary>
        public List<int> SkillStrengLvs;
        /// <summary>
        /// 技能强化
        /// </summary>
        public List<int> SkillStrengthens;
        
        /// <summary>
        /// 战斗单位数据集
        /// </summary>
        public FightUnitPOD[] ArrFightUnitPOD;

        //********************************************************************

        public FightTroopPOD()
        {
        }

        public FightTroopPOD(IDictionary dic)
        {
            Pid = Convert.ToString(dic["Pid"]);
            
            object[] tempList = DictionaryUtil.Dic2Arr(dic["Buffs"] as IDictionary);
            if (tempList != null)
            {
                Buffs = new List<int>(Array.ConvertAll(tempList, s => Convert.ToInt32(s)));
            }
            else
            {
                Buffs = new List<int>();
            }

            tempList = DictionaryUtil.Dic2Arr(dic["Terrains"] as IDictionary);
            if (tempList != null)
            {
                Terrains = Array.ConvertAll(tempList, s => Convert.ToInt32(s));
            }
            else
            {
                Terrains = new int[9];
            }
            
            tempList = DictionaryUtil.Dic2Arr(dic["Skills"] as IDictionary);
            if (tempList != null)
            {
                Skills = new List<int>(Array.ConvertAll(tempList, s => Convert.ToInt32(s)));
            }
            else
            {
                Skills = new List<int>();
            }
            
            tempList = DictionaryUtil.Dic2Arr(dic["SkillLvs"] as IDictionary);
            if (tempList != null)
            {
                SkillLvs = new List<int>(Array.ConvertAll(tempList, s => Convert.ToInt32(s)));
            }
            else
            {
                SkillLvs = new List<int>();
            }

            tempList = DictionaryUtil.Dic2Arr(dic["SkillPurifyLvs"] as IDictionary);
            if (tempList != null)
            {
                SkillPurifyLvs = new List<int>(Array.ConvertAll(tempList, s => Convert.ToInt32(s)));
            }
            else
            {
                SkillPurifyLvs = new List<int>();
            }
            
            tempList = DictionaryUtil.Dic2Arr(dic["SkillStrengLvs"] as IDictionary);
            if (tempList != null)
            {
                SkillStrengLvs = new List<int>(Array.ConvertAll(tempList, s => Convert.ToInt32(s)));
            }
            else
            {
                SkillStrengLvs = new List<int>();
            }
            
            tempList = DictionaryUtil.Dic2Arr(dic["SkillStrengthens"] as IDictionary);
            if (tempList != null)
            {
                SkillStrengthens = new List<int>(Array.ConvertAll(tempList, s => Convert.ToInt32(s)));
            }
            else
            {
                SkillStrengthens = new List<int>();
            }            

            
            tempList = DictionaryUtil.Dic2Arr(dic["ArrFightUnitPOD"] as IDictionary);
            if (tempList != null)
            {
                ArrFightUnitPOD = Array.ConvertAll(tempList, s => new FightUnitPOD(s as IDictionary));
            }
            else
            {
                ArrFightUnitPOD = new FightUnitPOD[0];
            }

        }

        public override IDictionary ToDic()
        {
            IDictionary dic = new Dictionary<string, object>();
            dic["Pid"] = Pid;
            dic["Buffs"] = Buffs;
            dic["Terrains"] = Terrains;
            dic["Skills"] = Skills;
            dic["SkillLvs"] = SkillLvs;
            dic["SkillPurifyLvs"] = SkillPurifyLvs;
			dic["SkillStrengLvs"] = SkillStrengLvs;
            dic["SkillStrengthens"] = SkillStrengthens;
            dic["ArrFightUnitPOD"] = ArrFightUnitPOD;
            return dic;
        }
}