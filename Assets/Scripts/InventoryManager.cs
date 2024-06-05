using System;
using System.Collections.Generic;
using System.Linq;
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
        IngredientsList.Clear();
        foreach (var i in preset.Ingredients)
        {
            IngredientsList.Add(new InventorySlot(i));
        }
    }

    internal void ResetIngredients(object inventoryPreset)
    {
        throw new NotImplementedException();
    }
}
