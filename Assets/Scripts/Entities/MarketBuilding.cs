using UnityEngine;
using UnityEngine.AI;

public class MarketBuilding : BaseBuilding
{
    float price = 1.0f; // 기준 시세

    float currentPrice; // 현재 시세
    float minPrice = 0.2f; // 최소 시세
    float maxPrice = 3.0f; // 최대 시세

    float buyPenalty = 1.1f; // 구매 패널티
    float sellPenalty = 0.9f; // 판매 패널티

    int goldToWood = 10;
    // 골드 -> 목재 교환비


    public float CurrentPrice { get { return currentPrice; } }

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
    }

    public void BuyResource()
    {

    }

}
