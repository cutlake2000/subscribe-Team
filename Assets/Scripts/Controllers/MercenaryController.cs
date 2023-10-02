using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MercenaryController : MonoBehaviour
{
    public Rigidbody mercenary;
    public MercenaryData data;
    public List<GameObject> target;
    public GameObject enemy;
    Animator animator;
    Monster monster;
    public new SpriteRenderer renderer;
    private float targetDistance;
    public string TagName = "Monster";
    public MonsterData thisMonster;
    public Monster CloseMonster;
    private bool isCoroutineRunning = false;
    private bool isAttackObject = false;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        target = new List<GameObject>();
    }

    private void Start() { }

    private void Update()
    {
        target.Clear();
        target.AddRange(GameObject.FindGameObjectsWithTag(TagName));
        if (target.Count <= 0)
        {
            if(isCoroutineRunning)
            {
                return;
            }
            else
            {
                StartCoroutine(MoveObject());
            }
        }
        else if(target.Count > 0)
        {
            if (isAttackObject)
            {
                return;
            }
            else
            {
                Invoke("AttackObject", data.AttackSpeed);
            }
        }
    }

    IEnumerator MoveObject()
    {
        isCoroutineRunning = true;
        mercenary = GetComponent<Rigidbody>();

        while (true)
        {
            float dir1 = Random.Range(-1f, 1f);
            float dir2 = Random.Range(-1f, 1f);

            yield return new WaitForSeconds(1);
            mercenary.velocity = new Vector3(dir1, 0, dir2);
        }
        isCoroutineRunning=false;
    }

    void AttackObject()
    {
        GameObject CloseEnemy = GetClosest();
        monster = CloseEnemy.GetComponent<Monster>();
        monster.monsterData = WhichMonster(monster);
        float distance = GetDistance(CloseEnemy);
        if (CloseEnemy.transform.position.x < gameObject.transform.position.x)
        {
            renderer.flipX = true;
        }
        Moving(CloseEnemy, data.MovingSpeed);

        if (distance < data.AttackRange)
        {
            monster.TakePhysicalDamage(data.Attack);
        }
    }
    GameObject GetClosest()
    {
        enemy = target[0];
        float ShortDistance = Vector3.Distance(
            gameObject.transform.position,
            enemy.transform.position
        );
        foreach (GameObject found in target)
        {
            float distance = GetDistance(found);
            if (distance < ShortDistance)
            {
                enemy = found;
                ShortDistance = distance;
            }
        }
        return enemy;
    }

    void Attacking()
    {
        animator.SetTrigger("Attack");
    }

    float GetDistance(GameObject target)
    {
        targetDistance = Vector3.Distance(target.transform.position, gameObject.transform.position);
        return targetDistance;
    }

    void Moving(GameObject target, float speed)
    {
        Vector3 destination = target.transform.position;
        transform.position = Vector3.Lerp(transform.position, destination, 0.001f * speed);
    }

    void Hit(Monster target)
    {
        Attacking();
        target.monsterData.MonsterHp -= data.Attack;
    }

    MonsterData WhichMonster(Monster monster)
    {
        thisMonster = ScriptableObject.CreateInstance<MonsterData>();

        for (int i = 0; i < DataManager.Instance.monsterDatas.Length; i++)
        {
            MonsterData monsterdata = DataManager.Instance.monsterDatas[i];
            if (monsterdata.MonsterName == monster.monsterData.MonsterName)
            {
                thisMonster.MonsterHp = DataManager.Instance.monsterDatas[i].MonsterHp;
                break;
            }
        }
        return thisMonster;
    }

    bool CheckDayandNight()
    {
        // 지금 낮인지 밤인지 확인
        return true;
    }

    //public void DayandNight()
    //{
    //    // 각 낮, 밤의 행동을 정의해주는 함수
    //    // 낮 : 밤 시간대의 용병스포너의 좌표를 받아 그쪽으로 용병 이동 > 낮 시간대의 용병스포너의 좌표로 이동, 동시에 y축을 기준으로 뒤집기
    //    GameObject DaySpawner = GameObject.FindGameObjectWithTag("DaySpawner");
    //    GameObject NightSpawner = GameObject.FindGameObjectWithTag("NightSpawner");
    //    if () // 밤일 경우
    //    {
    //        Vector3 destination = DaySpawner.transform.position;
    //        transform.position = Vector3.Lerp(transform.position, destination, 0.001f );
    //        if(gameObject.transform.position == destination)
    //        {
    //            gameObject.transform.position = NightSpawner.transform.position;
    //            renderer.flipY = true;
    //        }
    //    }
    //    else if () // 낮일 경우
    //    {
    //        Vector3 destination = NightSpawner.transform.position;
    //        transform.position = Vector3.Lerp(transform.position, destination, 0.001f);
    //        if (gameObject.transform.position == destination)
    //        {
    //            gameObject.transform.position = DaySpawner.transform.position;
    //            renderer.flipY = true;
    //        }
    //    }
    //}

    public void temp()
    {
        GameManager.Instance.MercenaryUI.Mercenary = data;
        GameManager.Instance.clickMercenaryUI.PopUp(gameObject.transform.position);
    }
}
