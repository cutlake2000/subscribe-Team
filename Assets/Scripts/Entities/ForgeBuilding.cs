internal class ForgeBuilding : BaseBuilding
{
    int addUnitAtk = 1; // 1레벨 수치
    public int AddUnitAtk
    {
        get { return addUnitAtk; }
    }

    public override void Initialization()
    {
        base.Initialization();
        addUnitAtk = 1;
    }

    public override void LevelUP()
    {
        base.LevelUP();
        addUnitAtk += 1;
    }
}
