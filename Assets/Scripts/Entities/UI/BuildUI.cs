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
        UIManger.DayUsedUIOn += Off;
    }

    public void SwitchButton()
    {
        if (buildingListUI.gameObject.activeSelf)
            ListUIOff();
        else
            ListUIOn();
    }

    private void ListUIOn()
    {
        buildingListUI.gameObject.SetActive(true);
        animator.SetTrigger("AppearList");
    }

    private void ListUIOff()
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
