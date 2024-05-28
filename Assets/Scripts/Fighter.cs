using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fighter : MonoBehaviour, IAmTarget
{
    public int MaxHealth;
    public int CurrHealth;
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
        switch (potion.CodeEffect)
        {
            case "ForLoop":
                interations += potion.CodeEffectStrength;
                break;

            case "Multiply":
                multiples += potion.CodeEffectStrength;
                break;

            case "Return":
                ReturnToMenu();
                break;


        }

        for (int i = 0; i < interations; i++)
        {
            var effectList = potion.PotionEffects;
            var strengthList = potion.PotionEffectStrengths;
            int index = 0;
            foreach (var e in effectList)
            {

                switch (e)
                {
                    case "Damage":
                        ApplyDamage(strengthList[index] * multiples);
                        potion.TriggerParticle("Damage");
                        potion.FmodEmitter.EventInstance.setParameterByNameWithLabel("EffectType", "Damage");
                        potion.FmodEmitter.Play();
                        yield return new WaitForSeconds(0.2f);
                        break;
                    case "Heal":
                        ApplyHealing(strengthList[index] * multiples);
                        potion.TriggerParticle("Heal");
                        potion.FmodEmitter.EventInstance.setParameterByNameWithLabel("EffectType", "Heal");
                        potion.FmodEmitter.Play();
                        yield return new WaitForSeconds(0.2f);
                        break;
                }
                index++;
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
        if(CurrHealth <= 0 && !GameManager.instance.GameOver)
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
            model.enabled= false;
        }
        yield return new WaitForSeconds(1);
        
    }
}
