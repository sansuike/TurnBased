
using System;
using UnityEngine;

public enum BuffTimeUpdateEnum
{
    Add,
    Replace,
    Remove,
    Keep,
}

public enum BuffRemoveStackUpdateEnum
{
    Clear,
    Reudce,
}

public class BuffInfo
{
    public BuffData buffData;
    public GameObject Creator;
    public GameObject Target;
    public float durationTimer;
    public float tickTimer;
    public float curStack;
}

public class DamageInfo
{
    public GameObject Creator;
    public GameObject Target;
    public float damage;
}

[Serializable]
public class Property
{
    public float hp;
    public float speed;
    public float attack;
    public float def;
}