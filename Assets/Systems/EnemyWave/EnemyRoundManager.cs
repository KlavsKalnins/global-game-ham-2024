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

    private void Awake()
    {
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
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(0.1f);
        mainDoorWave.enabled = true;
    }
}
