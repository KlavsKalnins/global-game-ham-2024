using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

[System.Serializable] 
public enum WaveDoorType
{
    Main,
    Left,
    Right,
}

public class EnemyWaveManager : MonoBehaviour
{
    [SerializeField] WaveDoorType waveDoorType;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] EnemyBehavior[] enemyPrefabs;

    public int waveIndex = 0;
    public static Action<int, WaveDoorType> OnNextWave;
    public WaveSO[] waves;

    public bool isAllWaveCompleted;

    public bool waveScript = true;

    private void OnEnable()
    {
        EnemyTest.NoEnemiesAreAlive += CheckIfDemoWaveCompleted;
        if (waveScript)
            StartDemoWave();
    }

    void CheckIfDemoWaveCompleted()
    {
        if (isAllWaveCompleted)
        {
            Debug.LogWarning("GAROJEM SLAVA");
        }
    }

    public void StartDemoWave()
    {
        Debug.Log($"STARTING WAVE {gameObject.name}");
        StartCoroutine(StartNextWave());
    }


    IEnumerator StartNextWave()
    {
        while (waveIndex < waves.Length)
        {
            WaveSO currentWave = waves[waveIndex];
            yield return new WaitForSeconds(currentWave.startDelay);

            // SpawnEnemies(currentWave.enemyPrefab);

            for(int i = 0; i < currentWave.enemyCounts; i++)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Instantiate(currentWave.enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                // TODO: if currentWave.enemyPrefab == null then  rand enemyPrefabs
                yield return new WaitForSeconds(1.3f);
            }

            yield return new WaitForSeconds(currentWave.timeBetweenEnemies);

            waveIndex++;
            OnNextWave?.Invoke(waveIndex, waveDoorType);
            // Debug.Log($"WW: NEXT WAVE {waveIndex}");
        }

        // Debug.Log("All waves completed!");
        isAllWaveCompleted = true;
        EnemyRoundManager.Instance.CheckIfCanSpawnBoss();
    }

    public void SpawnEnemies(EnemyTest enemyPrefab)
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
