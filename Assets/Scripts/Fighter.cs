using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fighter : MonoBehaviour, IAmTarget
{
    public int MaxHealth;
    public int CurrHealth;

    /// <summary>
    /// Updates health values, args are Current Health then Max Health
    /// </summary>
    public Action<int, int> OnHealthChanged;

    protected Animator _animator;

    protected virtual void OnEnable()
    {
        _animator = GetComponent<Animator>();
    }

    public IEnumerator ApplyEffects(Potion potion)
    {
        int interations = 1;
        int multiples = 1;
        Ingredient codeIngredient = null;
        foreach (var i in potion.IngredientsInPotion)
        {
            if (i.Type == IngredientDataOptions.IngredientType.Code)
                codeIngredient = i;
        }
        if (codeIngredient != null)
        {
            switch (codeIngredient.Effect)
            {
                case IngredientDataOptions.EffectType.ForLoop:
                    interations += potion.CodeStrength;
                    break;

                case IngredientDataOptions.EffectType.Multiply:
                    multiples += potion.CodeStrength;
                    break;

                case IngredientDataOptions.EffectType.Return:
                    ReturnToMenu();
                    break;
            }
        }

        foreach (var i in potion.IngredientsInPotion)
        {
            if (i.Type != IngredientDataOptions.IngredientType.Potion)
                continue;

            for (int j = 0; j < interations; j++)
            {
                switch (i.Effect)
                {
                    case IngredientDataOptions.EffectType.Damage:
                        ApplyDamage(i.EffectStrength * multiples);
                        potion.TriggerParticle("Damage");
                        potion.FmodEmitter.EventInstance.setParameterByNameWithLabel("EffectType", "Damage");
                        potion.FmodEmitter.Play();
                        yield return new WaitForSeconds(0.2f);
                        break;
                    case IngredientDataOptions.EffectType.Healing:
                        ApplyHealing(i.EffectStrength * multiples);
                        potion.TriggerParticle("Heal");
                        potion.FmodEmitter.EventInstance.setParameterByNameWithLabel("EffectType", "Heal");
                        potion.FmodEmitter.Play();
                        yield return new WaitForSeconds(0.2f);
                        break;
                }
            }

        }
    }

    private void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    protected void ApplyHealing(int healing)
    {
        if (CurrHealth > 0)
            CurrHealth += healing;

        OnHealthChanged?.Invoke(CurrHealth, MaxHealth);
    }

    protected virtual void ApplyDamage(int damage)
    {
        CurrHealth -= damage;
        OnHealthChanged?.Invoke(CurrHealth, MaxHealth);

        if (CurrHealth <= 0 && !GameManager.instance.GameOver)
        {
            GameManager.instance.GameOver = true;
            var player = GetComponent<PlayerAvatar>();
            if (player != null) { StartCoroutine(player.Die()); }
            var enemy = GetComponent<EnemyAvatar>();
            if (enemy != null && GameManager.instance.Player.CurrHealth > 0) { StartCoroutine(enemy.Die()); }
        }
    }

    protected virtual IEnumerator Die()
    {
        _animator.SetTrigger("Die");
        yield return new WaitForSeconds(2);
        foreach (var model in GetComponentsInChildren<MeshRenderer>())
        {
            model.enabled = false;
        }
        yield return new WaitForSeconds(1);

    }
}
