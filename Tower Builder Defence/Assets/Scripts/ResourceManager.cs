using System;
using System.Collections.Generic;
using UnityEngine;
public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }//it can read from anywhere but it can only be changed within this class.
    private Dictionary<ResourceTypeScriptableObject, int> resourceAmountDictionary;
    public event EventHandler OnResourceAmountChanged;
    [SerializeField] private List<ResourceAmount> startingResourceAmount;
    
    private void Awake()
    {
        Instance = this;//singleton
        resourceAmountDictionary = new Dictionary<ResourceTypeScriptableObject, int>();
        ResourcesTypeList resourcesTypeList = Resources.Load<ResourcesTypeList>(nameof(ResourcesTypeList));//listeye erişim

        foreach (ResourceTypeScriptableObject resourceType in resourcesTypeList.resourceList)
        {
            resourceAmountDictionary[resourceType] = 0;//baslangıcta 0 resource
        }

        foreach (ResourceAmount resourceAmounts in startingResourceAmount)
        {
            AddResource(resourceAmounts.resourceType,resourceAmounts.resourceAmount);//for testing
        }
    }
   

    public void AddResource(ResourceTypeScriptableObject resourceType, int amount)
    {
        resourceAmountDictionary[resourceType] += amount;
        OnResourceAmountChanged?.Invoke(this,EventArgs.Empty);
        
    }

    public void DecreaseResource(ResourceAmount[] resourceAmounts)
    {
        foreach (ResourceAmount resourceAmount in resourceAmounts)
        {
            resourceAmountDictionary[resourceAmount.resourceType] -= resourceAmount.resourceAmount;
        }
    }

    public int GetResourceAmount(ResourceTypeScriptableObject resourceType)
    {
        return resourceAmountDictionary[resourceType];
    }

    public bool CanAfford(ResourceAmount[] resourceAmounts)
    {
        foreach (ResourceAmount resourceAmount in resourceAmounts )
        {
            if (GetResourceAmount(resourceAmount.resourceType) >= resourceAmount.resourceAmount)
            {
                
            }
            else
            {
                return false;
            }
        }

        return true;
    }
}
