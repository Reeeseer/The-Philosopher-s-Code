using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleTurnManager : MonoBehaviour
{
    public static BattleTurnManager instance;

    public Action<PlayerAvatar> OnPlayerTurnStart;
    public Action<PlayerAvatar> OnPlayerTurnEnd;
    public Action<int> OnAPChange;

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

        if (SceneManager.GetSceneByName("Battle UI").isLoaded != true)
            SceneManager.LoadSceneAsync("Battle UI", LoadSceneMode.Additive);

    }

    private void OnEnable()
    {
        StartCoroutine(StartGame());
    }

    public IEnumerator StartGame()
    {
        while (!SceneManager.GetSceneByName("Battle UI").isLoaded) { yield return null; }
        PlayerTurn(GameManager.Instance.Player);
    }
    public void PlayerAttacks()
    {
        GameManager.Instance.Player.Attack();
    }

    public void EndPlayerTurn()
    {
        OnPlayerTurnEnd?.Invoke(GameManager.Instance.Player);
    }

    public void EnemyTurn()
    {
        if (GameManager.Instance.Enemy.CurrHealth > 0)
            GameManager.Instance.Enemy.Attack();
    }

    internal void PlayerTurn(PlayerAvatar player = null)
    {
        OnPlayerTurnStart?.Invoke(player);
        player.RestoreAP();
        GameManager.Instance.Targets.Clear();
    }


}
