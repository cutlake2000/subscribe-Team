using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Building", fileName = "Building")]
public class BuildingSO : ScriptableObject
{
    public BuildingData[] buildingDatas;
}

[Serializable]
public class BuildingData
{
    public BuildingType bidildingType;
    public int level;
    public string name;
    [Multiline(2)] public string desc;
    public int upgradeGold;
}

