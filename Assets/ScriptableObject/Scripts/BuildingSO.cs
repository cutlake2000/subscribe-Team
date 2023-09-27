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
    public BuildingType buildingType;
    public int level;
    public int maxLevel;
    public string name;

    [Multiline(2)]
    public string desc;
    public int upgradeWood;
}
