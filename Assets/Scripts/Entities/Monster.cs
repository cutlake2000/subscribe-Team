using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
   
    public MonsterData MonsterData;
    float MonsterHp;
    private void Start()
    {
        MonsterHp = MonsterData.MonsterHp;
    }


    public void TakePhysicalDamage(int damageAmount)
    {
        MonsterHp -= damageAmount;
        if (MonsterData.MonsterHp <= 0)
            Die();     
    }


    void Die()
    {
         //ToDo ÀÚ¿ø Å‰µæ ±â´É 

        Destroy(gameObject);
}
    }
