using System;
using System.Collections.Generic;
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
    public int maxBuildLimit;
    public int level;
    public int maxLevel;
    public string name;
    public int buildWood;
    public int upgradeWood;

    public List<ClickBtnType> optionTypeList;
}
