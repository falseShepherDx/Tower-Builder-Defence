using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyWaveUI : MonoBehaviour
{
    [SerializeField] private EnemyWaveManager enemyWaveManager;
    private TextMeshProUGUI _waveNumberText;
    private TextMeshProUGUI _waveMessageText;
    private RectTransform _enemySpawnPosIndıcator;
    private Camera mainCamera;
    private void Awake()
    {
        _waveNumberText=transform.Find("WaveNumberTxt").GetComponent<TextMeshProUGUI>();
        _waveMessageText=transform.Find("WaveMessageTxt").GetComponent<TextMeshProUGUI>();
        _enemySpawnPosIndıcator = transform.Find("EnemySpawnPosIndıcator").GetComponent<RectTransform>();
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
        float nextWaveSpawnTimer = enemyWaveManager.GetNextWaveSpawnTimer();
        if (nextWaveSpawnTimer <= 0)
        {
            SetMessageText("");
        }
        else
        {
            SetMessageText("Next Wave in " + nextWaveSpawnTimer.ToString("F1") + "s");
        }

        Vector3 directionToNextWaveSpawnPos=(enemyWaveManager.GetSpawnPosition() - mainCamera.transform.position).normalized;
        _enemySpawnPosIndıcator.anchoredPosition = directionToNextWaveSpawnPos * 300f;
        _enemySpawnPosIndıcator.eulerAngles =
            new Vector3(0, 0, MouseCursorPos.GetAngleFromVector(directionToNextWaveSpawnPos));
        float distanceToNextSpawnPosition =
            Vector3.Distance(enemyWaveManager.GetSpawnPosition(), mainCamera.transform.position);
        _enemySpawnPosIndıcator.gameObject.SetActive(distanceToNextSpawnPosition>mainCamera.orthographicSize*1.5f);
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
