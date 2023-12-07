using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FightPOD : BaseBattlePOD
{
    /// <summary>
    /// 唯一id
    /// </summary>
    public string ID;
    /// <summary>
    /// 战斗类型
    /// </summary>
    public int BattleType;
    /// <summary>
    /// 地图id
    /// </summary>
    public int MapID;
    /// <summary>
    /// 最大回合数
    /// </summary>
    public int MaxRound;
    /// <summary>
    /// 当前波次
    /// </summary>
    public int CurrentBigRound;
    /// <summary>
    /// 最大波次
    /// </summary>
    public int MaxBigRound;
    /// <summary>
    /// 随机种子
    /// </summary>
    public long RandomSeed;
    /// <summary>
    /// 进攻部队数据
    /// </summary>
    public FightTroopPOD Attacker;
    /// <summary>
    /// 防御部队数据
    /// </summary>
    public FightTroopPOD Defender;
    /// <summary>
    /// 参与战斗的玩家
    /// </summary>
    public List<string> Players;
    /// <summary>
    /// 怪物组
    /// </summary>
    //public int MonsterTeamID;
    /// <summary>
    /// 战斗参数
    /// </summary>
    public Dictionary<int, int> BattleParams;
    public abstract IDictionary ToDic();
}
