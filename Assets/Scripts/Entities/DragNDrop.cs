using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DragNDrop : MonoBehaviour
{
    public BuildingCreator buildingCreator;
    public RectTransform rectTransform;
    public Vector3 lastPosition;
    public Vector3 firstPosition;
    public void OnDragBegin(BaseEventData data)
    {
        Debug.Log(data);
        rectTransform.position = GetMousePosisiton();
    }

    public void OnDrag(BaseEventData data)
    {
        Debug.Log(data);
        rectTransform.position = GetMousePosisiton();

    }
    public void OnDrop(BaseEventData data)
    {
        rectTransform.position = firstPosition;
    }

    public void Start()
    {
        firstPosition = rectTransform.position;
    }
    public Vector3 GetMousePosisiton()
    {
        Vector3 mousePos = Input.mousePosition;
     
        return mousePos;
    }
}
