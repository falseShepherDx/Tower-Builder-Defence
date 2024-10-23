using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingType")]
public class BuildingTypeScriptableObject : ScriptableObject
{
    public string BuildingName;
    public Transform Prefab;
    public ResourceGeneratorData ResourceGeneratorData; 
    public bool HasResourceGenerator;
    public Sprite BuildingSprite;
    public float MinConstructionRadius;
    public ResourceAmount[] ConstructionCostArray;
    public int BuildingHealthMax;
    public float ConstructionTimerMax;

    public string GetCollectorCostString()
    {
        string costString = "";
        foreach (ResourceAmount resourceAmount in ConstructionCostArray)
        {
            string colorCode = resourceAmount.resourceType.HexColorCode;
            
            costString += "<color=#" + colorCode + ">" + resourceAmount.resourceType.ResourceShortName + ": " + resourceAmount.resourceAmount + "</color> ";
        }

        return costString;
    }
    
}
