using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class KeyInputController : MonoBehaviour
{
    [SerializeField]
    private LayerMask BuildingLayer;
    private Vector2 mousePos;

    public void GetMousePos(InputAction.CallbackContext context)
    {
        mousePos = context.ReadValue<Vector2>();
    }

    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if (!context.started || EventSystem.current.IsPointerOverGameObject() == true)
            return;

        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit target))
        {
            if (
                BuildingLayer.value
                != (BuildingLayer.value | 1 << target.transform.gameObject.layer)
            )
                return;

            BaseBuilding building = target.transform.GetComponent<BaseBuilding>();
            BuildingController.Instance.ActiveClickBuildingUI(building);
        }
    }
}
