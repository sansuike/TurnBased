using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CharacterStateControl
{
    //是否可移动
    public bool canMove;
    //是否可使用技能
    public bool canUseSkill;
    //是否可普攻
    public bool canGeneralattack;

    public CharacterStateControl(bool canMove=true,bool canGeneralattack=true,bool canUseSkill=true)
    {
        this.canMove = canMove;
        this.canUseSkill = canUseSkill;
        this.canGeneralattack = canGeneralattack;
    }

    public void Init()
    {
        this.canMove = true;
        this.canUseSkill = true;
        this.canGeneralattack = true;
    }
}
