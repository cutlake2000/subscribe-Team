using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObject/Player")]
public class PlayerSO : ScriptableObject
{
    int maxUnitCount = 0;
    int defaultUnitCount = 5;
    int currentUnitCount = 0;
    int addUnitAtk = 0;
    int gold = 1000;
    int wood = 10;

    private Dictionary<BuildingType, int> currentBuildingCount = new Dictionary<BuildingType, int>()
    {
        { BuildingType.Inn, 0 },
        { BuildingType.Forge, 0 },
        { BuildingType.Market, 0 }
    };

    public int MaxUnitCount
    {
        get { return maxUnitCount + defaultUnitCount; }
        set { maxUnitCount = value; }
    }
    public int CurrentUnitCountt
    {
        get { return currentUnitCount; }
        set { currentUnitCount = value; }
    }
    public int Gold
    {
        get { return gold; }
        set { gold = value; UIManger.Instance.HUDRefresh(); }
    }
    public int Wood
    {
        get { return wood; }
        set { wood = value; UIManger.Instance.HUDRefresh(); }
    }
    public int AddUnitAtk
    {
        get { return addUnitAtk; }
        set { addUnitAtk = value; }
    }

    public void SetCurrentBuildingCount(BuildingType type, int count)
    {
        currentBuildingCount[type] = count;
    }

    public int GetCurrentBuildingCount(BuildingType type)
    {
        return currentBuildingCount[type];
    }
}
