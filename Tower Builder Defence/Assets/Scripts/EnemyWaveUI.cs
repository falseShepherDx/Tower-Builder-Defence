using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using RectTransform = UnityEngine.RectTransform;

public class EnemyWaveUI : MonoBehaviour
{
    [SerializeField] private EnemyWaveManager enemyWaveManager;
    private TextMeshProUGUI _waveNumberText;
    private TextMeshProUGUI _waveMessageText;
    private RectTransform _enemySpawnPosIndıcator;
    private RectTransform _closestEnemyIndicator;
    private Camera mainCamera;
    private void Awake()
    {
        _waveNumberText=transform.Find("WaveNumberTxt").GetComponent<TextMeshProUGUI>();
        _waveMessageText=transform.Find("WaveMessageTxt").GetComponent<TextMeshProUGUI>();
        _enemySpawnPosIndıcator = transform.Find("EnemySpawnPosIndıcator").GetComponent<RectTransform>();
        _closestEnemyIndicator = transform.Find("ClosestEnemyIndıcator").GetComponent<RectTransform>();
    }

    private void Start()
    {
        mainCamera = Camera.main;
        enemyWaveManager.OnWaveNumberChanged += EnemyWaveManager_OnWaveNumberChanged;
        SetNumberText("Wave " + enemyWaveManager.GetWaveNumber());
    }

    private void EnemyWaveManager_OnWaveNumberChanged(object sender, EventArgs e)
    {
        SetNumberText("Wave " + enemyWaveManager.GetWaveNumber());
    }

    private void Update()
    {
        HandleNextWaveMessage();
        HandleEnemyWaveSpawnPositionIndicator();
        HandleEnemyClosestPosIndicator();
    }

    private void HandleNextWaveMessage()
    {
        float nextWaveSpawnTimer = enemyWaveManager.GetNextWaveSpawnTimer();
        if (nextWaveSpawnTimer <= 0)
        {
            SetMessageText("");
        }
        else
        {
            SetMessageText("Next Wave in " + nextWaveSpawnTimer.ToString("F1") + "s");
        }
    }
    private void HandleEnemyWaveSpawnPositionIndicator()
    {
        Vector3 directionToNextWaveSpawnPos=(enemyWaveManager.GetSpawnPosition() - mainCamera.transform.position).normalized;
        _enemySpawnPosIndıcator.anchoredPosition = directionToNextWaveSpawnPos * 300f;
        _enemySpawnPosIndıcator.eulerAngles =
            new Vector3(0, 0, MouseCursorPos.GetAngleFromVector(directionToNextWaveSpawnPos));
        float distanceToNextSpawnPosition =
            Vector3.Distance(enemyWaveManager.GetSpawnPosition(), mainCamera.transform.position);
        _enemySpawnPosIndıcator.gameObject.SetActive(distanceToNextSpawnPosition>mainCamera.orthographicSize*1.5f);
    }

    private void HandleEnemyClosestPosIndicator()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(mainCamera.transform.position, Mathf.Infinity);
        Enemy targetEnemy = null;

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
                        Vector3.Distance(transform.position, targetEnemy.transform.position))
                    {
                        targetEnemy = enemy;
                    }
                }
            }
        }

        if (targetEnemy != null)
        {
            Vector3 directionToClosestEnemy = (targetEnemy.transform.position - mainCamera.transform.position).normalized;

            // Smoothly update the indicator position
            float smoothingFactor = 0.1f;
            _closestEnemyIndicator.anchoredPosition = Vector3.Lerp(_closestEnemyIndicator.anchoredPosition, directionToClosestEnemy * 250f, smoothingFactor);

            _closestEnemyIndicator.eulerAngles =
                new Vector3(0, 0, MouseCursorPos.GetAngleFromVector(directionToClosestEnemy));
            float distanceToClosestEnemy =
                Vector3.Distance(targetEnemy.transform.position, mainCamera.transform.position);

            // Show the indicator only when the enemy is significantly closer
            float thresholdDistance = mainCamera.orthographicSize * 1.5f;
            _closestEnemyIndicator.gameObject.SetActive(distanceToClosestEnemy > thresholdDistance);
        }
        else
        {
            _closestEnemyIndicator.gameObject.SetActive(false);
        }
    }


    private void SetMessageText(string message)
    {
        _waveMessageText.SetText(message);
    }

    private void SetNumberText(string text)
    {
        _waveNumberText.SetText(text);
    }
}
