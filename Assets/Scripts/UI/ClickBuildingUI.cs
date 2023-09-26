using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickBuildingUI : MonoBehaviour
{
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
}
