using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
   private HealthSystem _healthSystem;
   private BuildingTypeScriptableObject buildingType;
   private void Awake()
   {
      buildingType = GetComponent<BuildingTypeClass>().buildingType;
      _healthSystem = GetComponent<HealthSystem>();
      _healthSystem.SetMaxHealth(buildingType.buildingHealthMax,true);
      _healthSystem.OnDied += _healthSystem_OnDied;

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
}
