using System;
using System.Collections;
using UnityEngine;

public class PlayerAvatar : Fighter
{
    public int CurrentAP;
    public int MaxAp;

    public Action<int> OnAPChange;
    public Action<int> OnHPChange;

    protected override void OnEnable()
    {
        StartCoroutine(Load());
    }

    public IEnumerator Load()
    {
        while (BattleTurnManager.instance == null)
        {
            yield return null;
        }

        base.OnEnable();
        BattleTurnManager.instance.OnPlayerTurnStart += TurnStart;
    }
    private void TurnStart(PlayerAvatar obj)
    {
        CurrentAP = MaxAp;
    }

    internal void TakeAttack(int damage)
    {
        ApplyDamage(damage);
    }

    protected override IEnumerator Die()
    {
        yield return base.Die();
        GameManager.instance.LoseState();
    }

    internal void RestoreAP()
    {
        CurrentAP = MaxAp;
        OnAPChange?.Invoke(CurrentAP);
    }

    internal void RemoveAP(int ap)
    {
        CurrentAP -= ap;
        OnAPChange?.Invoke(CurrentAP);
    }

    private void OnDisable()
    {
        BattleTurnManager.instance.OnPlayerTurnStart -= TurnStart;
    }

    internal void Attack()
    {
        _animator.SetTrigger("Throw");
    }

    public void Throw()
    {
        PotionManager.instance.ThrowPotion();
    }
}
