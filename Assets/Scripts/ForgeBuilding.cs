internal class ForgeBuilding : BaseBuilding
{

    protected override void Awake()
    {
        base.Awake();
        buildingType = BuildingType.Inn;
    }

    public void UpgradeUnit()
    {
        // 유닛 업그레이드
    }
}

