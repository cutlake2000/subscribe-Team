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
        isGameOver = false;
        dayNight = DayNight.Night;
        rotationController = map.GetComponent<RotationController>();
    }

    void InitPlayer()
    {
        player = ScriptableObject.CreateInstance<PlayerSO>();
    }

    private void Start()
    {
        rotationController.CallSkyRotationCoroutine();
    }

    private void Update()
    {
        Debug.Log("now Time : " + DataManager.Instance.NowTime);
    }
}
