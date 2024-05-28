using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PotionIngredientCard : MonoBehaviour
{
    [SerializeField] TMP_Text _name;
    [SerializeField] TMP_Text _amount;
    [SerializeField] TMP_Text _apCost;

    PotionIngredientDescription _ingredientDescription;
    PotionIngredientsPanel _ingrdientsPanel;
    PotionIngredient _ingredient;
    Button _button;

    public void Intialize(IngredientCardData data, PotionIngredient ingredient)
    {
        var rec = GetComponent<RectTransform>();
        rec.sizeDelta = data.Size;

        _ingrdientsPanel = GetComponentInParent<PotionIngredientsPanel>();
        _ingredientDescription = FindObjectOfType<PotionIngredientDescription>();
        _button = GetComponent<Button>();

        _name.font = data.Font;
        _amount.font = data.Font;
        _apCost.font = data.Font;
        _ingredient = ingredient;
        _name.text = ingredient.Name;
        _amount.text = ingredient.Amount.ToString();
        _apCost.text = ingredient.APCost.ToString();

        

        if(ingredient.Amount == 0)
        {
            _button.interactable = false;
        }
    }

    public void OnClick()
    {
        _ingrdientsPanel.SetSelectedIngredient(_ingredient);
        _ingredientDescription.SetText(_ingredient.Description);
    }
}
