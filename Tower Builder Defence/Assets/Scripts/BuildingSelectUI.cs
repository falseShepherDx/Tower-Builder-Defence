using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSelectUI : MonoBehaviour
{
    [SerializeField] private List<Button> buildingButtons;
    [SerializeField] private List<Image> selectedOutlines;
    [SerializeField] private Button cursorSelectButton;
    [SerializeField] private GameObject cursor;
    private BuildManager _buildManager;

    private void Start()
    {
        _buildManager = BuildManager.Instance;
        for (int i = 0; i < buildingButtons.Count; i++)
        {
            int index = i;
            buildingButtons[i].onClick.AddListener(() => SelectBuilding(index));
            
        }
        cursorSelectButton.onClick.AddListener(() =>ActivateCursor());
    }

    private void ActivateCursor()
    {
        cursor.SetActive(true);
        for (int i = 0; i < selectedOutlines.Count; i++)
        {
            selectedOutlines[i].gameObject.SetActive(false);
        }
        selectedOutlines[3].gameObject.SetActive(true);
        _buildManager.SetActiveBuildingType(null);
    }

    private void SelectBuilding(int index)
    {
        cursor.SetActive(false);
        for (int i = 0; i < selectedOutlines.Count; i++)
        {
            selectedOutlines[i].gameObject.SetActive(false);
            
        }
        selectedOutlines[index].gameObject.SetActive(true);
        
        BuildingTypeScriptableObject selectedBuilding = _buildManager.GetBuildingTypeByIndex(index);
        _buildManager.SetActiveBuildingType(selectedBuilding);
    }


 
}
