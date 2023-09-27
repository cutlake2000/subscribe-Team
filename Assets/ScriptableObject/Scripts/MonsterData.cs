
using UnityEngine;
[CreateAssetMenu(fileName = " MonsterData", menuName = "Scriptable Object/ MonsterData", order = int.MaxValue)]
public class MonsterData : ScriptableObject
{
    public string MonsterName;

    public float MonsterHp;

    public float MonsterDF;

    public float MonsterSpeed;

    public GameObject dropPrefab;

}
