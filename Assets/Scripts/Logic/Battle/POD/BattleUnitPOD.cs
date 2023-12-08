using System.Collections;
using System.Collections.Generic;
using FixMath.NET;

    /// <summary>
    /// 战斗单位通信数据
    /// </summary>
    public class BattleUnitPOD : BaseBattlePOD
    {
        /// <summary>
        /// 战斗单位唯一ID
        /// </summary>
        public int ID;

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
        /// 战斗力
        /// </summary>
        public int Power;

        /// <summary>
        /// 生命
        /// </summary>
        public int HP;

        /// <summary>
        /// 最大生命
        /// </summary>
        public int MaxHP;

        /// <summary>
        /// 护盾
        /// </summary>
        public int Shield;

        /// <summary>
        /// 大招怒气
        /// </summary>
        public int SkillEnergy;

        /// <summary>
        /// 怒气上限
        /// </summary>
        public int MaxEnergy;

        /// <summary>
        /// 技能会消耗的AP
        /// </summary>
        public int AP;

        /// <summary>
        /// AP上限
        /// </summary>
        public int MaxAP;

        /// <summary>
        /// 速度
        /// </summary>
        public int Speed;

        /// <summary>
        /// 所属队伍类型
        /// </summary>
        public int TroopType;

        /// <summary>
        /// 站位，[1,10]
        /// </summary>
        public int BattlePos;

        /// <summary>
        /// 所有技能
        /// key=skillCid
        /// </summary>
        public List<int> Skills;
        /// <summary>
        /// 技能强化等级（存放技能的等级，key为技能的cid）
        /// </summary>
        public Dictionary<int, int> SkillsLevel;
        
        /// <summary>
        /// 技能精炼等级（存放技能的等级，key为技能的cid）
        /// </summary>
        public Dictionary<int, int> SkillPurifyLv;
        
        /// <summary>
        /// 技能突破等级（存放技能的等级，key为技能的cid）
        /// </summary>
        public Dictionary<int, int> SkillStrengLv;
        /// <summary>
        /// 所有buff
        /// </summary>
        public List<BattleBuffPOD> Buffs;
        /// <summary>
        /// 单位状态
        /// </summary>
        public bool[] Status;
        /// <summary>
        /// 单位特殊状态
        /// </summary>
        public bool[] SPStatus;
        /// <summary>
        /// 技能cd
        /// </summary>
        public Dictionary<int, int> SkillCD;
        /// <summary>
        /// 技能怒气消耗
        /// </summary>
        public Dictionary<int, int> SkillCostEnergy;
        /// <summary>
        /// 技能AP消耗
        /// </summary>
        public Dictionary<int, int> SkillCostAP;

        /// <summary>
        /// 状态Timeline
        /// </summary>
        public Dictionary<int, int> StateTimeLine;
        
        /// <summary>
        /// 技能Timeline
        /// </summary>
        public Dictionary<int, int> SkillSkinId;
        
        /// <summary>
        /// 技能最大释放次数
        /// </summary>
        public Dictionary<int, int> SkillReleaseLimit;
        
        /// <summary>
        /// 技能目标
        /// </summary>
        public Dictionary<int, int> SkillTarget;
        
        /// <summary>
        /// 弱点<ID,状态>
        /// </summary>
        public Dictionary<int, int> WeakStatus;
        /// <summary>
        /// 弱点最大层数
        /// </summary>
        public int WeakMaxNum;
        /// <summary>
        /// 当前弱点层数
        /// </summary>
        public int WeakNum;

        /// <summary>
        /// 皮肤ID
        /// </summary>
        public int SkinId;

        /// <summary>
        /// 
        /// </summary>
        public int IconId;

        /// <summary>
        /// 创建类型
        /// 0 原始怪 1 召唤怪 2 替补怪
        /// </summary>
        public int CreateType;
        
        /// <summary>
        /// 公用技能CD
        /// </summary>
        public int CommonSkillCD;
        
        /// <summary>
        /// 大小
        /// </summary>
        public float Size;

        /// <summary>
        /// AI类型
        /// </summary>
        public int AI_TYPE;
        //*********************************************************

        public BattleUnitPOD()
        {

        }

        public override IDictionary ToDic()
        {
            IDictionary dic = new Dictionary<string, object>();
            dic["ID"] = ID;
            dic["Pid"] = Pid;
            dic["MonsterCfgId"] = MonsterCfgId;
            dic["Level"] = Level;
            dic["Power"] = Power;
            dic["HP"] = HP;
            dic["MaxHP"] = MaxHP;
            dic["Shield"] = Shield;
            dic["SkillEnergy"] = SkillEnergy;
            dic["MaxEnergy"] = MaxEnergy;
            dic["AP"] = AP;
            dic["MaxAP"] = MaxAP;
            dic["Speed"] = Speed;
            dic["TroopType"] = TroopType;
            dic["BattlePos"] = BattlePos;
            dic["Skills"] = Skills;
            dic["SkillsLevel"] = SkillsLevel;
            dic["SkillPurifyLv"] = SkillPurifyLv;
            dic["SkillStrengLv"] = SkillStrengLv;
            dic["Buffs"] = Buffs;
            dic["Status"] = Status;
            dic["SPStatus"] = SPStatus;
            dic["SkillCD"] = SkillCD;
            dic["SkillCostEnergy"] = SkillCostEnergy;
            dic["SkillCostAP"] = SkillCostAP;
            dic["StateTimeLine"] = StateTimeLine;
            dic["SkillSkinId"] = SkillSkinId;
            dic["SkillReleaseLimit"] = SkillReleaseLimit;
            dic["SkillTarget"] = SkillTarget;
            dic["WeakStatus"] = WeakStatus;
            dic["WeakMaxNum"] = WeakMaxNum;
            dic["WeakNum"] = WeakNum;
            dic["SkinId"] = SkinId;
            dic["IconId"] = IconId;
            dic["CreateType"] = CreateType;
            dic["CommonSkillCD"] = CommonSkillCD;
            dic["Size"] = Size;
            dic["AI_TYPE"] = AI_TYPE;
            return dic;
        }
    }


