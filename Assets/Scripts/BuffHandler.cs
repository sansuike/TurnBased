using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffHandler : MonoBehaviour
{
    public LinkedList<BuffInfo> buffList = new LinkedList<BuffInfo>();

    public void AddBuff(BuffInfo buffInfo)
    {
        BuffInfo findBuffInfo = FindBuffInfo(buffInfo.buffData.id);
        if (findBuffInfo == null)
        {
            buffList.AddLast(buffInfo);
        }
        else
        {
            if (findBuffInfo.curStack < findBuffInfo.buffData.maxStack)
            {
                findBuffInfo.curStack ++;
            }

            switch (buffInfo.buffData.buffTimeUpdateEnum)
            {
                case BuffTimeUpdateEnum.Add:
                    findBuffInfo.durationTimer += buffInfo.buffData.duration;
                    findBuffInfo.tickTimer += buffInfo.buffData.tickTime;
                    break;
                case BuffTimeUpdateEnum.Replace:
                    findBuffInfo.durationTimer = buffInfo.buffData.duration;
                    findBuffInfo.tickTimer = buffInfo.buffData.tickTime;
                    break;
                case BuffTimeUpdateEnum.Keep:
                    break;
            }
        }
        findBuffInfo.buffData.OnCreate.Apply(findBuffInfo);
    }

    private BuffInfo FindBuffInfo(int buffDataID)
    {
        foreach (var buffInfo in buffList)
        {
            if (buffInfo.buffData.id == buffDataID)
            {
                return buffInfo;
            }
        }
        return  null;
    }
}
