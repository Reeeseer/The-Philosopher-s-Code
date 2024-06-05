using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Preset", menuName = "Create Inventory Preset", order = 1)]
public class IngredientPreset : ScriptableObject
{
    public List<InventorySlot> Ingredients;
}
