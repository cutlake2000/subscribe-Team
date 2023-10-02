using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MarketBuilding : BaseBuilding, ITradeResource
{
    float price; // 기준 시세

    float currentPrice; // 현재 시세
    float minPrice = 0.2f; // 최소 시세
    float maxPrice = 3.0f; // 최대 시세

    public float CurrentPrice { get { return currentPrice; } }
    public List<ResourceType> SellResourceOption;
    public List<ResourceType> BuyResourceOption;

    private void Awake()
    {
        SellResourceOption = new() { ResourceType.Wood };
        BuyResourceOption = new() { ResourceType.Wood };
    }

    public override void Initialization()
    {
        base.Initialization();
        price = 1.0f;
        CheckTodayPrice();
        BuildingController.instance.DayChange -= CheckTodayPrice;
        BuildingController.instance.DayChange += CheckTodayPrice;

    }

    public void CheckTodayPrice()
    {
        // TODO 연결 시키기
        float per = Random.Range(0f, 1f);
        float Gaussian = Random.Range(per, 1f);
        float plusminus = Random.Range(0, 2);

        if (plusminus == 0)
            currentPrice = (price - minPrice) * Gaussian + minPrice;
        else
        {
            Gaussian = 1 - Gaussian;
            currentPrice = (maxPrice - price) * Gaussian + price;
        }

        GameManager.Instance.currentPrice = currentPrice;
    }

    public List<ResourceType> GetResourceList(string name)
    {
        switch (name)
        {
            case "Sell":
                return SellResourceOption;
            case "Buy":
                return BuyResourceOption;
            default:
                Debug.Log("입력 오류");
                return SellResourceOption;
        }
    }
}

interface ITradeResource
{
    public List<ResourceType> GetResourceList(string name);
}