using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CharacterProperty
{
   public int hp;

   public int attack;

   public int speed;
   public CharacterProperty(int hp=0,int attack=0,int speed=0)
   {
      this.hp = hp;
      this.attack = attack;
      this.speed = speed;
   }
   
   public static CharacterProperty zero = new CharacterProperty(0,0,0);
}

///<summary>
///角色的资源类属性，比如hp，mp等都属于这个
///</summary>
public class ChaResource{
   ///<summary>
   ///当前生命
   ///</summary>
   public int hp;

   ///<summary>
   ///当前弹药量，在这游戏里就相当于mp了
   ///</summary>
   public int ammo;

   ///<summary>
   ///当前耐力，耐力是一个百分比消耗，实时恢复的概念，所以上限按规则就是100了，这里是现有多少
   ///</summary>
   public int stamina;

   public ChaResource(int hp, int ammo = 0, int stamina = 0){
      this.hp = hp;
      this.ammo = ammo;
      this.stamina = stamina;
   }

   ///<summary>
   ///是否足够
   ///</summary>
   public bool Enough(ChaResource requirement){
      return (
         this.hp >= requirement.hp &&
         this.ammo >= requirement.ammo &&
         this.stamina >= requirement.stamina
      );
   }

   public static ChaResource operator +(ChaResource a, ChaResource b){
      return new ChaResource(
         a.hp + b.hp,
         a.ammo + b.ammo,
         a.stamina + b.stamina
      );
   }
   public static ChaResource operator *(ChaResource a, float b){
      return new ChaResource(
         Mathf.FloorToInt(a.hp * b),
         Mathf.FloorToInt(a.ammo * b),
         Mathf.FloorToInt(a.stamina * b)
      );
   }
   public static ChaResource operator *(float a, ChaResource b){
      return new ChaResource(
         Mathf.FloorToInt(b.hp * a),
         Mathf.FloorToInt(b.ammo * a),
         Mathf.FloorToInt(b.stamina * a)
      );
   }
   public static ChaResource operator -(ChaResource a, ChaResource b){
      return a + b * (-1);
   }

   public static ChaResource Null = new ChaResource(0);
}
