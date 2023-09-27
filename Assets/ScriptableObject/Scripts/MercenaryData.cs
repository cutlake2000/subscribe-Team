using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName ="new Mercenary", fileName = "Mercenary")]
public class MercenaryData : ScriptableObject
{
    [Header("Mercenary Stats")]
    public float Attack;
    public float AttackSpeed;
    public float Critical;
    public float AttackRange;
    public float MovingSpeed;
}
