using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionTımerUI : MonoBehaviour
{
    private Image constructionProgressImage;
    [SerializeField] private BuildingConstruction buildingConstruction;
    
    private void Awake()
    {
        constructionProgressImage = transform.Find("constructionLoadingCircleImage").GetComponent<Image>();
    }

   
    void Update()
    {
        constructionProgressImage.fillAmount = buildingConstruction.GetConstructionTimerProgress();
    }
}
