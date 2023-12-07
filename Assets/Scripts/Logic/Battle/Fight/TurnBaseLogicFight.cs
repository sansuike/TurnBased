using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBaseLogicFight : BaseLogicFight
{
    /// <summary>
    /// 进攻方
    /// </summary>
    private List<BattleUnitVO> _attackers;
    /// <summary>
    /// 防守方
    /// </summary>
    private List<BattleUnitVO> _defensers;
    
    /// <summary>
    /// 战斗瓦片
    /// key 位置
    /// </summary>
    private Dictionary<int, BattleTileVO> _allBattleTile;
    
    /// <summary>
    /// 当前回合,初始为0
    /// </summary>
    public int _round;

    public TurnBaseLogicFight(string id) : base(id)
    {
        
    }
}
