using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isGameOver;
    public MercenaryUI MercenaryUI;
    public ClickMercenaryUI clickMercenaryUI;
    public MercenaryController mercenaryController;

    [Header("거래 밸런스")]
    private readonly int goldToWood = 10;
    public float currentPrice = 1.0f;
    public int GoldToWood
    {
        get { return (int)Mathf.Round(goldToWood * currentPrice); }
    }

    //public int goldToSteel = 15;

    private void Awake()
    {
        Instance = this;

        InitValue();
    }

    private void InitValue()
    {
        isGameOver = false;
    }
}
