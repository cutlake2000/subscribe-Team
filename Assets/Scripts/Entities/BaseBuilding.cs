using UnityEngine;


// 건물 짓기 -> 컨트롤러
// 업그레이드 스스로
// Inn : 유닛 수 증가
// Forge : 유닛 업그레이드
//
public class BaseBuilding : MonoBehaviour
{
    public BuildingData baseData;
    
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator animator;

    public BuildingType buildingType;
    public int level;
    public string buildingName;
    public string desc;
    public int upgradeWood;



    // 데이터 불러오기
    public virtual void Initialization() 
    {
        buildingType = baseData.buildingType;
        level = baseData.level;
        buildingName = baseData.name;
        desc = baseData.desc;
        upgradeWood = baseData.upgradeWood;
    }

    public virtual void LevelUP()
    {
        level++;
        upgradeWood *= 2;
    }

    

    public void ActiveAnimation(bool isActive)
    {
        animator.enabled = isActive;
    }

    public void OnMouseDown()
    {
        BuildingController.instance.ActiveClickBuildingUI(this);
    }
}
