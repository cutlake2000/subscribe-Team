using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum MonsterType {Dog,Ghost,Tiger }
public class MonsterController : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> monsters;
    [SerializeField]
    private GameObject Spawnner;

    [SerializeField]
    private TextMeshProUGUI TxtMonsterCount;


    private int previousChildCount = -1; //이전 프레임의 자식 오브젝트 개수
    private int CurMonsterCount;

    [Header("level")]
    public int MonsterCount = 10;  
    public int Level = 1;

    private void Awake()
    {
        int currentChildCount = Spawnner.transform.childCount;
    }
    void Start()
    {    
        StartCoroutine(SpawnMonsters(Level)) ;    
    }
    void Update()
    {
        int currentChildCount = Spawnner.transform.childCount;
        ChangeCountText(currentChildCount);
        changeLevel(currentChildCount);
    }

         private Monster SpwanMonster(int Level)
      {

        var newMonster = Instantiate(monsters[Level]).GetComponent<Monster>();
        newMonster.transform.SetParent(Spawnner.transform);
        //newMonster.monsterData = monsterDatas[(int)type];
        newMonster.name = newMonster.MonsterData.MonsterName;    
        
        return newMonster;
    }
    IEnumerator SpawnMonsters(int Level)
    {
        for (int i = 0; i < MonsterCount; i++)
        {
            SetPosition(Level);
            SpwanMonster(Level);         
            yield return new WaitForSeconds(1.0f);
        }
    }

    private void SetPosition(int Level)
    {
        Vector3 newPosition = Spawnner.transform.position;
        monsters[Level].transform.position = newPosition;
    }






    private void changeLevel(int currentChildCount) // 몬스터 수다 0이됬을때 
    {
        if (currentChildCount <= 0)
        {

        }


    }
    private void ChangeCountText(int currentChildCount)// 이전 프레임과 현재 프레임의 자식 오브젝트 개수를 비교 하여 바뀌었을때만 텍스트바꿔줌 
    {



        if (currentChildCount != previousChildCount)
        {

            TxtMonsterCount.text = currentChildCount.ToString();


            previousChildCount = currentChildCount;
        }
    }

}
