using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDamaged;
    public event EventHandler OnDied;
    private int currentHealth;
    [SerializeField] private int maxHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        OnDamaged?.Invoke(this, EventArgs.Empty);
        if (IsDead())
        {
            OnDied?.Invoke(this,EventArgs.Empty);
        }

    }

    public bool IsDead()
    {
        return currentHealth == 0;
    }

    public bool IsFullHealth()
    {
        return currentHealth == maxHealth;
    }

    public int GetHealthAmount()
    {
        return currentHealth;
    }

    public float GetHealthAmountPercentage()
    {
        return (float)currentHealth / maxHealth;
    }

    public void SetMaxHealth(int maxHealth,bool updateHealthAmount)
    {
        this.maxHealth = maxHealth;
        if (updateHealthAmount)
        {
            currentHealth = this.maxHealth;
        }
    }
}
