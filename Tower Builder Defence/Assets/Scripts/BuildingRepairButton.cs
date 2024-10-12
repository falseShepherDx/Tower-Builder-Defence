using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingRepairButton : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    public ResourceTypeScriptableObject goldResource;
    
    private void Awake()
    {
        transform.Find("Button").GetComponent<Button>().onClick.AddListener(() =>
        {
            int remainingHealth = healthSystem.GetFullHealth() - healthSystem.GetHealthAmount();
            var repairCost = remainingHealth / 4;
            ResourceAmount[] resourceAmountCost = new ResourceAmount[]
            {
                new ResourceAmount()
                {
                    resourceType = goldResource,
                    resourceAmount = repairCost
                }
            };
            if (ResourceManager.Instance.CanAfford(resourceAmountCost))
            {
                ResourceManager.Instance.DecreaseResource(resourceAmountCost);
                healthSystem.HealMax();
            }
            else
            {
                PlayerTipUI.Instance.Show("Can't afford repair cost.", new PlayerTipUI.TipMessageTimer(){timer = 2f});
            }
         

        });
    }
}
