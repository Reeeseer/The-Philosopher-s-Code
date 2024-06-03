using System;
using System.Collections.Generic;
using UnityEngine;

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

    public void SetInventory(IngredientPreset preset)
    {
        //TODO: set up a scriptable object for inventory resetting
        IngredientsList = preset.Ingredients;
    }

    internal void ResetIngredients(object inventoryPreset)
    {
        throw new NotImplementedException();
    }
}
