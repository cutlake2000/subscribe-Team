
using UnityEngine;
[CreateAssetMenu(fileName = " MonsterData", menuName = "Scriptable Object/ MonsterData", order = int.MaxValue)]
public class MonsterData : ScriptableObject
{
    public string MonsterName;

    public int MonsterHp;

    public int MonsterDF;

    public int MonsterSpeed;

    public Sprite Monstersprite;
   
}
