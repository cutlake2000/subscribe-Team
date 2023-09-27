using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    public Monster thisMonster;
    public Monster CloseMonster;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Start() { }

    private void Update()
    {
        target = new List<GameObject>(GameObject.FindGameObjectsWithTag(TagName));

        // ���� �Ǿ� �����Ӱ� ������϶�
        //StartCoroutine(MoveObject());

        GameObject CloseEnemy = GetClosest();
        monster = CloseEnemy.GetComponent<Monster>();
        monster.monsterData = WhichMonster(monster);
        float distance = GetDistance(CloseEnemy);
        // ���� ���ʿ� ������� �������ش�.
        if (CloseEnemy.transform.position.x < gameObject.transform.position.x)
        {
            renderer.flipX = true;
        }
        //���� ����� ���� ���� �����δ�.
        Moving(CloseEnemy, data.MovingSpeed);

        if (distance < data.AttackRange)
        {
            Hit(monster);
        }
        if (CloseMonster.monsterData.MonsterHp <= 0)
        {
            Destroy(CloseEnemy);
        }
    }

    IEnumerator MoveObject()
    {
        mercenary = GetComponent<Rigidbody>();

        while (true)
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
        // Ÿ�ٰ��� �Ÿ��� ���ݹ������� ������ ����!
        animator.SetTrigger("Attack");
    }

    float GetDistance(GameObject target)
    {
        targetDistance = Vector3.Distance(target.transform.position, gameObject.transform.position);
        return targetDistance;
    }

    void Moving(GameObject target, float Objectspeed)
    {
        // ���� �����̿� �ִ� ������ ����
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
        thisMonster = new Monster();
        switch (monster.monsterData.MonsterName)
        {
            case "��":
                thisMonster.monsterData = DataManager.instance.monsterDatas[0];
                break;
            case "����":
                thisMonster.monsterData = DataManager.instance.monsterDatas[1];
                break;
            case "ȣ����":
                thisMonster.monsterData = DataManager.instance.monsterDatas[2];
                break;
        }
        return thisMonster;
    }
}
