using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthSystem _healthSystem;
    private Transform bar;

    private void Awake()
    {
        bar = transform.Find("bar");
    }

    private void Start()
    {
        _healthSystem.OnDamaged += healthSystem_OnDamaged;
        UpdateBar();
        ManageBarVisibility();
    }

    private void healthSystem_OnDamaged(object sender, EventArgs e)
    {
        UpdateBar();
        ManageBarVisibility();
    }

    private void UpdateBar()
    {
        bar.localScale = new Vector3(_healthSystem.GetHealthAmountPercentage(), 1, 1);
    }

    private void ManageBarVisibility()
    {
        if (_healthSystem.IsFullHealth())
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
    
}
