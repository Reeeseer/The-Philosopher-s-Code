using System;
using UnityEngine;

[Serializable, CreateAssetMenu(fileName = "Potion Ingredient", menuName = "New Potion Ingredient", order = 1)]
public class PotionIngredient : Ingredient
{
    public string Effect;
    public int EffectStrength;
    public int APCost;
}
