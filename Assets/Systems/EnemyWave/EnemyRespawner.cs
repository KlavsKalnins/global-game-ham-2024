using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawner : MonoBehaviour
{
    [SerializeField] EnemyTest prefab;
    [SerializeField] EnemyTest spawnedEnemy;

    private void Start()
    {
        InvokeRepeating("ResetCube", 1, 3);
    }

    void ResetCube()
    {
        if (spawnedEnemy != null) return;

        var spawned = Instantiate(prefab,transform.position, Quaternion.identity);
        spawnedEnemy = spawned;
    }
}
