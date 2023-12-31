using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public MonsterData monsterData;
    public Image healthBarFiled;
    float MonsterHp;
    float CurHp;
    private void Start()
    {
        MonsterHp = monsterData.MonsterHp;
        CurHp = MonsterHp;
        healthBarFiled.fillAmount = 1f;
    }

    public IEnumerator TakePhysicalDamage(int damageAmount)
    {
      
         CurHp -= damageAmount + DataManager.Instance.player.AddUnitAtk;
         Debug.Log(CurHp);
         healthBarFiled.fillAmount = CurHp / MonsterHp;
        if (CurHp <= 0)
            Die();
        SoundManager.instance.HitSound();
        yield return new WaitForSeconds(0.1f);
    }

    void Update()
    {
      
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
