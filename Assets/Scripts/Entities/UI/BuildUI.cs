using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class BuildUI : MonoBehaviour
{
    [SerializeField] GameObject buildingListUI;
    [SerializeField] GameObject buildButton;
    [SerializeField] Animator animator;

    private void Awake()
    {
        UIManger.Instance.DayUsedUIOn += Off;
    }

    public void SwitchButton()
    {
        if (buildingListUI.gameObject.activeSelf)
            ListUIOff();
        else
            ListUIOn();
    }

    public void ListUIOn()
    {
        buildingListUI.gameObject.SetActive(true);
        UIManger.Instance.BuyMercenaryUIList.SetActive(false);
        UIManger.Instance.buyMercenaryUI.ChangeClicked(true);
        animator.SetTrigger("AppearList");
    }

    public void ListUIOff()
    {
        buildingListUI.gameObject.SetActive(false);
    }

    public void On()
    {
        buildButton.SetActive(true);
    }

    public void Off()
    {
        ListUIOff();
        buildButton.SetActive(false);
    }
}
