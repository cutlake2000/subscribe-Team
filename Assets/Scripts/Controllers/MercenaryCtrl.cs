using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MercenaryCtrl : MonoBehaviour
{
    public Rigidbody mercenary;
    public MercenaryData data;
    public List<GameObject> target;
    public GameObject enemy;
    Animator animator;
    Monster monster;
    MonsterData thisMonster;
    public SpriteRenderer renderer;
    private float targetDistance;
    public string TagName = "Monster";
    private void Awake()
    {
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {

    }

    private void Update()
    {
        target = new List<GameObject>(GameObject.FindGameObjectsWithTag(TagName));

        // 낮이 되어 자유롭게 대기중일때
        //StartCoroutine(MoveObject());

        GameObject CloseEnemy = GetClosest();
        monster = CloseEnemy.GetComponent<Monster>();
        monster.monsterData = WhichMonster(monster);
        float distance = GetDistance(CloseEnemy);
        // 적이 왼쪽에 있을경우 뒤집어준다.
        if(CloseEnemy.transform.position.x < gameObject.transform.position.x)
        {
            renderer.flipX = true;
        }
        //가장 가까운 적을 향해 움직인다.
        Moving(CloseEnemy, data.MovingSpeed);

        if (distance < data.AttackRange) 
        {
            Hit(monster);
        }
        if(monster.monsterData.MonsterHp <= 0)
        {
            Destroy(CloseEnemy);
        }
    }

    IEnumerator MoveObject()
    {
        mercenary = GetComponent<Rigidbody>();

        while(true)
        {
            float dir1 = Random.Range(-1f, 1f);
            float dir2 = Random.Range(-1f, 1f);

            yield return new WaitForSeconds(1);
            mercenary.velocity = new Vector3(dir1, 0, dir2);
        }
    }

    GameObject GetClosest()
    {
        enemy = target[0];
        float ShortDistance = Vector3.Distance(gameObject.transform.position, enemy.transform.position);
        foreach(GameObject found in target)
        {
           float distance =  GetDistance(found);
            if(distance < ShortDistance)
            {
                enemy = found;
                ShortDistance = distance;
            }
        }
        return enemy;
    }

    void Attacking()
    {
        // 타겟과의 거리가 공격범위보다 작으면 공격!
        animator.SetTrigger("Attack");
    }

    float GetDistance(GameObject target)
    { 
        targetDistance = Vector3.Distance(target.transform.position, gameObject.transform.position);
        return targetDistance;
    }

    void Moving(GameObject target, float Objectspeed)
    {
        // 가장 가까이에 있는 적에게 접근
        Vector3 speed = Vector3.zero;
        Vector3 destination = target.transform.position;
        transform.position = Vector3.Lerp(transform.position, destination, 0.001f * Objectspeed);
    }

    void Hit(Monster target)
    {
        Attacking();
        target.monsterData.MonsterHp -= data.Attack;
    }

    MonsterData WhichMonster(Monster monster)
    {
        thisMonster = new MonsterData();
        switch(monster.monsterData.MonsterName)
        {
            case "개":
                thisMonster.MonsterHp = DataManager.instance.monsterDatas[0].MonsterHp;
                thisMonster.MonsterDF = DataManager.instance.monsterDatas[0].MonsterDF;
                thisMonster.MonsterSpeed = DataManager.instance.monsterDatas[0].MonsterSpeed;

                break;
            case "유령":
                thisMonster.MonsterHp = DataManager.instance.monsterDatas[1].MonsterHp;
                thisMonster.MonsterDF = DataManager.instance.monsterDatas[1].MonsterDF;
                thisMonster.MonsterSpeed = DataManager.instance.monsterDatas[1].MonsterSpeed;
                break;
            case "호랑이":
                thisMonster.MonsterHp = DataManager.instance.monsterDatas[2].MonsterHp;
                thisMonster.MonsterDF = DataManager.instance.monsterDatas[2].MonsterDF;
                thisMonster.MonsterSpeed = DataManager.instance.monsterDatas[2].MonsterSpeed;
                break;
        }
        return thisMonster;
    }
    
}
