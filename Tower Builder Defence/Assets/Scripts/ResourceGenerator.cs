using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private float timer;
    private float generateRate;
    private BuildingTypeScriptableObject buildingType;

    private void Awake()
    {
        buildingType=GetComponent<BuildingTypeClass>().buildingType;
        generateRate = buildingType.resourceGeneratorData.generateRateTimer;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer += generateRate;
            Debug.Log(buildingType.resourceGeneratorData.resourceType.resourceName);
            ResourceManager.Instance.AddResource(buildingType.resourceGeneratorData.resourceType,1);
        }
    }
}
