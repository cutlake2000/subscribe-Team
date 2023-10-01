using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMercenaryUI : MonoBehaviour
{
    public GameObject StatusWindow;
    public GameObject popupUI;

    public void PopUp()
    {
        Vector3 UIPos = Camera.main.WorldToScreenPoint(transform.position);
        popupUI.transform.position = UIPos;
        popupUI.SetActive(true);
    }

    public void PopOff()
    {
        popupUI.SetActive(false);
    }

    public void OpenStatusUI()
    {
        StatusWindow.SetActive(true) ;
    }

    public void CloseStatusUI()
    {
        StatusWindow.SetActive(false) ;
    }
}
