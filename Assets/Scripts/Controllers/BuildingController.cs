using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.GraphicsBuffer;

public enum BuildingType
{
    Inn,
    Forge,
    Market
}

public class BuildingController : MonoBehaviour
{
    public static BuildingController Instance;

    [SerializeField]
    private GameObject InstantiateLocation;

    [SerializeField]
    private GameObject[] buildingPrefabs;

    public BuildingSO buildingSO;
    private List<BaseBuilding> buildingList;
    private Dictionary<BuildingType, List<BaseBuilding>> buildingTypeList;

    public ClickBuildingUI clickBuildingUI;
    public ClickBuildingUIModel clickBuildingUIModel;
    public BuildingCreator buildingCreator;

    public Action DayChange;

    private void Awake()
    {
        Instance = this;
        buildingList = new List<BaseBuilding>();
        buildingTypeList = new()
        {
            { BuildingType.Inn, new() },
            { BuildingType.Forge, new() },
            { BuildingType.Market, new() }
        };
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
        List<BaseBuilding> list = buildingTypeList[type];
        BaseBuilding newBuilding;

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].gameObject.activeSelf == true)
                continue;

            newBuilding = list[i];
            ResetBuildingData(newBuilding);
            CheckBuildingCount(type);
            RefreshEffect(type);
            return newBuilding;
        }

        newBuilding = Instantiate(buildingPrefabs[(int)type]).GetComponent<BaseBuilding>();
        newBuilding.transform.parent = InstantiateLocation.transform;
        newBuilding.name = type.ToString();
        newBuilding.baseData = buildingSO.buildingDatas[(int)type];
        newBuilding.Initialization();
        buildingList.Add(newBuilding);
        list.Add(newBuilding);
        CheckBuildingCount(type);
        RefreshEffect(type);
        return newBuilding;
    }

    // ClickBuildingUI 스위치
    public void ActiveClickBuildingUI(BaseBuilding clickBuilding)
    {
        if (buildingCreator._isEditMode == true)
            return;

        if (
            clickBuildingUI.gameObject.activeSelf == true
            && clickBuilding != null
            && clickBuildingUIModel.clickBuilding == clickBuilding
        )
        {
            clickBuildingUI.Off();
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
        if (target.level >= target.maxLevel)
        {
            Debug.Log("TODO : UI 출력 - 최대 레벨");
            return;
        }
        else if (target.upgradeWood > DataManager.Instance.player.Wood)
        {
            Debug.Log("TODO : UI 출력 - 재료 부족");
            return;
        }

        DataManager.Instance.player.Wood -= target.upgradeWood;
        target.LevelUP();
        int upCount = 1;

        if (isLoop)
        {
            while (
                target.upgradeWood <= DataManager.Instance.player.Wood
                && target.level < target.maxLevel
            )
            {
                DataManager.Instance.player.Wood -= target.upgradeWood;
                target.LevelUP();
                upCount++;
            }
        }

        if (isLoop)
            Debug.Log($"TODO : UI 출력 - 승급 {upCount}번 완료");
        else
            Debug.Log($"TODO : UI 출력 - 승급 완료");

        RefreshEffect(target.buildingType);
        clickBuildingUI.Refresh(target);
    }

    // 건물 파괴
    public void DestroyBuilding()
    {
        BaseBuilding target = clickBuildingUIModel.clickBuilding;
        target.gameObject.SetActive(false);
        CheckBuildingCount(target.buildingType);
        Vector2 allowPos = new Vector2(target.transform.position.x, target.transform.position.z);
        buildingCreator.AllowTileData(allowPos);
        Debug.Log(target.transform.position);
        clickBuildingUI.Off();
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

    // 활성화 건물 체크
    private void CheckBuildingCount(BuildingType type)
    {
        List<BaseBuilding> list = buildingTypeList[type];
        int count = 0;
        for (int i = 0; i < list.Count; i++)
        {
            if (!list[i].gameObject.activeSelf)
                continue;

            count++;
        }

        DataManager.Instance.player.SetCurrentBuildingCount(type, count);
    }

    public void RefreshEffect(BuildingType type)
    {
        switch (type)
        {
            case BuildingType.Inn:
                RefreshInnEffect();
                break;
            case BuildingType.Forge:
                RefreshForgeEffect();
                break;
        }
    }

    // 여관 효과 갱신
    private void RefreshInnEffect()
    {
        int sum = 0;
        for (int i = 0; i < buildingList.Count; i++)
        {
            if (buildingList[i].buildingType != BuildingType.Inn)
                continue;
            if (buildingList[i].gameObject.activeSelf == false)
                continue;

            InnBuilding building = (InnBuilding)buildingList[i];
            sum += building.MaxUnitValue;
        }

        DataManager.Instance.player.MaxUnitCount = sum;
        // HUD에 갱신

        Debug.Log(DataManager.Instance.player.MaxUnitCount);
    }

    // 대장간 효과 갱신
    private void RefreshForgeEffect()
    {
        int sum = 0;
        for (int i = 0; i < buildingList.Count; i++)
        {
            if (buildingList[i].buildingType != BuildingType.Forge)
                continue;
            if (buildingList[i].gameObject.activeSelf == false)
                continue;

            ForgeBuilding building = (ForgeBuilding)buildingList[i];
            sum += building.AddUnitAtk;
        }

        DataManager.Instance.player.AddUnitAtk = sum;
        Debug.Log("TODO : HUD 갱신 추가 공격력 : " + DataManager.Instance.player.AddUnitAtk);
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
            case ClickUIType.Buy:
                clickBuildingUI.RefreshOptionButton(
                    clickBuildingUIModel.ChangeMode<ResourceType>(type, isGoback),
                    type
                );
                break;
            case ClickUIType.Sell:
                clickBuildingUI.RefreshOptionButton(
                    clickBuildingUIModel.ChangeMode<ResourceType>(type, isGoback),
                    type
                );
                break;
            case ClickUIType.Default:
            default:
                clickBuildingUI.RefreshOptionButton(
                    clickBuildingUIModel.ChangeMode<ClickBtnType>(type, isGoback),
                    type
                );
                break;
        }
    }

    // 뒤로 가기
    public void GoBack()
    {
        if (clickBuildingUIModel.beforeUIType.Count == 0)
        {
            clickBuildingUI.Off();
            return;
        }
        ClickUIType type = clickBuildingUIModel.beforeUIType.Pop();
        ChangeBuildingUIMode(type, true);
    }

    #endregion
}
