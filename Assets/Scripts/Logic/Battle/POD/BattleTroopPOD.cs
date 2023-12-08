using System.Collections;
using System.Collections.Generic;


/// <summary>
/// 战斗队伍
/// </summary>
public class BattleTroopPOD : BaseBattlePOD
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
    /// 技能会消耗的AP
    /// </summary>
    public int AP;

    /// <summary>
    /// AP上限
    /// </summary>
    public int MaxAP;

    /// <summary>
    /// 所属队伍类型
    /// </summary>
    public int TroopType;

    /// <summary>
    /// 所有buff
    /// </summary>
    public List<BattleBuffPOD> Buffs;


    /// <summary>
    /// 公用技能CD
    /// </summary>
    public int CommonSkillCD;

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
    /// 技能cd
    /// </summary>
    public Dictionary<int, int> SkillCD;

    /// <summary>
    /// 技能AP消耗
    /// </summary>
    public Dictionary<int, int> SkillCostAP;

    /// <summary>
    /// 技能最大释放次数
    /// </summary>
    public Dictionary<int, int> SkillReleaseLimit;
    //********************************************************************

    public BattleTroopPOD()
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
        dic["AP"] = AP;
        dic["MaxAP"] = MaxAP;
        dic["TroopType"] = TroopType;
        dic["Buffs"] = Buffs;
        dic["CommonSkillCD"] = CommonSkillCD;
        dic["Skills"] = Skills;
        dic["SkillsLevel"] = SkillsLevel;
        dic["SkillPurifyLv"] = SkillPurifyLv;
        dic["SkillStrengLv"] = SkillStrengLv;
        dic["SkillCD"] = SkillCD;
        dic["SkillCostAP"] = SkillCostAP;
        dic["SkillReleaseLimit"] = SkillReleaseLimit;
        return dic;
    }
}