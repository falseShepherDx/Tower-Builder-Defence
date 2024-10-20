using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
   private HealthSystem _healthSystem;
   private BuildingTypeScriptableObject buildingType;
   private Transform buildingDemolishButton;
   private Transform buildingRepairButton;
   
   private void Awake()
   {
      buildingType = GetComponent<BuildingTypeClass>().buildingType;
      _healthSystem = GetComponent<HealthSystem>();
      _healthSystem.SetMaxHealth(buildingType.BuildingHealthMax,true);
      _healthSystem.OnDied += _healthSystem_OnDied;
      _healthSystem.OnHealed += _healthSystem_OnHealed;
      _healthSystem.OnDamaged += _healthSystem_OnDamaged;
      buildingDemolishButton = transform.Find("BuildingDemolishButton").GetComponent<Transform>();
      buildingRepairButton = transform.Find("BuildingRepairButton").GetComponent<Transform>();
      if (buildingDemolishButton != null)
      {
         buildingDemolishButton.gameObject.SetActive(false);
      }
      
   }

   private void Start()
   {
      HideRepairButton();
   }

   private void _healthSystem_OnHealed(object sender, EventArgs e)
   {
      if (_healthSystem.IsFullHealth())
      {
         HideRepairButton();
      }
      else
      {
         ShowRepairButton();
      }
   }

   private void _healthSystem_OnDamaged(object sender, EventArgs e)
   {
      ShowRepairButton();
   }
   


   private void _healthSystem_OnDied(object sender, EventArgs e)
   {
      Destroy(gameObject);
      
   }
   

   private void OnMouseOver()
   {
      ShowDemolishButton();
   }

   private void OnMouseExit()
   {
      HideDemolishButton();
   }

   private void ShowDemolishButton()
   {
      if (buildingDemolishButton != null)
      {
         buildingDemolishButton.gameObject.SetActive(true);
      }
   }
   private void HideDemolishButton()
   {
      if (buildingDemolishButton != null)
      {
         buildingDemolishButton.gameObject.SetActive(false);
      }
   }
   private void ShowRepairButton()
   {
      if (buildingRepairButton != null)
      {
         buildingRepairButton.gameObject.SetActive(true);
      }
   }
   private void HideRepairButton()
   {
      if (buildingRepairButton != null)
      {
         buildingRepairButton.gameObject.SetActive(false);

      }
   }
}
