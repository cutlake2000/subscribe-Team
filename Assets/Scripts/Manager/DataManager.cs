using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public PlayerSO player;

    public void Awake()
    {
        instance = this;
        MakeNewPlayer();
    }

    void MakeNewPlayer()
    {
        player = ScriptableObject.CreateInstance<PlayerSO>();
    }
}
