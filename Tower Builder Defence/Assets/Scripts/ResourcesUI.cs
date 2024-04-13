using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResourcesUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI woodCountText,goldCountText,stoneCountText;
    private ResourcesTypeList resourcesTypeList;
    private void Start()
    {
        resourcesTypeList = Resources.Load<ResourcesTypeList>(nameof(ResourcesTypeList));
        ResourceManager.Instance.OnResourceAmountChanged += ResourceManager_OnResourceAmountChanged;
        UpdateResourceAmountsUI();
    }

    private void ResourceManager_OnResourceAmountChanged(object sender, EventArgs e)
    {
        UpdateResourceAmountsUI();
    }

    private void UpdateResourceAmountsUI()
    {
        woodCountText.text =ResourceManager.Instance.GetResourceAmount(resourcesTypeList.resourceList[0]).ToString();
        stoneCountText.text = ResourceManager.Instance.GetResourceAmount(resourcesTypeList.resourceList[1]).ToString();
        goldCountText.text = ResourceManager.Instance.GetResourceAmount(resourcesTypeList.resourceList[2]).ToString();
        
    }
}
