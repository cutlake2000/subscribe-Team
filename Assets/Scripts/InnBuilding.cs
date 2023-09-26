using UnityEngine;

internal class InnBuilding : BaseBuilding
{
    int maxUnitValue = 3;

    protected override void Awake()
    {
        base.Awake();
        buildingType = BuildingType.Forge;
    }

    protected override void Initialization(BuildingData data)
    {
        buildingType = BuildingType.Forge;
        level = data.level;
        buildingName = data.name;
        desc = data.desc;
        upgradeGold = data.upgradeGold;
    }

}

