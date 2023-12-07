using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 战斗命令委托
/// </summary>
/// <param name="pid"></param>
/// <param name="command"></param>
public delegate void BattleCommandHandler(string fightID, string playerID, BaseBattleCommand command);

public class BattleManager
{
    private static BattleManager _instance;
    
    /// <summary>
    /// 战斗命令委托
    /// </summary>
    private BattleCommandHandler _commandHandler;
    
    /// <summary>
    /// 所有战斗集
    /// </summary>
    private Dictionary<string, BaseLogicFight> _dictAllFight;

    //**************************************************************************************

    private BattleManager()
    {
        _dictAllFight = new Dictionary<string, BaseLogicFight>();
    }
    
    /// <summary>
    /// 对表现层发送命令
    /// </summary>
    public void SendCommand(string fightID, string pid, BaseBattleCommand command)
    {
        _commandHandler.Invoke(fightID, pid, command);
    }

    /// <summary>
    /// 注册战斗命令委托
    /// </summary>
    /// <param name="commandHandler"></param>
    public void RegisterCommandHandler(BattleCommandHandler commandHandler)
    {
        _commandHandler = commandHandler;
    }
    
    /// <summary>
    /// 添加指令
    /// </summary>
    /// <param name="fightID"></param>
    /// <param name=""></param>
    public void AddOrder(string fightID, BaseBattleOrder order)
    {
        lock (_dictAllFight)
        {
            if (_dictAllFight.ContainsKey(fightID))
            {
                _dictAllFight[fightID].AddOrder(order);
            }
        }
    }
    
    /// <summary>
    /// 创建战斗
    /// </summary>
    /// <param name="fightID"></param>
    public BaseLogicFight CreateFight(string fightID, int type)
    {
        ShutDownFight(fightID);
        BaseLogicFight fight = FightFactory(fightID, type);
        lock (_dictAllFight)
        {
            _dictAllFight[fightID] = fight;
        }
        return fight;
    }
    
    /// <summary>
    /// 战斗工厂
    /// </summary>
    /// <param name="fightID"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    private BaseLogicFight FightFactory(string fightID, int type)
    {
        switch (type)
        {
            default:
                return new TurnBaseLogicFight(fightID);
        }
    }
    
    /// <summary>
    /// 帧刷
    /// </summary>
    public void Update()
    {
        lock (_dictAllFight)
        {
            foreach (KeyValuePair<string, BaseLogicFight> kv in _dictAllFight)
            {
                kv.Value.Update();
            }
        }
    }

    /// <summary>
    /// 关闭所有战斗
    /// </summary>
    public void ShutDown()
    {
        lock (_dictAllFight)
        {
            foreach (KeyValuePair<string, BaseLogicFight> kv in _dictAllFight)
            {
                kv.Value.Shutdown();
            }
            _dictAllFight.Clear();
        }
    }
    
    /// <summary>
    /// 关闭指定战斗
    /// </summary>
    /// <param name="fightID"></param>
    public void ShutDownFight(string fightID)
    {
        lock (_dictAllFight)
        {
            BaseLogicFight fight = null;
            if (_dictAllFight.TryGetValue(fightID, out fight))
            {
                fight.Shutdown();
                _dictAllFight.Remove(fightID);
            }
        }
    }

    public static BattleManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BattleManager();
            }
            return _instance;
        }
    }
}