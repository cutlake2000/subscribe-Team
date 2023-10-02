using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("거래 밸런스")]
    public float buyPenalty = 1.2f; // 구매 패널티
    public float sellPenalty = 0.8f; // 판매 패널티
    private readonly int goldToWood = 10;
    public float currentPrice =1.0f;
    public int GoldToWood 
    {
        get 
        {
            return (int)Mathf.Round(goldToWood * currentPrice); 
        } 
    }
    //public int goldToSteel = 15;

    private void Awake()
    {
        Instance = this;
    }
}
