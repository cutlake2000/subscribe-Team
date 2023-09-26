using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
   public MonsterData monsterData;

      public void printMonsterData()
    {
        Debug.Log("MonsterName:" + monsterData.MonsterName);
        Debug.Log("MonsterHp:" + monsterData.MonsterHp);
        Debug.Log("MonsterDF:" + monsterData.MonsterDF);
        Debug.Log("MonsterSpeed:" + monsterData.MonsterSpeed);
        Debug.Log("-================================================================-");
    }
}
