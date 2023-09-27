using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
   public MonsterData monsterData;
    private Vector3 currentDirection = Vector3.forward;

    public void FixedUpdate()
    {
        transform.Translate(currentDirection * monsterData.MonsterSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Trigger")
        {
            Debug.Log("Ãæµ¹");          
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
    
        if (currentDirection == Vector3.forward)
        {
            currentDirection = Vector3.left;
        }
        else if (currentDirection == Vector3.left)
        {
            currentDirection = Vector3.back;
        }
        else if (currentDirection == Vector3.back)
        {
            currentDirection = Vector3.right;
        }
        else if (currentDirection == Vector3.right)
        {
            currentDirection = Vector3.forward;
        }
    }

}
