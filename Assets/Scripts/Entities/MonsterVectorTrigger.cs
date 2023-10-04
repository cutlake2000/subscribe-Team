using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterVectorTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        {
          
            Vector3 currentRotation = other.transform.rotation.eulerAngles;
            currentRotation.y -= 180f;
            other.transform.rotation = Quaternion.Euler(currentRotation);
        }
    }
}
