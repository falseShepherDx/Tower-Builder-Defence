using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class NearbyResourceEffiiencyUI : MonoBehaviour
{
    private ResourceGeneratorData resourceGeneratorData;
    private void Awake()
    {
        Hide();
    }

    private void Update()
    {
        int nearbyResourceAmount = ResourceGenerator.GetNearbyResourceAmount(resourceGeneratorData, transform.position-transform.localPosition);
        float percentage = Mathf.RoundToInt((float)nearbyResourceAmount / resourceGeneratorData.maxResourceSource * 100f);
        transform.Find("text").GetComponent<TextMeshPro>().SetText("%" + percentage);
    }

    public void Show(ResourceGeneratorData resourceGeneratorData)
    {
        this.resourceGeneratorData = resourceGeneratorData;
        gameObject.SetActive(true);
        transform.Find("icon").GetComponent<SpriteRenderer>().sprite =
            resourceGeneratorData.resourceType.resourceSprite;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
