using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyWaveManager : MonoBehaviour
{
    public event EventHandler OnWaveNumberChanged;
    private enum State
    {
        WaitingToSpawnNextWave,
        SpawningWave
    }
    private float nextWaveSpawnTimer;
    private float nextEnemySpawnTimer;
    private int remainingEnemySpawnAmount;
    private Vector3 spawnPos;
    private State state;
    private int waveNumber;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private Transform nextWaveIndicator;
    
    
    private void Start()
    {
        state = State.WaitingToSpawnNextWave;
        nextWaveSpawnTimer = 3f;
        spawnPos = spawnPoints[Random.Range(0, spawnPoints.Count)].transform.position;
        nextWaveIndicator.position = spawnPos;

    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToSpawnNextWave:
                nextWaveSpawnTimer -= Time.deltaTime;
                if (nextWaveSpawnTimer < 0f)
                {
                    SpawnWave();
                }
                break;
            case State.SpawningWave:
                if (remainingEnemySpawnAmount > 0f)
                {
                    nextEnemySpawnTimer -= Time.deltaTime;
                    if (nextEnemySpawnTimer < 0f)
                    {
                        nextEnemySpawnTimer = UnityEngine.Random.Range(0f, 0.2f);
                        Enemy.Create(spawnPos + MouseCursorPos.RandomizeSpawnDirection() * Random.Range(0, 10f));
                        remainingEnemySpawnAmount--;
                        if (remainingEnemySpawnAmount <= 0f)
                        {
                            state = State.WaitingToSpawnNextWave;
                            spawnPos = spawnPoints[Random.Range(0, spawnPoints.Count)].transform.position;
                            nextWaveIndicator.position = spawnPos;
                            nextWaveSpawnTimer = 15f;
                        }
                    }
                }
                break;
        }
        
    }

    private void SpawnWave()
    {
        
        remainingEnemySpawnAmount = 5+2*waveNumber;
        state = State.SpawningWave;
        waveNumber++;
        OnWaveNumberChanged?.Invoke(this,EventArgs.Empty);
    }

    public int GetWaveNumber()
    {
        return waveNumber;
    }

    public float GetNextWaveSpawnTimer()
    {
        return nextWaveSpawnTimer;
    }

    public Vector3 GetSpawnPosition()
    {
        return spawnPos;
    }
}
