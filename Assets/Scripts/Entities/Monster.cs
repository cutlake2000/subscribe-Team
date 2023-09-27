using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterData monsterData;
    float MonsterHp;

    private void Start()
    {
        MonsterHp = monsterData.MonsterHp;
    }

    public void TakePhysicalDamage(int damageAmount)
    {
        MonsterHp -= damageAmount;
        if (MonsterHp <= 0)
            Die();
    }

    void Die()
    {
        //ToDo �ڿ� ŉ�� ���

        Destroy(gameObject);
    }
}
