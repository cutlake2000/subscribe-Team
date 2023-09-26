using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName ="new Mercenary", fileName = "Mercenary")]
public class MercenaryData : ScriptableObject
{
    [Header("Mercenary Stats")]
    public int Attack;
    public int AttackSpeed;
    public int Critical;
}
