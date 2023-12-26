using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInfo
{
    public GameObject attacker;
    public GameObject defender;
    public DamageInfoTag[] tags;
    //伤害值
    public Damage damage;
    //是否暴击
    public float criticalRate;
    //伤害产生后 给角色施加的buff
    public List<AddBuffInfo> addBuffs = new List<AddBuffInfo>();
}

public struct Damage
{
    public int fire;
    public int water;

    public Damage(int fire, int water)
    {
        this.fire = fire;
        this.water = water;
    }
    public static Damage operator +(Damage a, Damage b){
        return new Damage(
            a.fire + b.fire,
            a.water + b.water
        );
    }
    public static Damage operator *(Damage a, float b){
        return new Damage(
            Mathf.RoundToInt(a.fire * b),
            Mathf.RoundToInt(a.water * b)
        );
    }
}

public enum DamageInfoTag{
    directDamage = 0,   //直接伤害
    periodDamage = 1,   //间歇性伤害
    reflectDamage = 2,  //反噬伤害
    directHeal = 10,    //直接治疗
    periodHeal = 11,    //间歇性治疗
    monkeyDamage = 9999    //这个类型的伤害在目前这个demo中没有意义，只是告诉你可以随意扩展，仅仅比string严肃些。
}