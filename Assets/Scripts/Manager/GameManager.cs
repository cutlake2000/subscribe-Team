using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DayNight
{
    Day,
    Night
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isGameOver;

    public GameObject map;
    public DayNight dayNight;

    private RotationController rotationController;

    private void Awake()
    {
        Instance = this;

        InitValue();
    }

    private void InitValue()
    {
        isGameOver = false;
        dayNight = DayNight.Night;
        rotationController = map.GetComponent<RotationController>();
    }

    private void Start()
    {
        rotationController.CallSkyRotationCoroutine();
    }

    private void Update()
    {
        DataManager.Instance.EntireTime += Time.deltaTime;
    }
}
