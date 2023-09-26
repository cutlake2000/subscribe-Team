
using UnityEngine;
[CreateAssetMenu(fileName = " MonsterData", menuName = "Scriptable Object/ MonsterData", order = int.MaxValue)]
public class MonsterData : ScriptableObject
{
    public string MonsterName;

    public int Hp;

    public int DF;

    public int Speed;

    public Sprite sprite;
   
}
