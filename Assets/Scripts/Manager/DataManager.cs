using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public PlayerSO player;
    public MonsterData[] monsterDatas;

    public float currentTime;
    public float nowTime;

    public void Awake()
    {
        Instance = this;

        InitTime();
        MakeNewPlayer();
    }

    private void InitTime()
    {
        nowTime = 0.0f;
        currentTime = 0.0f;
    }

    private void Update() { }

    void MakeNewPlayer()
    {
        player = ScriptableObject.CreateInstance<PlayerSO>();
    }
}
