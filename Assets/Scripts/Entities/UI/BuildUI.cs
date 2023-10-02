using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class BuildUI : MonoBehaviour
{
    [SerializeField] GameObject BuildingListUI;


    public void SwitchButton()
    {
        if (BuildingListUI.gameObject.activeSelf)
            Off();
        else
            On();
    }

    public void On()
    {
        BuildingListUI.gameObject.SetActive(true);
    }

    public void Off()
    {
        BuildingListUI.gameObject.SetActive(false);
    }
}
