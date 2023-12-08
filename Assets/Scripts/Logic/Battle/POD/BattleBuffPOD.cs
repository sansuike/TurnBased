using System.Collections;
using System.Collections.Generic;


/// <summary>
/// Buff
/// </summary>
public class BattleBuffPOD : BaseBattlePOD
{
    public int Cid;

    public int Stack;

    public int LeftTime;

    public BattleBuffPOD()
    {
    }

    public override IDictionary ToDic()
    {
        IDictionary dic = new Dictionary<string, object>();
        dic["Cid"] = Cid;
        dic["Stack"] = Stack;
        dic["LeftTime"] = LeftTime;
        return dic;
    }
}