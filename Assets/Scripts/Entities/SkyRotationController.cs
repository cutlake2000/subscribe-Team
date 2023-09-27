using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DayNight
{
    Day,
    Night
}

public abstract class SkyRotationController : MonoBehaviour
{
    public DayNight day;

    public float currentTime = 0.0f;

    protected float entireTime = 5.0f;
    protected float initTime = 0.0f;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(StartRotation());
        }
    }

    private void Init()
    {
        day = DayNight.Day;
        currentTime = initTime;
    }

    protected abstract IEnumerator StartRotation();

    protected float Round180(float eulerAngles)
    {
        float newEulerAngles = eulerAngles % 180;

        return (newEulerAngles < 90)
            ? eulerAngles - newEulerAngles
            : eulerAngles - newEulerAngles + 180;
    }
}
