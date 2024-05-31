using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] ContinuePrompt _victoryScreen;
    [SerializeField] ContinuePrompt _gameoverScreen;

    public static GameManager instance;

    public PlayerAvatar Player;
    public EnemyAvatar Enemy;
    public PlayerActionUI PlayerActionUi;
    public EnemyData EnemyData;

    public List<Fighter> targets = new();

    public Action GameRestart;
    public Action<bool> OnGameOver;
    public bool GameOver { get; internal set; }

    internal void Victory()
    {
        OnGameOver.Invoke(true);
    }

    internal void LoseState()
    {
        OnGameOver.Invoke(false);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Player = FindObjectOfType<PlayerAvatar>();
        Enemy = FindObjectOfType<EnemyAvatar>();
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        Debug.Log(SceneManager.GetActiveScene().name);
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
            InventoryManager.Instance.ResetIngredients();
        }

    }

}
