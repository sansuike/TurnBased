using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using FixMath.NET;

public class FightUnitPOD : BaseBattlePOD
{
    /// <summary>
        /// 玩家ID
        /// </summary>
        public string Pid;

        /// <summary>
        /// 怪物配表ID
        /// </summary>
        public int MonsterCfgId;

        /// <summary>
        /// 等级
        /// </summary>
        public int Level;

        /// <summary>
        /// 属性集
        /// </summary>
        public Fix64[] Attributes;

        /// <summary>
        /// 所属队伍类型
        /// </summary>
        public int TroopType;

        /// <summary>
        /// 站位
        /// </summary>
        public int BattlePos;

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
        /// 特殊状态
        /// </summary>
        public bool[] SPStatus;

        /// <summary>
        /// 是否是援手
        /// </summary>
        public bool IsHelper;

        /// <summary>
        /// 战斗力
        /// </summary>
        public int Power;
        /// <summary>
        /// 初始化buff
        /// </summary>
        public List<int> InitBuff;

        /// <summary>
        /// 皮肤
        /// </summary>
        public int SkinId;
        
        /// <summary>
        /// 头像
        /// </summary>
        public int IconId;

        /// <summary>
        /// 排序
        /// </summary>
        public int Order;
        
        /// <summary>
        /// 排序
        /// </summary>
        public int AIType;
        
        /// <summary>
        /// 基础属性集
        /// </summary>
        public Fix64[] BaseAttrs;

        public FightUnitPOD()
        {
            
        }

        public FightUnitPOD(IDictionary dic)
        {
            
        }
        
        public override IDictionary ToDic()
        {
            IDictionary dic = new Dictionary<string, object>();
            return dic;
        }
}