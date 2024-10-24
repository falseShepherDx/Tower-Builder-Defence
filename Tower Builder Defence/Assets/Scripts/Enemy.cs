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
    private Vector3 currentVelocity;
    [SerializeField] private float enemyMoveSpeed;
    [SerializeField] private int enemyDamage;
    [SerializeField] private float targetChaseRadius;
    private float targetChaseTimer, 
        targetChaseTimerMax=0.2f;

    private HealthSystem _healthSystem;
    
    
    void Start()
    {
        if (BuildManager.Instance.GetHqBuilding().transform != null)
        {
            target=BuildManager.Instance.GetHqBuilding().transform;
        }
        rb = GetComponent<Rigidbody2D>();
        targetChaseTimer = Random.Range(0f, targetChaseTimerMax);
        _healthSystem=GetComponent<HealthSystem>();
        _healthSystem.OnDied += HealthSystem_OnDied;
        _healthSystem.OnDamaged += HealthSystem_OnDamaged;
    }

    private void HealthSystem_OnDamaged(object sender, EventArgs e)
    {
        CameraShake.Instance.Shake(2,0.1f);
        ChromaticAberration.Instance.SetWeight(0.4f);
        SoundManager.Instance.PlaySound(SoundManager.Sound.EnemyHit);
    }

    private void HealthSystem_OnDied(object sender, EventArgs e)
    {
        CameraShake.Instance.Shake(3.2f,0.15f);
        ChromaticAberration.Instance.SetWeight(0.7f);
        Instantiate((Resources.Load<Transform>("enemyDeathParticle")), transform.position,
            Quaternion.identity);
        SoundManager.Instance.PlaySound(SoundManager.Sound.EnemyDie);
        Destroy(gameObject);
        
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
            this._healthSystem.TakeDamage(999);
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
            Vector3 targetVelocity = moveDirection * enemyMoveSpeed;
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref currentVelocity, 0.3f);
        }
        if (rb.velocity.x > 0)
        {
            
            transform.localScale = new Vector3(-2, transform.localScale.y, transform.localScale.z);
        }
        else if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(2, transform.localScale.y, transform.localScale.z);
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
                        Vector3.Distance(transform.position, target.position)) 
                    {
                        target = building.transform;
                    }
                }
            }
        }

        if (target == null)
        {
            if (BuildManager.Instance.GetHqBuilding().transform != null)
            {
                target = BuildManager.Instance.GetHqBuilding().transform;
            }
            
        }
    }
}
