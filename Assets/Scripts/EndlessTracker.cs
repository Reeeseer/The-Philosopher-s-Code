// This script was created to track a run during endless mode

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessTracker : MonoBehaviour
{
    public int EnemiesKilledThisRun;

    void Awake()
    {
        StartCoroutine(Load());
    }

    IEnumerator Load()
    {
        while (GameManager.Instance == null || PlayerDataTracker.Instance == null) yield return null;
        
        GameManager.Instance.OnEnemyKilled += HandleEnemyKilled;
        PlayerDataTracker.Instance.UpdateScores(addRun: 1);
    }

    void HandleEnemyKilled()
    {
        EnemiesKilledThisRun += 1;
        PlayerDataTracker.Instance.UpdateScores(enemiesKilledThisRun: EnemiesKilledThisRun, addTotalEnemiesKilled: 1);
    }
}