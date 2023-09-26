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
        // 테스트용
        DeliverNewBuilding(BuildingType.Inn);
        DeliverNewBuilding(BuildingType.Forge);
        //

        DeliverNewBuilding(BuildingType.Inn);

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



    // '비활성화'된 빌딩 데이터 초기화
    private void ResetBuildingData(BaseBuilding newBuilding)
    {
        newBuilding.Initialization();
        newBuilding.gameObject.SetActive(true);
    }


    // 빌딩 UI
    public void ActiveBuildingUI()
    {
        // 날이 바뀌면 자동으로 꺼짐(이벤트 등록)
        // 빌딩 누르면 UI 나오기
    }


}
