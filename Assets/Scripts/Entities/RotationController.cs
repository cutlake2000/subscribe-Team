using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    [SerializeField]
    private GameObject Ground;

    [SerializeField]
    private GameObject Cloud1;

    [SerializeField]
    private GameObject Cloud2;

    [SerializeField]
    private GameObject Cloud3;

    [SerializeField]
    private GameObject Background;

    public bool isSkyRotating;
    public bool isGroundRotating;

    public void CallSkyRotationCoroutine()
    {
        StartCoroutine(RotationSky());
    }

    public void CallGroundRotationCoroutine()
    {
        StartCoroutine(RotationGround());
    }

    private IEnumerator RotationSky()
    {
        isSkyRotating = true;

        Quaternion currentRotation = Cloud1.transform.rotation;

        Vector3 targetEulerAngles = Cloud1.transform.rotation.eulerAngles;
        targetEulerAngles.z += 179.9f;

        Quaternion targetRotation = Quaternion.Euler(targetEulerAngles);

        while (DayManager.Instance.NowTime < DayManager.Instance.DayTime)
        {
            float flowTime = DayManager.Instance.NowTime / DayManager.Instance.DayTime;
            Cloud1.transform.rotation = Quaternion.Euler(
                Vector3.Lerp(currentRotation.eulerAngles, targetRotation.eulerAngles, flowTime)
            );
            Cloud2.transform.rotation = Quaternion.Euler(
                Vector3.Lerp(currentRotation.eulerAngles, targetRotation.eulerAngles, flowTime)
            );
            Cloud3.transform.rotation = Quaternion.Euler(
                Vector3.Lerp(currentRotation.eulerAngles, targetRotation.eulerAngles, flowTime)
            );
            Background.transform.rotation = Quaternion.Euler(
                Vector3.Lerp(currentRotation.eulerAngles, targetRotation.eulerAngles, flowTime)
            );

            DayManager.Instance.NowTime += Time.deltaTime;

            yield return null;
        }

        targetEulerAngles.z = Round180(targetEulerAngles.z);

        Cloud1.transform.rotation = Quaternion.Euler(targetEulerAngles);
        Cloud2.transform.rotation = Quaternion.Euler(targetEulerAngles);
        Cloud3.transform.rotation = Quaternion.Euler(targetEulerAngles);
        Background.transform.rotation = Quaternion.Euler(targetEulerAngles);

        DayManager.Instance.NowTime = 0.0f;

        isSkyRotating = false;
    }

    private IEnumerator RotationGround()
    {
        isGroundRotating = true;

        float startTime = 0.0f;
        float targetTime = 5.0f;

        Quaternion currentRotation = Ground.transform.rotation;

        Vector3 targetEulerAngles = Ground.transform.rotation.eulerAngles;
        targetEulerAngles.z += 179.9f;

        Quaternion targetRotation = Quaternion.Euler(targetEulerAngles);

        while (startTime < targetTime)
        {
            float flowTime = startTime / targetTime;
            Ground.transform.rotation = Quaternion.Euler(
                Vector3.Lerp(currentRotation.eulerAngles, targetRotation.eulerAngles, flowTime)
            );
            startTime += Time.deltaTime;

            yield return null;
        }

        targetEulerAngles.z = Round180(targetEulerAngles.z);

        Ground.transform.rotation = Quaternion.Euler(targetEulerAngles);

        isGroundRotating = false;
    }

    private float Round180(float eulerAngles)
    {
        float newEulerAngles = eulerAngles % 180;

        return (newEulerAngles < 90)
            ? eulerAngles - newEulerAngles
            : eulerAngles - newEulerAngles + 180;
    }
}
