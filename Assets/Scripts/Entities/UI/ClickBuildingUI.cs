using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class ClickBuildingUI : MonoBehaviour
{
    [SerializeField] TMP_Text lvNameText;
    [SerializeField] TMP_Text currentEffectText;
    [SerializeField] TMP_Text upgradeText;
    private StringBuilder newStatusText = new();


    public void On(BaseBuilding building)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(building.transform.position);
        transform.position = screenPos;
        gameObject.SetActive(true);
    }

    public void OFF()
    {
        gameObject.SetActive(false);
    }

    public void Refresh(BaseBuilding target)
    {
        newStatusText.Clear();
        newStatusText.Append($"Lv. {target.level} {target.buildingName}");
        lvNameText.text = newStatusText.ToString();

        newStatusText.Clear();
        switch (target.buildingType)
        {
            case BuildingType.Inn:
                newStatusText.Append($"인구 수 증가 : {((InnBuilding)target).MaxUnitValue}");
                break;
            case BuildingType.Forge:
                newStatusText.Append($"공격력 증가 : {((ForgeBuilding)target).AddUnitAtk}");
                break;
            default:
                break;
        }
        currentEffectText.text = newStatusText.ToString();

        newStatusText.Clear();
        newStatusText.Append($"{target.upgradeWood}");
        upgradeText.text = newStatusText.ToString();
    }
}
