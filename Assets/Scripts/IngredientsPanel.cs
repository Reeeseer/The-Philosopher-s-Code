using FMODUnity;
using System;
using System.Collections.Generic;
using UnityEngine;


public class IngredientsPanel : MonoBehaviour
{
    [SerializeField] IngredientCard _prefab;
    [SerializeField] IngredientCardData _data;
    [SerializeField] Transform _selectionParent;
    [SerializeField] Transform _throwButton;
    [SerializeField] Transform _addIngredientButton;
    [SerializeField] Transform _codeButton;

    InventorySlot _selectedSlot;
    List<IngredientCard> _cards = new();

    public Action<string> OnFailedAttempt;
    StudioEventEmitter _emitter;

    private void OnEnable()
    {
        _emitter = GetComponent<StudioEventEmitter>();
    }

    public void ListOutIngredients(IngredientDataOptions.IngredientType type)
    {
        foreach (var c in _cards)
        {
            Destroy(c.gameObject);
        }

        _cards.Clear();

        if (type == IngredientDataOptions.IngredientType.Potion)
        {
            _throwButton.gameObject.SetActive(false);
            _addIngredientButton.gameObject.SetActive(true);
            _codeButton.gameObject.SetActive(true);
        }
        else
        {
            _throwButton.gameObject.SetActive(true);
            _addIngredientButton.gameObject.SetActive(false);
            _codeButton.gameObject.SetActive(false);
        }

        foreach (var i in InventoryManager.Instance.IngredientsList)
        {
            if (i.Ingredient.Type != type)
                continue; 

            var card = Instantiate(_prefab, _selectionParent);
            card.Initialize(_data, i);
            _cards.Add(card);
        }
    }

    public void SetSelectedSlot(InventorySlot slot)
    {
        _selectedSlot = slot;
    }

    public void AddIngredientToPotion()
    {
        if (_selectedSlot == null)
        {
            ErrorMessaging.instance.ShowError("No Ingreident Selected");
            Debug.Log("No Ingredient Selected");
            return;
        }

        if (_selectedSlot.Amount == 0)
        {
            ErrorMessaging.instance.ShowError("No Ingreidents Left");
            Debug.Log("No Ingreidents Left");
            return;
        }

        if(_selectedSlot.Ingredient.APCost > GameManager.instance.Player.CurrentAP)
        {
            ErrorMessaging.instance.ShowError("Not enough AP");
            return;
        }

        _emitter.Play();

        if (_selectedSlot.Ingredient.Type == IngredientDataOptions.IngredientType.Code)
        {
            PotionManager.instance.CurrentPotion.CodeStrength = GameManager.instance.Player.CurrentAP;
        }

        PotionManager.instance.AddIngredientToPotion(_selectedSlot.Ingredient);
        GameManager.instance.Player.RemoveAP(_selectedSlot.Ingredient.APCost);
        _selectedSlot.AddAmount(-1);
        ListOutIngredients(_selectedSlot.Ingredient.Type);
        

        _selectedSlot = null;
    }
}
