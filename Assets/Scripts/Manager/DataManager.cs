using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ResourceType
{
    Gold, Wood, Steel
}

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public PlayerSO player;
    public MonsterData[] monsterDatas;

    private void Awake()
    {
        Instance = this;

        InitPlayer();
    }

    private void InitPlayer()
    {
        player = ScriptableObject.CreateInstance<PlayerSO>();
    }
}
