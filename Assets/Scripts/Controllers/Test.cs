using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        {
            Debug.Log("충돌");
            Vector3 currentRotation = other.transform.rotation.eulerAngles;
            currentRotation.y -= 90f;
            other.transform.rotation = Quaternion.Euler(currentRotation);
        }
    }
}
