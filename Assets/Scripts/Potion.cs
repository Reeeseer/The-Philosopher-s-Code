using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[Serializable]
public class Potion : MonoBehaviour
{
    [SerializeField] float _potionAnimationTime;

    public List<string> PotionEffects = new List<string>();
    public List<int> PotionEffectStrengths = new List<int>();
    public string CodeEffect;
    public int CodeEffectStrength;

    public Rigidbody RB { get; private set; }

    Collider _collider;

    public StudioEventEmitter FmodEmitter { get; private set; }

    VisualEffect _particles;

    public void Initialize()
    {
        RB = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _collider.enabled = false;
        _particles = GetComponent<VisualEffect>();
        FmodEmitter = GetComponent<StudioEventEmitter>();
    }

    internal void AddCodeIngredient(CodeIngredient ingredient)
    {
        ingredient.SetStrength();
        CodeEffect = ingredient.Effect;
        CodeEffectStrength = ingredient.EffectStrength;
    }

    internal void AddPotionIngredient(PotionIngredient ingredient)
    {
        PotionEffects.Add(ingredient.Effect);
        PotionEffectStrengths.Add(ingredient.EffectStrength);
    }

    private IEnumerator ApplyEffectsToTargets()
    {

        foreach (var t in GameManager.instance.targets)
        {
            StartCoroutine(t.ApplyEffects(this));
        }
        yield return null;
    }

    private void Disappear()
    {
        foreach (var model in GetComponentsInChildren<MeshRenderer>())
        {
            model.enabled = false;
        }
        RB.constraints = RigidbodyConstraints.FreezeAll;
    }

    internal void TriggerParticle(string particleName)
    {
        _particles.SendEvent(particleName);
    }

    private void OnCollisionEnter(Collision collision)
    {
            StartCoroutine(Hit());
    }

    private IEnumerator Hit()
    {
        Disappear();
        yield return StartCoroutine(ApplyEffectsToTargets());
        yield return new WaitForSeconds(3);
        TurnManager.instance.EnemyTurn();
        Destroy(gameObject);
    }

    internal IEnumerator Activate()
    {
        RB.useGravity = true;
        yield return new WaitForSeconds(.5f);
        _collider.enabled = true;
    }
}
