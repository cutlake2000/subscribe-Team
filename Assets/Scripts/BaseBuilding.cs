using UnityEngine;


// 건물 짓기 -> 컨트롤러
// 업그레이드 스스로
// Inn : 유닛 수 증가
// Forge : 유닛 업그레이드


public enum BuildingType
{
    Inn, Forge
}

public class BaseBuilding : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;

    public BuildingType buildingType;
    public int level;
    public string buildingName;
    public string desc;
    public int upgradeGold;


    //건물 짓기,
    protected virtual void Awake() { }

    protected virtual void Initialization(BuildingData data) { }

    protected virtual void UpgradeBuilding()
    {

    }

    void Start()
    {
        
    }


}
