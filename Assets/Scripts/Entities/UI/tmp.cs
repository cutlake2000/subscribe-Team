// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class RotationController : MonoBehaviour
// {
//     public GameObject Ground;

//     public GameObject Cloud1;
//     public GameObject Cloud2;
//     public GameObject Cloud3;
//     public GameObject Background;

//     public void CallRotationCoroutine()
//     {
//         GameManager.Instance.dayNight =
//             GameManager.Instance.dayNight == DayNight.Day ? DayNight.Night : DayNight.Day;

//         StartCoroutine(RotationSky());
//     }

//     private IEnumerator RotationSky()
//     {
//         Quaternion currentCloud1Rotation = Cloud1.transform.rotation;

//         Vector3 targetEulerAngles = transform.rotation.eulerAngles;
//         targetEulerAngles.z += 179.9f;

//         Quaternion targetRotation = Quaternion.Euler(targetEulerAngles);

//         while (DataManager.Instance.nowTime < DataManager.Instance.entireTime)
//         {
//             float flowTime = DataManager.Instance.nowTime / DataManager.Instance.entireTime;

//             Cloud1.transform.rotation = Quaternion.Euler(
//                 Vector3.Lerp(
//                     currentCloud1Rotation.eulerAngles,
//                     targetRotation.eulerAngles,
//                     flowTime
//                 )
//             );

//             Cloud2.transform.rotation = Quaternion.Euler(
//                 Vector3.Lerp(
//                     currentCloud1Rotation.eulerAngles,
//                     targetRotation.eulerAngles,
//                     flowTime
//                 )
//             );
//             Cloud3.transform.rotation = Quaternion.Euler(
//                 Vector3.Lerp(
//                     currentCloud1Rotation.eulerAngles,
//                     targetRotation.eulerAngles,
//                     flowTime
//                 )
//             );
//             Background.transform.rotation = Quaternion.Euler(
//                 Vector3.Lerp(
//                     currentCloud1Rotation.eulerAngles,
//                     targetRotation.eulerAngles,
//                     flowTime
//                 )
//             );

//             DataManager.Instance.nowTime += Time.deltaTime;

//             yield return null;
//         }

//         targetEulerAngles.z = Round180(targetEulerAngles.z);

//         Cloud1.transform.rotation = Quaternion.Euler(targetEulerAngles);
//         Cloud2.transform.rotation = Quaternion.Euler(targetEulerAngles);
//         Cloud3.transform.rotation = Quaternion.Euler(targetEulerAngles);
//         Background.transform.rotation = Quaternion.Euler(targetEulerAngles);

//         Debug.Log("Cloud1.transform.rotation : " + Cloud1.transform.rotation);

//         DataManager.Instance.nowTime = 0.0f;
//     }

//     private float Round180(float eulerAngles)
//     {
//         float newEulerAngles = eulerAngles % 180;

//         return (newEulerAngles < 90)
//             ? eulerAngles - newEulerAngles
//             : eulerAngles - newEulerAngles + 180;
//     }
// }
