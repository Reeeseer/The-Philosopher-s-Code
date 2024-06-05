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

    public List<Ingredient> IngredientsInPotion;
    public int CodeStrength;
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

    public void AddIngredient(Ingredient ingredient)
    {
        if (ingredient.Type == IngredientDataOptions.IngredientType.Code)
            ingredient.SetStrength();

        IngredientsInPotion.Add(ingredient);
    }

    private IEnumerator ApplyEffectsToTargets()
    {

        foreach (var t in GameManager.Instance.Targets)
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
        yield return new WaitForSeconds(1.5f);
        BattleTurnManager.instance.EnemyTurn();
        Destroy(gameObject);
    }

    internal IEnumerator Activate()
    {
        RB.useGravity = true;
        yield return new WaitForSeconds(.5f);
        _collider.enabled = true;
    }
}
