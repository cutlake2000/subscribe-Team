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
    }

    private void Start() 
    {
        //StartCoroutine(MoveObject());
    }

    private void Update()
    {
        target = new List<GameObject>(GameObject.FindGameObjectsWithTag(TagName));
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
        else
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
        thisMonster = new MonsterData();
        for(int i =0; i < DataManager.instance.monsterDatas.Length; i++)
        {
            MonsterData monsterdata = DataManager.instance.monsterDatas[i]; 
            if(monsterdata.MonsterName == monster.monsterData.MonsterName)
            {
                thisMonster.MonsterHp = DataManager.instance.monsterDatas[i].MonsterHp;
                break;
            }
        }
        //switch (monster.monsterData.MonsterName)
        //{
        //    case "개":
        //        thisMonster.MonsterHp = DataManager.instance.monsterDatas[0].MonsterHp;
        //        break;
        //    case "유령":
        //        thisMonster.MonsterHp = DataManager.instance.monsterDatas[1].MonsterHp;
        //        break;
        //    case "호랑이":
        //        thisMonster.MonsterHp = DataManager.instance.monsterDatas[2].MonsterHp;
        //        break;
        //}
        return thisMonster;
    }
}
