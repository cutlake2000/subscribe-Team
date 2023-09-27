using UnityEngine;
using UnityEngine.AI;

public class MarketBuilding : BaseBuilding
{
    public GameObject temp;
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


    private void Update()
    {
        float A = Random.Range(0f, 0.5f);
        float B = Random.Range(A, 0.5f);
        float C = Random.Range(0, 2);

        if (C == 1)
        {
            B = 1-B;
        }

        Vector2 AB = new Vector2(B, 10.5f);

        GameObject dot = Instantiate(temp);
        dot.transform.position = AB;
    }

    public void CheakTodayPrice()
    {

    }

    public void BuyResource()
    {

    }

}
