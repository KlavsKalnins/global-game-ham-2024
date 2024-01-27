using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] Light[] spawnPointLights;
    [SerializeField] EnemyTest[] enemyPrefabs;

    public int waveIndex = 0;
    public Wave[] waves;

    void Start()
    {
        StartDemoWave();
    }

    void Update()
    {
        
        if (AllEnemiesKilled()) // change to static int
        { 
            // Start the next wave
            StartCoroutine(StartNextWave());
        }
    }

    public void StartDemoWave()
    {
        for (int i = 0; i < spawnPointLights.Length; i++)
        {
            spawnPointLights[i].enabled = true;
        }
    }

    bool AllEnemiesKilled()
    {
        // Check if all enemies in the current wave are killed
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        return enemies.Length == 0;
    }

    IEnumerator StartNextWave()
    {
        if (waveIndex < waves.Length)
        {
            Wave currentWave = waves[waveIndex];
            yield return new WaitForSeconds(currentWave.startDelay);

            for (int i = 0; i < currentWave.enemyCounts.Length; i++)
            {
                SpawnEnemies(currentWave.enemyPrefab, currentWave.enemyCounts[i]);
                yield return new WaitForSeconds(currentWave.timeBetweenEnemies);
            }

            waveIndex++;
        }
        else
        {
            Debug.Log("All waves completed!");
        }
    }

    void SpawnEnemies(EnemyTest enemyPrefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}



[System.Serializable]
public class Wave
{
    public float startDelay = 2.0f;
    public float timeBetweenEnemies = 1.0f;
    public EnemyTest enemyPrefab;
    public int[] enemyCounts;
}