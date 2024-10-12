using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Tower : MonoBehaviour
{
    private Enemy targetEnemy;
    [SerializeField] private float shootRadius;
    private float targetChaseTimer, targetChaseTimerMax=0.2f;
    [SerializeField] private Transform shootingPos;
    private float shootTimer;
    [SerializeField] private float shootTimerMax;
    
    
    private void Update()
    {
        HandleTargetTimer();
        HandleShooting();
    }

    private void HandleTargetTimer()
    {
        targetChaseTimer -= Time.deltaTime;
        if (targetChaseTimer< 0f)
        {
            targetChaseTimer += targetChaseTimerMax;
            LookForTargets();
        }
    }

    private void HandleShooting()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0)
        {
            shootTimer += shootTimerMax;
            if (targetEnemy != null)
            {
                ArrowProjectile.Create(shootingPos.position, targetEnemy);
            }
        }
       
        
    }
    private void LookForTargets()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, shootRadius);

        foreach (Collider2D collider2D in collider2Ds)
        {
            Enemy enemy = collider2D.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (targetEnemy == null)
                {
                    targetEnemy = enemy;
                }
                else
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) <
                        Vector3.Distance(transform.position, targetEnemy.transform.position)) ;
                    {
                        targetEnemy = enemy;
                    }
                }
            }
        }
    }
}
