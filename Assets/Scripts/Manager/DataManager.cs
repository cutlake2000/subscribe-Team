using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ResourceType
{
    Gold, Wood, Steel
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public PlayerSO player;
    public MonsterData[] monsterDatas;



    public void Awake()
    {
        instance = this;
        MakeNewPlayer();
    }

    private void Update() { }

    void MakeNewPlayer()
    {
        player = ScriptableObject.CreateInstance<PlayerSO>();
    }
}
