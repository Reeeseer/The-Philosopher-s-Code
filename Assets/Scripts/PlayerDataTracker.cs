using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataTracker : MonoBehaviour
{
    int _totalEndlessRuns;
    int _totalEndlessEnemiesKilled;
    int _highestEndlessKills;

    public static PlayerDataTracker Instance;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != null) Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
        LoadScores();
    }

    void SaveScores()
    {
        PlayerPrefs.SetInt("TotalEndlessRuns", _totalEndlessRuns);
        PlayerPrefs.SetInt("TotalEndlessEnemiesKilled", _totalEndlessEnemiesKilled);
        PlayerPrefs.SetInt("HighestEndlessKills", _highestEndlessKills);
    }
    
    void LoadScores()
    {
        _totalEndlessRuns = PlayerPrefs.GetInt("TotalEndlessRuns");
        _totalEndlessEnemiesKilled = PlayerPrefs.GetInt("TotalEndlessEnemiesKilled");
        _highestEndlessKills = PlayerPrefs.GetInt("HighestEndlessKills");
    }

    [ContextMenu("Test Scores")]
    void TestScores()
    {
        var message = 
                $"TotalEndlessRuns = {PlayerPrefs.GetInt("TotalEndlessRuns")} \n" +
                $"TotalEndlessEnemiesKilled = {PlayerPrefs.GetInt("TotalEndlessEnemiesKilled")}\n " +
                $"HighestEndlessKills = {PlayerPrefs.GetInt("HighestEndlessKills")}";
        
        Debug.Log(message);
    }

    /// <summary>
    /// Used to add to the scores of the player
    /// </summary>
    /// <param name="enemiesKilledThisRun">uses this score to tell if the high score should be changed</param>
    /// <param name="addTotalEnemiesKilled">this is added to the players total enemy killed</param>
    /// <param name="addRun">This is used to add a run to the players total runs score</param>
    public void UpdateScores(int enemiesKilledThisRun = 0, int addTotalEnemiesKilled = 0, int addRun = 0)
    {
        if (_highestEndlessKills < enemiesKilledThisRun) _highestEndlessKills = enemiesKilledThisRun;
        _totalEndlessEnemiesKilled += addTotalEnemiesKilled;
        _totalEndlessRuns += addRun;
        SaveScores();
    }
}



