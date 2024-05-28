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

    public List<IAmTarget> targets = new();

    public Action GameRestart;
    public bool GameOver { get; internal set; }

    internal void EndRound()
    {
        _victoryScreen.Activate();
    }

    internal void LoseState()
    {
            _gameoverScreen.Activate();
            PlayerActionUi.DisableActionUi();
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
        PlayerActionUi = FindObjectOfType<PlayerActionUI>();
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
            IngredientsManager.Instance.ResetIngredients();
        }

    }

}
