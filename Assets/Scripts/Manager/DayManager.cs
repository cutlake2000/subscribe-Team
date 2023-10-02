using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public static DayManager Instance;
    public float NowTime { get; set; }
    public float DayTime { get; set; }
    public float EntireTime { get; set; }

    private void Awake()
    {
        Instance = this;

        InitTime();
    }

    private void InitTime()
    {
        NowTime = 0.0f;
        DayTime = 10.0f;
        EntireTime = 0.0f;
    }
}
