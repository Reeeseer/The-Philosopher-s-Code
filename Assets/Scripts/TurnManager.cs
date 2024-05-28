using System;
using System.Collections;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance;

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
    }

    private void OnEnable()
    {
        StartCoroutine(StartGame());
    }
    IEnumerator StartGame()
    {
        while (OnPlayerTurnStart == null) { yield return null; }
        PlayerTurn(GameManager.instance.Player);
    }
    public void PlayerAttacks()
    {
        GameManager.instance.Player.Attack();
    }

    public void EndPlayerTurn()
    {
        OnPlayerTurnEnd?.Invoke(GameManager.instance.Player);
    }

    public void EnemyTurn()
    {
        if (GameManager.instance.Enemy.CurrHealth > 0)
            GameManager.instance.Enemy.Attack();
    }

    internal void PlayerTurn(PlayerAvatar player = null)
    {
        OnPlayerTurnStart?.Invoke(player);
        player.RestoreAP();
        GameManager.instance.targets.Clear();
    }


}
