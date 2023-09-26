using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MercenaryCtrl : MonoBehaviour
{
    public Rigidbody mercenary;
    Animator animator;
    public MercenaryData data;
    GameObject target;
    private float targetDistance;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Enemy");
        
    }
    private void Start()
    {
        // 낮이 되어 자유롭게 대기중일때
       StartCoroutine(MoveObject());
    }

    private void Update()
    {
        float Distance = GetDistance();
        Debug.Log(data.AttackRange);
        if(Distance < data.AttackRange)
        {
            Invoke(nameof(Attacking),0.5f);
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

    void Attacking()
    {
        // 타겟과의 거리가 공격범위보다 작으면 공격!

        animator.SetTrigger("Attack");
        
    }
    float GetDistance()
    { 
        targetDistance = Vector3.Distance(target.transform.position, mercenary.transform.position);
        return targetDistance;
    }
    void Moving()
    {

    }

}
