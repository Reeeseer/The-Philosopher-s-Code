using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public List<InventorySlot> IngredientsList;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void ResetIngredients(IngredientPreset preset)
    {
        //TODO: set up a scriptable object for inventory resetting
        IngredientsList = preset.Ingredients;
    }
}

public class IngredientPreset : ScriptableObject
{
    public List<InventorySlot> Ingredients;
}
