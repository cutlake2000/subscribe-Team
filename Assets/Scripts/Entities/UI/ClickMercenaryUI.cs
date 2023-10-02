using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMercenaryUI : MonoBehaviour
{
    public GameObject StatusWindow;
    public GameObject popupUI;
    private Vector3 UIPos;
    public List<string> name;
    private void Awake()
    {
        MercenaryController controller = gameObject.GetComponent<MercenaryController>();
        name .Add(controller.data.MercenaryName);
        DataManager.Instance.MercenaryID = controller.data.MercenaryID;
    }

    public void Update()
    {
        UIPos = Camera.main.WorldToScreenPoint(transform.position);
    }
    public void PopUp()
    {
        //Vector3 UIPos = Camera.main.WorldToScreenPoint(transform.position);
        popupUI.transform.position = UIPos;
        popupUI.SetActive(true);
        Invoke(nameof(PopOff), 3.0f);
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
