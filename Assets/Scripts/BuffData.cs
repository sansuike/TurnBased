using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffData",menuName = "BuffSystem/BuffData",order = 1)]
public class BuffData :ScriptableObject
{
    public int id;
    public string buffName;
    public string description;
    public Sprite icon;
    public int priority;
    public int maxStack;
    public string[] tags;

    
    public bool isForever;
    public float duration;
    public float tickTime;
    
    //buff更新方式
    public BuffTimeUpdateEnum buffTimeUpdateEnum;
    public BuffRemoveStackUpdateEnum buffRemoveStackUpdateEnum;

    //buff创建回调点
    public BaseBuffMoudle OnCreate;
    //buff移除回调点
    public BaseBuffMoudle OnRemove;
    //
    public BaseBuffMoudle OnTick;
    
    //伤害回调点
    public BaseBuffMoudle OnHit;
    public BaseBuffMoudle OnBeHurt;
    public BaseBuffMoudle OnKill;
    public BaseBuffMoudle OnBeKill;

}
