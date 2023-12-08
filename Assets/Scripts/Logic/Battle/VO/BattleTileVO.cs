using System;
using System.Collections.Generic;
using FixMath.NET;
using IQIGame.Onigao.Config;


/// <summary>
/// 战斗瓦片数据(地块)
/// </summary>
public class BattleTileVO : BattleUnitVO
{
    /// <summary>
    /// 战斗单位
    /// </summary>
    public BattleUnitVO BattleUnitVO { get; set; }

    public Dictionary<int, Buff> tempBuffCache = new Dictionary<int, Buff>();

    private int terrainCid = 0;

    private static FightUnitPOD CreateTileUnitPOD(int troopType, int battlePos, int terrain)
    {
        FightUnitPOD fightUnitPOD = new FightUnitPOD();
        fightUnitPOD.InitBuff = new List<int>();
        fightUnitPOD.Skills = new List<int>();
        fightUnitPOD.SkillLvs = new List<int>();
        fightUnitPOD.SkillPurifyLvs = new List<int>();
        fightUnitPOD.SkillStrengLvs = new List<int>();
        fightUnitPOD.SkillStrengthens = new List<int>();

        CfgMonsterData cfgMonsterData = CfgMonsterTable.Instance.GetDataByID(1);
        fightUnitPOD.MonsterCfgId = cfgMonsterData.Id;

        fightUnitPOD.Attributes = new Fix64[BattleConstant.Attribute.ATTRIBUTE_NUM];
        fightUnitPOD.Attributes[BattleConstant.Attribute.TYPE_HP] = 1;
        fightUnitPOD.Attributes[BattleConstant.Attribute.TYPE_HP_MAX] = 1;
        fightUnitPOD.BaseAttrs = new Fix64[0];
        fightUnitPOD.TroopType = troopType;
        fightUnitPOD.BattlePos = battlePos;
        fightUnitPOD.SkinId = cfgMonsterData.EntityID;
        return fightUnitPOD;
    }

    public BattleTileVO(TurnBaseLogicFight fight, int troopType, int battlePos, int terrain) : base(
        BattleConstant.ScopeType.TILE, fight, CreateTileUnitPOD(troopType, battlePos, terrain))
    {
        // 地块配置
        terrainCid = terrain;
    }


    /// <summary>
    /// 初始化buff
    /// </summary>
    public override void InitBuff()
    {
        CfgTerrainData cfgTerrainData = CfgTerrainTable.Instance.GetDataByID(terrainCid);
        if (cfgTerrainData != null && cfgTerrainData.BaseBuff != 0)
        {
            // 添加基础BUFF
            Buff baseBuff = GetBuffManager().AddBuffInner(this, null, cfgTerrainData.BaseBuff, cfgTerrainData.BuffTime);
            // 给基础BUFF添加子BUFF
            AddTileBuff(baseBuff);


            if (BattleUnitVO != null)
            {
                BattleUnitVO.GetBuffManager().TriggerBuff(typeof(BattleUnitTerrainChangeTrigger), 1, terrainCid);
            }

            GetBuffManager().TriggerBuff(typeof(BattleUnitTerrainChangeTrigger), 1, terrainCid);
        }
    }

    public void InitBattleUnit(BattleUnitVO battleUnitVO)
    {
        BattleUnitVO = battleUnitVO;
        battleUnitVO.Tile = this;
    }

    /// <summary>
    /// 进入
    /// </summary>
    /// <param name="BattleUnitVO"></param>
    public void Enter(BattleUnitVO battleUnitVO)
    {
        BattleUnitVO = battleUnitVO;
        battleUnitVO.Tile = this;
        foreach (Buff buff in GetBuffManager().GetAllBuffs(true))
        {
            OnBuffAdd(null, buff);
        }

        if (BattleUnitVO != null)
        {
            BattleUnitVO.GetBuffManager().TriggerBuff(typeof(BattleUnitTerrainChangeTrigger), 1, terrainCid);
        }
    }

    /// <summary>
    /// 离开
    /// </summary>
    public void Leave()
    {
        foreach (Buff buff in GetBuffManager().GetAllBuffs(true))
        {
            OnBuffRemove(null, buff, BuffRemoveReason.MOVE);
        }

        if (BattleUnitVO != null)
        {
            // 地形移除BUFF
            BattleUnitVO.GetBuffManager().TriggerRemove(BuffConstant.BUFF_REMOVE_TRIGGER_TYPE_MOVE);

            // 失去地形触发 
            BattleUnitVO.GetBuffManager().TriggerBuff(typeof(BattleUnitTerrainChangeTrigger), 0, this.terrainCid);

            BattleUnitVO.Tile = null;
            BattleUnitVO = null;
        }
    }


    public override void OnBuffAdd(BattleUnitVO notifyUnit, Buff buff)
    {
        // 效果作用于英雄
        if (notifyUnit != null)
        {
            Fight.AddUpdateUnit(this, BattleConstant.UpdateType.UPDATE_BUFF, buff.BuffCfg.Id, buff.StackCount,
                buff.LeftTime, 1);
        }

        if (BattleUnitVO != null && !BattleUnitVO.IsDead())
        {
            // if (buff.Effects.Count < 1)
            // {
            //     Buff _buff = BattleUnitVO.GetBuffManager().AddBuffInner(buff.Caster, buff.SkillData, buff.BuffCfg.Id,  buff.LeftTime,buff.StackCount);
            //     tempBuffCache.Add(buff.BuffCfg.Id,_buff);
            // }
            // else
            // {
            // BUFF持续效果
            BattleUnitVO.OnBuffAdd(null, buff);
            // }
        }
    }

    public override void OnBuffStack(BattleUnitVO notifyUnit, Buff buff, int addStackCount)
    {
        // 效果作用于英雄
        if (notifyUnit != null)
        {
            Fight.AddUpdateUnit(this, BattleConstant.UpdateType.UPDATE_BUFF, buff.BuffCfg.Id, buff.StackCount,
                buff.LeftTime, 1);
        }

        if (BattleUnitVO != null && !BattleUnitVO.IsDead())
        {
            // BUFF持续效果
            BattleUnitVO.OnBuffStack(null, buff, addStackCount);
        }
    }

    public override void OnBuffTriggerEffect(Buff buff, BuffEffect buffEffect, Type triggerType, object[] triggerArgs)
    {
        if (buffEffect.EffectType == BuffConstant.BUFF_CHANGE_TILE_TERRAIN)
        {
            int terrainCid = Convert.ToInt32(buffEffect.EffectParams[0]);
            int buffTime = Convert.ToInt32(buffEffect.EffectParams[1]);
            // 修改地形
            ChangeTerrain(terrainCid, buffTime);
        }
        else
        {
            if (BattleUnitVO != null && !BattleUnitVO.IsDead())
            {
                // 触发BUFF效果
                BattleUnitVO.OnBuffTriggerEffect(buff, buffEffect, triggerType, triggerArgs);
            }
        }
    }


    public override void OnBuffRemove(BattleUnitVO notifyUnit, Buff buff, BuffRemoveReason removeReason)
    {
        if (notifyUnit != null)
        {
            Fight.AddUpdateUnit(this, BattleConstant.UpdateType.REMOVE_BUFF, buff.BuffCfg.Id, (int)removeReason);
        }


        if (BattleUnitVO != null && !BattleUnitVO.IsDead())
        {
            // if (buff.Effects.Count < 1)
            // {
            //     Buff _buff = tempBuffCache[buff.BuffCfg.Id];
            //     BattleUnitVO.GetBuffManager().RemoveBuff(_buff,removeReason);
            //     tempBuffCache.Remove(buff.BuffCfg.Id);
            // }
            // else
            // {
            // 移除BUFF效果
            BattleUnitVO.OnBuffRemove(null, buff, removeReason);
            // }
        }
    }

    public override int GetScope()
    {
        return 1;
    }

    public virtual TurnBaseLogicFight GetFight()
    {
        return Fight;
    }

    public BattleTilePOD ToData()
    {
        BattleTilePOD data = new BattleTilePOD();
        data.ID = ID;
        data.TroopType = TroopType;
        data.BattlePos = BattlePos;
        List<BattleBuffPOD> buffs = new List<BattleBuffPOD>();
        foreach (Buff buff in GetBuffManager().GetAllBuffs(true))
        {
            BattleBuffPOD pod = new BattleBuffPOD();
            pod.Cid = buff.BuffCfg.Id;
            pod.Stack = buff.StackCount;
            pod.LeftTime = buff.LeftTime;
            buffs.Add(pod);
        }

        data.Buffs = buffs;
        data.Status = FightStatus;
        data.SPStatus = _SPStatus;
        data.TerrainCid = terrainCid;
        return data;
    }


    private void AddTileBuff(Buff baseBuff)
    {
        CfgTerrainData cfgTerrainData = CfgTerrainTable.Instance.GetDataByID(terrainCid);
        // 配置表BUFF
        for (int i = 0; i < cfgTerrainData.Buff.Length; i++)
        {
            int buffCid = cfgTerrainData.Buff[i];
            // GetBuffManager().AddBuff(this, null, buffCid, -1);
            Buff childBuff = GetBuffManager().AddBuffInner(this, null, buffCid, baseBuff.LeftTime);
            if (childBuff != null && !childBuff.IsRemoved)
            {
                baseBuff.Children.Add(childBuff);
            }
        }
    }


    /// <summary>
    /// 修改地格
    /// </summary>
    public bool ChangeTerrain(int terrainCid, int buffTime)
    {
        CfgTerrainData nowCfg = CfgTerrainTable.Instance.GetDataByID(terrainCid);
        if (nowCfg == null)
        {
            return false;
        }

        // 移除之前的地形BUFF
        CfgTerrainData oldCfg = CfgTerrainTable.Instance.GetDataByID(this.terrainCid);
        if (oldCfg.BaseBuff != 0)
        {
            GetBuffManager().RemoveBuff(oldCfg.BaseBuff);

            // 地形移除触发
            if (BattleUnitVO != null)
            {
                BattleUnitVO.GetBuffManager().TriggerBuff(typeof(BattleUnitTerrainChangeTrigger), 0, this.terrainCid);
            }
            else
            {
                GetBuffManager().TriggerBuff(typeof(BattleUnitTerrainChangeTrigger), 0, this.terrainCid);
            }
        }

        if (nowCfg.BaseBuff != 0)
        {
            Buff baseBuff = GetBuffManager().AddBuffInner(this, null, nowCfg.BaseBuff, buffTime);
            this.terrainCid = nowCfg.Id;
            AddTileBuff(baseBuff);
            Fight.AddUpdateUnit(this, BattleConstant.UpdateType.CHANGE_TILE_TERRAIN, this.terrainCid);

            if (BattleUnitVO != null)
            {
                BattleUnitVO.GetBuffManager().TriggerBuff(typeof(BattleUnitTerrainChangeTrigger), 1, this.terrainCid);
            }
            else
            {
                GetBuffManager().TriggerBuff(typeof(BattleUnitTerrainChangeTrigger), 1, this.terrainCid);
            }
        }

        return true;
    }
}