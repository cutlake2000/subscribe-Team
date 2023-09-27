using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.Diagnostics;

public class MarketBuilding : BaseBuilding
{
    float price = 1.0f; // 기준 시세

    float currentPrice; // 현재 시세
    float minPrice = 0.2f; // 최소 시세
    float maxPrice = 3.0f; // 최대 시세
    // 1.0을 기준으로 레일리 분포?? 분포도는 잘 몰라요

    float buyItem = 1.1f;
    float sellItem = 0.9f;
    // 살때 팔때 = 110% 90%?

    int goldToWood = 10; 
    // 골드 -> 목재 교환비


    public float CurrentPrice { get { return currentPrice; } }

    public override void Initialization()
    {
        base.Initialization();
        price = 1.0f;
    }

    public void CheakTodayPrice()
    {
    }

    public void BuyResource()
    {

    }


}
