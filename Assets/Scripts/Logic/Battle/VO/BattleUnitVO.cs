using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnitVO : BaseBattleVO,IBuffHolder
{
    /// <summary>
    /// buff管理器
    /// </summary>
    private BuffManager _BuffManager;
    
    /// <summary>
    /// 所有技能
    /// </summary>
    public Dictionary<int,int> _skills { get; set; }
    
    /// <summary>
    /// 所属队伍类型
    /// </summary>
    public int TroopType { get; private set; }
    /// <summary>
    /// 站位ID
    /// </summary>
    public int BattlePos { get; set; }
        
    /// <summary>
    /// 站位顺序
    /// </summary>
    public int BattleOrder { get; set; }
    
    /// <summary>
    /// 瓦片ID
    /// </summary>
    public BattleTileVO Tile { get; set; }
    
    /// <summary>
    /// 战斗力
    /// </summary>
    private int _power;
    
    /// <summary>
    /// 初始化buff
    /// </summary>
    private List<int> _initBuff;

    /// <summary>
    /// 作用域
    /// </summary>
    public int ScopeType { get;}
    public BattleUnitVO ( int scopeType,TurnBaseLogicFight fight):base(fight)
    {
        
    }
}
