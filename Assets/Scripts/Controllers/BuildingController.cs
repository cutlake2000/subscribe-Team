using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BuildingType
{
    Inn, Forge
}

public class BuildingController : MonoBehaviour
{
    public static BuildingController instance;

    [SerializeField] BuildingSO buildingSO;
    [SerializeField] GameObject[] BuildingPrefabs;
    public List<BaseBuilding> buildings;

    private void Awake()
    {
        instance = this;
        buildings = new List<BaseBuilding>();

    }

    public void Start()
    {
        // - 테스트용
        DeliverNewBuilding(BuildingType.Inn);
        DeliverNewBuilding(BuildingType.Forge);
        //
    }

    // 빌딩 추가와 해당 위치로 이동
    public void SetNewBuildingOnMap(BuildingType type, Vector3 pos)
    {
        BaseBuilding newBuilding = DeliverNewBuilding(type);
        newBuilding.transform.position = pos;
    }

    // 빌딩 오브젝트만 새로 전달
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

        newBuilding = Instantiate(BuildingPrefabs[(int)type]).GetComponent<BaseBuilding>();
        newBuilding.name = type.ToString();
        newBuilding.baseData = buildingSO.buildingDatas[(int)type];
        newBuilding.Initialization();
        buildings.Add(newBuilding);
        return newBuilding;
    }

    public void UpgradeBuilding(BaseBuilding tagetBuilding)
    {
        if (tagetBuilding.upgradeGold >= DataManager.instance.player.Gold)
        {
            // + 실패 처리
        }

        DataManager.instance.player.Gold -= tagetBuilding.upgradeGold;
        tagetBuilding.LevelUP();
        // + 성공 처리
    }


    // '비활성화'된 빌딩 데이터 초기화
    private void ResetBuildingData(BaseBuilding newBuilding)
    {
        newBuilding.Initialization();
        newBuilding.gameObject.SetActive(true);
    }


    // 빌딩 UI
    public void ActiveBuildingUI()
    {
        // + 날이 바뀌면 자동으로 꺼지게
        // + 빌딩 누르면 UI 나오기
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
        Debug.Log("대장간 효과 : " + DataManager.instance.player.MaxUnitCount);
        // ++ UI 연결
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

        DataManager.instance.player.AddUnitAtk = sum;
        Debug.Log("대장강 효과 : " + DataManager.instance.player.MaxUnitCount);

        // ++ UI 연결
    }


}
