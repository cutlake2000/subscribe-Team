using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MercenaryCtrl : MonoBehaviour
{
    public Rigidbody mercenary;

    private void Start()
    {
        StartCoroutine(MoveObject());
    }
    
    IEnumerator MoveObject()
    {
        mercenary = GetComponent<Rigidbody>();

        while(true)
        {
            float dir1 = Random.Range(-1f, 1f);
            float dir2 = Random.Range(-1f, 1f);

            yield return new WaitForSeconds(2);
            mercenary.velocity = new Vector3(dir1, 0, dir2);
        }
    }

}
