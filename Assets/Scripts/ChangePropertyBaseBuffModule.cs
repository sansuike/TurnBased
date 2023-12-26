using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePropertyBaseBuffModule : BaseBuffMoudle
{
    public Property property;
    public override void Apply(BuffInfo buffInfo, DamageInfo damageInfo = null)
    {
        var character = buffInfo.Target.GetComponent<Character>();
        if (character!= null)
        {
            character.property.hp += property.hp;
            character.property.speed += property.speed;
            character.property.attack += property.attack;
            character.property.def += property.def;
        }
    }
}
