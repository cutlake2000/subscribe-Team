using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
//using Unity.VisualScripting;
using UnityEditor;

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
    public GameObject NightSpawner;
    public GameObject DaySpawner;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        target = new List<GameObject>();
        DaySpawner = GameObject.FindGameObjectWithTag("DaySpawner");
        NightSpawner = GameObject.FindGameObjectWithTag("NightSpawner");
    }

    private void Start() { }

    private void Update()
    {
        if (DayManager.Instance.isGroundRotating == true)
        {
            DayandNight();
        }
        else if (DayManager.Instance.isGroundRotating == false)
        {
            if (DayManager.Instance.dayNight == DayNight.Day)
            {
                StartCoroutine(MoveObject());
            }
            else if (DayManager.Instance.dayNight == DayNight.Night)
            {
                target.Clear();

                if (GameObject.FindGameObjectsWithTag(TagName) != null)
                {
                    target.AddRange(GameObject.FindGameObjectsWithTag(TagName));
                }

                if (target.Count <= 0)
                {
                    StartCoroutine(MoveObject());
                }
                else if (target.Count > 0)
                {
                    // Invoke(nameof(AttackObject), data.AttackSpeed);
                    AttackObject();
                }
            }
        }
    }

    //else
    //{
    //    if (DayManager.Instance.NowTime == DayManager.Instance.DayTime)
    //    {
    //        if (DayManager.Instance.dayNight == DayNight.Day)
    //        {
    //            daynight = false;
    //        }
    //        else if (DayManager.Instance.dayNight == DayNight.Night)
    //        {
    //            daynight = true;
    //        }
    //        DayandNight(daynight); // 낮>밤으로 바뀔때
    //    }
    //}

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

    void AttackObject()
    {
        GameObject CloseEnemy = GetClosest();
        if (CloseEnemy == null)
            return;

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
            // Attack();
            //StartCoroutine(monster.TakePhysicalDamage(data.Attack));
            Attacking();
        }
    }

    GameObject GetClosest()
    {
        if (target.Count ==0 || target[0] == null)
            return null;

        enemy = target[0];
        float ShortDistance = Vector3.Distance(
            gameObject.transform.position,
            enemy.transform.position
        );
        foreach (GameObject found in target)
        {
            if (found == null)
                return null;

            float distance = GetDistance(found);
            if (distance < ShortDistance)
            {
                enemy = found;
                ShortDistance = distance;
            }
        }
        return enemy;
    }

    void Attack()
    {
        StartCoroutine(monster.TakePhysicalDamage(data.Attack));
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

    public bool daynight;

    public void DayandNight()
    {
        // 각 낮, 밤의 행동을 정의해주는 함수
        // 낮 : 밤 시간대의 용병스포너의 좌표를 받아 그쪽으로 용병 이동 > 낮 시간대의 용병스포너의 좌표로 이동, 동시에 y축을 기준으로 뒤집기
        if (DayManager.Instance.dayNight == DayNight.Day) // 밤일 경우
        {
            Vector3 destination = DaySpawner.transform.position;
            transform.position = Vector3.Lerp(transform.position, destination, 0.1f);
        }
        else if (DayManager.Instance.dayNight == DayNight.Night) // 낮일 경우
        {
            Vector3 destination = NightSpawner.transform.position;
            transform.position = Vector3.Lerp(transform.position, destination, 0.1f);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (DayManager.Instance.isGroundRotating)
        {
            switch (collision.gameObject.tag)
            {
                case "DaySpawner":
                {
                    gameObject.transform.position = new Vector3(
                        NightSpawner.transform.position.x + 1,
                        NightSpawner.transform.position.y,
                        NightSpawner.transform.position.z + 1
                    );
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
                }
                case "NightSpawner":
                {
                    gameObject.transform.position = new Vector3(
                        DaySpawner.transform.position.x + 1,
                        DaySpawner.transform.position.y,
                        DaySpawner.transform.position.z + 1
                    );
                    //gameObject.transform.rotation = Quaternion.Euler(0, 0, 180);

                    break;
                }
            }
        }
    }

    //public void CheckDay(DayNight dayNight)
    //{
    //    switch (dayNight)
    //    {
    //        case DayNight.Day:
    //            gameObject.SetActive(false);
    //            break;
    //        case DayNight.Night:
    //            gameObject.SetActive(false);
    //            break;
    //    }
    //}
    public void temp()
    {
        GameManager.Instance.MercenaryUI.Mercenary = data;
        GameManager.Instance.clickMercenaryUI.PopUp(gameObject.transform.position);
    }
}
