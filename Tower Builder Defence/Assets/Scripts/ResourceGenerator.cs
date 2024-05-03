using System;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private float timer;
    private float generateRate;
    private ResourceGeneratorData _resourceGeneratorData;

    private void Awake()
    {
        _resourceGeneratorData=GetComponent<BuildingTypeClass>().buildingType.resourceGeneratorData;
        generateRate = _resourceGeneratorData.generateRateTimer;
    }

    public static int GetNearbyResourceAmount(ResourceGeneratorData resourceGeneratorData, Vector3 position)
    {
        Collider2D[] collider2DList = Physics2D.OverlapCircleAll(position, resourceGeneratorData.resourceDetectionRadius);
        int nearbyResourceAmount = 0;
        foreach (Collider2D collider2D in collider2DList)
        {
            ResourceSource resourceSource = collider2D.GetComponent<ResourceSource>();
            if (resourceSource != null)
            {
                if (resourceSource.resourceType == resourceGeneratorData.resourceType)
                {
                    nearbyResourceAmount++;
                }
            }
        }

        nearbyResourceAmount = Mathf.Clamp(nearbyResourceAmount, 0, resourceGeneratorData.maxResourceSource);
        return nearbyResourceAmount;
    }
    private void Start()
    {
        int nearbyResourceAmount = GetNearbyResourceAmount(_resourceGeneratorData,transform.position);
        if (nearbyResourceAmount == 0)
        {
            this.enabled = false;
        }
        else
        {
            //copilot
            float speedupFactor;
            if (nearbyResourceAmount >= _resourceGeneratorData.maxResourceSource / 2f)
            {
                speedupFactor = 1 + (nearbyResourceAmount / (float)_resourceGeneratorData.maxResourceSource);
            }
            else
            {
                speedupFactor = 1 / (1 + ((_resourceGeneratorData.maxResourceSource / 2f - nearbyResourceAmount) / (_resourceGeneratorData.maxResourceSource / 2f)));
            }
            generateRate = _resourceGeneratorData.generateRateTimer / speedupFactor;
        }
        

    }

    public ResourceGeneratorData GetResourceGeneratorData()
    {
        return _resourceGeneratorData;
    }

    public float GetTimer()
    {
        return timer / generateRate;
    }

    public float ResourcePerSecond()
    {
        return 1 / generateRate;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer += generateRate;
            ResourceManager.Instance.AddResource(_resourceGeneratorData.resourceType,1);
        }
    }
}
