using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "New IngredientCardData", menuName = "Ingredient Card Data", order = 1)]
public class IngredientCardData : ScriptableObject
{
    public Vector2 Size;
    public TMP_FontAsset Font;
}
