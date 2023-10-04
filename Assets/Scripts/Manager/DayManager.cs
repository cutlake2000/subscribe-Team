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
    public DayNight dayNight;
    public DayNight DayNight
    {
        set
        {
            dayNight = value;
            UIManger.Instance.DayUI(dayNight);
            if (dayNight == DayNight.Night)
            {
                UIManger.Instance.ShowLogUI("장림깊은 골로 대한 짐승이 내려온다~");
            }
            else
            {
                UIManger.Instance.ShowLogUI("새로운 아침이 왔다네~ 덩기덕쿵더러러");
            }

        }
    }

    public bool isSkyRotating;
    public bool isGroundRotating;
    public bool isDayNightChanged;
    public bool isMercenaryLocationMoved;

    public GameObject map;

    private RotationController rotationController;

    private void Awake()
    {
        Instance = this;

        Init();
    }

    private void Init()
    {
        isDayNightChanged = false;
        NowTime = 0.0f;
        DayTime = 20.0f;
        EntireTime = 0.0f;
        DayCount = 0;
        dayNight = DayNight.Day;
        isSkyRotating = false;
        isGroundRotating = false;
        isMercenaryLocationMoved = false;

        rotationController = map.GetComponent<RotationController>();
    }

    private void Start()
    {
        rotationController.CallSkyRotationCoroutine();
    }

    private void Update()
    {
        if (GameManager.Instance.isGameOver == false)
        {
            CallRotation();
            CalculateDay();
        }
        else
        {
            rotationController.StopAllCoroutines();
        }
    }

    private void CalculateDay()
    {
        DayCount = (int)(EntireTime / (DayTime * 2)) + 1;
    }

    private void CallRotation()
    {
        if (NowTime == 0.0f && isGroundRotating == false)
        {
            isDayNightChanged = false;
            rotationController.CallSkyRotationCoroutine();
        }
        else if (NowTime >= DayTime)
        {
            rotationController.CallGroundRotationCoroutine();

            if (isDayNightChanged == false)
            {
                isDayNightChanged = true;
                DayNight = dayNight == DayNight.Day ? DayNight.Night : DayNight.Day;
            }
        }

        if (isSkyRotating == true)
        {
            EntireTime += Time.deltaTime;
        }
        else
        {
            NowTime = 0.0f;
        }
    }
}
