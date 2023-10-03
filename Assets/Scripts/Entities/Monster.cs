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

    void Update()
    {
        // 스페이스바를 누를 때 Die() 메서드를 호출합니다.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Die();
        }
    }

    void Die()
    {
        float randomValue = Random.value;
        if (randomValue <= 0.5f)
        {
            DataManager.Instance.player.Gold++;
            DataManager.Instance.player.Wood++;
            Debug.Log("자원+");
        }

        Destroy(gameObject);
    }
}
