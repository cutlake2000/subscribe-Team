using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public enum BuildingType
{
    Inn,
    Forge
}

public class BuildingController : MonoBehaviour
{
    public static BuildingController instance;

    [SerializeField]
    BuildingSO buildingSO;

    [SerializeField]
    GameObject[] buildingPrefabs;
    public List<BaseBuilding> buildings;

    public BaseBuilding clickBuilding;
    public ClickBuildingUI clickBuildingUI;

    private void Awake()
    {
        instance = this;
        buildings = new List<BaseBuilding>();
    }

    public void Start()
    {
        // - �׽�Ʈ��
        SetNewBuildingOnMap(BuildingType.Inn, Vector2.left);
        SetNewBuildingOnMap(BuildingType.Forge, Vector2.right);
        //
    }

    // ���� �߰��� �ش� ��ġ�� �̵�
    public void SetNewBuildingOnMap(BuildingType type, Vector3 pos)
    {
        BaseBuilding newBuilding = DeliverNewBuilding(type);
        newBuilding.transform.position = pos;
    }

    // ������ ���� �߰�
    private BaseBuilding DeliverNewBuilding(BuildingType type)
    {
        BaseBuilding newBuilding;
        for (int i = 0; i < buildings.Count; i++)
        {
            if (buildings[i].buildingType != type)
                continue;
            if (buildings[i].gameObject.activeSelf == false)
                continue;

            newBuilding = buildings[i];
            ResetBuildingData(newBuilding);
            return newBuilding;
        }

        newBuilding = Instantiate(buildingPrefabs[(int)type]).GetComponent<BaseBuilding>();
        newBuilding.name = type.ToString();
        newBuilding.baseData = buildingSO.buildingDatas[(int)type];
        newBuilding.Initialization();
        buildings.Add(newBuilding);
        return newBuilding;
    }

    // Ŭ������ UI �¿���
    public void ActiveClickBuildingUI(BaseBuilding clickBuilding)
    {
        if (
            clickBuildingUI.gameObject.activeSelf == true // �ӽ�
            && clickBuilding != null
            && this.clickBuilding == clickBuilding
        )
        {
            clickBuildingUI.OFF();
            return;
        }
        this.clickBuilding = clickBuilding;
        clickBuildingUI.On(clickBuilding);
    }

    // ������ ����
    public void LevelUpBuilding(bool isLoop)
    {
        if (clickBuilding.upgradeWood >= DataManager.instance.player.Wood && isLoop == false)
        {
            if (!isLoop)
                Debug.Log("��� ����");

            return;
        }

        if (isLoop == false)
        {
            Debug.Log("������ ����");
            DataManager.instance.player.Wood -= clickBuilding.upgradeWood;
            clickBuilding.LevelUP();
        }
        else
        {
            int upCount = 0;
            while (clickBuilding.upgradeWood < DataManager.instance.player.Wood)
            {
                upCount++;
                DataManager.instance.player.Wood -= clickBuilding.upgradeWood;
                clickBuilding.LevelUP();
            }
            Debug.Log($"{upCount}��ŭ ������ ����");
        }
    }

    // Ŭ�� ���� �ı�
    public void DestroyBuilding()
    {
        clickBuilding.gameObject.SetActive(false); // ���� �߰�
    }

    // '��Ȱ��ȭ'�� ���� ������ �ʱ�ȭ
    private void ResetBuildingData(BaseBuilding newBuilding)
    {
        newBuilding.Initialization();
        newBuilding.gameObject.SetActive(true);
    }

    // ���� ȿ�� ����
    public void RefreshInnEffect()
    {
        int sum = 0;
        for (int i = 0; i < buildings.Count; i++)
        {
            if (buildings[i].buildingType != BuildingType.Inn)
                continue;
            if (buildings[i].gameObject.activeSelf == false)
                continue;

            InnBuilding building = (InnBuilding)buildings[i];
            sum += building.MaxUnitValue;
        }

        DataManager.instance.player.MaxUnitCount = sum;
        Debug.Log("���� ȿ�� : " + DataManager.instance.player.MaxUnitCount);
        // ++ UI ����
    }

    // ���尣 ȿ�� ����
    public void RefreshForgeEffect()
    {
        int sum = 0;
        for (int i = 0; i < buildings.Count; i++)
        {
            if (buildings[i].buildingType != BuildingType.Forge)
                continue;
            if (buildings[i].gameObject.activeSelf == false)
                continue;

            ForgeBuilding building = (ForgeBuilding)buildings[i];
            sum += building.AddUnitAtk;
        }

        DataManager.instance.player.AddUnitAtk = sum;
        Debug.Log("���尣 ȿ�� : " + DataManager.instance.player.AddUnitAtk);

        // ++ UI ����
    }
}
