using System;
using System.Collections;

public class PlayerAvatar : Fighter
{
    public int CurrentAP;
    public int MaxAp;

    public Action<int> OnApChange;
    public Action<int> OnHpChange;

    protected override void Awake()
    {
        StartCoroutine(Load());
    }

    IEnumerator Load()
    {
        while (BattleTurnManager.instance == null || GameManager.Instance == null)
        {
            yield return null;
        }

        base.Awake();
        BattleTurnManager.instance.OnPlayerTurnStart += TurnStart;
        GameManager.Instance.OnEnemySpawn += HandleEnemySpawn;
    }

    private void HandleEnemySpawn()
    {
        ApplyHealing(MaxHealth);
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
        GameManager.Instance.LoseState();
    }

    internal void RestoreAP()
    {
        CurrentAP = MaxAp;
        OnApChange?.Invoke(CurrentAP);
    }

    internal void RemoveAP(int ap)
    {
        CurrentAP -= ap;
        OnApChange?.Invoke(CurrentAP);
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
