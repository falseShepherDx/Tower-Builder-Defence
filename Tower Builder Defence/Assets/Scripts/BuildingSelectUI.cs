using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSelectUI : MonoBehaviour
{
    [SerializeField] private List<Button> buildingButtons;
    [SerializeField] private List<Image> selectedOutlines;
    
    private BuildManager _buildManager;

    private void Start()
    {
        _buildManager = BuildManager.Instance;
        _buildManager.OnBuildSelected += BuildManager_OnBuildSelected;
        for (int i = 0; i < buildingButtons.Count; i++)
        {
            int index = i;
            buildingButtons[i].onClick.AddListener(() => SelectBuilding(index));
            
        }
    }
    private void SelectBuilding(int index)
    {
        for (int i = 0; i < selectedOutlines.Count; i++)
        {
            selectedOutlines[i].gameObject.SetActive(false);
        }
        selectedOutlines[index].gameObject.SetActive(true);
        
        BuildingTypeScriptableObject selectedBuilding = _buildManager.GetBuildingTypeByIndex(index);
        _buildManager.SetActiveBuildingType(selectedBuilding);
    }


    private void BuildManager_OnBuildSelected(object sender, EventArgs e)
    {
        
        //buraya bir şeyler eklerim 
    }
}
