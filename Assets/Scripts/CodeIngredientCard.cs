using TMPro;
using UnityEngine;

public class CodeIngredientCard : MonoBehaviour
{
    [SerializeField] TMP_Text _name;
    [SerializeField] TMP_Text _amount;

    CodeIngredientDescription _ingredientDescription;
    CodeIngredientsPanel _ingrdientsPanel;
    CodeIngredient _ingredient;

    public void Intialize(IngredientCardData data, CodeIngredient ingredient)
    {
        var rec = GetComponent<RectTransform>();
        rec.sizeDelta = data.Size;

        _ingrdientsPanel = GetComponentInParent<CodeIngredientsPanel>();
        _ingredientDescription = FindObjectOfType<CodeIngredientDescription>();

        _name.font = data.Font;
        _amount.font = data.Font;
        _ingredient = ingredient;
        _name.text = ingredient.Name;
        _amount.text = ingredient.Amount.ToString();
    }

    public void OnClick()
    {
        _ingrdientsPanel.SetSelectedIngredient(_ingredient);
        _ingredientDescription.SetText(_ingredient.Description);
    }
}
