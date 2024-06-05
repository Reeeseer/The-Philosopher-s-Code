//This script is used for holding data about the ingredients the player has access to

using UnityEditor;
using UnityEngine;
using static IngredientDataOptions;

[CreateAssetMenu(fileName = "new Ingredient", menuName = "Create Ingredient", order = 1)]
public class Ingredient : ScriptableObject
{
    public string Name = "new Ingredient";
    [TextArea] public string Description = "A new ingredient with placeholder text";

    /// <summary>
    /// How many action points the ingredient costs to add to a potion.
    /// use whole positive numbers or "var" for costs that are not constant
    /// </summary>
    public int APCost;

    /// <summary>
    /// the effect the ingredient has on the target of the potion
    /// </summary>
    public EffectType Effect;

    /// <summary>
    /// tells if this is a potion or code ingredient
    /// </summary>
    public IngredientType Type;

    /// <summary>
    /// the strength of the ingredient's effect, this is a constant for potion ingredients. For code ingredients this is determined by the left AP of the player.
    /// </summary>
    public int EffectStrength;

    /// <summary>
    /// use for code ingredients to set their strength, will do nothing on potion ingredients
    /// </summary>
    public void SetStrength()
    {
        if (Type != IngredientType.Code)
            return;

        EffectStrength = GameManager.Instance.Player.CurrentAP;
        APCost = EffectStrength;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        Name = name;
    }
#endif
}
