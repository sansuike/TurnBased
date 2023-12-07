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

    /// <summary>
    /// 行动
    /// </summary>
    /// <returns>战斗是否完成</returns>
    protected override bool Action()
    {
        if (_isGiveUp)
        {
            Debug.Log("-------------- 放弃战斗 ---------------");
            SendCommand(_GetBattleOverCommand(BattleConstant.FightResult.GIVE_UP));
            return true;
        }

        return true;
    }

    private BattleOverCommand _GetBattleOverCommand(int  fightResult)
    {
        return new BattleOverCommand();
    }
    
    /// <summary>
    /// 判断任意阵营死亡
    /// </summary>
    /// <returns></returns>
    private  bool _CheckAnyTroopDead()
    {
        bool atkDeadAll = true;
        foreach (BattleUnitVO unit in _attackers)
        {
            if (!unit.IsHelper && !unit.IsDead())
            {
                atkDeadAll = false;
                break;
            }
        }
        if (atkDeadAll)
        {
            return true;
        }

        bool defDeadAll = true;
        foreach (BattleUnitVO unit in _defensers)
        {
            if (!unit.IsHelper && !unit.IsDead())
            {
                defDeadAll = false;
                break;
            }
        }
        if (defDeadAll)
        {
            return true;
        }
        return false;
    }
}
