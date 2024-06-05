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
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddIngredientToPotion(Ingredient ingredient)
    {
        if (CurrentPotion == null)
        {
            CurrentPotion = Instantiate(PotionPrefab, _potionSpawn);
            CurrentPotion.Initialize();
        }

        CurrentPotion.AddIngredient(ingredient);
    }

    public void ThrowPotion()
    {
        CurrentPotion.transform.SetParent(null);
        StartCoroutine(CurrentPotion.Activate());

        if (GameManager.Instance.Targets.Count == 2)
            CurrentPotion.RB.velocity = _bothThrowVelocity;
        else if (GameManager.Instance.Targets.Contains(GameManager.Instance.Player))
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
