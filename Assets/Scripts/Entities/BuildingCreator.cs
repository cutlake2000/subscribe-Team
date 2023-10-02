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

    public DragNDrop dnd;

    public bool _isEditMode = false;

    public Dictionary<Vector2,bool> TileData = new Dictionary<Vector2,bool>();
    public Vector2 selectVec;
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

            if (Input.GetMouseButtonDown(0))
            {
                if (TileChecker())
                {
                    //설치 가능
                    Debug.Log("설치 완료!");
                    //건물 오브젝트 해당 위치에 건설. // selectVec   //x,y -> x,z


                    //설치후 타일 막기
                    TileData[selectVec] = false;
                }
                else
                {
                    //설치 실패
                    Debug.Log("설치 실패!");
                    dnd.ReturnUI();
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                //설치 취소
                Debug.Log("설치 취소!");
                dnd.ReturnUI();
            }
        }
      

    }

    public bool TileChecker()
    {
        Vector2 v = new Vector2(selectObj.transform.position.x, selectObj.transform.position.z);
        selectVec = v;
        return TileData[v];
    }

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

        for (int i = -7; i < 8; i++) 
        {
            for (int j = -7; j < 8; j++)
            {
                Vector2 v = new Vector2(i, j);
                TileData.Add(v, true);
            }
        }
    }
}
