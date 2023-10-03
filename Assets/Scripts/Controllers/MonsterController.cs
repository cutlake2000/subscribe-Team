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

    private bool IsClear = true;


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

        if ( DayManager.Instance.dayNight == DayNight.Night&& DayManager.Instance.isGroundRotating == false&& IsClear == true)
        {
            StartCoroutine(SpawnMonsters(DayManager.Instance.DayCount - 1, currentChildCount));
        }

        if(DayManager.Instance.isSkyRotating ==false && currentChildCount>0 )
        {
            DestroyAllChildrenObjects();
        }
        
        MonsterspawnTrriger();

        ChangeCountText(currentChildCount);
      
    }


    void MonsterspawnTrriger()
    {
        if (DayManager.Instance.dayNight == DayNight.Day)
        {
            IsClear = true;
        }    
    }

    void DestroyAllChildrenObjects()
    {
        GameManager.Instance.isGameOver = true;
        foreach (Transform child in Spawnner.transform)
        {
            Destroy(child.gameObject);
        }
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
        if ( currentChildCount <= 0 )
        {
            for (int i = 0; i < MonsterCount; i++)
            {
                if (DayManager.Instance.dayNight == DayNight.Night)
                {
                    SetPosition(Level);
                    SpwanMonster(Level);
                    yield return new WaitForSeconds(1.0f);
                }
                else
                {
                    break;
                }              
            }
         
        }
        IsClear = false;
    }

    private void SetPosition(int Level)
    {
        Vector3 newPosition = Spawnner.transform.position;
        monsters[Level].transform.position = newPosition;
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
