using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum BuildingType
{
    Inn, Forge
}

public class BuildingController : MonoBehaviour
{
    public static BuildingController instance;

    [SerializeField] BuildingSO buildingSO;
    [SerializeField] GameObject[] buildingPrefabs;
    public List<BaseBuilding> buildings;

    public BaseBuilding clickBuilding;
    public ClickBuildingUI clickBuildingUI;

    private void Awake()
    {
        instance = this;
        buildings = new List<BaseBuilding>();

    }

    public void Start()
    {
        // - 테스트용
        SetNewBuildingOnMap(BuildingType.Inn, Vector2.left);
        SetNewBuildingOnMap(BuildingType.Forge, Vector2.right);
        //
    }

    // 빌딩 추가와 해당 위치로 이동
    public void SetNewBuildingOnMap(BuildingType type, Vector3 pos)
    {
        BaseBuilding newBuilding = DeliverNewBuilding(type);
        newBuilding.transform.position = pos;
    }

    // 빌딩만 새로 추가
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

    // 클릭빌딩 UI 온오프
    public void ActiveClickBuildingUI(BaseBuilding clickBuilding)
    {
        if (clickBuildingUI.gameObject.activeSelf == true) // 임시
        {
            clickBuildingUI.OFF();
            return;
        }
        this.clickBuilding = clickBuilding;
        clickBuildingUI.On(clickBuilding);
    }

    // 레벨업 빌딩
    // + 루프 만들 것
    public void LevelUpBuilding(bool isLoop)
    {
        if (clickBuilding.upgradeWood >= DataManager.instance.player.Wood && isLoop == false)
        {
            if (!isLoop)
                Debug.Log("골드 부족");

            return;
        }

        if (isLoop == false)
        {
            Debug.Log("레벨업 성공");
            DataManager.instance.player.Wood -= clickBuilding.upgradeWood;
            clickBuilding.LevelUP();
        }
        else
        {
            int upCount = 0;
            while (clickBuilding.upgradeWood < DataManager.instance.player.Wood)
            {
                upCount++;
                DataManager.instance.player.Wood -= clickBuilding.upgradeWood;
                clickBuilding.LevelUP();
            }
            Debug.Log($"{upCount}만큼 레벨업 성공");
        }

    }


    // 클릭 빌딩 파괴
    public void DestroyBuilding()
    {
        clickBuilding.gameObject.SetActive(false);
    }


    // '비활성화'된 빌딩 데이터 초기화
    private void ResetBuildingData(BaseBuilding newBuilding)
    {
        newBuilding.Initialization();
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
        Debug.Log("여관 효과 : " + DataManager.instance.player.MaxUnitCount);
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
        Debug.Log("대장간 효과 : " + DataManager.instance.player.AddUnitAtk);

        // ++ UI 연결
    }


}
