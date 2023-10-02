using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DayNight
{
    Day,
    Night
}

public class DayManager : MonoBehaviour
{
    public static DayManager Instance;
    public float NowTime { get; set; }
    public float DayTime { get; set; }
    public float EntireTime { get; set; }
    public int DayCount { get; set; }
    public DayNight dayNight { get; set; }

    public GameObject map;

    private RotationController rotationController;

    private void Awake()
    {
        Instance = this;

        Init();
    }

    private void Init()
    {
        NowTime = 0.0f;
        DayTime = 10.0f;
        EntireTime = 0.0f;
        DayCount = 1;
        dayNight = DayNight.Day;

        rotationController = map.GetComponent<RotationController>();
    }

    private void Start()
    {
        rotationController.CallSkyRotationCoroutine();
    }

    private void Update()
    {
        CalculateTime();
    }

    private void CalculateTime()
    {
        if (NowTime >= DayTime)
        {
            NowTime = 0.0f;

            rotationController.CallGroundRotationCoroutine();

            if (dayNight == DayNight.Night)
            {
                DayCount++;
            }

            dayNight = dayNight == DayNight.Day ? DayNight.Night : DayNight.Day;
        }
        else if (rotationController.isGroundRotating == false)
        {
            NowTime += Time.deltaTime;
        }

        EntireTime += Time.deltaTime;
    }
}
