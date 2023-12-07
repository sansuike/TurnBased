using System.Collections;

public class InitFightOrder:BaseBattleOrder
{
    /// <summary>
    /// 持有战斗的模块id
    /// </summary>
    public long HolderID;
    /// <summary>
    /// 战斗数据
    /// </summary>
    public FightPOD FightPOD;
    /// <summary>
    /// 自定义数据
    /// </summary>
    public string UserData;
    
    public override void Parse(IDictionary pod)
    {
    }
}