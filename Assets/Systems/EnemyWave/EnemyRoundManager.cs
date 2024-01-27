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

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        EnemyWaveManager.OnNextWave += OnWavesNext;
    }

    private void OnDisable()
    {
        EnemyWaveManager.OnNextWave -= OnWavesNext;
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

    void Start()
    {
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1f);
        mainDoorWave.enabled = true;
    }
}
