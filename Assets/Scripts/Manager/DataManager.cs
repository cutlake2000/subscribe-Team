using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public MonsterData[] monsterDatas;

    public float NowTime { get; set; }
    public float DayTime { get; set; }
    public float EntireTime { get; set; }
}
