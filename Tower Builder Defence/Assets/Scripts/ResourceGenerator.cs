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

    private void Start()
    {
        Collider2D[] collider2DList = Physics2D.OverlapCircleAll(transform.position, _resourceGeneratorData.resourceDetectionRadius);
        int nearbyResourceAmount = 0;
        foreach (Collider2D collider2D in collider2DList)
        {
            ResourceSource resourceSource = collider2D.GetComponent<ResourceSource>();
            if (resourceSource != null)
            {
                if (resourceSource.resourceType == _resourceGeneratorData.resourceType)
                {
                    nearbyResourceAmount++;
                }
            }
        }

        nearbyResourceAmount = Mathf.Clamp(nearbyResourceAmount, 0, _resourceGeneratorData.maxResourceSource);
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

        Debug.Log("Nearby Resource Amount : " + nearbyResourceAmount + "timerMax:" + generateRate);

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
