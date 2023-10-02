using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragNDrop : MonoBehaviour
{
    public BuildingCreator buildingCreator;
    public GameObject clickObj;

    //마지막에 눌린 버튼 오브젝트로서 설치가 안됬을 경우 되돌리는 용도로 사용된다.
    public GameObject lastUIObj;
    public bool _isClick;

    //lastUIObj : 건물 선택 메뉴 버튼
    //clickObj : 버튼 클릭후 반투명 건물 이미지(마우스 따라다님)

    public void Start()
    {
        buildingCreator.dnd = this;
    }

    //선택된 UI 되돌리기
    public void ReturnUI()
    {
        _isClick = false;
        clickObj.SetActive(false);
        lastUIObj.SetActive(true);
        buildingCreator._isEditMode = false;
        buildingCreator.selectObj.SetActive(false);
    }

    public void GetButton(GameObject obj)
    {
        clickObj = obj;
        clickObj.SetActive(true);
        _isClick = true;
        buildingCreator._isEditMode = true;
        buildingCreator.selectObj.SetActive(true);
    }

    public void TestButton(GameObject obj)
    {
        lastUIObj = obj;
        lastUIObj.SetActive(false);
    }

   

    public void Update()
    {
        if(_isClick)
        clickObj.transform.position = GetMousePosisiton();
    }

   
    public Vector3 GetMousePosisiton()
    {
        Vector3 mousePos = Input.mousePosition;
     
        return mousePos;
    }

    
}
