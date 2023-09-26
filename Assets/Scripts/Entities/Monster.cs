using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
   public MonsterData monsterData;


    public void FixedUpdate()
    {
        transform.Translate(Vector3.forward * monsterData.MonsterSpeed * Time.deltaTime);
    }

}
