using UnityEngine;

internal class InnBuilding : BaseBuilding
{

    int maxUnitValue = 3; // 1레벨 수치
    public int MaxUnitValue {  get { return maxUnitValue; } }

    public override void Initialization()
    {
        base.Initialization();
        maxUnitValue = 3;
    }

    public override void LevelUP()
    {
        base.LevelUP();
        maxUnitValue += 1;
    }
}

