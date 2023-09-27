using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyRotation : SkyRotationController
{
    protected override IEnumerator StartRotation()
    {
        currentTime = 0.0f;

        Quaternion currentRotation = transform.rotation;
        Vector3 targetEulerAngles = transform.rotation.eulerAngles;
        targetEulerAngles.z += 179.9f;

        Quaternion targetRotation = Quaternion.Euler(targetEulerAngles);

        while (currentTime < entireTime)
        {
            transform.rotation = Quaternion.Euler(
                Vector3.Lerp(
                    currentRotation.eulerAngles,
                    targetRotation.eulerAngles,
                    currentTime / entireTime
                )
            );

            currentTime += Time.deltaTime;
            yield return null;
        }

        targetEulerAngles.z = Round180(targetEulerAngles.z);

        transform.rotation = Quaternion.Euler(targetEulerAngles);
    }
}
