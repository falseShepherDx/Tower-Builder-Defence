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
   
   private void Awake()
   {
      buildingType = GetComponent<BuildingTypeClass>().buildingType;
      _healthSystem = GetComponent<HealthSystem>();
      _healthSystem.SetMaxHealth(buildingType.BuildingHealthMax,true);
      _healthSystem.OnDied += _healthSystem_OnDied;
      buildingDemolishButton = transform.Find("BuildingDemolishButton").GetComponent<Transform>();
      if (buildingDemolishButton != null)
      {
         buildingDemolishButton.gameObject.SetActive(false);
      }
   }

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.R))
      {
         _healthSystem.TakeDamage(10);
      }
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
}
