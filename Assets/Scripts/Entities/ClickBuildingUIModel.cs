using System;
using System.Collections.Generic;
using UnityEngine;

public enum ClickUIType { Default, Buy, Sell }

public class ClickBuildingUIModel : MonoBehaviour
{

    public ClickUIType UIMode;

    public Stack<ClickUIType> beforeUIType; // 되돌아가기 기능
    public List<ResourceType> SellTypeList;
    public List<ResourceType> BuyTypeList; 
    public Dictionary<ClickBtnType, IClickBuildingAction> allActionList;
    public Dictionary<ClickUIType, List<IClickBuildingAction>> actionList;
    public Dictionary<ClickUIType, List<ClickBtnType>> buttonList;

    public BaseBuilding clickBuilding;

    public void Awake()
    {
        int count = Enum.GetValues(typeof(ClickBtnType)).Length;
        allActionList = new();

        List<IClickBuildingAction> temp = new()
        {
            new BackOption(), new MaxLvUpBulding(), new LvUpBulding(), new DestroyBulding(), new ChangeBuyMode(), new ChangeSellMode(), new TradeResourse()
        };

        for (int i = 0; i < count; i++)
        {
            allActionList.Add((ClickBtnType)i, temp[i]);
        }

        beforeUIType = new();
        actionList = new() { { ClickUIType.Default, new() }, { ClickUIType.Buy, new() }, { ClickUIType.Sell, new() } };
        buttonList = new() { { ClickUIType.Default, new() }, { ClickUIType.Buy, new() }, { ClickUIType.Sell, new() } };
    }

    // 초기화 & ClickBuilding 데이터 불러오기
    public List<ClickBtnType> Initialization(BaseBuilding target)
    {
        UIMode = ClickUIType.Default;
        actionList[ClickUIType.Default].Clear();
        actionList[ClickUIType.Sell].Clear();
        actionList[ClickUIType.Buy].Clear();
        beforeUIType.Clear();

        clickBuilding = target;
        buttonList[ClickUIType.Default] = target.DefaultOptionType;

        // 버튼 불러오기와 돌아가기 버튼 추가
        for (int i = 0; i < target.DefaultOptionType.Count; i++)
        {
            ClickBtnType action = target.DefaultOptionType[i];
            actionList[ClickUIType.Default].Add(allActionList[action]);
        }

        if (target is ITradeResource resourceList)
        {
            SellTypeList = resourceList.GetResourceList("Sell");
            BuyTypeList = resourceList.GetResourceList("Buy");

            for (int i = 0; i < resourceList.GetResourceList("Sell").Count; i++)
                actionList[ClickUIType.Sell].Add(allActionList[ClickBtnType.Trade]);

            for (int i = 0; i < resourceList.GetResourceList("Buy").Count; i++)
                actionList[ClickUIType.Buy].Add(allActionList[ClickBtnType.Trade]);
        }
        actionList[ClickUIType.Default].Add(allActionList[0]);
        actionList[ClickUIType.Buy].Add(allActionList[0]);
        actionList[ClickUIType.Sell].Add(allActionList[0]);

        return target.DefaultOptionType;
    }

    // 버튼 누르면 Mode에 맞게 함수 실행
    public void StartBtnAction(int index)
    {
        switch (UIMode)
        {
            case ClickUIType.Default:
                actionList[ClickUIType.Default][index].Click();
                break;
            case ClickUIType.Buy:
                if (BuyTypeList.Count > index)
                {
                    ((TradeResourse)actionList[ClickUIType.Buy][index]).type = BuyTypeList[index];
                    ((TradeResourse)actionList[ClickUIType.Buy][index]).isbuy = true;
                }
                actionList[ClickUIType.Buy][index].Click();
                break;
            case ClickUIType.Sell:
                if (BuyTypeList.Count > index)
                {
                    ((TradeResourse)actionList[ClickUIType.Sell][index]).type = SellTypeList[index];
                    ((TradeResourse)actionList[ClickUIType.Sell][index]).isbuy = false;
                }
                actionList[ClickUIType.Sell][index].Click();
                break;
            default: Debug.Log("ClickUIType 오류"); break;
        }
    }

    // 모드를 변경하고 clickbuilding의 리스트를 리턴한다.
    // 되돌아가기가 아니면 이전 `beforeUItype`에 기록
    public List<T> ChangeMode<T>(ClickUIType type,bool isGoback = false) where T : Enum
    {
        if (!isGoback)
            beforeUIType.Push(UIMode);

        UIMode = type;
        List<T> TypeList;
        switch (UIMode)
        {
            case ClickUIType.Buy:
                TypeList = BuyTypeList as List<T>;
                break;
            case ClickUIType.Sell:
                TypeList = SellTypeList as List<T>;
                break;
            case ClickUIType.Default:
            default:
                TypeList = buttonList[ClickUIType.Default] as List<T>;
                break;
        }

        return TypeList;
    }
}

// Building의 List<ClickBtnType>과 대칭되게 사용할 인터페이스들
#region
public interface IClickBuildingAction
{
    public void Click();
}

class MaxLvUpBulding : IClickBuildingAction
{
    public void Click()
    {
        BuildingController.instance.LevelUpBuilding(true);
    }
}
class LvUpBulding : IClickBuildingAction
{
    public void Click()
    {
        BuildingController.instance.LevelUpBuilding();
    }
}
class DestroyBulding : IClickBuildingAction
{
    public void Click()
    {
        BuildingController.instance.DestroyBuilding();
    }
}
class BackOption : IClickBuildingAction
{
    public void Click()
    {
        BuildingController.instance.GoBack();
    }
}
class ChangeBuyMode : IClickBuildingAction
{
    public void Click()
    {
        BuildingController.instance.ChangeBuildingUIMode(ClickUIType.Buy, false);
    }
}
class ChangeSellMode : IClickBuildingAction
{
    public void Click()
    {
        BuildingController.instance.ChangeBuildingUIMode(ClickUIType.Sell, false);
    }
}
class TradeResourse : IClickBuildingAction
{
    public ResourceType type;
    public bool isbuy;
    public void Click()
    {
        BuildingController.instance.TradeResource(isbuy, type);
    }
}
#endregion