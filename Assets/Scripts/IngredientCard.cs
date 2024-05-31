//This class will be formatting the button cards that show up on UI elements during ingredient selection parts of the player's turn

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngredientCard : MonoBehaviour
{
    [SerializeField] TMP_Text _name;
    [SerializeField] TMP_Text _amount;
    [SerializeField] TMP_Text _apCost;

    IngredientDescription _ingredientDescription;
    IngredientsPanel _ingrdientsPanel;
    InventorySlot _slot;
    Button _button;

    /// <summary>
    /// Intializes the data shown on the card
    /// </summary>
    /// <param name="data">determines the visuals of the card</param>
    /// <param name="slot">the slot the card will reference</param>
    public void Initialize(IngredientCardData data, InventorySlot slot)
    {
        var rec = GetComponent<RectTransform>();
        rec.sizeDelta = data.Size;

        _ingrdientsPanel = GetComponentInParent<IngredientsPanel>();
        _ingredientDescription = FindObjectOfType<IngredientDescription>();
        _button = GetComponent<Button>();

        _name.font = data.Font;
        _amount.font = data.Font;
        _apCost.font = data.Font;
        _slot = slot;
        _name.text = slot.Ingredient.Name;
        _amount.text = slot.Amount.ToString();

        // Sets the numbers for an ingredients cost and strength if needed
        slot.Ingredient.SetStrength();

        _apCost.text = slot.Ingredient.APCost.ToString();

        if(slot.Ingredient.Type == IngredientDataOptions.IngredientType.Code)
        {
            _apCost.text = GameManager.instance.Player.CurrentAP.ToString();
        }

        if(slot.Amount == 0)
        {
            _button.interactable = false;
        }
    }

    public void OnClick()
    {
        _ingrdientsPanel.SetSelectedSlot(_slot);
        _ingredientDescription.SetText(_slot.Ingredient.Description);
    }
}

