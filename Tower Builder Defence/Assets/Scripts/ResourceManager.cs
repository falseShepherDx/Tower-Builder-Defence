using System;
using System.Collections.Generic;
using UnityEngine;
public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }//it can read from anywhere but it can only be changed within this class.
    private Dictionary<ResourceTypeScriptableObject, int> resourceAmountDictionary;
    public event EventHandler OnResourceAmountChanged;
    private void Awake()
    {
        Instance = this;//singleton
        resourceAmountDictionary = new Dictionary<ResourceTypeScriptableObject, int>();
        ResourcesTypeList resourcesTypeList = Resources.Load<ResourcesTypeList>(nameof(ResourcesTypeList));//listeye erişim

        foreach (ResourceTypeScriptableObject resourceType in resourcesTypeList.resourceList)
        {
            resourceAmountDictionary[resourceType] = 0;//baslangıcta 0 resource
        }
    }
    private void Update()
    {
        //testing
        if (Input.GetKeyDown(KeyCode.T))
        {
            ResourcesTypeList resourcesTypeList = Resources.Load<ResourcesTypeList>(nameof(ResourcesTypeList));
            AddResource(resourcesTypeList.resourceList[2], 2);//0 wood 1 stone 2 gold
            
            
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            ResourcesTypeList resourcesTypeList = Resources.Load<ResourcesTypeList>(nameof(ResourcesTypeList));
            DecreaseResource(resourcesTypeList.resourceList[2],1);
            
        }
    }
   

    public void AddResource(ResourceTypeScriptableObject resourceType, int amount)
    {
        resourceAmountDictionary[resourceType] += amount;
        OnResourceAmountChanged?.Invoke(this,EventArgs.Empty);
        
    }

    public void DecreaseResource(ResourceTypeScriptableObject resourceType, int amount)
    {
        resourceAmountDictionary[resourceType] -= amount;
    }

    public int GetResourceAmount(ResourceTypeScriptableObject resourceType)
    {
        return resourceAmountDictionary[resourceType];
    }
}
