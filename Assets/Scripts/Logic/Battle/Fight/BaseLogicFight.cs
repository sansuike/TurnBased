using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

/// <summary>
/// 战斗基类
/// </summary>
public abstract class BaseLogicFight
{
    /// <summary>
    /// 战斗唯一ID
    /// </summary>
    protected string _fightID;

    /// <summary>
    /// 战斗类型
    /// </summary>
    protected int _battleType;

    /// <summary>
    /// 自定义数据
    /// </summary>
    protected string _userData;

    public Hashtable userDataDict;

    /// <summary>
    /// 随机类
    /// </summary>
    protected LogicStableRandom _random;

    /// <summary>
    /// 当前帧
    /// </summary>
    protected int _currentFrame;

    /// <summary>
    /// 是否开始
    /// </summary>
    private bool _isStart;

    /// <summary>
    /// 战斗是否结束
    /// </summary>
    private bool _isOver;

    /// <summary>
    /// 是否放弃
    /// </summary>
    protected bool _isGiveUp;

    /// <summary>
    /// 战斗等待执行的指令
    /// </summary>
    private Queue<BaseBattleOrder> _WaitOrders;
    /// <summary>
    /// 战斗已完成指令
    /// </summary>
    protected Queue<BaseBattleOrder> _CompleteOrders;
    /// <summary>
    /// 跳过战斗,需要忽略消息和自动战斗
    /// </summary>
    public bool Quickly { get; private set; }
    /// <summary>
    /// 还原中
    /// </summary>
    public bool Restoreing { get; private set; }
    /// <summary>
    /// 所有玩家
    /// </summary>
    public Dictionary<string, BattlePlayerVO> Players { get; private set; }
    /// <summary>
    /// 战斗完成命令
    /// </summary>
    private BattleOverCommand _battleOverCommand;
    /// <summary>
    /// 战场参数
    /// </summary>
    public Dictionary<int, int> BattleParams { get; private set; }
    
    /// <summary>
    /// 行动
    /// </summary>
    /// <returns>是否结束</returns>
    protected abstract bool Action();
    
    public BaseLogicFight(string id)
    {
        _fightID = id;
        _WaitOrders = new Queue<BaseBattleOrder>();
        _CompleteOrders = new Queue<BaseBattleOrder>();
        //Players = new Dictionary<string, BattlePlayerVO>();
    }


    /// <summary>
    /// 战斗帧刷
    /// </summary>
    public void Update()
    {
        try
        {
            _currentFrame++;
            if (_WaitOrders.Count > 0)
            {
                BaseBattleOrder order = _WaitOrders.Peek();
                if (order.Frame <= _currentFrame)
                {
                    _WaitOrders.Dequeue();
                    if (HandleOrder(order))
                    {
                        _CompleteOrders.Enqueue(order);
                    }
                }
            }

            if (_isOver || !_isStart)
            {
                return;
            }

            if (Action())
            {
                _isOver = true;
            }
        }
        catch (Exception e)
        {
            _isOver = true;
            UnityEngine.Debug.LogError(e.ToString());
        }
    }
    
    public void SendCommand(BaseBattleCommand command)
    {
        Debug.Log($"SendCommand {command.GetType()}");
        if (command is BattleOverCommand)
        {
            _battleOverCommand = command as BattleOverCommand;
        }
        if ((Quickly ))
        {
            return;
        }
        command.Frame = _currentFrame;
        foreach (KeyValuePair<string, BattlePlayerVO> item in Players)
        {
            BattleManager.Instance.SendCommand(_fightID, item.Key, command);
        }
    }

    /// <summary>
    /// 处理初始化战斗指令
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    protected virtual bool _HandleInitFightOrder(BaseBattleOrder order)
    {
        InitFightOrder initOrder = order as InitFightOrder;
        foreach (string playerPid in initOrder.FightPOD.Players)
        {
            Players.Add(playerPid, new BattlePlayerVO(this, playerPid));
        }
        _random = new LogicStableRandom(initOrder.FightPOD.RandomSeed);
        BattleParams = initOrder.FightPOD.BattleParams;
        _battleType = initOrder.FightPOD.BattleType;
        _userData = initOrder.UserData;
        // <String, object> userDatas = GameFramework.Utility.Json.ToObject(IDictionary., "");
        //userDataDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(_userData);
        userDataDict = JsonConvert.DeserializeObject<Hashtable>(_userData);
        UnityEngine.Debug.Log("Add HandleInitFightOrder");
        return true;
    }

    /// <summary>
    /// 初始化战斗开启指令
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    protected virtual bool _HandleStartFightOrder(BaseBattleOrder order)
    {
        if (_isStart)
        {
            return false;
        }
        Debug.Log("Add StartFightOrder");
        Players[order.Pid].Ready = true;
        foreach (KeyValuePair<string, BattlePlayerVO> kv in Players)
        {
            if (!kv.Value.Ready)
            {
                return true;
            }
        }
        _isStart = true;
        return true;
    }
    
    /// <summary>
    /// 执行战斗指令
    /// </summary>
    /// <param name="order"></param>
    public virtual bool HandleOrder(BaseBattleOrder order)
    {
        if (order is InitFightOrder)
        {
            return _HandleInitFightOrder(order);
        }
        else if (order is StartFightOrder)
        {
            return _HandleStartFightOrder(order);
        }
        else if (order is SkipFightOrder)
        {
            return _HandleSkipFightOrder(order);
        }
        else if (order is GiveUpOrder)
        {
            return _HandleGiveUpOrder(order as GiveUpOrder);
        }

        return false;
    }

    /// <summary>
    /// 放弃战斗
    /// </summary>
    /// <param name="giveUpOrder"></param>
    /// <returns></returns>
    private bool _HandleGiveUpOrder(GiveUpOrder giveUpOrder)
    {
        _isGiveUp = true;
        return true;
    }

    /// <summary>
    /// 跳过战斗指令
    /// </summary>
    /// <param name="skipFightOrder"></param>
    /// <returns></returns>
    protected virtual bool _HandleSkipFightOrder(BaseBattleOrder order)
    {
        if (Quickly || _isOver)
        {
            return false;
        }

        Quickly = true;
        _isStart = true;
        _WaitOrders = new Queue<BaseBattleOrder>();
        while (!_isOver)
        {
            Update();
        }

        Quickly = false;
        _battleOverCommand.Frame = _currentFrame;
        _battleOverCommand.SkipBattle = true;
        //if (!Restoreing)
        {
            foreach (KeyValuePair<string, BattlePlayerVO> item in Players)
            {
                BattleManager.Instance.SendCommand(_fightID, item.Key, _battleOverCommand);
            }
        }

        return true;
    }

    /// <summary>
    /// 表现层，添加指令
    /// </summary>
    /// <param name="order"> </param>
    public void AddOrder(BaseBattleOrder order)
    {
        order.Frame = _currentFrame;
        _WaitOrders.Enqueue(order);
    }

    /// <summary>
    /// 关闭战斗
    /// </summary>
    public virtual void Shutdown()
    {
        _isOver = true;
    }
    
    /// <summary>
    /// 随机数
    /// </summary>
    public LogicStableRandom Random
    {
        get
        {
            return _random;
        }
    }
}