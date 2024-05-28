using System;
using UnityEngine;

[Serializable, CreateAssetMenu(fileName = "Code Ingredient", menuName = "New Code Ingredient", order = 1)]
public class CodeIngredient : Ingredient
{
    public string Effect;
    public int EffectStrength;

    public void SetStrength()
    {
        EffectStrength = GameManager.instance.Player.CurrentAP;
        GameManager.instance.Player.RemoveAP(EffectStrength);
    }
}
