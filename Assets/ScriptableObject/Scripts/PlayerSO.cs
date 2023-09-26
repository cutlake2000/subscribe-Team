using UnityEngine;

[CreateAssetMenu (fileName = "Player", menuName = "ScriptableObject/Player")]
public class PlayerSO : ScriptableObject
{
    int maxUnitCount =0;
    int currentUnitCount=0;
    int addUnitAtk=0;
    int gold=1000;
    int wood=100;

    public int MaxUnitCount { get { return maxUnitCount; } set { maxUnitCount = value; } }
    public int CurrentUnitCountt { get { return currentUnitCount; } set { currentUnitCount = value; } }
    public int Gold { get { return gold; } set { gold = value; } }
    public int Wood { get { return wood; } set { wood = value; } }
    public int AddUnitAtk { get { return addUnitAtk; } set { addUnitAtk = value; } }
}
