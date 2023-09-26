using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType {Normal,Power,Speed }
public class MonsterController : MonoBehaviour
{
    [SerializeField]
    private List<MonsterData> monsterDatas;
    [SerializeField]
    private GameObject MonsterPrefab;

    public int monsterCount = 10;
    public int Level = 1;
    void Start()
    {
   
          
            StartCoroutine(SpawnMonsters()) ;
    
    }

   public Monster SpawnMonster(MonsterType type)
    {

        var newMonster = Instantiate(MonsterPrefab).GetComponent<Monster>();
        newMonster.monsterData = monsterDatas[(int)type];
        newMonster.name = newMonster.monsterData.MonsterName;      
        return newMonster;
    }
    IEnumerator SpawnMonsters()
    {
        for (int i = 0; i < monsterCount; i++)
        {
            SpawnMonster((MonsterType)Level);
            yield return new WaitForSeconds(1.0f); // 10초 동안 대기
        }
    }

}
