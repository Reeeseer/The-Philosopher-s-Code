// This script was created to handle the behavior for the target selection part of the player's turn

using System.Collections.Generic;
using UnityEngine;

public class EndlessEnemyGenerator : MonoBehaviour
{
    public List<EnemyData> PossibleEnemies;
    public Transform EnemySpawnArea;

    private void Awake()
    {
        GameManager.instance.OnEnemyKilled += NewEnemy;
    }

    public void NewEnemy()
    {
        var enemyData = PossibleEnemies[Random.Range(0, PossibleEnemies.Count)];
        var createdEnemy = Instantiate(enemyData.EnemyPrefab, EnemySpawnArea);
        StartCoroutine(createdEnemy.SetStats(CalculateNewHealth(), CalculateNewDamge()));
        GameManager.instance.EnemySpawned(createdEnemy, enemyData);
    }

    public int CalculateNewHealth()
    {
        var x = EndlessTracker.instance.EnemiesKilledThisRun;
        return 100 + (100 * x);
    }

    public int CalculateNewDamge()
    {
        var x = EndlessTracker.instance.EnemiesKilledThisRun;
        return x / (x + 2);
    }
}