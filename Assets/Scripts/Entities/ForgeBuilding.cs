internal class ForgeBuilding : BaseBuilding
{
    int addUnitAtk = 1; // 1레벨 수치
    public int AddUnitAtk
    {
        get { return addUnitAtk; }
    }

    public override void LevelUP()
    {
        base.LevelUP();
        addUnitAtk += 1;
    }
}
