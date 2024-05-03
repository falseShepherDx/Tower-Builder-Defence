using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using Random = UnityEngine.Random;


public class Enemy : MonoBehaviour
{
    private Transform target;
    private Rigidbody2D rb;
    [SerializeField] private float enemyMoveSpeed;
    [SerializeField] private int enemyDamage;
    [SerializeField] private float targetChaseRadius;
    private float targetChaseTimer, 
        targetChaseTimerMax=0.2f;
    
    
    void Start()
    {
        target=BuildManager.Instance.GetHqBuilding().transform;
        rb = GetComponent<Rigidbody2D>();
        targetChaseTimer = Random.Range(0f, targetChaseTimerMax);
    }

    
    void Update()
    {
        HandleMovement();
        HandleTargetTimer();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Building building = other.gameObject.GetComponent<Building>();
        if (building != null)
        {
            HealthSystem healthSystem = building.GetComponent<HealthSystem>();
            healthSystem.TakeDamage(enemyDamage);
            Destroy(gameObject);
        }
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

    private void HandleMovement()
    {
        if (target != null)
        {
            Vector3 moveDirection = (target.position - transform.position).normalized;
            rb.velocity = moveDirection *enemyMoveSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    public static Enemy Create(Vector3 position)
    {
        Transform enemyPrefab = Resources.Load<Transform>("Enemy");
        Transform enemyTransform=Instantiate(enemyPrefab, position, quaternion.identity);
        Enemy enemy = enemyTransform.GetComponent<Enemy>();
        return enemy;
    }

    private void LookForTargets()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, targetChaseRadius);

        foreach (Collider2D collider2D in collider2Ds)
        {
            Building building = collider2D.GetComponent<Building>();
            if (building != null)
            {
                if (target == null)
                {
                    target = building.transform;
                }
                else
                {
                    if (Vector3.Distance(transform.position, building.transform.position) <
                        Vector3.Distance(transform.position, target.position)) ;
                    {
                        target = building.transform;
                    }
                }
            }
        }

        if (target == null)
        {
            target = BuildManager.Instance.GetHqBuilding().transform;
        }
    }
}
