using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuffHandler : MonoBehaviour
{
    public List<BuffInfo> buffList = new List<BuffInfo>();

    private void Update()
    {
        BuffTickAndRemove();
    }

    //添加buff
    public void AddBuff(BuffInfo buffInfo)
    {
        BuffInfo findBuffInfo = FindBuffInfo(buffInfo.buffData.id);
        //添加buff时 buff不存在
        if (findBuffInfo == null)
        {
            buffInfo.durationTimer= buffInfo.buffData.duration;
            buffInfo.tickTimer = buffInfo.buffData.tickTime;
            buffList.Add(buffInfo);
            //排序Buff
            SortBuff();
            buffInfo.buffData.OnCreate.Apply(buffInfo);
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
            findBuffInfo.buffData.OnCreate.Apply(findBuffInfo);
        }
    }

    //移除buff
    public void RemoveBuff(BuffInfo buffInfo)
    {
        if (FindBuffInfo(buffInfo.buffData.id) == null)
        {
            Debug.LogError($"移除不存在Buff:{buffInfo.buffData.id}");
            return;
        }
        switch (buffInfo.buffData.buffRemoveStackUpdateEnum)
        {
            case BuffRemoveStackUpdateEnum.Clear:
                buffInfo.buffData.OnRemove.Apply(buffInfo);
                buffList.Remove(buffInfo);
                break;
            case BuffRemoveStackUpdateEnum.Reudce:
                buffInfo.curStack --;
                buffInfo.buffData.OnRemove.Apply(buffInfo);
                if (buffInfo.curStack <= 0)
                {
                    buffInfo.buffData.OnRemove.Apply(buffInfo);
                    buffList.Remove(buffInfo);
                }
                else
                {
                    buffInfo.durationTimer= buffInfo.buffData.duration;
                }
                break;
        }
    }


    //buff Tick触发
    private void BuffTickAndRemove()
    {
        List<BuffInfo> removebuffInfoList = new List<BuffInfo>();

        foreach (var buffInfo in buffList)
        {
            if (buffInfo.buffData.OnTick != null)
            {
                if (buffInfo.tickTimer <= 0)
                {
                    buffInfo.buffData.OnTick.Apply(buffInfo);
                    buffInfo.tickTimer=buffInfo.buffData.tickTime;
                }
                else
                {
                    buffInfo.tickTimer-=Time.deltaTime;
                }
            }
            //更新buff时间
            if (buffInfo.durationTimer <= 0)
            {
                removebuffInfoList.Add(buffInfo);
            }
            else
            {
                buffInfo.durationTimer -= Time.deltaTime;
            }
        }
        foreach (var buffInfo in removebuffInfoList)
        {
            RemoveBuff(buffInfo);
        }
    }

    //排序buff
    private void SortBuff()
    {
        buffList.OrderBy(a => a.buffData.priority);
    }

    //查找buff
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
