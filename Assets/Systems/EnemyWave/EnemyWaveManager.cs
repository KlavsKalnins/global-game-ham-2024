using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] Light[] spawnPointLights;
    public int waveIndex = 0;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void StartDemoWave()
    {
        for (int i = 0; i < spawnPointLights.Length; i++)
        {
            spawnPointLights[i].enabled = true;
        }
    }

    IEnumerator CheckProgress()
    {
        yield return new WaitForSeconds(1.0f);
    }
}
