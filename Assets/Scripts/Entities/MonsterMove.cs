using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    public MonsterData monsterData;
    private Vector3 currentDirection = Vector3.back;

    public void FixedUpdate()
    {
        transform.Translate(
            currentDirection * monsterData.MonsterSpeed * Time.deltaTime,
            Space.World
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Trigger")
        {
            Debug.Log("�浹");
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        if (currentDirection == Vector3.back)
        {
            currentDirection = Vector3.right;
        }
        else if (currentDirection == Vector3.right)
        {
            currentDirection = Vector3.forward;
        }
        else if (currentDirection == Vector3.forward)
        {
            currentDirection = Vector3.left;
        }
        else if (currentDirection == Vector3.left)
        {
            currentDirection = Vector3.back;
        }
    }
}
