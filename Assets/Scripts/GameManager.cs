//This script is in charge of managing the state of a combat sequence;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField] ContinuePrompt _victoryScreen;
    [SerializeField] ContinuePrompt _gameoverScreen;
    [SerializeField] IngredientPreset _inventoryPreset;
    [SerializeField] bool _isEndless;

    public static GameManager Instance;

    public PlayerAvatar Player;
    public EnemyAvatar Enemy;
    public PlayerActionUI PlayerActionUi;
    public EnemyData EnemyData;

    [FormerlySerializedAs("targets")] public List<Fighter> Targets = new();

    public Action GameRestart;
    public Action<bool> OnGameOver;
    public Action OnEnemyKilled;
    public Action OnEnemySpawn;

    /// <summary>
    /// used to update the UI when new enemy is spawned
    /// </summary>
    public Action<EnemyAvatar, EnemyData> OnEnemyChange;

    public bool GameOver { get; internal set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        Player = FindObjectOfType<PlayerAvatar>();
        Enemy = FindObjectOfType<EnemyAvatar>();
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        Debug.Log(SceneManager.GetActiveScene().name);
        SetInventory(_inventoryPreset);
        OnEnemySpawn += StartNextFight;
    }

    private void SetInventory(IngredientPreset inventoryPreset)
    {
        if (inventoryPreset == null)
            return;

        InventoryManager.Instance.SetInventory(inventoryPreset);
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        StartCoroutine(Reset());
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }

    private IEnumerator Reset()
    {
        yield return null;
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
        {
            InventoryManager.Instance.SetInventory(_inventoryPreset);
        }

    }
    internal void EnemyKilled()
    {
        OnEnemyKilled.Invoke();

        if (!_isEndless)
        {
            OnGameOver.Invoke(true);
        }

    }

    void StartNextFight()
    {
        SetInventory(_inventoryPreset);
        StartCoroutine(BattleTurnManager.instance.StartGame());
    }

    internal void LoseState()
    {
        OnGameOver.Invoke(false);
    }

    internal void EnemySpawned(EnemyAvatar enemy, EnemyData data)
    {
        OnEnemySpawn.Invoke();
        Enemy = enemy;
        OnEnemyChange.Invoke(enemy, data);
    }

    internal void SetGameOver()
    {
        if (!_isEndless) GameOver = true;
    }
}

