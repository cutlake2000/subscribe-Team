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

    public PlayerSO player;

    public bool isGameOver;

    public GameObject map;
    public DayNight dayNight;

    private RotationController rotationController;

    private void Awake()
    {
        Instance = this;

        InitValue();
        InitPlayer();
    }

    private void InitValue()
    {
        DataManager.Instance.NowTime = 0.0f;
        DataManager.Instance.DayTime = 10.0f;
        DataManager.Instance.EntireTime = 0.0f;

        isGameOver = false;
        dayNight = DayNight.Night;
        rotationController = map.GetComponent<RotationController>();
    }

    void InitPlayer()
    {
        player = ScriptableObject.CreateInstance<PlayerSO>();
    }
}
