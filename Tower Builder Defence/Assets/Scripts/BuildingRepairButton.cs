using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
            if (!healthSystem.IsFullHealth())
            {
                SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingRepair);
            }
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
    private int CalculateRepairCost()
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
        return repairCost;
    }
    

    private void OnMouseOver()
    {
        var repairCost = CalculateRepairCost();
        string goldHexCode = "#FFD700";
        string message = $"Repair Cost:<color={goldHexCode}>{repairCost}</color>";
        PlayerTipUI.Instance.Show(message, new PlayerTipUI.TipMessageTimer() { timer = 0.6f });
    }
}
