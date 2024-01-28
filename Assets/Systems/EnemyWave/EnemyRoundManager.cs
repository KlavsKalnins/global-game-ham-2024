using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoundManager : MonoBehaviour
{
    public static EnemyRoundManager Instance;

    [SerializeField] int leftDoorOpensAfterMainDoorWaveCount = 3;
    [SerializeField] int rightDoorOpensAfterMainDoorWaveCount = 5;

    [SerializeField] EnemyWaveManager mainDoorWave; // + 3 + 5 wave + rightDoorWave.count
    [SerializeField] EnemyWaveManager leftDoorWave; // + 5  + rightDoorWave.count
    [SerializeField] EnemyWaveManager rightDoorWave;

    [SerializeField] GameObject[] startingEnemies;

    [SerializeField] EnemyBehavior[] enemyTypePrefabs;
    [SerializeField] EnemyWaveManager[] spawnPoints;

    public bool isAdaptive;

    public float initialSpawnRate = 2.0f;
    public float spawnRateIncrease = 0.1f;
    public float maxSpawnRate = 0.5f;

    public float initialSecondEnemyChance = 0.2f;
    public float secondEnemyChanceIncrease = 0.05f;
    public float maxSecondEnemyChance = 0.5f;

    private float currentSpawnRate;
    private float currentSecondEnemyChance;

    private void Awake()
    {
        if (isAdaptive)
        {
            mainDoorWave.waveScript = false;
            leftDoorWave.waveScript = false;
            rightDoorWave.waveScript = false;
        }
        Instance = this;
        foreach (GameObject go in startingEnemies)
        {
            go.SetActive(false);
        }
    }

    private void OnEnable()
    {
        EnemyWaveManager.OnNextWave += OnWavesNext;
        HudManager.OnGameStart += DoStart;
    }

    private void OnDisable()
    {
        EnemyWaveManager.OnNextWave -= OnWavesNext;
        HudManager.OnGameStart -= DoStart;
    }

    void OnWavesNext(int waveIndex, WaveDoorType doorType)
    {
        if (doorType == WaveDoorType.Main && waveIndex == leftDoorOpensAfterMainDoorWaveCount)
        {
            leftDoorWave.enabled = true;
        }
        if (doorType == WaveDoorType.Main && waveIndex == rightDoorOpensAfterMainDoorWaveCount)
        {
            rightDoorWave.enabled = true;
        }
    }

    public void CheckIfCanSpawnBoss()
    {
        // this is true if all waves is done
        if (mainDoorWave.isAllWaveCompleted
            && leftDoorWave.isAllWaveCompleted
            && rightDoorWave.isAllWaveCompleted)
        {
            Debug.Log("BRING OUT BOSS DIMITRI");
            Debug.Log(" camera shake and spawn him");
        }
    }

    void DoStart()
    {
        Debug.Log("KKKKK:: start");
        foreach (GameObject go in startingEnemies)
        {
            go.SetActive(true);
        }
        if (isAdaptive)
        {
            InvokeRepeating("AdaptimeGameplay", 0f, currentSpawnRate);
        }
        else
        {
            StartCoroutine(StartGame());
        }

    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(0.1f);
        mainDoorWave.enabled = true;  
    }

    private void AdaptimeGameplay()
    {
        // Check if a second enemy should be spawned
        bool spawnSecondEnemy = Random.value < currentSecondEnemyChance;

        // Randomly select an enemy type
        EnemyBehavior enemyPrefab = enemyTypePrefabs[Random.Range(0, enemyTypePrefabs.Length)];
        if (spawnSecondEnemy)
        {
            enemyPrefab = enemyTypePrefabs[1];
        } else
        {
            enemyPrefab = enemyTypePrefabs[0];
        }

        // Randomly select a spawn point array
        EnemyWaveManager selectedSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Spawn the selected enemy at the chosen spawn point
        Instantiate(enemyPrefab, selectedSpawnPoint.gameObject.transform.position, selectedSpawnPoint.gameObject.transform.rotation);


        // Increase difficulty over time
        if (currentSpawnRate > maxSpawnRate)
        {
            currentSpawnRate -= spawnRateIncrease;
        }

        if (currentSecondEnemyChance < maxSecondEnemyChance)
        {
            currentSecondEnemyChance += secondEnemyChanceIncrease;
        }
    }

    private EnemyBehavior GetDifferentEnemyType(EnemyBehavior original)
    {
        // Ensure the second enemy type is different from the original
        EnemyBehavior newEnemyType = original;

        while (newEnemyType == original)
        {
            newEnemyType = enemyTypePrefabs[Random.Range(0, enemyTypePrefabs.Length)];
        }

        return newEnemyType;
    }
}
