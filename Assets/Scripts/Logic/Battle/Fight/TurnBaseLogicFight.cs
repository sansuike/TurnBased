using FixMath.NET;
//using IQIGame.Onigao.Config;
//using IQIGame.Onigao.Logic.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using CxExtension;
//using Json.Lite;
using Newtonsoft.Json;
using UnityEngine;

/// <summary>
/// 回合战斗
/// </summary>
public partial class TurnBaseLogicFight : BaseLogicFight
{
    /// <summary>
    /// 持有战斗的外部模块id
    /// </summary>
    private long _holderId;

    /// <summary>
    /// 最大回合数
    /// </summary>
    private int _maxRound;

    /// <summary>
    /// 召唤物
    /// </summary>
    private Dictionary<int, BattleCallVO> _allBattleCalls;

    /// <summary>
    /// 进攻方
    /// </summary>
    private List<BattleUnitVO> _attackers;

    /// <summary>
    /// 防守方
    /// </summary>
    private List<BattleUnitVO> _defensers;

    /// <summary>
    /// 所有战斗单位
    /// 不包括队伍单位
    /// key=id
    /// </summary>
    private Dictionary<int, BattleUnitVO> _allBattleUnits;

    /// <summary>
    /// 进攻方的队伍单位
    /// </summary>
    public BattleTroopUnitVO AttackTroopUnitVO { get; private set; }

    /// <summary>
    /// 防守方的队伍单位
    /// </summary>
    public BattleTroopUnitVO DefendTroopUnitVO { get; private set; }

    /// <summary>
    /// 战斗瓦片
    /// key 位置
    /// </summary>
    private Dictionary<int, BattleTileVO> _allBattleTile;

    /// <summary>
    /// 当前回合,初始为0
    /// </summary>
    public int _round;

    /// <summary>
    /// 替补下标,初始为0
    /// </summary>
    public int tubstituteIndex;

    /// <summary>
    /// 回合切换
    /// </summary>
    public bool _roundToggle;

    /// <summary>
    /// 回合是否开始
    /// </summary>
    private bool _roundStart;

    /// <summary>
    /// 延迟行动帧
    /// </summary>
    private int _actionFrame;

    /// <summary>
    /// 检查外挂加速用的理论行动帧
    /// </summary>
    private int _checkActionFrame;

    /// <summary>
    /// 等待单位行动
    /// </summary>
    private int _waitAction;

    /// <summary>
    /// 本回合内，战斗单位行动顺序
    /// </summary>
    public List<BattleUnitVO> _unitActionOrders;

    /// <summary>
    /// 当前行动单位位置，_unitActionOrders的下标
    /// </summary>
    private int _actionOrderIndex;

    /// <summary>
    /// 当前更新战斗单位列表
    /// </summary>
    private List<BattleUpdateUnitPOD> _updateUnits;

    /// <summary>
    /// 触发了技能的单位
    /// </summary>
    public Queue<KeyValuePair<BattleUnitVO, BattleSkillVO>> TriggerSkillUnits { get; private set; }

    /// <summary>
    /// PVE怪物组
    /// </summary>
    //public CfgMonsterTeamData CfgMonsterTeamData;

    /// <summary>
    /// 移动中
    /// </summary>
    public bool Moving = false;

    /// <summary>
    /// 检查战斗是否结束
    /// </summary>
    public bool CheckFight = false;

    /// <summary>
    /// 结果状态
    /// </summary>
    private bool[] _resultStatus;

    #region 当前临时状态全局记录

    /// <summary>
    /// 当前行动者
    /// </summary>
    public BattleUnitVO CurrMover { get; private set; }

    /// <summary>
    /// 是否是当前行动的技能
    /// </summary>
    public bool IsActionSkill { get; private set; }

    /// <summary>
    /// 当前的施放技能者
    /// </summary>
    public BattleUnitVO CurrSkillCaster { get; private set; }

    /// <summary>
    /// 当前施放主技能
    /// </summary>
    public BattleSkillVO CurrMainSkill { get; private set; }

    /// <summary>
    /// 当前施放技能
    /// </summary>
    public BattleSkillVO CurrSkill { get; private set; }

    /// <summary>
    /// 当前技能目标
    /// </summary>
    public List<BattleUnitVO> CurrSkillTargets { get; private set; }

    /// <summary>
    /// 本次攻击目标
    /// </summary>
    public HashSet<BattleUnitVO> CurrSkillAllTargets { get; private set; }

    /// <summary>
    /// 当前攻击目标
    /// </summary>
    public BattleUnitVO CurrAtkTarget { get; private set; }

    /// <summary>
    /// 当前元素反应目标
    /// </summary>
    public BattleUnitVO CurrAtkElementReactionTarget { get; set; }

    /// <summary>
    /// 技能施放
    /// </summary>
    public CastSkillOrder CurrCastInfo { get; private set; }

    /// <summary>
    /// 移动指令
    /// </summary>
    public MovePosOrder CurrMoveInfo { get; private set; }

    /// <summary>
    /// AI大招开关指令
    /// </summary>
    public MovePosOrder AISwitchInfo { get; private set; }

    /// <summary>
    /// 休息指令
    /// </summary>
    public WaitToEndOrder CurrRestInfo { get; private set; }

    /// <summary>
    /// 当前锚点
    /// </summary>
    public BattleUnitVO CurrAnchorPoint { get; set; }

    /// <summary>
    /// 聚合统计信息
    /// </summary>
    /// <returns></returns>
    public Hashtable Statistics { get; set; }

    /// <summary>
    /// 战斗积分
    /// </summary>
    /// <returns></returns>
    public int FightIntegral { get; set; }

    /// <summary>
    /// 触发标记
    /// 在触发阶段生成的buff不能立即触发（当前触发类型），需要等标记清除后才能开始触发
    /// </summary>
    public Type triggerStepType;

    /// <summary>
    /// 触发阶段生成的buff记录
    /// </summary>
    public HashSet<Buff> triggerStepBuffs = new HashSet<Buff>();

    /// <summary>
    /// 默认攻击方先手
    /// </summary>
    private bool AttackFirstMove = true;

    /// <summary>
    /// 是否自动战斗过
    /// </summary>
    private bool IsAutoFight = false;


    private int CurrentBigRound = 0;

    private int MaxBigRound = 0;

    #endregion


    public TurnBaseLogicFight(string fightID) : base(fightID)
    {
    }

    /// <summary>
    /// 初始化战斗指令
    /// </summary>
    /// <param name="initOrder"></param>
    /// <returns></returns>
    private bool _HandleInitFightOrder(InitFightOrder initOrder)
    {
        Statistics = new Hashtable();
        if (userDataDict == null)
        {
            userDataDict = new Hashtable();
        }


        // 初始化积分
        if (userDataDict.ContainsKey(BattleConstant.UserData.FIGHT_INTEGRAL))
        {
            FightIntegral = Convert.ToInt32(userDataDict[BattleConstant.UserData.FIGHT_INTEGRAL]);
        }

        _holderId = initOrder.HolderID;
        _maxRound = initOrder.FightPOD.MaxRound;
        _unitActionOrders = new List<BattleUnitVO>();
        _actionOrderIndex = 0;
        _round = 0;
        _roundToggle = false;

        _updateUnits = new List<BattleUpdateUnitPOD>(); //让初始化的时候不报错

        _allBattleUnits = new Dictionary<int, BattleUnitVO>();
        _allBattleCalls = new Dictionary<int, BattleCallVO>();
        TriggerSkillUnits = new Queue<KeyValuePair<BattleUnitVO, BattleSkillVO>>();
        CurrSkillAllTargets = new HashSet<BattleUnitVO>();

        _allBattleTile = new Dictionary<int, BattleTileVO>();


        List<int> backTeam = new List<int>();
        // CfgMonsterTeamData = CfgMonsterTeamTable.Instance.GetDataByID(initOrder.FightPOD.MonsterTeamID);
        // if (CfgMonsterTeamData != null) { 
        //     _resultStatus = new bool[CfgMonsterTeamData.ResultType.Length];
        //     // 取得地形
        //     backTeam.AddRange(CfgMonsterTeamData.BackTeam);
        // }

        // 创建瓦片
        for (int i = 1; i <= 9; i++)
        {
            // 攻击方地格
            BattleTileVO atkTile = new BattleTileVO(this, BattleConstant.TroopType.ATTACK, i,
                initOrder.FightPOD.Attacker.Terrains[i - 1]);
            _allBattleTile.Add(atkTile.ID, atkTile);

            // 防御方地格
            BattleTileVO defTile = new BattleTileVO(this, BattleConstant.TroopType.DEFEND, i,
                initOrder.FightPOD.Defender.Terrains[i - 1]);
            _allBattleTile.Add(defTile.ID, defTile);
        }

        //创建攻击方
        _attackers = new List<BattleUnitVO>();
        foreach (FightUnitPOD unitPOD in initOrder.FightPOD.Attacker.ArrFightUnitPOD)
        {
            if (unitPOD != null)
            {
                unitPOD.TroopType = BattleConstant.TroopType.ATTACK;
                BattleUnitVO unit = new BattleUnitVO(BattleConstant.ScopeType.MONSTER, this, unitPOD);
                AddUnit(unit);
                BattleTileVO tile = _allBattleTile
                    .Where(t => t.Value.BattlePos == unit.BattlePos && t.Value.TroopType == unit.TroopType).First()
                    .Value;
                tile.InitBattleUnit(unit);
                // 测试
                //tile.GetBuffManager().AddBuff(tile, null, 100);
            }
        }

        // 创建防守方
        _defensers = new List<BattleUnitVO>();
        foreach (FightUnitPOD unitPOD in initOrder.FightPOD.Defender.ArrFightUnitPOD)
        {
            if (unitPOD != null)
            {
                unitPOD.TroopType = BattleConstant.TroopType.DEFEND;
                BattleUnitVO unit = new BattleUnitVO(BattleConstant.ScopeType.MONSTER, this, unitPOD);

                AddUnit(unit);
                BattleTileVO tile = _allBattleTile
                    .Where(t => t.Value.BattlePos == unit.BattlePos && t.Value.TroopType == unit.TroopType).First()
                    .Value;
                tile.InitBattleUnit(unit);
            }
        }

        //创建队伍单位
        AttackTroopUnitVO = new BattleTroopUnitVO(this, BattleConstant.TroopType.ATTACK, initOrder.FightPOD.Attacker);
        DefendTroopUnitVO = new BattleTroopUnitVO(this, BattleConstant.TroopType.DEFEND, initOrder.FightPOD.Defender);


        //初始化队伍buff
        for (int i = 0; i < initOrder.FightPOD.Attacker.Buffs.Count; i++)
        {
            AttackTroopUnitVO.GetBuffManager().AddBuff(AttackTroopUnitVO, null, initOrder.FightPOD.Attacker.Buffs[i]);
        }

        for (int i = 0; i < initOrder.FightPOD.Defender.Buffs.Count; i++)
        {
            DefendTroopUnitVO.GetBuffManager().AddBuff(DefendTroopUnitVO, null, initOrder.FightPOD.Defender.Buffs[i]);
        }

        // 初始化瓦块BUFF
        foreach (KeyValuePair<int, BattleTileVO> kv in _allBattleTile)
        {
            kv.Value.InitBuff();
        }

        //初始化战斗单位的buff
        foreach (KeyValuePair<int, BattleUnitVO> kv in _allBattleUnits)
        {
            kv.Value.InitBuff();
        }

        StartTriggerStep(typeof(BattleInitCompleteBuffTrigger));
        // 队伍
        AttackTroopUnitVO.GetBuffManager().TriggerBuff(typeof(BattleInitCompleteBuffTrigger));
        DefendTroopUnitVO.GetBuffManager().TriggerBuff(typeof(BattleInitCompleteBuffTrigger));

        // // 瓦片
        // foreach (KeyValuePair<int, BattleTileVO> kv in _allBattleTile)
        // {
        //     kv.Value.GetBuffManager().TriggerBuff(typeof(BattleInitCompleteBuffTrigger));
        // }

        // 英雄
        foreach (KeyValuePair<int, BattleUnitVO> kv in _allBattleUnits)
        {
            kv.Value.GetBuffManager().TriggerBuff(typeof(BattleInitCompleteBuffTrigger));
        }

        EndTriggerStep();
        _updateUnits = null;


        // 初始先手
        AttackFirstMove = AttackTroopUnitVO.GetSelfBattleAttribute(BattleConstant.Attribute.TYPE_SPEED) >=
                          DefendTroopUnitVO.GetSelfBattleAttribute(BattleConstant.Attribute.TYPE_SPEED);

        // 初始化出手顺序
        _InitRoundActionOrders();
        this.CurrentBigRound = initOrder.FightPOD.CurrentBigRound;
        this.MaxBigRound = initOrder.FightPOD.MaxBigRound;

        // SendCommand(new BattleInitCommand(initOrder.FightPOD.MapID, initOrder.FightPOD.MaxRound, _fightID, _battleType,
        //     initOrder.FightPOD.MonsterTeamID, _attackers.ConvertAll(s => s.ToData()),
        //     _defensers.ConvertAll(s => s.ToData()), AttackTroopUnitVO.ToData(), DefendTroopUnitVO.ToData(),
        //     _allBattleTile.Values.ToList<BattleTileVO>().ConvertAll(s => s.ToData()),
        //     _unitActionOrders.ConvertAll(s => s.ID), backTeam, initOrder.FightPOD.CurrentBigRound,
        //     initOrder.FightPOD.MaxBigRound));
        
        initOrder.FightPOD = null; //这里需要置空，就不会存储到mazeSave中
        return true;
    }

    public void AddUnit(BattleUnitVO unit)
    {
        if (unit.TroopType == BattleConstant.TroopType.ATTACK)
        {
            _attackers.Add(unit);
        }

        if (unit.TroopType == BattleConstant.TroopType.DEFEND)
        {
            _defensers.Add(unit);
        }

        _allBattleUnits.Add(unit.ID, unit);
    }

    public Dictionary<int, BattleCallVO> AllBattleCalls()
    {
        return _allBattleCalls;
    }

    public void RemoveUnit(BattleUnitVO unit)
    {
        if (unit.TroopType == BattleConstant.TroopType.ATTACK)
        {
            _attackers.Remove(unit);
        }

        if (unit.TroopType == BattleConstant.TroopType.DEFEND)
        {
            _defensers.Remove(unit);
        }

        _allBattleUnits.Remove(unit.ID);
    }

    /// <summary>
    /// 所有战斗单位
    /// </summary>
    /// <returns></returns>
    public Dictionary<int, BattleUnitVO> GetAllBattleUnits()
    {
        return _allBattleUnits;
    }


    /// <summary>
    /// 所有战斗单位
    /// </summary>
    /// <returns></returns>
    public Dictionary<int, BattleTileVO> GetAllBattleTiles()
    {
        return _allBattleTile;
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

        // 添加是否检查战斗结束，避免每帧执行检查(移动被攻击死亡)
        if (_currentFrame < _actionFrame && !CheckFight)
        {
            return false;
        }

        CheckFight = false;
        //检查战斗结果
        int fightResult = _CheckFightResult();
        if (fightResult != BattleConstant.FightResult.NOT_END)
        {
            bool gameOver = true;
            foreach (KeyValuePair<int, BattleCallVO> kv in _allBattleCalls)
            {
                // 还有未生效的替补怪 
                if (kv.Value.CallStatus == 0 && kv.Value.CreateType == 2)
                {
                    gameOver = false;
                    break;
                }
            }

            if (gameOver)
            {
                Debug.Log("-------------- 战斗结束 ---------------" + fightResult);
                SendCommand(_GetBattleOverCommand(fightResult));
                return true;
            }
            else
            {
                Debug.Log("-------------- 强制回合结束 ---------------" + fightResult);
                _RoundEnd();
                return false;
            }
        }

        if (_currentFrame < _actionFrame)
        {
            return false;
        }

        //检查回合开始
        if (_CheckRoundStart())
        {
            _RoundStart();
            return false;
        }


        // 手动修改战斗顺序

        int moverUnitId = 0;

        // 攻击
        if (CurrCastInfo != null)
        {
            moverUnitId = CurrCastInfo.UnitID;
        }

        // 移动
        if (CurrMoveInfo != null)
        {
            moverUnitId = CurrMoveInfo.UnitID;
        }

        // 休息
        if (CurrRestInfo != null)
        {
            moverUnitId = CurrRestInfo.UnitID;
        }


        if (moverUnitId != 0)
        {
            BattleUnitVO unit = null;

            if (moverUnitId == AttackTroopUnitVO.ID)
            {
                unit = AttackTroopUnitVO;
            }
            else if (moverUnitId == DefendTroopUnitVO.ID)
            {
                unit = DefendTroopUnitVO;
            }
            else
            {
                unit = _allBattleUnits[moverUnitId];
            }

            // 死亡 || 已行动 || 晕眩
            if (!unit.IsAction() || unit.GetSpStatus(BattleConstant.SPStatus.ACTION))
            {
                //Logger.Error("select action unit id = {0} error", moverUnitId);
                CurrCastInfo = null;
                CurrMoveInfo = null;
                CurrRestInfo = null;
                // unit.SetSpStatus(BattleConstant.SPStatus.ACTION, true);
                return false;
            }

            CurrMover = unit;
            Debug.Log($"moverUnitId = {moverUnitId} ");
        }

        //选取行动单位
        if (CurrMover == null)
        {
            _actionOrderIndex = 0;
            while (_actionOrderIndex < _unitActionOrders.Count)
            {
                BattleUnitVO unit = _unitActionOrders[_actionOrderIndex];
                _actionOrderIndex++;
                // if (unit.IsAction() && !unit.CurrTurnRoundAction)
                if (unit.IsAction() && !unit.GetSpStatus(BattleConstant.SPStatus.ACTION) &&
                    unit.ScopeType != BattleConstant.ScopeType.TEAM)
                {
                    CurrMover = unit;
                    break;
                }
            }

            if (CurrMover == null)
            {
                //回合结束
                _RoundEnd();
                return false;
            }

            //Debug.Log("CurrMover.Player1 = {0} {1} {2} {3}", CurrMover.Player, CurrMover.ID,CurrMover.GetSpStatus(BattleConstant.SPStatus.ACTION), _actionOrderIndex);


            //设置自动战斗
            if (CurrMover.Player != null && !Quickly && CurrCastInfo == null && CurrMoveInfo == null &&
                CurrRestInfo == null)
            {
                _actionFrame =
                    _currentFrame + TimeUtil.TimeToFrame(BattleConstant.Frame.FRAME_RATE, 3600); //玩家选择技能,最多等待客户端3600s
                return false;
            }
        }

        // 出手标识

        //Debug.Log("CurrMover.Player2 = {0} {1} {2} {3}", CurrMover.Player, CurrMover.ID,CurrMover.GetSpStatus(BattleConstant.SPStatus.ACTION), _actionOrderIndex);

        CurrMover.ActionStart();
        BattleTurnCommand _currTurn = new BattleTurnCommand();
        _currTurn.MoverID = CurrMover.ID;
        _currTurn.BeforeActionPOD = new BattleBeforeActionPOD();
        _currTurn.BeforeActionPOD.UpdateUnits = new List<BattleUpdateUnitPOD>();
        _currTurn.InActionPOD = new BattleInActionPOD();
        _currTurn.InActionPOD.CastSkills = new List<BattleCastSkillPOD>();
        _currTurn.AfterActionPOD = new BattleAfterActionPOD();
        _currTurn.AfterActionPOD.UpdateUnits = new List<BattleUpdateUnitPOD>();
        _updateUnits = _currTurn.BeforeActionPOD.UpdateUnits;
        CurrMover.SetSpStatus(BattleConstant.SPStatus.ACTION, true);

        if (CurrMover.TroopType != _unitActionOrders.First().TroopType && !_roundToggle)
        {
            SendCommand(new BattleChooseSkillCommand(CurrMover.ID, CurrMover.TroopType));
            _roundToggle = true;
            // 队伍回合切换
            foreach (KeyValuePair<int, BattleUnitVO> kv in _allBattleUnits)
            {
                if (kv.Value.TroopType == CurrMover.TroopType)
                {
                    // 我方回合开始
                    Debug.Log(kv.Value.TroopType + " -> 我方回合开始 Action");
                    kv.Value.GetBuffManager().TriggerBuff(typeof(BattleRoundStartBuffTrigger), _round, 1);
                    // 敌方回合结束
                    Debug.Log(kv.Value.TroopType + " -> 敌方回合结束 Action");
                    kv.Value.GetBuffManager().TriggerBuff(typeof(BattleRoundEndBuffTrigger), _round, 2);
                }
                else
                {
                    // 我方回合结束
                    Debug.Log(kv.Value.TroopType + " -> 我方回合结束 Action");
                    kv.Value.GetBuffManager().TriggerBuff(typeof(BattleRoundEndBuffTrigger), _round, 1);
                    // 敌方回合开始
                    Debug.Log(kv.Value.TroopType + " -> 敌方回合开始 Action");
                    kv.Value.GetBuffManager().TriggerBuff(typeof(BattleRoundStartBuffTrigger), _round, 2);
                }
            }
        }

        // 非眩晕
        if (CurrMover.IsAction())
        {
            TriggerSkillUnits.Clear();

            CurrMover.GetBuffManager().TriggerBuff(typeof(BattleActionStartBuffTrigger), CurrMover.ActionCount);
            foreach (KeyValuePair<int, BattleUnitVO> kv in _allBattleUnits)
            {
                kv.Value.GetBuffManager().TriggerBuff(typeof(BattleActionStartBuffTrigger2), CurrMover.ActionCount);
            }

            //CurrMover.Tile.GetBuffManager().TriggerBuff(typeof(BattleActionStartBuffTrigger), CurrMover.ActionCount);
            _CheckUnitSpStatus();


            if (CurrMoveInfo != null)
            {
                // 执行移动
                MoveAction(CurrMoveInfo);
            }
            else if (CurrRestInfo != null)
            {
                // 执行休息
                RestAction();
            }
            else
            {
                // 执行攻击
                AttackAction(_currTurn);
                if (_currTurn.InActionPOD.CastSkills.Count > 1 && _currTurn.InActionPOD.CastSkills[0].SkillID == 0)
                {
                    _currTurn.InActionPOD.CastSkills.RemoveAt(0);
                }
            }

            //行动结束
            _updateUnits = _currTurn.AfterActionPOD.UpdateUnits;
            CurrMover.GetBuffManager().TriggerBuff(typeof(BattleActionEndBuffTrigger), CurrMover.ActionCount);
            foreach (KeyValuePair<int, BattleUnitVO> kv in _allBattleUnits)
            {
                kv.Value.GetBuffManager().TriggerBuff(typeof(BattleActionEndBuffTrigger2), CurrMover.ActionCount);
            }

            _CheckUnitSpStatus();
        }


        // CheckCall();
        CurrMover.ActionOver();
        // 处理召唤物
        CheckCall();
        // 怪物不需要行动完成指令
        //if (CurrMover.Player != null) {
        //    _actionFrame = _currentFrame + TimeUtil.TimeToFrame(BattleConstant.Frame.FRAME_RATE, 30);//表现回合数据,最多等待客户端30s
        //    _checkActionFrame = _currentFrame + TimeUtil.TimeToFrame(BattleConstant.Frame.FRAME_RATE, (Fix64)0.9 / 2) - 3;// 2倍速,再多3帧的保险,0.9为出手间隔
        //    foreach (BattleCastSkillPOD skill in _currTurn.InActionPOD.CastSkills)
        //    {
        //        if (skill.MainSkillID == 0)
        //        {
        //            //主技能才算时间
        //            _checkActionFrame += TimeUtil.TimeToFrame(BattleConstant.Frame.FRAME_RATE, skill.TotalTime / 2);// 2倍速
        //        }
        //    }
        //}


        _waitAction = CurrMover.ID;
        CurrMover = null;
        CurrCastInfo = null;
        CurrMoveInfo = null;
        CurrRestInfo = null;
        CurrAnchorPoint = null;
        SendCommand(_currTurn);
        _actionFrame = _currentFrame + TimeUtil.TimeToFrame(BattleConstant.Frame.FRAME_RATE, 30); //表现回合数据,最多等待客户端30s
        _checkActionFrame =
            _currentFrame + TimeUtil.TimeToFrame(BattleConstant.Frame.FRAME_RATE, (Fix64)0.9 / 2) -
            3; // 2倍速,再多3帧的保险,0.9为出手间隔
        foreach (BattleCastSkillPOD skill in _currTurn.InActionPOD.CastSkills)
        {
            if (skill.MainSkillID == 0)
            {
                //主技能才算时间
                _checkActionFrame += TimeUtil.TimeToFrame(BattleConstant.Frame.FRAME_RATE, skill.TotalTime / 2); // 2倍速
            }
        }

        _updateUnits = null;
        return false;
    }

    private void RestAction()
    {
        CurrMover.GetBuffManager().TriggerBuff(typeof(BattleUnitWaitBuffTrigger));
    }

    private void AttackAction(BattleTurnCommand _currTurn)
    {
        // 手动选择的技能
        if (CurrCastInfo == null)
        {
            //设置自动战斗
            CurrCastInfo = new CastSkillOrder();
            CurrCastInfo.UnitID = CurrMover.ID;
            CurrCastInfo.SkillCid = 0;
        }

        //CurrCastInfo.SkillCid = 0;

        if (CurrCastInfo.UnitID != CurrMover.ID)
        {
            Debug.LogError($"CastSkill unit id = {CurrCastInfo.UnitID}, CurrMover id = {CurrMover.ID}");
            //修正操作
            CurrCastInfo.UnitID = CurrMover.ID;
            CurrCastInfo.SkillCid = 0;
            CurrCastInfo.Targets = null;
        }


        // 选择技能
        BattleSkillVO actionSkill = ChooseSkill();

        if (actionSkill != null)
        {
            // 查看攻击距离


            // 添加触发技能单位
            TriggerSkillUnits.Enqueue(new KeyValuePair<BattleUnitVO, BattleSkillVO>(CurrMover, actionSkill));
        }
        else
        {
            //待机 即pass
            if (!CurrMover.FightStatus[BattleConstant.Status.SEAL_STAND_BY])
            {
                /* 根据策划，待机（本回合不出手）并不会影响下回合的出手顺序！
                int standByCount = 0;
                foreach(BattleUnitVO unit in _allBattleUnits.Values)
                {
                    if(unit.StandByIndex > 0)
                    {
                        standByCount++;
                    }
                }
                CurrMover.StandByIndex = standByCount + 1; */
            }

            //待机需要加入一个空的技能
            BattleCastSkillPOD emptyCastSkill = new BattleCastSkillPOD();
            emptyCastSkill.CasterID = CurrMover.ID;
            emptyCastSkill.SkillID = -1;
            _currTurn.InActionPOD.CastSkills.Add(emptyCastSkill);
        }

        //施放技能阶段，包括反击，连击，亡语等技能，上限100次
        LoopSkill(_currTurn, actionSkill);
    }

    public void LoopSkill(BattleTurnCommand _currTurn, BattleSkillVO actionSkill)
    {
        int castSkillCount = 0;
        while (TriggerSkillUnits.Count > 0)
        {
            castSkillCount++;
            // if (castSkillCount > 100 || _CheckAnyTroopDead())
            if (castSkillCount > 100)
            {
                //达到施放技能次数上限,或者任意阵营死亡
                TriggerSkillUnits.Clear();
                break;
            }

            // 获得一个技能触发单位
            KeyValuePair<BattleUnitVO, BattleSkillVO> item = TriggerSkillUnits.Dequeue();
            BattleUnitVO unit = item.Key;
            BattleSkillVO skill = item.Value;
            IsActionSkill = skill == actionSkill && castSkillCount == 1;

            // 检查技能是否被封印
            bool isSeal = skill.IsSeal;

            if (isSeal)
            {
                if (IsActionSkill)
                {
                    //如果是当前行动的角色技能被封印,加入一个空的技能
                    BattleCastSkillPOD emptyCastSkill = new BattleCastSkillPOD();
                    emptyCastSkill.CasterID = unit.ID;
                    _currTurn.InActionPOD.CastSkills.Add(emptyCastSkill);
                    unit.GetBuffManager().TriggerBuff(typeof(BattleSkillGroupOverTrigger), skill);
                }
            }
            else
            {
                //重置技能CD
                if (IsActionSkill)
                {
                    // 修改技能CD

                    unit.CommonSkillCD = skill.SkillData.CommonCD;
                    AddUpdateUnit(unit, BattleConstant.UpdateType.CHANGE_COMMON_CD, unit.CommonSkillCD);

                    skill.CoolDown = skill.SkillData.CoolDown;
                    //AddUpdateUnit(unit, BattleConstant.UpdateType.CHANGE_SKILL_CD, skill.CfgSkillData.Id,skill.CoolDown);

                    if (CurrCastInfo.SkillCid == 0)
                    {
                        //自动战斗，设置ai的cd
                        skill.AICoolDown = skill.SkillData.AICoolDown;
                    }
                }

                //选择目标
                List<BattleUnitVO> targetRange = new List<BattleUnitVO>();
                // if (skill.SkillData.GetTargetTypeData().SearchScope == 0)
                // {
                //     targetRange.AddRange(_allBattleUnits.Values);
                // }
                // else
                // {
                    targetRange.AddRange(_allBattleTile.Values);
                //}


                // 取得指定锚点
                if (CurrCastInfo != null && CurrCastInfo.Targets != null && CurrCastInfo.Targets.Count > 0)
                {
                    // if (skill.SkillData.GetTargetTypeData().SearchScope == 0)
                    // {
                    //     CurrAnchorPoint = _allBattleTile[CurrCastInfo.Targets[0]].BattleUnitVO;
                    // }
                    // else
                    // {
                        CurrAnchorPoint = _allBattleTile[CurrCastInfo.Targets[0]];
                    //}
                }

                if (skill == actionSkill &&
                    CurrCastInfo.SkillCid != 0) //因为再次触发主技能，是需要限定玩家指定的目标，所以用skill == actionSkill而不用IsActionSkill
                {
                    // if (CurrAnchorPoint == null || !CurrAnchorPoint.IsSelect())
                    // {
                    //     CurrAnchorPoint =
                    //         PointAI.getAnchorPoint(unit, skill.SkillData.GetTargetTypeData(), targetRange);
                    // }


                    //如果是再次触发主技能，玩家指定的目标全部死亡，就走ai
                    if (!IsActionSkill)
                    {
                        // AI选择锚点
                        // if (CurrAnchorPoint == null || !CurrAnchorPoint.IsSelect())
                        // {
                        //     CurrAnchorPoint =
                        //         PointAI.getAnchorPoint(unit, skill.SkillData.GetTargetTypeData(), targetRange);
                        // }
                        
                    }
                }
                else
                {

                    // AI选择锚点
                    // if (CurrAnchorPoint == null || !CurrAnchorPoint.IsSelect())
                    // {
                    //     CurrAnchorPoint =
                    //         PointAI.getAnchorPoint(unit, skill.SkillData.GetTargetTypeData(), targetRange);
                    // }
                }

                // if (CurrAnchorPoint == null)
                // {
                //     CurrAnchorPoint = PointAI.getAnchorPoint(unit, skill.SkillData.GetTargetTypeData(), targetRange);
                // }

                // 释放技能
                _currTurn.InActionPOD.CastSkills.AddRange(_CastMainSkill(unit, skill, targetRange));
            }
        }
    }

    /// <summary>
    /// AI大招行为
    /// </summary>
    private void StatusSwitchAction(StatusSwitchOrder order)
    {
        var vo = _allBattleUnits[order.UnitID];
        vo.AIType = order.Status;
        AddUpdateUnit(vo, BattleConstant.UpdateType.UPDATE_AI_TYPE, vo.AIType);
    }

    /// <summary>
    /// 移动行为
    /// </summary>
    private void MoveAction(MovePosOrder order)
    {
        var vo = _allBattleUnits[order.UnitID];
        // 禁止移动
        if (vo.FightStatus[BattleConstant.Status.MOVE])
        {
            return;
        }

        BattleTileVO newTile = GetAllBattleTiles()
            .Where(t => (t.Value.BattlePos == order.BattlePos && t.Value.TroopType == vo.TroopType)).First().Value;
        if (newTile.BattleUnitVO == null)
        {
            vo.MovePos(order.BattlePos);
        }
        // else
        // {
        //     RestAction();
        // }
    }

    /// <summary>
    /// 选择技能
    /// </summary>
    /// <returns></returns>
    private BattleSkillVO ChooseSkill()
    {
        if (CurrCastInfo.SkillCid == -1)
        {
            //待机 即pass
            return null;
        }
        else if (CurrCastInfo.SkillCid == 0)
        {
            //AI
            if (CurrMover.TroopType == BattleConstant.TroopType.ATTACK)
            {
                IsAutoFight = true;
            }

            return CurrMover.ChooseSkill();
        }
        else
        {
            BattleSkillVO skill = null;
            //手动
            // if (CurrMover.NormalSkill != null && CurrMover.NormalSkill.CfgSkillData.Id == CurrCastInfo.SkillCid)
            // {
            //     skill = CurrMover.NormalSkill;
            // }
            //
            // if (CurrMover.UltimateSkill != null && CurrMover.UltimateSkill.CfgSkillData.Id == CurrCastInfo.SkillCid)
            // {
            //     skill = CurrMover.UltimateSkill;
            // }
            //
            // // cost技能 or 代理人技能
            // if (skill == null)
            // {
            //     skill = CurrMover.AllSkills.Find(s => s.CfgSkillData.Id == CurrCastInfo.SkillCid);
            // }

            if (skill.ReleaseCountLimit() &&
                skill.SkillData.GetCostEnergy(CurrMover.TroopType) <= CurrMover.SkillEnergy && skill.CoolDown < 1 &&
                CurrMover.CommonSkillCD < 1 && !skill.IsSeal)
            {
                return skill;
            }

            if (CurrMover.NormalSkill != null && CurrMover.NormalSkill.IsSeal)
            {
                return null;
            }

            CurrCastInfo.SkillCid = 0;
            return CurrMover.NormalSkill;
        }
    }

    // /// <summary>
    // /// 检查技能是否被封印
    // /// </summary>
    // /// <param name="caster"></param>
    // /// <param name="skillType"></param>
    // /// <returns></returns>
    // private bool _CheckSealSkill(BattleUnitVO caster, int skillType)
    // {
    //     switch (skillType)
    //     {
    //         case SkillConstant.TYPE_DEADTH_RATTLE:
    //             //亡语，检查是否被封印，并且必须是死亡状态
    //             if (caster.FightStatus[BattleConstant.Status.SEAL_DEAD_RATTLE] || !caster.IsDead())
    //             {
    //                 return true;
    //             }
    //             break;
    //         case SkillConstant.TYPE_BEAT_BACK:
    //             //反击，检查是否被封印，并且必须存活
    //             if (caster.FightStatus[BattleConstant.Status.SEAL_BEAT_BACK] || caster.IsDead())
    //             {
    //                 return true;
    //             }
    //             break;
    //         case SkillConstant.TYPE_NORMAL:
    //             //普攻是否被封印，并且必须存活
    //             if (caster.FightStatus[BattleConstant.Status.SEAL_NORMAL] || caster.IsDead())
    //             {
    //                 return true;
    //             }
    //             break;
    //         //case SkillConstant.TYPE_CORE:
    //         //    //核心，检查是否被封印，并且必须存活
    //         //    if (caster.FightStatus[BattleConstant.Status.SEAL_CORE] || caster.IsDead())
    //         //    {
    //         //        return true;
    //         //    }
    //         //    break;
    //         //case SkillConstant.TYPE_CORE_2:
    //         //    //核心2，检查是否被封印，并且必须存活
    //         //    if (caster.FightStatus[BattleConstant.Status.SEAL_CORE_2] || caster.IsDead())
    //         //    {
    //         //        return true;
    //         //    }
    //         //    break;
    //         case SkillConstant.TYPE_DERIVATION:
    //             //衍生技能，检查是否被封印，并且必须存活
    //             if (caster.FightStatus[BattleConstant.Status.SEAL_DERIVATION] || caster.IsDead())
    //             {
    //                 return true;
    //             }
    //             break;
    //         case SkillConstant.TYPE_ULTIMATE:
    //             //大招，检查是否被封印，并且必须存活
    //             if (caster.FightStatus[BattleConstant.Status.SEAL_ULTIMATE] || caster.IsDead())
    //             {
    //                 return true;
    //             }
    //             break;
    //         default:
    //             break;
    //     }
    //     return false;
    // }

    /// <summary>
    /// 施放主技能
    /// </summary>
    /// <param name="caster"></param>
    /// <param name="skill"></param>
    /// <param name="targetRange">限定目标</param>
    /// <returns></returns>
    private List<BattleCastSkillPOD> _CastMainSkill(BattleUnitVO caster, BattleSkillVO skill,
        List<BattleUnitVO> targetRange)
    {
        CurrSkillCaster = caster;
        CurrMainSkill = skill;
        caster.GetFightAttribute()[BattleConstant.Attribute.TYPE_LAST_HURT] = 0;
        List<BattleCastSkillPOD> allSkillCastList = new List<BattleCastSkillPOD>();
        BattleCastSkillPOD mainSkillCast = _CastSkill(caster, skill, targetRange);
        //主技能
        allSkillCastList.Add(mainSkillCast);
        //子技能
        foreach (BattleSkillVO subSkill in skill.SubSkills)
        {
            // //跟随主技能目标
            // if (subSkill.SkillData.GetTargetTypeData().SelectCamp == BattleConstant.SearchTarget.TYPE_FOLLOW_MAIN_SKILL)
            // {
            //     subSkill.SpecifyTargets = new List<BattleUnitVO>();
            //     foreach (BattleSkillHitPOD hit in mainSkillCast.SkillHits)
            //     {
            //         subSkill.SpecifyTargets.Add(hit.Scope == 0
            //             ? _allBattleUnits[hit.UnitID]
            //             : _allBattleTile[hit.UnitID]);
            //     }
            // }
            // else
            // {
            //     subSkill.SpecifyTargets = null;
            // }
            //
            // List<BattleUnitVO> tmp = new List<BattleUnitVO>();
            // if (subSkill.SkillData.GetTargetTypeData().SearchScope == 0)
            // {
            //     tmp.AddRange(_allBattleUnits.Values);
            // }
            // else
            // {
            //     tmp.AddRange(_allBattleTile.Values);
            // }

            // BattleCastSkillPOD battleCastSkillPod = _CastSkill(caster, subSkill, tmp);
            // if (battleCastSkillPod != null)
            // {
            //     allSkillCastList.Add(battleCastSkillPod);
            // }
        }

        //临时子技能
        //临时子技能可能增加临时子技能，所以只能用fori循环
         for (int i = 0; i < skill.TmpSubSkills.Count; i++)
         {
             // BattleSkillVO subSkill = skill.TmpSubSkills[i];
             // //跟随主技能目标
             // if (subSkill.SkillData.GetTargetTypeData().SelectCamp == BattleConstant.SearchTarget.TYPE_FOLLOW_MAIN_SKILL)
             // {
             //     subSkill.SpecifyTargets = new List<BattleUnitVO>();
             //     foreach (BattleSkillHitPOD hit in mainSkillCast.SkillHits)
             //     {
             //         subSkill.SpecifyTargets.Add(hit.Scope == 0
             //             ? _allBattleUnits[hit.UnitID]
             //             : _allBattleTile[hit.UnitID]);
             //     }
             // }
             // else
             // {
             //     subSkill.SpecifyTargets = null;
             // }
             //
             // List<BattleUnitVO> tmp = new List<BattleUnitVO>();
             // if (skill.SkillData.GetTargetTypeData().SearchScope == 0)
             // {
             //     tmp.AddRange(_allBattleUnits.Values);
             // }
             // else
             // {
             //     tmp.AddRange(_allBattleTile.Values);
             // }
             //
             // BattleCastSkillPOD battleCastSkillPod = _CastSkill(caster, subSkill, tmp);
             // if (battleCastSkillPod != null)
             // {
             //     allSkillCastList.Add(battleCastSkillPod);
             // }
         }

        // 本次攻击的所有目标
        HashSet<BattleUnitVO> targets = new HashSet<BattleUnitVO>();
        // 本次攻击的所有真实命中的目标
        HashSet<BattleUnitVO> hitTargets = new HashSet<BattleUnitVO>();

        //自己释放技能后目标特殊状态
        foreach (BattleCastSkillPOD castSkill in allSkillCastList)
        {
            foreach (BattleSkillHitPOD hit in castSkill.SkillHits)
            {
                targets.Add(hit.Scope == 0 ? _allBattleUnits[hit.UnitID] : _allBattleTile[hit.UnitID]);
                if (hit.Hit && !hit.Immunity)
                {
                    hitTargets.Add(hit.Scope == 0 ? _allBattleUnits[hit.UnitID] : _allBattleTile[hit.UnitID]);
                }
            }
        }

        // 添加元素BUFF
        // CfgElementBuffData cfgElementBuffData = CfgElementBuffTable.Instance.GetDataByID(CurrMainSkill.GetElement());
        // if (cfgElementBuffData != null && cfgElementBuffData.ElementBuff != 0)
        // {
        //     foreach (BattleUnitVO target in hitTargets)
        //     {
        //         // if (target.IsDead())
        //         // {
        //         //         // 作用域不符 || 已死亡
        //         //         continue;
        //         // }
        //         // CfgBuffData addBuffCfg = CfgBuffTable.Instance.GetDataByID(cfgElementBuffData.ElementBuff);
        //         // if (target.IsDead() || addBuffCfg.Scope != target.GetScope())
        //         // if (addBuffCfg.Scope != target.GetScope())
        //         // {
        //         //     // 作用域不符 || 已死亡
        //         //     continue;
        //         // }
        //
        //         if (Random.randomFix64() < cfgElementBuffData.BuffProbability)
        //         {
        //             target.GetBuffManager().AddBuffInner(caster, CurrMainSkill.SkillData,
        //                 cfgElementBuffData.ElementBuff, cfgElementBuffData.BuffTime, cfgElementBuffData.BuffStackNum);
        //         }
        //     }
        // }
        //
        // // 触发弱点
        // foreach (BattleUnitVO target in hitTargets)
        // {
        //     target.TriggerWeak(caster, BattleConstant.WeakType.ELEMENT_ATTACK, CurrMainSkill.GetElement());
        //     target.TriggerWeak(caster, BattleConstant.WeakType.ARMS_TYPE, caster.CfgMonsterData.ArmsType);
        // }


        // 技能组释放完毕触发
        caster.GetBuffManager().TriggerBuff(typeof(BattleSkillGroupOverTrigger), skill);
        foreach (BattleUnitVO unit in CurrSkillAllTargets)
        {
            unit.GetBuffManager().TriggerBuff(typeof(BattleBeSkillGroupOverTrigger), skill);
        }

        //检查状态
        _CheckUnitSpStatus();

        foreach (BattleUnitVO target in targets)
        {
            for (int i = 0; i < BattleConstant.SPStatus.SPSTATUS_NUM; i++)
            {
                if (target.GetSpStatus(i))
                {
                    //按主技能属性触发，目标是所有技能
                    caster.GetBuffManager().TriggerBuff(typeof(BattleCastSkillEndTargetSPStatusBuffTrigger), skill,
                        target, i);
                }
            }
        }

        //技能组施放完毕，触发移除
        foreach (BattleUnitVO unit in _allBattleUnits.Values)
        {
            unit.GetBuffManager().TriggerRemove(BuffConstant.BUFF_REMOVE_TRIGGER_TYPE_CAST_ALL_SKILL_OVER);
        }

        //检查状态
        _CheckUnitSpStatus();

        CurrSkillCaster = null;
        CurrMainSkill = null;
        CurrSkillAllTargets.Clear();
        skill.TmpSubSkills.Clear();
        return allSkillCastList;
    }


    /// <summary>
    /// 施放技能内部逻辑
    /// </summary>
    /// <param name="caster"></param>
    /// <param name="skill"></param>
    /// <param name="targetRange">限定目标</param>
    private BattleCastSkillPOD _CastSkill(BattleUnitVO caster, BattleSkillVO skill, List<BattleUnitVO> targetRange)
    {
        //选取目标
        List<BattleUnitVO> targets = skill.SpecifyTargets;
        skill.SpecifyTargets = null;
        //if (targets == null)
        //{
            //if (skill.SkillData.GetTargetTypeData() != null)
            //{
                //var SelectCamp = skill.SkillData.TargetTypeData.SelectCamp;
                //caster.FightStatus[BattleConstant.Status.CHAOS] = true;
                //if (SelectCamp > 10) //11 12
                //{


                //    // 是否是敌对
                //    bool isHostile = SelectCamp == BattleConstant.SearchTarget.TYPE_PATTERN_HOSTILE;

                //    // 魅惑
                //    if (caster.FightStatus[BattleConstant.Status.CHARM]) {
                //        isHostile = !isHostile;
                //    }

                //    //List<BattleUnitVO> list = new List<BattleUnitVO>(_allBattleTile.Values);

                //    List<BattleUnitVO> list = new List<BattleUnitVO>();
                //    if (skill.CfgSkillData.Scope == 0)
                //    {
                //        list.AddRange(_allBattleUnits.Values);
                //    }
                //    else
                //    {
                //        list.AddRange(_allBattleTile.Values);
                //    }


                //    // 选择敌对 or 友方所有单位
                //    var t = list.FindAll(vo => {
                //        // 混乱
                //        if (caster.FightStatus[BattleConstant.Status.CHAOS])
                //        {
                //            // 所有
                //            return true;
                //        }
                //        else {
                //            return (isHostile && vo.TroopType != caster.TroopType) || (!isHostile && vo.TroopType == caster.TroopType);
                //        }
                //    });
                //    list.Clear();
                //    list.AddRange(targetRange);
                //    //TYPE_PATTERN模式下，第一个元素为玩家选择的目标(center)，后面传入对方或己方所有元素用以筛选。
                //    t.Insert(0, list[0]); 
                //    targets = SearchTarget.SearchSkillTargets(caster, skill, t, true);
                //}
                //else
                //targets = SearchTarget.SearchSkillTargets(caster, skill, targetRange);
            //}
            //else
            //{
                //targets = new List<BattleUnitVO>();
            //}
        //}

        //Debug.Log("targets = {0}", targets.Count);
        //去掉死亡单位
        for (int i = targets.Count - 1; i >= 0; i--)
        {
            if (targets[i].IsDead())
            {
                targets.RemoveAt(i);
            }
        }


        //Debug.Log("UnitPOD={0}, Cast Skill={1}",caster.TroopType == BattleConstant.TroopType.DEFEND ? 10 + caster.BattlePos : caster.BattlePos,skill.CfgSkillData.Id);


        BattleCastSkillPOD castSkillPOD = new BattleCastSkillPOD();
        castSkillPOD.CasterID = caster.ID;
        castSkillPOD.BeforeUpdateUnits = new List<BattleUpdateUnitPOD>();
        castSkillPOD.AfterUpdateUnits = new List<BattleUpdateUnitPOD>();
        castSkillPOD.SkillHits = new List<BattleSkillHitPOD>();
        //castSkillPOD.SkillID = skill.CfgSkillData.Id;
        castSkillPOD.DelayTime = skill.DelayTime;
        //castSkillPOD.MainSkillID = skill.MainSkill == null ? 0 : skill.MainSkill.CfgSkillData.Id;
        // CfgSkillActionData skillActionData = caster.TroopType == BattleConstant.TroopType.ATTACK ? skill.CfgAttackerActionData : skill.CfgDefenderActionData;
        // CfgSkillActionData skillActionData = skill.CfgAttackerActionData;
        // castSkillPOD.SkillAction = skillActionData == null ? 0 : skillActionData.Id;
        // castSkillPOD.TotalTime = skillActionData == null ? 0 : skillActionData.TotalTime;
        _updateUnits = castSkillPOD.BeforeUpdateUnits;

        //技能准备施放buff
        for (int j = 0; j < skill.SkillData.BuffTarget.Count; j++)
        {
            if (skill.SkillData.BuffOrder[j] == 5 && Random.randomFix64() < skill.SkillData.BuffProbability[j])
            {
                caster.GetBuffManager().AddBuff(skill.SkillData.BuffTarget[j], caster, skill.SkillData,
                    skill.SkillData.BuffID[j], skill.SkillData.BuffTime[j], skill.SkillData.BuffStackNum[j]);
            }
        }

        if (targets.Count < 1)
        {
            //Logger.Warning("targets is null");
            return castSkillPOD;
        }

        CurrSkill = skill;
        //施放技能扣怒气
        if (IsActionSkill && !skill.IsSubSkill)
        {
            // 消耗个人怒气
            caster.ChangeSkillEnergy(-skill.SkillData.GetCostEnergy(caster.TroopType));

            // 消耗队伍AP
            var troopVO = caster.TroopType == AttackTroopUnitVO.TroopType ? AttackTroopUnitVO : DefendTroopUnitVO;
            troopVO.ChangeAP(-skill.SkillData.NeedAp);

            // 修改技能次数
            skill.SkillData.ReleaseCount += 1;
            //AddUpdateUnit(caster, BattleConstant.UpdateType.CHANGE_SKILL_COUNT, skill.CfgSkillData.Id,
                //skill.SkillData.ReleaseCount);

            // 修改技能怒气
            //AddUpdateUnit(caster, BattleConstant.UpdateType.CHANGE_SKILL_ENERGY, skill.CfgSkillData.Id,
                //skill.SkillData.GetCostEnergy(caster.TroopType));
        }


        //施放技能前移除对应标签的buff
        if (skill.SkillData.CastRemoveBuffTag.Length > 0)
        {
            foreach (Buff buff in caster.GetBuffManager().GetAllBuffs())
            {
                foreach (int buffTag in buff.BuffCfg.BuffTag)
                {
                    if (Array.IndexOf(skill.SkillData.CastRemoveBuffTag, buffTag) > -1)
                    {
                        caster.GetBuffManager().RemoveBuff(buff, BuffRemoveReason.CLEAN);
                    }
                }
            }
        }


        //设置当前技能目标,给技能前buff用
        CurrSkillTargets = targets;

        // 本次攻击所有目标
        foreach (var v in CurrSkillTargets)
        {
            CurrSkillAllTargets.Add(v);
        }

        //施放技能前触发
        caster.GetBuffManager().TriggerBuff(typeof(BattleCastSkillStartBuffTrigger), skill);
        //施放技能前施放buff
        for (int j = 0; j < skill.SkillData.BuffTarget.Count; j++)
        {
            if (skill.SkillData.BuffOrder[j] == 1 && Random.randomFix64() < skill.SkillData.BuffProbability[j])
            {
                caster.GetBuffManager().AddBuff(skill.SkillData.BuffTarget[j], caster, skill.SkillData,
                    skill.SkillData.BuffID[j], skill.SkillData.BuffTime[j], skill.SkillData.BuffStackNum[j]);
            }
        }

        //施放技能时增加临时属性
        for (int i = 0; i < skill.SkillData.TemporaryAttType.Count; i++)
        {
            int attType = skill.SkillData.TemporaryAttType[i];
            if (skill.SkillData.TemporaryAttType[i] != 0)
            {
                Fix64 oldValue = 0;
                caster.SkillTemporaryAttrs.TryGetValue(attType, out oldValue);
                caster.SkillTemporaryAttrs.Remove(attType);
                caster.SkillTemporaryAttrs.Add(attType, skill.SkillData.TemporaryAttValue[i] + oldValue);
            }
        }

        List<BattleUnitVO> hitTargets = new List<BattleUnitVO>(); //击中的目标
        //施放技能
        foreach (BattleUnitVO target in targets)
        {
            //BattleUnitVO t = target;
            //if (target.GetType() == typeof(BattleTileVO) && skill.SkillData.CfgSkillData.Scope == 0) {
            //    BattleTileVO tile = (BattleTileVO)target;
            //    if (tile.BattleUnitVO != null)
            //    {
            //        t = tile.BattleUnitVO;
            //    }
            //    else {
            //        continue;
            //    }
            //}
            BattleSkillHitPOD skillHit = _Attack(caster, target, skill);
            if (skillHit.Hit)
            {
                hitTargets.Add(target);
            }

            castSkillPOD.SkillHits.Add(skillHit);
        }

        _updateUnits = castSkillPOD.AfterUpdateUnits;
        CurrSkillTargets = hitTargets; //攻击后技能目标只会选择实际击中的单位
        //施放技能后施放buff
        for (int j = 0; j < skill.SkillData.BuffTarget.Count; j++)
        {
            if (skill.SkillData.BuffOrder[j] == 2 && Random.randomFix64() < skill.SkillData.BuffProbability[j])
            {
                caster.GetBuffManager().AddBuff(skill.SkillData.BuffTarget[j], caster, skill.SkillData,
                    skill.SkillData.BuffID[j], skill.SkillData.BuffTime[j], skill.SkillData.BuffStackNum[j]);
            }
        }

        //加怒气,命中才加，暴击额外加
        if (IsActionSkill)
        {
            bool isHit = false;
            bool isCrit = false;
            foreach (BattleSkillHitPOD skillHitPOD in castSkillPOD.SkillHits)
            {
                isHit |= skillHitPOD.Hit;
                isCrit |= skillHitPOD.Crit;
            }
            //int addEnergy = skill.SkillData.AddEnergy;
            //            if (isCrit)
            //            {
            //	addEnergy = Fix64.ToInt32(addEnergy * (1 + CfgDiscreteDataTable.Instance.GetDataByID(6506025).Data[0] / (Fix64)100));
            //            }
            //caster.ChangeSkillEnergy(addEnergy);
        }

        /**************************各种触发start**********************************/
        {
            //自己释放技能后伤害效果
            foreach (BattleSkillHitPOD skillHitPOD in castSkillPOD.SkillHits)
            {
                if (skillHitPOD.Hit && !skillHitPOD.Immunity)
                {
                    caster.GetBuffManager().TriggerBuff(typeof(BattleCastSkillEndResultBuffTrigger), skill, 0);
                }

                if (skillHitPOD.Crit)
                {
                    caster.GetBuffManager().TriggerBuff(typeof(BattleCastSkillEndResultBuffTrigger), skill, 1);
                }

                if (!skillHitPOD.Hit)
                {
                    caster.GetBuffManager().TriggerBuff(typeof(BattleCastSkillEndResultBuffTrigger), skill, 2);
                }

                if (skillHitPOD.Block)
                {
                    caster.GetBuffManager().TriggerBuff(typeof(BattleCastSkillEndResultBuffTrigger), skill, 3);
                }

                if (skillHitPOD.Immunity)
                {
                    caster.GetBuffManager().TriggerBuff(typeof(BattleCastSkillEndResultBuffTrigger), skill, 4);
                }

                if (skillHitPOD.Hit && !skillHitPOD.Crit)
                {
                    caster.GetBuffManager().TriggerBuff(typeof(BattleCastSkillEndResultBuffTrigger), skill, 5);
                }

                if (skillHitPOD.Hit && !skillHitPOD.Block)
                {
                    caster.GetBuffManager().TriggerBuff(typeof(BattleCastSkillEndResultBuffTrigger), skill, 6);
                }
            }

            //自己释放技能后目标buff状态
            foreach (BattleUnitVO target in targets)
            {
                caster.GetBuffManager().TriggerBuff(typeof(BattleCastSkillEndTargetStatusBuffTrigger), skill, target);
            }

            //施放技能后触发
            caster.GetBuffManager().TriggerBuff(typeof(BattleCastSkillEndBuffTrigger), skill);
        }
        /**************************各种触发end**********************************/


        //施放技能结束施放buff
        for (int j = 0; j < skill.SkillData.BuffTarget.Count; j++)
        {
            if (skill.SkillData.BuffOrder[j] == 3 && Random.randomFix64() < skill.SkillData.BuffProbability[j])
            {
                caster.GetBuffManager().AddBuff(skill.SkillData.BuffTarget[j], caster, skill.SkillData,
                    skill.SkillData.BuffID[j], skill.SkillData.BuffTime[j], skill.SkillData.BuffStackNum[j]);
            }
        }

        //清理当前技能目标
        CurrSkillTargets = null;
        //清理技能临时属性
        caster.SkillTemporaryAttrs.Clear();
        foreach (BattleUnitVO unit in _allBattleUnits.Values)
        {
            unit.GetBuffManager().TriggerRemove(BuffConstant.BUFF_REMOVE_TRIGGER_TYPE_CAST_SKILL_OVER);
        }

        // 添加怒气
        caster.ChangeSkillEnergy(CurrSkill.SkillData.AddEnergy + CurrSkill.SkillData.ChangeADDEnergy);

        CurrSkill = null;
        return castSkillPOD;
    }

    /// <summary>
    /// 添加更新战斗单位数据
    /// </summary>
    /// <param name="unitID"></param>
    /// <param name="type"></param>
    /// <param name="param"></param>
    public void AddUpdateUnit(BattleUnitVO unit, int type, params int[] param)
    {
        AddUpdateUnit(unit, type, null, param);
    }

    public void AddUpdateUnit(BattleUnitVO unit, int type, BattleUnitPOD call, params int[] param)
    {
        // if (unit.GetType() == typeof(BattleTileVO))
        // {
        //     return;
        // }

        if (!_allBattleUnits.ContainsKey(unit.ID)
            && !_allBattleTile.ContainsKey(unit.ID)
            && DefendTroopUnitVO.ID != unit.ID
            && AttackTroopUnitVO.ID != unit.ID)
        {
            //队伍的单位更新

            return;
        }

        if (_updateUnits == null)
        {
            //Logger.Error("AddUpdateUnit unit is null");
            return;
        }

        // 作用域
        int scope = 0;
        if (unit.GetType() == typeof(BattleTileVO))
        {
            scope = 1;
        }
        else if (unit.GetType() == typeof(BattleTroopUnitVO))
        {
            scope = 2;
        }

        //if (type == BattleConstant.UpdateType.UPDATE_BUFF || type == BattleConstant.UpdateType.REMOVE_BUFF)
        //{
        //    return;
        //}
        BattleUpdateUnitPOD battleUpdateUnitPOD = new BattleUpdateUnitPOD(unit.ID, scope, type, param, call);
        //_logger.Info("->" + GameFramework.Utility.Json.ToJson(battleUpdateUnitPOD));
        _updateUnits.Add(battleUpdateUnitPOD);
    }

    ///// <summary>
    ///// 添加更新战斗队伍数据
    ///// </summary>
    ///// <param name="unitID"></param>
    ///// <param name="type"></param>
    ///// <param name="param"></param>
    //public void AddUpdateTroopUnit(int unitID, int type, params int[] param)
    //{
    //    //if (!_allBattleUnits.ContainsKey(unitID))
    //    //{
    //    //    //队伍的单位更新

    //    //    return;
    //    //}
    //    if (_updateUnits == null)
    //    {
    //        Logger.Error("AddUpdateUnit unit is null");
    //        return;
    //    }
    //    BattleUpdateUnitPOD battleUpdateUnitPOD = new BattleUpdateUnitPOD(unitID,1, type, param);
    //    //_logger.Info("->" + GameFramework.Utility.Json.ToJson(battleUpdateUnitPOD));
    //    _updateUnits.Add(battleUpdateUnitPOD);
    //}

    /// <summary>
    /// 攻击
    /// </summary>
    /// <param name="murder"></param>
    /// <param name="victim"></param>
    /// <param name="skill"></param>
    private BattleSkillHitPOD _Attack(BattleUnitVO murder, BattleUnitVO victim, BattleSkillVO skill)
    {
        if (!skill.IsSubSkill)
        {
            victim.GetFightAttribute()[BattleConstant.Attribute.TYPE_CURR_BE_MAIN_SKILL_HURT] = 0;
        }

        victim.GetFightAttribute()[BattleConstant.Attribute.TYPE_CURR_BE_SKILL_HURT] = 0;

        CurrAtkTarget = victim;
        BattleSkillHitPOD skillHitPOD = new BattleSkillHitPOD();
        skillHitPOD.UpdateUnits = new List<BattleUpdateUnitPOD>();
        _updateUnits = skillHitPOD.UpdateUnits;
        skillHitPOD.UnitID = victim.ID;
       // skillHitPOD.Scope = skill.SkillData.GetTargetTypeData().ActionScope;

        murder.GetBuffManager().TriggerBuff(typeof(BattlePreAtkTargetBuffBuffTrigger), skill, victim);
        victim.GetBuffManager().TriggerBuff(typeof(BattlePreBeAtkBuffBuffTrigger), skill);


        // bool hit = SkillFunction.IsHit(murder, victim, skill.CfgSkillFunctionData);
        // skillHitPOD.Hit = hit;
        // // skillHitPOD.DamageType = skill.CfgSkillFunctionData.DamageType;
        // // skillHitPOD.ElementType = skill.GetElement();
        // if (hit)
        // {
        //     bool immunity = SkillFunction.IsImmunity(murder, victim, skill.GetElement(), true);
        //     skillHitPOD.Immunity = immunity;
        //     if (!immunity)
        //     {
        //         // 攻击前触发
        //         murder.GetBuffManager().TriggerBuff(typeof(BattlePreAtkHitBuffTrigger), skill, 0);
        //         // 受攻击前触发
        //         victim.GetBuffManager().TriggerBuff(typeof(BattleBePreAtkHitBuffTrigger), skill, 0);
        //
        //         // 判断格挡
        //         bool isBlock = SkillFunction.IsBlock(murder, victim, skill.CfgSkillFunctionData);
        //         skillHitPOD.Block = isBlock;
        //
        //         // 格挡后不判断暴击
        //         bool isCrit = isBlock ? false : SkillFunction.IsCrit(murder, victim, skill.CfgSkillFunctionData);
        //         skillHitPOD.Crit = isCrit;
        //
        //         // 暴击触发
        //         if (isCrit)
        //         {
        //             murder.GetBuffManager().TriggerBuff(typeof(BattlePreAtkHitBuffTrigger), skill, 1);
        //             victim.GetBuffManager().TriggerBuff(typeof(BattleBePreAtkHitBuffTrigger), skill, 1);
        //         }
        //         else
        //         {
        //             murder.GetBuffManager().TriggerBuff(typeof(BattlePreAtkHitBuffTrigger), skill, 5);
        //             victim.GetBuffManager().TriggerBuff(typeof(BattleBePreAtkHitBuffTrigger), skill, 5);
        //         }
        //
        //
        //         // 格挡触发
        //         if (isBlock)
        //         {
        //             murder.GetBuffManager().TriggerBuff(typeof(BattlePreAtkHitBuffTrigger), skill, 3);
        //             victim.GetBuffManager().TriggerBuff(typeof(BattleBePreAtkHitBuffTrigger), skill, 3);
        //         }
        //         else
        //         {
        //             murder.GetBuffManager().TriggerBuff(typeof(BattlePreAtkHitBuffTrigger), skill, 6);
        //             victim.GetBuffManager().TriggerBuff(typeof(BattleBePreAtkHitBuffTrigger), skill, 6);
        //         }
        //
        //         //技能伤害
        //         int skillDmg = SkillFunction.CalcFunctionDamage(murder, victim, skill.GetElement(), skill.SkillData,
        //             skill.CfgSkillFunctionData, isCrit, isBlock, skill.SkillData.SkillRatio);
        //
        //
        //         if (skill.CfgSkillFunctionData.DamageType == BattleConstant.SkillFunction.TYPE_HEAL)
        //         {
        //             //治疗，伤害为负
        //             skillHitPOD.ActualDmg = skillDmg;
        //             victim.ChangeHP(-skillDmg, false, true, murder, skill.GetElement());
        //         }
        //         else
        //         {
        //             //护盾减伤
        //             int shieldReduce = Math.Min(skillDmg, victim.GetShield());
        //             skillHitPOD.ShieldReduce = shieldReduce;
        //
        //             // 伤害限制
        //             skillDmg = victim.DamageLimit(skillDmg);
        //
        //             //扣除血量
        //             victim.ChangeHP(-skillDmg, false, true, murder, skill.GetElement(), true);
        //
        //             //实际伤害
        //             int actualDmg = Math.Max(0, skillDmg - shieldReduce);
        //             //if (actualDmg > 0)
        //             //{
        //             //    victim.ChangeSkillEnergy(victim.CfgMonsterData.HitAddEnergy);//受击增加怒气
        //             //}
        //             skillHitPOD.ActualDmg = actualDmg;
        //             //吸血
        //             int bloodSuck = SkillFunction.CalcBloodSuck(murder, victim, skill.SkillData,
        //                 skill.CfgSkillFunctionData, actualDmg);
        //             murder.ChangeHP(bloodSuck, false, true, victim, skill.GetElement());
        //             skillHitPOD.BloodSuck = bloodSuck;
        //
        //             //反伤
        //             int thornsDmg = SkillFunction.CalcThornsDamage(murder, victim, skill.SkillData,
        //                 skill.CfgSkillFunctionData, actualDmg);
        //             murder.ChangeHP(-thornsDmg, false, true, victim, skill.GetElement());
        //
        //             //击穿
        //             int breakThroughDmg = Fix64.ToInt32(actualDmg * skill.SkillData.BreakThrough);
        //             if (breakThroughDmg > 0)
        //             {
        //                 List<BattleUnitVO> breakThroughTargets =
        //                     SearchTarget.GetBreakThroughTargets(victim, _allBattleUnits.Values);
        //                 foreach (BattleUnitVO unit in breakThroughTargets)
        //                 {
        //                     if (!unit.IsDead())
        //                     {
        //                         unit.ChangeHP(-breakThroughDmg, true, true, murder, skill.GetElement());
        //                     }
        //                 }
        //             }
        //
        //             //溅射
        //             int sputterDmg = Fix64.ToInt32(actualDmg * skill.SkillData.Sputter);
        //             if (sputterDmg > 0)
        //             {
        //                 List<BattleUnitVO> sputterTargets =
        //                     SearchTarget.GetSputterTargets(victim, _allBattleUnits.Values);
        //                 foreach (BattleUnitVO unit in sputterTargets)
        //                 {
        //                     if (!unit.IsDead())
        //                     {
        //                         unit.ChangeHP(-sputterDmg, true, true, murder, skill.GetElement());
        //                     }
        //                 }
        //             }
        //         }
        //     }
        // }

        //skillHitPOD.DamageType = skill.CfgSkillFunctionData.DamageType;
        skillHitPOD.ElementType = skill.GetElement();
        // 弱点层数减少判断
        // if (skillHitPOD.Hit)
        // {
        // if(victim.WeakNum > 0 && Array.IndexOf(victim.WeakType, skill.GetElement()) > -1) {
        //     victim.WeakNum--;
        //     AddUpdateUnit(victim, BattleConstant.UpdateType.UPDATE_WEAK_NUM, victim.WeakNum);
        //     if (victim.WeakNum == 0)
        //     {
        //         victim.WeakBreakRound = _round;
        //         victim.GetBuffManager().TriggerBuff(typeof(BattleUnitWeakBeBreakBuffTrigger));
        //     }
        // }
        // }

        //攻击完毕，触发移除
        foreach (BattleUnitVO unit in _allBattleUnits.Values)
        {
            unit.GetBuffManager().TriggerRemove(BuffConstant.BUFF_REMOVE_TRIGGER_TYPE_AFTER_ATK_TARGET);
        }

        //攻击后伤害效果
        if (skillHitPOD.Hit && !skillHitPOD.Immunity)
        {
            murder.GetBuffManager().TriggerBuff(typeof(BattleAfterAtkHitBuffTrigger), skill, 0);
            victim.GetBuffManager().TriggerBuff(typeof(BattleBeAtkResultBuffTrigger), skill, 0);
        }

        if (skillHitPOD.Crit)
        {
            murder.GetBuffManager().TriggerBuff(typeof(BattleAfterAtkHitBuffTrigger), skill, 1);
            victim.GetBuffManager().TriggerBuff(typeof(BattleBeAtkResultBuffTrigger), skill, 1);
        }

        if (!skillHitPOD.Hit)
        {
            murder.GetBuffManager().TriggerBuff(typeof(BattleAfterAtkHitBuffTrigger), skill, 2);
            victim.GetBuffManager().TriggerBuff(typeof(BattleBeAtkResultBuffTrigger), skill, 2);
        }

        if (skillHitPOD.Block)
        {
            murder.GetBuffManager().TriggerBuff(typeof(BattleAfterAtkHitBuffTrigger), skill, 3);
            victim.GetBuffManager().TriggerBuff(typeof(BattleBeAtkResultBuffTrigger), skill, 3);
        }

        if (skillHitPOD.Immunity)
        {
            murder.GetBuffManager().TriggerBuff(typeof(BattleAfterAtkHitBuffTrigger), skill, 4);
            victim.GetBuffManager().TriggerBuff(typeof(BattleBeAtkResultBuffTrigger), skill, 4);
        }

        if (skillHitPOD.Hit && !skillHitPOD.Crit)
        {
            murder.GetBuffManager().TriggerBuff(typeof(BattleAfterAtkHitBuffTrigger), skill, 5);
            victim.GetBuffManager().TriggerBuff(typeof(BattleBeAtkResultBuffTrigger), skill, 5);
        }

        if (skillHitPOD.Hit && !skillHitPOD.Block)
        {
            murder.GetBuffManager().TriggerBuff(typeof(BattleAfterAtkHitBuffTrigger), skill, 6);
            victim.GetBuffManager().TriggerBuff(typeof(BattleBeAtkResultBuffTrigger), skill, 6);
        }

        if (!skillHitPOD.Hit)
        {
            CurrAtkTarget = null;
        }


        for (int j = 0; j < skill.SkillData.BuffTarget.Count; j++)
        {
            if (skill.SkillData.BuffOrder[j] == 4 && Random.randomFix64() < skill.SkillData.BuffProbability[j])
            {
                murder.GetBuffManager().AddBuff(skill.SkillData.BuffTarget[j], murder, skill.SkillData,
                    skill.SkillData.BuffID[j], skill.SkillData.BuffTime[j], skill.SkillData.BuffStackNum[j]);
            }
        }

        CurrAtkTarget = null;
        return skillHitPOD;
    }

    /// <summary>
    /// 回合结束
    /// </summary>
    private void _RoundEnd()
    {
        BattleRoundEndCommand endRoundCommand = new BattleRoundEndCommand();
        endRoundCommand.Round = _round;
        endRoundCommand.UpdateUnits = new List<BattleUpdateUnitPOD>();
        _updateUnits = endRoundCommand.UpdateUnits;

        StartTriggerStep(typeof(BattleRoundEndBuffTrigger));
        //回合结束触发
        AttackTroopUnitVO.GetBuffManager().TriggerBuff(typeof(BattleRoundEndBuffTrigger), _round, 0);
        DefendTroopUnitVO.GetBuffManager().TriggerBuff(typeof(BattleRoundEndBuffTrigger), _round, 0);

        // foreach (KeyValuePair<int, BattleTileVO> kv in _allBattleTile)
        // {
        //     kv.Value.GetBuffManager().TriggerBuff(typeof(BattleRoundEndBuffTrigger), _round,0);
        // }

        // 确定攻击方 or 防御方回合
        BattleUnitVO last = _unitActionOrders.Last();

        Debug.Log("最后出手队伍 -> " + last.TroopType + " ID = " + last.ID);
        foreach (KeyValuePair<int, BattleUnitVO> kv in _allBattleUnits)
        {
            kv.Value.GetBuffManager().TriggerBuff(typeof(BattleRoundEndBuffTrigger), _round, 0);
            if (kv.Value.TroopType == last.TroopType)
            {
                // 我方回合结束
                Debug.Log(kv.Value.TroopType + " -> 我方回合结束 _RoundEnd" + " ID = " + kv.Value.ID);
                kv.Value.GetBuffManager().TriggerBuff(typeof(BattleRoundEndBuffTrigger), _round, 1);
            }
            else
            {
                // 敌方回合结束
                Debug.Log(kv.Value.TroopType + " -> 敌方回合结束 _RoundEnd" + " ID = " + kv.Value.ID);
                kv.Value.GetBuffManager().TriggerBuff(typeof(BattleRoundEndBuffTrigger), _round, 2);
            }
        }

        EndTriggerStep();

        //StartTriggerStep(typeof(TimeBuffTrigger));
        // 英雄
        foreach (KeyValuePair<int, BattleUnitVO> kv in _allBattleUnits)
        {
            kv.Value.GetBuffManager().Update();
        }

        // 瓦片
        foreach (KeyValuePair<int, BattleTileVO> kv in _allBattleTile)
        {
            kv.Value.GetBuffManager().Update();
        }

        EndTriggerStep();

        //检查弱点护盾恢复
        StartTriggerStep(typeof(BattleUnitWeakRecoverBuffTrigger));
        foreach (KeyValuePair<int, BattleUnitVO> kv in _allBattleUnits)
        {
            BattleUnitVO unitVO = kv.Value;
            if (unitVO.IsDead())
            {
                continue;
            }

            //间隔一回合恢复
            if (unitVO.WeakNum < 1 && unitVO.WeakBreakRound == _round - 1)
            {
                unitVO.WeakBreakRound = -1;
                unitVO.WeakNum = unitVO.WeakMaxNum;
                AddUpdateUnit(unitVO, BattleConstant.UpdateType.UPDATE_WEAK_NUM, unitVO.WeakNum);
                unitVO.GetBuffManager().TriggerBuff(typeof(BattleUnitWeakChangeBuffTrigger));
                unitVO.GetBuffManager().TriggerBuff(typeof(BattleUnitWeakRecoverBuffTrigger));
                // 移除弱点击破BUFF
                //unitVO.GetBuffManager().RemoveBuff(unitVO.CfgMonsterData.WeakDestroy);
            }
        }

        EndTriggerStep();


        foreach (KeyValuePair<int, BattleUnitVO> kv in _allBattleUnits)
        {
            if (kv.Value.IsDead())
            {
                continue;
            }

            //增加怒气
            int addEnergy = Fix64.ToInt32(kv.Value.GetBattleAttribute(BattleConstant.Attribute.TYPE_ENERGY_RECOVER));
            kv.Value.ChangeSkillEnergy(addEnergy);
        }

        // 增加AP
        int addAp = this.GetAddAP();
        AttackTroopUnitVO.ChangeAP(addAp);
        DefendTroopUnitVO.ChangeAP(addAp);


        // 改变最大AP
        int maxAP = GetMaxAP();
        AddUpdateUnit(AttackTroopUnitVO, BattleConstant.UpdateType.CHANGE_MAX_AP, maxAP);
        AddUpdateUnit(DefendTroopUnitVO, BattleConstant.UpdateType.CHANGE_MAX_AP, maxAP);

        //单位特殊状态检测（大破、小破、死亡）
        _CheckUnitSpStatus();

        SendCommand(endRoundCommand);
        _updateUnits = null;
        _roundStart = false;
    }

    /// <summary>
    /// 检查召唤物
    /// </summary>
    private void CheckCall()
    {
        foreach (KeyValuePair<int, BattleCallVO> kv in _allBattleCalls)
        {
            if (kv.Value.CallStatus == 2)
            {
                // AddUpdateUnit(kv.Value.Summoner, BattleConstant.UpdateType.SUMMON_REMOVE, kv.Key);
                RemoveUnit(kv.Value);
                // 离开地块
                BattleTileVO tile = _allBattleTile
                    .Where(t => t.Value.BattlePos == kv.Value.BattlePos && t.Value.TroopType == kv.Value.TroopType)
                    .First()
                    .Value;
                tile.Leave();
                kv.Value.CallStatus = 3;
            }
        }

        foreach (KeyValuePair<int, BattleCallVO> kv in _allBattleCalls)
        {
            if (kv.Value.CallStatus == 0)
            {
                kv.Value.SetSpStatus(BattleConstant.SPStatus.ACTION, true);
                AddUnit(kv.Value);
                // kv.Value.InitBuff();
                // 进入地块
                BattleTileVO tile = _allBattleTile
                    .Where(t => t.Value.BattlePos == kv.Value.BattlePos && t.Value.TroopType == kv.Value.TroopType)
                    .First()
                    .Value;
                tile.Enter(kv.Value);

                if (kv.Value.CreateType == 2)
                {
                    AddUpdateUnit(kv.Value.Summoner, BattleConstant.UpdateType.SUMMON_ADD, kv.Value.ToData());
                }

                kv.Value.CallStatus = 1;
            }
        }
    }

    /// <summary>
    /// 检查回合开始
    /// </summary>
    /// <returns></returns>
    private bool _CheckRoundStart()
    {
        return !_roundStart;
    }

    /// <summary>
    /// 回合开始
    /// </summary>
    private void _RoundStart()
    {
        _roundStart = true;
        BattleRoundCommand battleRoundCommand = new BattleRoundCommand();
        battleRoundCommand.UpdateUnits = new List<BattleUpdateUnitPOD>();
        _updateUnits = battleRoundCommand.UpdateUnits;
        _round++;
        _roundToggle = false;

        _InitRoundActionOrders();

        Dictionary<int, BattleUnitVO> allUnits = new Dictionary<int, BattleUnitVO>();
        //allUnits.Add(_allBattleUnits);
        allUnits.Add(AttackTroopUnitVO.ID, AttackTroopUnitVO);
        allUnits.Add(DefendTroopUnitVO.ID, DefendTroopUnitVO);

        //扣除技能CD
        foreach (KeyValuePair<int, BattleUnitVO> kv in allUnits)
        {
            if (!kv.Value.FightStatus[BattleConstant.Status.CD_STOP])
            {
                if (kv.Value.CommonSkillCD > 0)
                {
                    kv.Value.CommonSkillCD--;
                    AddUpdateUnit(kv.Value, BattleConstant.UpdateType.CHANGE_COMMON_CD, kv.Value.CommonSkillCD);
                }

                foreach (BattleSkillVO skill in kv.Value.AllSkills)
                {
                    if (skill.CoolDown > 0)
                    {
                        skill.CoolDown--;
                        //AddUpdateUnit(kv.Value, BattleConstant.UpdateType.CHANGE_SKILL_CD, skill.CfgSkillData.Id,skill.CoolDown);
                    }

                    if (skill.AICoolDown > 0)
                    {
                        skill.AICoolDown--;
                    }
                }
            }

            kv.Value.GetFightAttribute()[BattleConstant.Attribute.TYPE_CURR_BE_CURR_HURT] = 0;
            kv.Value.GetFightAttribute()[BattleConstant.Attribute.TYPE_CURRENT_ROUND_CHANGE_SKILL_ENERGY] = 0;
        }

        // // buff重置
        // foreach (KeyValuePair<int, BattleUnitVO> kv in _allBattleUnits){
        //     foreach (Buff buff in kv.Value.GetBuffManager().GetAllBuffs())
        //     {
        //         buff.RoundReset();
        //     }
        // }
        // foreach (KeyValuePair<int, BattleTileVO> kv in _allBattleTile){
        //     foreach (Buff buff in kv.Value.GetBuffManager().GetAllBuffs())
        //     {
        //         buff.RoundReset();
        //     }
        // }
        //

        //回合开始触发
        StartTriggerStep(typeof(BattleRoundStartBuffTrigger));
        AttackTroopUnitVO.GetBuffManager().TriggerBuff(typeof(BattleRoundStartBuffTrigger), _round, 0);
        DefendTroopUnitVO.GetBuffManager().TriggerBuff(typeof(BattleRoundStartBuffTrigger), _round, 0);
        AttackTroopUnitVO.SetSpStatus(BattleConstant.SPStatus.ACTION, false);
        DefendTroopUnitVO.SetSpStatus(BattleConstant.SPStatus.ACTION, false);
        // AttackTroopUnitVO.CurrTurnRoundAction = false;
        // DefendTroopUnitVO.CurrTurnRoundAction = false;

        // foreach (KeyValuePair<int, BattleTileVO> kv in _allBattleTile)
        // {
        //     kv.Value.GetBuffManager().TriggerBuff(typeof(BattleRoundStartBuffTrigger), _round,0);
        // }

        // 确定攻击方 or 防御方回合
        BattleUnitVO first = _unitActionOrders.First();
        Debug.Log("最先出手队伍 -> " + first.TroopType + " ID = " + first.ID);
        foreach (KeyValuePair<int, BattleUnitVO> kv in _allBattleUnits)
        {
            kv.Value.GetBuffManager().TriggerBuff(typeof(BattleRoundStartBuffTrigger), _round, 0);

            if (kv.Value.TroopType == first.TroopType)
            {
                // 我方回合开始
                Debug.Log(kv.Value.TroopType + " -> 我方回合开始 _RoundStart" + " ID = " + kv.Value.ID);
                kv.Value.GetBuffManager().TriggerBuff(typeof(BattleRoundStartBuffTrigger), _round, 1);
            }
            else
            {
                // 敌方回合开始
                Debug.Log(kv.Value.TroopType + " -> 敌方回合开始 _RoundStart" + " ID = " + kv.Value.ID);
                kv.Value.GetBuffManager().TriggerBuff(typeof(BattleRoundStartBuffTrigger), _round, 2);
            }

            // kv.Value.CurrTurnRoundAction = false;
            // 设置已经行动状态
            kv.Value.SetSpStatus(BattleConstant.SPStatus.ACTION, false);
        }

        EndTriggerStep();

        _CheckUnitSpStatus();
        battleRoundCommand.FirstTroopType = _unitActionOrders.First().TroopType;
        battleRoundCommand.Round = _round;
        battleRoundCommand.TurnOrders = _unitActionOrders.ConvertAll(s => s.ID);
        SendCommand(battleRoundCommand);
        _updateUnits = null;
        CurrCastInfo = null;
        CurrMoveInfo = null;
        CurrRestInfo = null;

        SendCommand(new BattleChooseSkillCommand(_unitActionOrders.First().ID, _unitActionOrders.First().TroopType));
    }

    /// <summary>
    /// 检查战斗单位特殊状态
    /// </summary>
    private void _CheckUnitSpStatus(BattleUnitVO unit = null)
    {
        foreach (KeyValuePair<int, BattleUnitVO> kv in _allBattleUnits)
        {
            if (unit == null || kv.Value == unit)
            {
                kv.Value.CheckSpStatus();
            }
        }
    }

    /// <summary>
    /// 初始化回合阶段行动顺序
    /// </summary>
    private void _InitRoundActionOrders()
    {
        List<KeyValuePair<BattleUnitVO, int>>
            allActionUnits = new List<KeyValuePair<BattleUnitVO, int>>(); //所有参与的单位，value=速度

        // 参战队伍
        int troopSpeed = AttackTroopUnitVO.TroopType * 10000;
        int attackTroopSpeed =
            Fix64.ToInt32(AttackTroopUnitVO.GetSelfBattleAttribute(BattleConstant.Attribute.TYPE_SPEED)) * 100000 +
            troopSpeed;
        int defendTroopSpeed =
            Fix64.ToInt32(DefendTroopUnitVO.GetSelfBattleAttribute(BattleConstant.Attribute.TYPE_SPEED)) * 100000 +
            troopSpeed;
        // defendTroopSpeed = 99 * 100000 + troopSpeed;

        allActionUnits.Add(new KeyValuePair<BattleUnitVO, int>(AttackTroopUnitVO, attackTroopSpeed));
        allActionUnits.Add(new KeyValuePair<BattleUnitVO, int>(DefendTroopUnitVO, defendTroopSpeed));

        // 参战英雄
        foreach (BattleUnitVO unit in _allBattleUnits.Values)
        {
            int _troopSpeed = unit.TroopType == BattleConstant.TroopType.ATTACK ? attackTroopSpeed : defendTroopSpeed;
            int speed = _troopSpeed - (unit.BattleOrder * 1000) - (10 * Math.Max(0,
                100 - Fix64.ToInt32(unit.GetBattleAttribute(BattleConstant.Attribute.TYPE_SPEED)))) - unit.BattlePos;
            allActionUnits.Add(new KeyValuePair<BattleUnitVO, int>(unit, speed));
            unit.StandByIndex = 0;
        }

        //根据速度排序，速度一样防守方先出手，然后按站位计算
        allActionUnits.Sort((o1, o2) =>
        {
            //int value1 = o1.Value * 1000 + o1.Key.TroopType * 100 + o1.Key.BattlePos;
            //int value2 = o2.Value * 1000 + o2.Key.TroopType * 100 + o2.Key.BattlePos;

            // int value1 = o1.Value * 1000 + o1.Key.TroopType * 100 + o1.Key.BattlePos;
            // int value2 = o2.Value * 1000 + o2.Key.TroopType * 100 + o2.Key.BattlePos;
            return o2.Value.CompareTo(o1.Value);
        });

        _unitActionOrders.Clear();
        for (int i = 0; i < allActionUnits.Count; i++)
        {
            _unitActionOrders.Add(allActionUnits[i].Key);
            allActionUnits[i].Key.HasWaitToEnd = false;
            //Logger.Info("出手顺序 -> " + allActionUnits[i].Key.TroopType + " ID = " + allActionUnits[i].Key.ID);
        }

        _actionOrderIndex = 0;
    }

    private BattleOverCommand _GetBattleOverCommand(int fightResult)
    {
        Debug.Log("----------------_GetBattleOverCommand");
        BattleOverCommand overCommand = new BattleOverCommand();
        overCommand.UpdateUnits = new List<BattleUpdateUnitPOD>();
        _updateUnits = overCommand.UpdateUnits;


        // 战斗结束触发
        AttackTroopUnitVO.GetBuffManager().TriggerBuff(typeof(BattleFightOverBuffTrigger), fightResult);
        DefendTroopUnitVO.GetBuffManager().TriggerBuff(typeof(BattleFightOverBuffTrigger), fightResult);


        FightTroopPOD attackerTroopPOD = new FightTroopPOD();
        List<FightUnitPOD> attackerPODs = new List<FightUnitPOD>();
        foreach (BattleUnitVO unit in _attackers)
        {
            attackerPODs.Add(unit.ToFightData());
        }

        attackerTroopPOD.ArrFightUnitPOD = attackerPODs.ToArray();

        FightTroopPOD defenserTroopPOD = new FightTroopPOD();
        List<FightUnitPOD> defenserPODs = new List<FightUnitPOD>();
        foreach (BattleUnitVO unit in _defensers)
        {
            defenserPODs.Add(unit.ToFightData());
        }

        defenserTroopPOD.ArrFightUnitPOD = defenserPODs.ToArray();

        Dictionary<int, int> dmgRecords = new Dictionary<int, int>();
        foreach (BattleUnitVO unit in _attackers)
        {
            // dmgRecords.Add(unit.CfgMonsterData.Id, unit.DmgRecord);
            dmgRecords.Add(unit.ID, unit.DmgRecord);
        }


        overCommand.HolderID = _holderId;
        overCommand.FightID = _fightID;
        overCommand.FightResult = fightResult;
        overCommand.Round = _round;
        overCommand.BattleType = _battleType;
        overCommand.Attacker = attackerTroopPOD;
        overCommand.Defender = defenserTroopPOD;
        overCommand.Orders = new List<BaseBattleOrder>(_CompleteOrders);
        overCommand.DmgRecords = new Dictionary<int, int>(dmgRecords);

        // 还未出现的替补怪
        // for (int i = tubstituteIndex; i < CfgMonsterTeamData.BackTeam.Length; i++)
        // {
        //     enemy_alive++;
        //     int monsterId = CfgMonsterTeamData.BackTeam[i];
        //     enemy_aliveinfo.Add(monsterId);
        // }
        return overCommand;
    }


    /// <summary>
    /// 判断任意阵营死亡
    /// </summary>
    /// <returns></returns>
    private bool _CheckAnyTroopDead()
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

    /// <summary>
    /// 战斗结果
    /// </summary>
    /// <returns></returns>
    private int _CheckFightResult()
    {
        Debug.Log("_maxRound = " + _maxRound);
        //if (_isGiveUp)
        //{
        //    return BattleConstant.FightResult.GIVE_UP;
        //}
        int realRound = _round;
        if (!_roundStart)
        {
            //避免最后一个回合开始后，才战斗结束，这里提前判断回合数是否超过最大值
            realRound += 1;
        }


        bool atkDeadAll = true;
        foreach (BattleUnitVO unit in _attackers)
        {
            if (!unit.IsHelper && !unit.IsDead() && unit.CreateType != 1)
            {
                atkDeadAll = false;
                break;
            }
        }

        bool defDeadAll = true;
        foreach (BattleUnitVO unit in _defensers)
        {
            if (!unit.IsHelper && !unit.IsDead() && unit.CreateType != 1)
            {
                defDeadAll = false;
                break;
            }
        }

        if (!atkDeadAll && !defDeadAll)
        {
            // 特殊结算
            // if (CfgMonsterTeamData != null)
            // {
            //     int len = CfgMonsterTeamData.ResultType.Length;
            //     for (int i = 0; i < len; i++)
            //     {
            //         // 已完成的状态不再检查
            //         if (_resultStatus[i])
            //         {
            //             continue;
            //         }
            //
            //         Logger.Info("结算类型 == " + (CfgMonsterTeamData.ResultType[i]) + " 长度 = " + _resultStatus.Length);
            //         switch (CfgMonsterTeamData.ResultType[i])
            //         {
            //             // 存活N个回合
            //             case 1:
            //             {
            //                 if (realRound > CfgMonsterTeamData.ResultParam[i][0])
            //                 {
            //                     _resultStatus[i] = true;
            //                 }
            //
            //                 break;
            //             }
            //             // 击败指定敌方单位
            //             case 2:
            //             {
            //                 int l = CfgMonsterTeamData.ResultParam[i].Length;
            //                 bool b = true;
            //                 for (int j = 0; j < l; j += 2)
            //                 {
            //                     if (!b)
            //                     {
            //                         break;
            //                     }
            //
            //                     int count = GetAllBattleUnits().Where(t =>
            //                         t.Value.IsDead()
            //                         && t.Value.CfgMonsterData != null
            //                         && (t.Value.CfgMonsterData.Id == CfgMonsterTeamData.ResultParam[i][j] ||
            //                             CfgMonsterTeamData.ResultParam[i][j] == 0)
            //                         && t.Value.TroopType == BattleConstant.TroopType.DEFEND).Count();
            //                     b &= count >= CfgMonsterTeamData.ResultParam[i][j + 1];
            //                 }
            //
            //                 _resultStatus[i] = b;
            //                 break;
            //             }
            //             // 将指定单位的生命值削减到N%以下
            //             case 3:
            //             {
            //                 int l = CfgMonsterTeamData.ResultParam[i].Length;
            //                 bool b = true;
            //                 for (int j = 0; j < l; j += 2)
            //                 {
            //                     if (!b)
            //                     {
            //                         break;
            //                     }
            //
            //                     KeyValuePair<int, BattleUnitVO> unit = GetAllBattleUnits().Where(t =>
            //                         t.Value.CfgMonsterData != null
            //                         && (t.Value.CfgMonsterData.Id == CfgMonsterTeamData.ResultParam[i][j] ||
            //                             CfgMonsterTeamData.ResultParam[i][j] == 0)
            //                         && t.Value.TroopType == BattleConstant.TroopType.DEFEND).First();
            //                     if (!default(KeyValuePair<int, BattleUnitVO>).Equals(unit))
            //                     {
            //                         Fix64 hpScale = unit.Value.GetBattleAttribute(BattleConstant.Attribute.TYPE_HP) /
            //                                         unit.Value.GetBattleAttribute(BattleConstant.Attribute.TYPE_HP_MAX);
            //                         b &= hpScale * 100 < CfgMonsterTeamData.ResultParam[i][j + 1];
            //                     }
            //                 }
            //
            //                 _resultStatus[i] = b;
            //                 break;
            //             }
            //             // 在N个回合内击败敌人
            //             case 4:
            //                 if (realRound > CfgMonsterTeamData.ResultParam[i][0])
            //                 {
            //                     return BattleConstant.FightResult.DEFENDER_WIN;
            //                 }
            //
            //                 break;
            //             // 保护NPC（ID）不死亡 X 个
            //             case 5:
            //             {
            //                 int l = CfgMonsterTeamData.ResultParam[i].Length;
            //                 for (int j = 0; j < l; j += 2)
            //                 {
            //                     int count = GetAllBattleUnits().Where(t =>
            //                         !t.Value.IsDead()
            //                         && t.Value.CfgMonsterData != null
            //                         && (t.Value.CfgMonsterData.Id == CfgMonsterTeamData.ResultParam[i][j] ||
            //                             CfgMonsterTeamData.ResultParam[i][j] == 0)
            //                         && t.Value.TroopType == BattleConstant.TroopType.ATTACK).Count();
            //                     if (count < CfgMonsterTeamData.ResultParam[i][j + 1])
            //                     {
            //                         return BattleConstant.FightResult.DEFENDER_WIN;
            //                     }
            //                 }
            //
            //                 break;
            //             }
            //         }
            //     }
            //
            //     bool attacker_win = true;
            //     for (int i = 0; i < _resultStatus.Length; i++)
            //     {
            //         Logger.Info("战斗判定类型 " + i + " = " + _resultStatus[i]);
            //         attacker_win &= _resultStatus[i];
            //     }
            //
            //     if (attacker_win)
            //     {
            //         return BattleConstant.FightResult.ATTACKER_WIN;
            //     }
            // }

            if (realRound > _maxRound)
            {
                return BattleConstant.FightResult.TIME_OUT;
            }

            return BattleConstant.FightResult.NOT_END;
        }

        if (atkDeadAll && defDeadAll)
        {
            return BattleConstant.FightResult.ALL_FAIL;
        }

        return defDeadAll ? BattleConstant.FightResult.ATTACKER_WIN : BattleConstant.FightResult.DEFENDER_WIN;
    }


    public override bool HandleOrder(BaseBattleOrder order)
    {
        bool baseReturn = base.HandleOrder(order);
        String json = JsonConvert.SerializeObject(order);
        Debug.Log($"客户端指令 {order.GetType()} {json}");
        if (order is CastSkillOrder)
        {
            return _HandleCastSkillOrder(order as CastSkillOrder);
        }
        else if (order is InitFightOrder)
        {
            return _HandleInitFightOrder(order as InitFightOrder);
        }
        else if (order is FightUnitActionOverOrder)
        {
            return _HandleFightUnitActionOverOrder(order as FightUnitActionOverOrder);
        }
        else if (order is WaitToEndOrder)
        {
            return _HandleWaitToEndOrder(order as WaitToEndOrder);
        }
        else if (order is MovePosOrder)
        {
            return _HandleMovePosOrder(order as MovePosOrder);
        }
        else if (order is StatusSwitchOrder)
        {
            return _HandleStatusSwitchOrder(order as StatusSwitchOrder);
        }

        return baseReturn;
    }

    /// <summary>
    /// 单位切换AI开关位置。
    /// </summary>
    protected virtual bool _HandleStatusSwitchOrder(StatusSwitchOrder order)
    {
        if (order.RoundNumber != _round)
        {
            return false;
        }

        // if (order.UnitID == CurrMover.ID)
        // {
        BattleTurnCommand _currTurn = new BattleTurnCommand();
        _currTurn.MoverID = order.UnitID;
        _currTurn.BeforeActionPOD = new BattleBeforeActionPOD();
        _currTurn.BeforeActionPOD.UpdateUnits = new List<BattleUpdateUnitPOD>();
        _currTurn.InActionPOD = new BattleInActionPOD();
        _currTurn.InActionPOD.CastSkills = new List<BattleCastSkillPOD>();
        _currTurn.AfterActionPOD = new BattleAfterActionPOD();
        _currTurn.AfterActionPOD.UpdateUnits = new List<BattleUpdateUnitPOD>();
        _updateUnits = _currTurn.BeforeActionPOD.UpdateUnits;
        StatusSwitchAction(order);
        SendCommand(_currTurn);
        return true;
    }

    /// <summary>
    /// 单位移动到新的位置。
    /// </summary>
    protected virtual bool _HandleMovePosOrder(MovePosOrder order)
    {
        if (order.RoundNumber != _round)
        {
            return false;
        }

        // if (order.UnitID == CurrMover.ID)
        // {
        BattleTurnCommand _currTurn = new BattleTurnCommand();
        _currTurn.MoverID = order.UnitID;
        _currTurn.BeforeActionPOD = new BattleBeforeActionPOD();
        _currTurn.BeforeActionPOD.UpdateUnits = new List<BattleUpdateUnitPOD>();
        _currTurn.InActionPOD = new BattleInActionPOD();
        _currTurn.InActionPOD.CastSkills = new List<BattleCastSkillPOD>();
        _currTurn.AfterActionPOD = new BattleAfterActionPOD();
        _currTurn.AfterActionPOD.UpdateUnits = new List<BattleUpdateUnitPOD>();
        _updateUnits = _currTurn.BeforeActionPOD.UpdateUnits;
        Moving = true;
        MoveAction(order);
        Moving = false;
        LoopSkill(_currTurn, null);
        SendCommand(_currTurn);
        CheckFight = true;
        // }
        // CurrMoveInfo = order;
        // _actionFrame = _currentFrame;//每次出手延迟N帧
        return true;
    }

    /// <summary>
    /// 等待到最后再行动
    /// </summary>
    protected virtual bool _HandleWaitToEndOrder(WaitToEndOrder order)
    {
        // //if (CurrMover.HasWaitToEnd)
        // //{
        // //    Logger.Error(CurrMover.ID + " HasWaitToEnd! can't wait again in one round!");
        // //    return false;
        // //}
        // //CurrMover.HasWaitToEnd = true;
        //
        // BattleUnitVO unit = _allBattleUnits[order.UnitID];
        // if (!unit.IsAction() || unit.CurrTurnRoundAction)
        // {
        //     // Logger.Error("select action unit id = {0} error", order.UnitID);
        //     return true;
        // }
        //
        // unit.CurrTurnRoundAction = true;
        //
        // BattleTurnCommand _currTurn = new BattleTurnCommand();
        // _currTurn.MoverID = unit.ID;
        // _currTurn.BeforeActionPOD = new BattleBeforeActionPOD();
        // _currTurn.BeforeActionPOD.UpdateUnits = new List<BattleUpdateUnitPOD>();
        // _currTurn.InActionPOD = new BattleInActionPOD();
        // _currTurn.InActionPOD.CastSkills = new List<BattleCastSkillPOD>();
        // _currTurn.AfterActionPOD = new BattleAfterActionPOD();
        // _currTurn.AfterActionPOD.UpdateUnits = new List<BattleUpdateUnitPOD>();
        //
        // _updateUnits = _currTurn.BeforeActionPOD.UpdateUnits;
        // // 休息触发
        // unit.GetBuffManager().TriggerBuff(typeof(BattleUnitWaitBuffTrigger));
        //
        // _updateUnits = _currTurn.AfterActionPOD.UpdateUnits;
        // // _unitActionOrders.Remove(CurrMover);
        // //_unitActionOrders.Add(CurrMover);
        //
        // //_actionOrderIndex--;
        // CurrMover = null;
        // _actionFrame = _currentFrame;
        // SendCommand(_currTurn);

        if (CurrRestInfo != null || order.RoundNumber != _round)
        {
            return false;
        }

        CurrRestInfo = order;
        _actionFrame = _currentFrame; //每次出手延迟N帧
        return true;
    }

    /// <summary>
    /// 开始战斗
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    protected override bool _HandleStartFightOrder(BaseBattleOrder order)
    {
        bool success = base._HandleStartFightOrder(order);
        if (!success)
        {
            return false;
        }

        BattleStartCommand battleStartCommand = new BattleStartCommand();
        _updateUnits = battleStartCommand.UpdateUnits = new List<BattleUpdateUnitPOD>();

        StartTriggerStep(typeof(BattleStartBuffTrigger));
        //战斗开始触发
        AttackTroopUnitVO.GetBuffManager().TriggerBuff(typeof(BattleStartBuffTrigger));
        DefendTroopUnitVO.GetBuffManager().TriggerBuff(typeof(BattleStartBuffTrigger));
        foreach (KeyValuePair<int, BattleUnitVO> kv in _allBattleUnits)
        {
            kv.Value.GetBuffManager().TriggerBuff(typeof(BattleStartBuffTrigger));
        }

        EndTriggerStep();

        _CheckUnitSpStatus();
        SendCommand(battleStartCommand);
        _updateUnits = null;
        return true;
    }

    /// <summary>
    /// 单位行动结束
    /// </summary>
    /// <param name="fightUnitActionOverOrder"></param>
    /// <returns></returns>
    private bool _HandleFightUnitActionOverOrder(FightUnitActionOverOrder fightUnitActionOverOrder)
    {
        // Logger.Warning("_checkActionFrame = {0}, _currentFrame = {1}, detal = {2} _waitAction = {3} UnitID = {4}",
        //     _checkActionFrame, _currentFrame, _currentFrame - _checkActionFrame, _waitAction,
        //     fightUnitActionOverOrder.UnitID);
        // if (LogicHelper.isDevMode() && _checkActionFrame > _currentFrame &&
        //     fightUnitActionOverOrder.UnitID != _waitAction)
        // {
        //     return false;
        // }
        //
        // if (CurrMover != null && !CurrMover.GetSpStatus(BattleConstant.SPStatus.ACTION))
        // {
        //     return false;
        // }

        _waitAction = 0;
        _actionFrame = _currentFrame + 0; //每次出手延迟N帧
        return true;
    }

    /// <summary>
    /// 释放大招指令
    /// </summary>
    /// <param name="castSkillOrder"></param>
    /// <returns></returns>
    private bool _HandleCastSkillOrder(CastSkillOrder castSkillOrder)
    {
        if (CurrCastInfo != null || castSkillOrder.RoundNumber != _round)
        {
            return false;
        }

        CurrCastInfo = castSkillOrder;
        _actionFrame = _currentFrame; //每次出手延迟N帧
        return true;
    }


    /// <summary>
    /// 取得当前回合最大AP上限
    /// </summary>
    /// <returns></returns>
    public int GetMaxAP()
    {
        // int[] datas = CfgDiscreteDataTable.Instance.GetDataByID(22).Data;
        // int len = datas.Length;
        // int idx = Math.Min(_round, len - 1);
        // int max = datas[idx];
        // return max;
        return 0;
    }

    /// <summary>
    /// 取得当前回合AP增加值
    /// </summary>
    /// <returns></returns>
    public int GetAddAP()
    {
        // int[] datas = CfgDiscreteDataTable.Instance.GetDataByID(21).Data;
        // int len = datas.Length;
        // int idx = Math.Min(_round, len - 1);
        // int max = datas[idx];
        // return max;
        return 0;
    }

    ///// <summary>
    ///// 取得当前回合最大怒气上限
    ///// </summary>
    ///// <returns></returns>
    //public int GetMaxEnergy()
    //{
    //    int[] datas = CfgDiscreteDataTable.Instance.GetDataByID(35).Data;
    //    int len = datas.Length;
    //    int idx = Math.Min(_round, len - 1);
    //    int max = datas[idx];
    //    return max;
    //}

    ///// <summary>
    ///// 取得当前回合怒气增加值
    ///// </summary>
    ///// <returns></returns>
    //public int GetAddEnergy()
    //{
    //    int[] datas = CfgDiscreteDataTable.Instance.GetDataByID(34).Data;
    //    int len = datas.Length;
    //    int idx = Math.Min(_round, len - 1);
    //    int max = datas[idx];
    //    return max;
    //}
    public int GetIdlePos(int troopType)
    {
        List<int> existsPos = new List<int>();

        foreach (KeyValuePair<int, BattleUnitVO> kv in _allBattleUnits)
        {
            if (kv.Value.TroopType != troopType)
            {
                continue;
            }

            if (kv.Value.IsDead())
            {
                continue;
            }

            existsPos.Add(kv.Value.BattlePos);
        }

        foreach (KeyValuePair<int, BattleCallVO> kv in _allBattleCalls)
        {
            if (kv.Value.TroopType != troopType)
            {
                continue;
            }

            if (kv.Value.IsDead())
            {
                continue;
            }

            existsPos.Add(kv.Value.BattlePos);
        }


        List<int> idlePos = new List<int>();
        for (int i = 1; i <= 9; i++)
        {
            if (!existsPos.Contains(i))
            {
                idlePos.Add(i);
            }
        }

        return idlePos.Count <= 0 ? 0 : idlePos[_random.randomInt(idlePos.Count)];
    }


    public void StartTriggerStep(Type triggerStepType)
    {
        this.triggerStepType = triggerStepType;
    }

    public void EndTriggerStep()
    {
        this.triggerStepType = null;
        this.triggerStepBuffs.Clear();
    }
}