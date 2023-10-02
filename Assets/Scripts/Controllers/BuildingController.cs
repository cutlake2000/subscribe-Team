using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.GraphicsBuffer;

public enum BuildingType
{
    Inn, Forge, Market
}

public class BuildingController : MonoBehaviour
{
    public static BuildingController Instance;

    [SerializeField] BuildingSO buildingSO;
    [SerializeField] GameObject[] buildingPrefabs;
    public List<BaseBuilding> buildings;

    public ClickBuildingUI clickBuildingUI;
    public ClickBuildingUIModel clickBuildingUIModel;

    public Action DayChange;

    private void Awake()
    {
        Instance = this;
        buildings = new List<BaseBuilding>();
    }

    public void Start()
    {
        // 임시 테스트
        SetNewBuildingOnMap(BuildingType.Inn, new Vector3(-2, 1.13f));
        SetNewBuildingOnMap(BuildingType.Forge, new Vector3(1, 1.13f));
        SetNewBuildingOnMap(BuildingType.Market, new Vector3(4, 1.13f));
        // 임시 테스트
    }

    // 좌표값에 건물 생성
    public void SetNewBuildingOnMap(BuildingType type, Vector3 pos)
    {
        BaseBuilding newBuilding = DeliverNewBuilding(type);
        newBuilding.transform.position = pos;
    }

    // 새로운 빌딩 게임 오브젝트 반환받기
    private BaseBuilding DeliverNewBuilding(BuildingType type)
    {
        BaseBuilding newBuilding;
        for (int i = 0; i < buildings.Count; i++)
        {
            if (buildings[i].buildingType != type)
                continue;
            if (buildings[i].gameObject.activeSelf == true)
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

    // ClickBuildingUI 스위치
    public void ActiveClickBuildingUI(BaseBuilding clickBuilding)
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
        clickBuildingUI.RefreshOptionButton(list, ClickUIType.Default);
    }

    // 빌딩 레벨업 isLoop : 자원 & 레벨 한계까지 레벨업
    public void LevelUpBuilding(bool isLoop = false)
    {
        BaseBuilding target = clickBuildingUIModel.clickBuilding;
        if (target.upgradeWood >= DataManager.Instance.player.Wood && isLoop == false)
        {
            if (!isLoop)
                Debug.Log("TODO : UI 출력 - 재료 부족");

            return;
        }

        if (isLoop == false)
        {
            Debug.Log("TODO : UI 출력 - 1회 성공");
            DataManager.Instance.player.Wood -= target.upgradeWood;
            target.LevelUP();
        }
        else
        {
            int upCount = 0;
            while (clickBuildingUIModel.clickBuilding.upgradeWood < DataManager.Instance.player.Wood)
            {
                upCount++;
                DataManager.Instance.player.Wood -= target.upgradeWood;
                target.LevelUP();
            }
            Debug.Log($"TODO : UI 출력 - {upCount}회 성공");
        }
        clickBuildingUI.Refresh(target);
    }

    // 건물 파괴
    public void DestroyBuilding()
    {
        BaseBuilding target = clickBuildingUIModel.clickBuilding;
        target.gameObject.SetActive(false);
        clickBuildingUI.OFF();
    }

    // 자원 거래
    public void TradeResource(bool isBuy, ResourceType type)
    {
        PlayerSO player = DataManager.Instance.player;
        int trademode = isBuy ? +1 : -1; // -1 골드 차감, +1 골드 추가

        switch (type)
        {
            case ResourceType.Wood:
                if (player.Gold < GameManager.Instance.GoldToWood && isBuy == true)
                {
                    Debug.Log($"TODO : UI 출력 - 골드 부족");
                    return;
                }
                if (player.Wood <= 0 && isBuy == false)
                {
                    Debug.Log($"TODO : UI 출력 - 재화 부족");
                    return;
                }
                player.Wood += +trademode * 1;
                player.Gold += -trademode * GameManager.Instance.GoldToWood;
                Debug.Log($"TODO : UI 출력 -" + "목재" + (player.Wood + "골드" + player.Gold));
                break;
            case ResourceType.Steel:

                break;
            default:
                Debug.Log(type + "거래 타입 오류");
                break;
        }

    }


    // 건물의 데이터 초기화
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

        DataManager.Instance.player.MaxUnitCount = sum;
        // HUD에 갱신
    }

    // 대장간 효과 갱신
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

        DataManager.Instance.player.AddUnitAtk = sum;
        Debug.Log("���尣 ȿ�� : " + DataManager.Instance.player.AddUnitAtk);

        // HUD에 갱신
    }

    // 테스트용
    public void TestDayChangeAction()
    {
        DayChange?.Invoke();
    }

    #region ClickBuildingUIModel과 상호작용 함수

    // 모델로 버튼 번호 전달
    public void ActionUIOptionSelect(int index)
    {
        clickBuildingUIModel.StartBtnAction(index);
    }

    // ClickUI모델 변경
    // type : 해당 값으로 변경, isGoback : 되돌아가기?
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
