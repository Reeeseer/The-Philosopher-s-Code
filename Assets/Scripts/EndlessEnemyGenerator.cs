// This script was created to handle the behavior for the target selection part of the player's turn

using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EndlessEnemyGenerator : MonoBehaviour
{
    public List<EnemyData> PossibleEnemies;
    public Transform EnemySpawnArea;
    public int CurrEnemyCount { private get; set; }

    private void Awake()
    {
        GameManager.Instance.OnEnemyKilled += NewEnemy;
    }

    void NewEnemy()
    {
        var enemyData = PossibleEnemies[Random.Range(0, PossibleEnemies.Count)];
        var createdEnemy = Instantiate(enemyData.EnemyPrefab, EnemySpawnArea);
        StartCoroutine(createdEnemy.SetStats(CalculateNewHealth(), CalculateNewDamage()));
        GameManager.Instance.EnemySpawned(createdEnemy, enemyData);
    }

    int CalculateNewHealth()
    {
        var x = CurrEnemyCount;
        return 100 + (100 * x);
    }

    int CalculateNewDamage()
    {
        float x = CurrEnemyCount;
        var damage = (int)(x / (x + 2)) * 49;
        if(damage < 5) { damage = 5; }
      
        return damage;
    }
}