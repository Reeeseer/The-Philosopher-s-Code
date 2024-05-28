using System;
using System.Collections;
using UnityEngine;

public class PotionManager : MonoBehaviour
{
    [SerializeField] Transform _potionSpawn;
    [SerializeField] Vector3 _enemyThrowVelocity;
    [SerializeField] Vector3 _selfThrowVelocity;
    [SerializeField] Vector3 _bothThrowVelocity;

    public static PotionManager instance;

    public Potion PotionPrefab;

    public Potion CurrentPotion;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddIngredientToPotion(PotionIngredient ingredient)
    {
        if(CurrentPotion == null)
        {
            CurrentPotion = Instantiate(PotionPrefab, _potionSpawn);
            CurrentPotion.Initialize();
        }

        CurrentPotion.AddPotionIngredient(ingredient);
    }

    public void AddCodeToPotion(CodeIngredient ingredient)
    {
        if (CurrentPotion == null)
        {
            CurrentPotion = new();
        }

        CurrentPotion.AddCodeIngredient(ingredient);
    }

    public void ThrowPotion()
    {
        CurrentPotion.transform.SetParent(null);
        StartCoroutine(CurrentPotion.Activate());

        if (GameManager.instance.targets.Count == 2)
            CurrentPotion.RB.velocity = _bothThrowVelocity;
        else if (GameManager.instance.targets.Contains(GameManager.instance.Player))
            CurrentPotion.RB.velocity = _selfThrowVelocity;
        else
            CurrentPotion.RB.velocity = _enemyThrowVelocity;

        CurrentPotion.RB.angularVelocity = new Vector3(
            UnityEngine.Random.Range(-20, 20),
            UnityEngine.Random.Range(-20, 20),
            UnityEngine.Random.Range(-20, 20));
    }

    [ContextMenu("Test Throw")]
    public void TestThrowPotion()
    {
        var potion = Instantiate(PotionPrefab, _potionSpawn.position, Quaternion.identity);
        potion.Initialize();
        potion.RB.velocity = _enemyThrowVelocity;
        potion.RB.angularVelocity = new Vector3(
            UnityEngine.Random.Range(-5, 5),
            UnityEngine.Random.Range(-5, 5),
            UnityEngine.Random.Range(-5, 5));
    }
}
