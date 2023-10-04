using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyMercenaryUI : MonoBehaviour
{
    public GameObject BuyUI;
    bool isClicked = true;

    public void ShowUI()
    {
        if (isClicked == true)
            UIManger.Instance.buildUI.ListUIOff();

        BuyUI.SetActive(isClicked);
    }

    public void CheckClick()
    {
        if(isClicked == true)
        {
            isClicked = false;
        }else if(isClicked == false)
        {
            isClicked = true;
        }
    }

    public void ChangeClicked(bool isClicked)
    {
        this.isClicked = isClicked;
    }
}
