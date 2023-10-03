using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum MonsterType
{
    Dog,
    Ghost,
    Tiger
}

public class MonsterController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> monsters;

    [SerializeField]
    private GameObject Spawnner;

    [SerializeField]
    private TextMeshProUGUI TxtMonsterCount;

    private int previousChildCount = -1; //���� �������� �ڽ� ������Ʈ ����
    private int CurMonsterCount;

    [Header("level")]
    public int MonsterCount = 10;
    DayManager dayManager;

    private void Awake()
    {
        int currentChildCount = Spawnner.transform.childCount;
    }

    void Start()
    {
        
    }

    void Update()
    {
       
        int currentChildCount = Spawnner.transform.childCount;
        StartCoroutine(SpawnMonsters(dayManager.DayCount, currentChildCount));
        ChangeCountText(currentChildCount);
        changeLevel(currentChildCount);
    }




    private Monster SpwanMonster(int Level)
    {
        var newMonster = Instantiate(monsters[Level]).GetComponent<Monster>();
        newMonster.transform.SetParent(Spawnner.transform);
        //newMonster.monsterData = monsterDatas[(int)type];
        newMonster.name = newMonster.monsterData.MonsterName;

        return newMonster;
    }

    IEnumerator SpawnMonsters(int Level, int currentChildCount)
    {
        if (dayManager.dayNight == DayNight.Night&& currentChildCount <= 0)
        {
            for (int i = 0; i < MonsterCount; i++)
            {
                SetPosition(Level);
                SpwanMonster(Level);
                yield return new WaitForSeconds(1.0f);
            }
        }
    }

    private void SetPosition(int Level)
    {
        Vector3 newPosition = Spawnner.transform.position;
        monsters[Level].transform.position = newPosition;
    }

    private void changeLevel(int currentChildCount) 
    {
        if (currentChildCount <= 0) { }
    }

    private void ChangeCountText(int currentChildCount) 
    {
        if (currentChildCount != previousChildCount)
        {
            TxtMonsterCount.text = currentChildCount.ToString();

            previousChildCount = currentChildCount;
        }
    }
}
