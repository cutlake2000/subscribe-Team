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
    void Start()
    {
        for (int i = 0; i < monsterDatas.Count; i++)
        {
            var monster = SpawnMonster((MonsterType)i);
            monster.printMonsterData();
        }
    }

   public Monster SpawnMonster(MonsterType type)
    {

        var newMonster = Instantiate(MonsterPrefab).GetComponent<Monster>();
        newMonster.monsterData = monsterDatas[(int)type];
        newMonster.name = newMonster.monsterData.MonsterName;      
        return newMonster;
    }

}
