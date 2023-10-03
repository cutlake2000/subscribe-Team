using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class BuildUI : MonoBehaviour
{
    [SerializeField] GameObject BuildingListUI;
    [SerializeField] Animator animator;


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
        animator.SetTrigger("AppearList");
    }

    public void Off()
    {
        BuildingListUI.gameObject.SetActive(false);
    }
}
