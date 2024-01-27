using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveSO", menuName = "HAM/WaveSO", order = 1)]
public class WaveSO : ScriptableObject
{
    public float startDelay = 2.0f;
    public float timeBetweenEnemies = 1.0f;
    public EnemyBehavior enemyPrefab;
    public int enemyCounts;
}
