using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingDemolishButton : MonoBehaviour
{
    [SerializeField] private Building building;
    
    private void Awake()
    {
        transform.Find("Button").GetComponent<Button>().onClick.AddListener(() =>
        {
            BuildingTypeScriptableObject buildingTypeScriptableObject=building.GetComponent<BuildingTypeClass>().buildingType;
            foreach (ResourceAmount resourceAmount in buildingTypeScriptableObject.ConstructionCostArray)
            {
                ResourceManager.Instance.AddResource(resourceAmount.resourceType,Mathf.FloorToInt(resourceAmount.resourceAmount*0.5f));
            }
            Destroy(building.gameObject);
        });
    }
}
