using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/BuildingType")]
public class BuildingTypeScriptableObject : ScriptableObject
{
    public string buildingName;
    public Transform prefab;
    public ResourceGeneratorData resourceGeneratorData;
    public Sprite buildingSprite;
    public float minConstructionRadius;
    public ResourceAmount[] constructionCostArray;
}
