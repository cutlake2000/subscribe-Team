using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUI : MonoBehaviour
{
    private BaseBuilding tagetBuilding;

    public void On(BaseBuilding building)
    {
        tagetBuilding = building;
        transform.position = building.transform.position;
        gameObject.SetActive(true);
    }

    public void OFF()
    {
        gameObject.SetActive(false);
    }

}
