using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class BuildingCreator : MonoBehaviour
{

    public Transform plane;
    public Grid grid;
    public GameObject selectObj;
    public float x, y;
    public Vector3 lastPosition;
    public LayerMask lm;

    public bool _isEditMode = false;
    // Start is called before the first frame update
    void Awake()
    {
        Init();
    }

    private void Update()
    {
        //에딧모드 일 때만 건물짓는 그리드가 형성됨. (드래그시 true)
        if (_isEditMode)
        {
            Vector3 mousePosition = GetMousePosisiton();
            Vector3Int gridPosition = grid.WorldToCell(mousePosition);
            selectObj.transform.position = new Vector3(grid.CellToWorld(gridPosition).x, -1.5f + 0.251f, grid.CellToWorld(gridPosition).z);
        }
      

    }
    //드래그앤 드롭할 오브젝트에 넣어서 자신을 움직이는 방식을 채택
 /*   public void OnDragBegin(BaseEventData data)
    {
        Debug.Log(data);
    }

    public void OnDrag(BaseEventData data)
    {
        Debug.Log(data);
        
    }*/
    public Vector3 GetMousePosisiton()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, lm))
        { lastPosition = hit.point; }
        return lastPosition;
    }

    private void Init()
    {
        x = plane.localScale.x;
        y = plane.localScale.y;
    }
}
