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
        Quaternion currentRotation = Cloud1.transform.rotation;

        Vector3 targetEulerAngles = Cloud1.transform.rotation.eulerAngles;
        targetEulerAngles.z += 179.9f;

        Quaternion targetRotation = Quaternion.Euler(targetEulerAngles);

        while (DataManager.Instance.NowTime < DataManager.Instance.DayTime)
        {
            float flowTime = DataManager.Instance.NowTime / DataManager.Instance.DayTime;

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

            DataManager.Instance.NowTime += Time.deltaTime;

            yield return null;
        }

        targetEulerAngles.z = Round180(targetEulerAngles.z);

        Cloud1.transform.rotation = Quaternion.Euler(targetEulerAngles);
        Cloud2.transform.rotation = Quaternion.Euler(targetEulerAngles);
        Cloud3.transform.rotation = Quaternion.Euler(targetEulerAngles);
        Background.transform.rotation = Quaternion.Euler(targetEulerAngles);

        DataManager.Instance.NowTime = 0.0f;
    }

    private IEnumerator RotationGround()
    {
        float nowTime = 0.0f;
        float dayTime = 5.0f;
        Quaternion currentRotation = Ground.transform.rotation;

        Vector3 targetEulerAngles = Ground.transform.rotation.eulerAngles;
        targetEulerAngles.z += 179.9f;

        Quaternion targetRotation = Quaternion.Euler(targetEulerAngles);

        while (nowTime < dayTime)
        {
            float flowTime = nowTime / dayTime;

            Ground.transform.rotation = Quaternion.Euler(
                Vector3.Lerp(currentRotation.eulerAngles, targetRotation.eulerAngles, flowTime)
            );

            DataManager.Instance.NowTime += Time.deltaTime;

            yield return null;
        }

        targetEulerAngles.z = Round180(targetEulerAngles.z);

        Ground.transform.rotation = Quaternion.Euler(targetEulerAngles);

        DataManager.Instance.NowTime = 0.0f;
    }

    private float Round180(float eulerAngles)
    {
        float newEulerAngles = eulerAngles % 180;

        return (newEulerAngles < 90)
            ? eulerAngles - newEulerAngles
            : eulerAngles - newEulerAngles + 180;
    }
}
