using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class ArrowProjectile : MonoBehaviour
{
    private Enemy targetEnemy;
    [SerializeField] private float arrowMoveSpeed;
    private float timeToDestroy = 2f;
    private Vector3 lastMoveDir; 
    [SerializeField] private int arrowDamage=10;
    
    
    public static ArrowProjectile Create(Vector3 position,Enemy enemy)
    {
        Transform arrowPrefab = Resources.Load<Transform>("ArrowProjectile");
        Transform arrowTransform=Instantiate(arrowPrefab, position,Quaternion.identity);
        ArrowProjectile arrowProjectile = arrowTransform.GetComponent<ArrowProjectile>();
        arrowProjectile.SetTarget(enemy);
        return arrowProjectile;
    }
    
    private void Update()
    {
        Vector3 moveDir;
        if (targetEnemy != null)
        {
            moveDir=(targetEnemy.transform.position - transform.position).normalized;
            lastMoveDir = moveDir;
        }
        else
        {
            moveDir = lastMoveDir;
        }
        
        float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
        transform.position += moveDir * (Time.deltaTime * arrowMoveSpeed);

        timeToDestroy -= Time.deltaTime;
        if (timeToDestroy <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetTarget(Enemy enemy)
    {
        this.targetEnemy = enemy;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if(enemy!=null)
        {
            enemy.GetComponent<HealthSystem>().TakeDamage(arrowDamage);
            Destroy(gameObject);
        }
            
            
    }
}
