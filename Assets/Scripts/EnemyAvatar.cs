using FMODUnity;
using System;
using System.Collections;
using UnityEngine;

public class EnemyAvatar : Fighter
{
    [SerializeField] int _damage;
    StudioEventEmitter _emitter;

    protected override void OnEnable()
    {
        base.OnEnable();
        _emitter = GetComponent<StudioEventEmitter>();
    }

    public void Attack()
    {
        _animator.SetTrigger("Attack");
    }

    /// <summary>
    /// Called by enemy attack animation
    /// </summary>
    public void EndOfAttack()
    {
        BattleTurnManager.instance.PlayerTurn(GameManager.instance.Player);
    }

    /// <summary>
    /// called by enemy attack animation
    /// </summary>
    public void AttackConnect()
    {
        GameManager.instance.Player.TakeAttack(_damage);
        _emitter.Play();

    }

    protected override IEnumerator Die()
    {
        yield return base.Die();
        GameManager.instance.EnemyKilled();
        Destroy(gameObject);
    }

    internal IEnumerator SetStats(int hp, int damage)
    {
        MaxHealth = hp;
        CurrHealth = MaxHealth;
        _damage = damage;
        while (OnHealthChanged == null) yield return null;

        OnHealthChanged.Invoke(CurrHealth, MaxHealth);
    }
}
