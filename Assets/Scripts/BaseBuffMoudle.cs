using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBuffMoudle:ScriptableObject
{
    // public abstract void Init(BuffData data);
    // public abstract void OnEnter();
    // public abstract void OnUpdate(float deltaTime);
    // public abstract void OnExit();
    // public abstract void OnRemove();
    // public abstract void OnRemoveStack(int count);
    // public abstract void OnRemoveStack(int count,BuffRemoveStackUpdateEnum updateEnum);
    // public abstract void OnRemoveStack(int count,BuffRemoveStackUpdateEnum updateEnum,float duration);
    public abstract void Apply(BuffInfo buffInfo,DamageInfo damageInfo=null);
}

