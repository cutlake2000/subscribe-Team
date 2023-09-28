using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ClickBtnOptionType { Back, MaxLvUp, LvUp, Destroy, Buy, Sell, Wood }

public class ClickBuildingUI : MonoBehaviour
{
    enum InfoTextType { Name, CurrentEffect, Upgrade }

    [SerializeField] Image[] optionButtonList;
    [SerializeField] TMP_Text[] texts;

    [SerializeField] List<Sprite> optionBtnSpriteList;
    private List<IClickBuildingOption> optionActionList;
    private List<IClickBuildingOption> currentOptionActions;

    private StringBuilder newStatusText;
    WaitForSecondsRealtime appearBtnTime;
    WaitForSecondsRealtime animationTime;

    public void Awake()
    {
        optionActionList = new()
        {  new BackOption(),new MaxLvUpBulding(), new LvUpBulding(), new DestroyBulding(), new BuyItem(), new SellItem()};

        currentOptionActions = new();
        newStatusText = new();
        appearBtnTime = new WaitForSecondsRealtime(0.05f);
        animationTime = new WaitForSecondsRealtime(0.2f);
    }

    public void On(BaseBuilding building)
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        Vector3 screenPos = Camera.main.WorldToScreenPoint(building.transform.position);
        transform.position = screenPos;
        RefreshOptionButton(building.OptionType);
        SortOptionButton();
    }

    public void OFF()
    {
        DeactivateRaycastTargrt();
        gameObject.SetActive(false);
    }

    public void ClickOptionBtn(int index)
    {
        currentOptionActions[index].Click();
    }

    // 옵션 리스트 불러오기
    public void RefreshOptionButton(List<ClickBtnOptionType> typeList)
    {
        if (typeList.Count >= 5)
            Debug.Log("옵션 왤케 마늠?");

        currentOptionActions.Clear();
        for (int i = 0; i < optionButtonList.Length; i++)
        {
            if (i >= typeList.Count)
            {
                optionButtonList[i].gameObject.SetActive(false);
                continue;
            }

            if (optionButtonList[i].gameObject.activeSelf == false)
                optionButtonList[i].gameObject.SetActive(true);

            int type = (int)(typeList[i]);
            currentOptionActions.Add(optionActionList[type]);
            optionButtonList[i].sprite = optionBtnSpriteList[type];
        }
    }

    // 옵션 버튼 정렬
    public void SortOptionButton()
    {
        float angle = 25;
        float startAngle = 0 + (angle * (currentOptionActions.Count - 1) * 0.5f);
        for (int i = 0; i < optionActionList.Count; i++)
        {
            optionButtonList[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, startAngle - angle * i));
            optionButtonList[i].rectTransform.localPosition = optionButtonList[i].rectTransform.up * 150;
        }
    }

    // 인포UI 정보 갱신
    public void Refresh(BaseBuilding target)
    {

        newStatusText.Clear();
        newStatusText.Append($"Lv. {target.level} {target.buildingName}");
        texts[(int)InfoTextType.Name].text = newStatusText.ToString();

        newStatusText.Clear();
        switch (target.buildingType)
        {
            case BuildingType.Inn:
                newStatusText.Append($"인구 수 증가 : {((InnBuilding)target).MaxUnitValue}");
                break;
            case BuildingType.Forge:
                newStatusText.Append($"공격력 증가 : {((ForgeBuilding)target).AddUnitAtk}");
                break;
            case BuildingType.Market:
                MarketBuilding market = (MarketBuilding)target;
                int price = (int)(market.CurrentPrice * 100);
                newStatusText.Append($"현재 시세 :{price}%");
                break;
            default:
                break;
        }
        texts[(int)InfoTextType.CurrentEffect].text = newStatusText.ToString();

        newStatusText.Clear();
        if (target.level < target.maxLevel)
            newStatusText.Append($"<sprite=0>{target.upgradeWood}");
        else
            newStatusText.Append($"최고 레벨");
        texts[(int)InfoTextType.Upgrade].text = newStatusText.ToString();
    }

    // 아이콘 레이캐스트 스위치
    public void DeactivateRaycastTargrt()
    {
        foreach (var item in optionButtonList)
        {
            item.raycastTarget = false;
        }
    }

    public void ActivateRaycastTargrt()
    {
        foreach (var item in optionButtonList)
        {
            item.raycastTarget = true;
        }
    }

    //// 순서대로 등장 애니메이션
    //IEnumerator AppearAniOptionBtn()
    //{
    //    for (int i = 0; i < currentOptionActions.Count; i++)
    //    {
    //        optionButtonList[i].rectTransform.localPosition = Vector3.zero;
    //        Vector3 moveDir = optionButtonList[i].rectTransform.up;

    //        StartCoroutine(MoveAniOptionBtn(optionButtonList[i].rectTransform, moveDir));
    //        yield return appearBtnTime;
    //    }
    //}
    //// 옵션 이동 애니메이션
    //IEnumerator MoveAniOptionBtn(RectTransform target, Vector3 moveDir)
    //{
    //    Vector3 targetPos = moveDir * 150;
    //    while (targetPos.magnitude > target.localPosition.magnitude)
    //    {
    //        target.localPosition += 50f * moveDir;
    //        yield return animationTime;
    //    }

    //}
}

interface IClickBuildingOption
{
    public void Click();
}

class MaxLvUpBulding : IClickBuildingOption
{
    public void Click()
    {
        BuildingController.instance.LevelUpBuilding(true);
    }
}

class LvUpBulding : IClickBuildingOption
{
    public void Click()
    {
        BuildingController.instance.LevelUpBuilding();
    }
}

class DestroyBulding : IClickBuildingOption
{
    public void Click()
    {
        BuildingController.instance.DestroyBuilding();
    }
}

class BackOption : IClickBuildingOption
{
    public void Click()
    {
        BuildingController.instance.clickBuildingUI.OFF();
    }
}

class BuyItem : IClickBuildingOption
{
    public void Click()
    {
        BuildingController.instance.BuyResource();
    }
}

class SellItem : IClickBuildingOption
{
    void IClickBuildingOption.Click()
    {
        BuildingController.instance.SellResource();
    }
}
