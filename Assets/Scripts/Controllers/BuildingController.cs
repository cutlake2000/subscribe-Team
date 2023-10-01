using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using static UnityEngine.GraphicsBuffer;

public enum BuildingType
{
    Inn, Forge, Market
}

public class BuildingController : MonoBehaviour
{
    public static BuildingController instance;

    [SerializeField] BuildingSO buildingSO;

    [SerializeField] GameObject[] buildingPrefabs;
    public List<BaseBuilding> buildings;

    public BaseBuilding clickBuildingtemp;
    public ClickBuildingUI clickBuildingUI;
    public ClickBuildingUIModel clickBuildingUIModel;

    public Action DayChange;

    private void Awake()
    {
        instance = this;
        buildings = new List<BaseBuilding>();
    }

    public void Start()
    {
        // - �׽�Ʈ��
        SetNewBuildingOnMap(BuildingType.Inn, Vector2.left * 2);
        SetNewBuildingOnMap(BuildingType.Forge, Vector2.right * 1);
        SetNewBuildingOnMap(BuildingType.Market, Vector2.right * 4);
        //
    }

    // ���� �߰��� �ش� ��ġ�� �̵�
    public void SetNewBuildingOnMap(BuildingType type, Vector3 pos)
    {
        BaseBuilding newBuilding = DeliverNewBuilding(type);
        newBuilding.transform.position = pos;
    }

    // ������ ���� �߰�
    private BaseBuilding DeliverNewBuilding(BuildingType type)
    {
        BaseBuilding newBuilding;
        for (int i = 0; i < buildings.Count; i++)
        {
            if (buildings[i].buildingType != type)
                continue;
            if (buildings[i].gameObject.activeSelf == false)
                continue;

            newBuilding = buildings[i];
            ResetBuildingData(newBuilding);
            return newBuilding;
        }

        newBuilding = Instantiate(buildingPrefabs[(int)type]).GetComponent<BaseBuilding>();
        newBuilding.name = type.ToString();
        newBuilding.baseData = buildingSO.buildingDatas[(int)type];
        newBuilding.Initialization();
        buildings.Add(newBuilding);
        return newBuilding;
    }

    // Ŭ������ UI �¿���
    public void ActiveClickBuildingUI(BaseBuilding clickBuilding)
    {
        if (
            clickBuildingUI.gameObject.activeSelf == true // �ӽ�
            && clickBuilding != null
            && this.clickBuildingtemp == clickBuilding
        )
        {
            clickBuildingUI.OFF();
            return;
        }
        this.clickBuildingtemp = clickBuilding;
        clickBuildingUI.On(clickBuilding);
        clickBuildingUI.Refresh(clickBuilding);
    }

    // ������ ����
    public void LevelUpBuilding(bool isLoop = false)
    {
        BaseBuilding target = clickBuildingUIModel.clickBuilding;
        if (target.upgradeWood >= DataManager.instance.player.Wood && isLoop == false)
        {
            if (!isLoop)
                Debug.Log("��� ����");

            return;
        }

        if (isLoop == false)
        {
            Debug.Log("������ ����");
            DataManager.instance.player.Wood -= target.upgradeWood;
            target.LevelUP();
        }
        else
        {
            int upCount = 0;
            while (target.upgradeWood < DataManager.instance.player.Wood)
            {
                upCount++;
                DataManager.instance.player.Wood -= target.upgradeWood;
                target.LevelUP();
            }
            Debug.Log($"{upCount}��ŭ ������ ����");
        }
        clickBuildingUI.Refresh(target);
    }

    // Ŭ�� ���� �ı�
    public void DestroyBuilding()
    {
        BaseBuilding target = clickBuildingUIModel.clickBuilding;
        target.gameObject.SetActive(false);
        clickBuildingUI.OFF();
    }

    // 자원 거래
    public void TradeResource(bool isBuy, ResourceType type)
    {
        PlayerSO player = DataManager.instance.player;
        int trademode = isBuy ? +1 : -1; // -1 골드 차감, +1 골드 추가

        switch (type)
        {
            case ResourceType.Wood:
                if (player.Gold < GameManager.Instance.goldToWood && isBuy == true)
                {
                    Debug.Log("골드 부족");//TODO 골드 부족 처리
                    return;
                }
                if (player.Wood <= 0 && isBuy == false)
                {
                    Debug.Log("재료 부족"); //TODO 재료 부족 처리
                    return;
                }
                player.Wood += +trademode * 1;
                player.Gold += -trademode * 10;
                Debug.Log("목재" + (player.Wood + "골드" + player.Gold));
                break;
            case ResourceType.Steel:

                break;
            default:
                break;
        }

        Debug.Log(type);
    }


    // '��Ȱ��ȭ'�� ���� ������ �ʱ�ȭ
    private void ResetBuildingData(BaseBuilding newBuilding)
    {
        newBuilding.Initialization();
        newBuilding.ActiveAnimation(true);
        newBuilding.gameObject.SetActive(true);
    }

    // 여관 효과 갱신
    public void RefreshInnEffect()
    {
        int sum = 0;
        for (int i = 0; i < buildings.Count; i++)
        {
            if (buildings[i].buildingType != BuildingType.Inn)
                continue;
            if (buildings[i].gameObject.activeSelf == false)
                continue;

            InnBuilding building = (InnBuilding)buildings[i];
            sum += building.MaxUnitValue;
        }

        DataManager.instance.player.MaxUnitCount = sum;
        Debug.Log("���� ȿ�� : " + DataManager.instance.player.MaxUnitCount);
        // ++ UI ����
    }

    // ���尣 ȿ�� ����
    public void RefreshForgeEffect()
    {
        int sum = 0;
        for (int i = 0; i < buildings.Count; i++)
        {
            if (buildings[i].buildingType != BuildingType.Forge)
                continue;
            if (buildings[i].gameObject.activeSelf == false)
                continue;

            ForgeBuilding building = (ForgeBuilding)buildings[i];
            sum += building.AddUnitAtk;
        }

        DataManager.instance.player.AddUnitAtk = sum;
        Debug.Log("���尣 ȿ�� : " + DataManager.instance.player.AddUnitAtk);

        // ++ UI ����
    }

    // 테스트용
    public void TestDayChangeAction()
    {
        DayChange?.Invoke();
    }

    #region 클릭 UI 리팩토링
    // 클릭시 ClickUI 활성,비활성
    public void ActiveClickBuildingUI2(BaseBuilding clickBuilding)
    {
        if (clickBuildingUI.gameObject.activeSelf == true && clickBuilding != null
            && clickBuildingUIModel.clickBuilding == clickBuilding)
        {
            clickBuildingUI.OFF();
            return;
        }
        clickBuildingUI.On(clickBuilding);
        clickBuildingUI.Refresh(clickBuilding);
        List<ClickBtnType> list = clickBuildingUIModel.Initialization(clickBuilding);
        clickBuildingUI.RefreshOptionButton(list,ClickUIType.Default);
    }

    // 모델로 전달
    public void ActionUIOptionSelect(int index)
    {
        clickBuildingUIModel.StartBtnAction(index);
    }

    // ClickUI모델 변경
    // type : 해당 값으로 변경, isGoback : 되돌아가기인지?
    // ChangeMode에서 모드 변경 후 리턴 값으로 버튼을 새로고침
    public void ChangeBuildingUIMode(ClickUIType type, bool isGoback)
    {
        switch (type)
        {
            case ClickUIType.Default:
                clickBuildingUI.RefreshOptionButton(clickBuildingUIModel.ChangeMode<ClickBtnType>(type, isGoback), type);
                break;
            case ClickUIType.Buy:
                clickBuildingUI.RefreshOptionButton(clickBuildingUIModel.ChangeMode<ResourceType>(type, isGoback), type);
                break;
            case ClickUIType.Sell:
                clickBuildingUI.RefreshOptionButton(clickBuildingUIModel.ChangeMode<ResourceType>(type, isGoback), type);
                break;
            default:
                break;
        }

    }

    // 뒤로 가기
    public void GoBack()
    {
        if (clickBuildingUIModel.beforeUIType.Count == 0)
        {
            clickBuildingUI.OFF();
            return;
        }
        ClickUIType type = clickBuildingUIModel.beforeUIType.Pop();
        ChangeBuildingUIMode(type, true);
    }

    #endregion
}
