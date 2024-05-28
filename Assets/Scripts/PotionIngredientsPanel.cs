using FMODUnity;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static IngredientsManager;

public class PotionIngredientsPanel : MonoBehaviour
{
    [SerializeField] PotionIngredientCard _prefab;
    [SerializeField] IngredientCardData _data;
    [SerializeField] Transform _selectionParent;

    PotionIngredient _selectedIngredient;
    List<PotionIngredientCard> _cards = new();

    public Action<string> OnFailedAttempt;
    private StudioEventEmitter _emitter;

    private void OnEnable()
    {
        _emitter = GetComponent<StudioEventEmitter>();
    }
    public void ListOutPotionIngredients()
    {
        foreach (var c in _cards)
        {
            Destroy(c.gameObject);
        }

        _cards.Clear();
        foreach (var i in IngredientsManager.Instance.PotionIngredientsList)
        {
            var card = Instantiate(_prefab, _selectionParent);
            card.Intialize(_data, i);
            _cards.Add(card);
        }
    }

    public void SetSelectedIngredient(PotionIngredient ingredient)
    {
        _selectedIngredient = ingredient;
    }

    public void AddIngredientToPotion()
    {
        if (_selectedIngredient == null)
        {
            ErrorMessaging.instance.ShowError("No Ingreident Selected");
            Debug.Log("No Ingredient Selected");
            return;
        }

        if (_selectedIngredient.Amount == 0)
        {
            ErrorMessaging.instance.ShowError("No Ingreidents Left");
            Debug.Log("No Ingreidents Left");
            return;
        }

        if(_selectedIngredient.APCost > GameManager.instance.Player.CurrentAP)
        {
            ErrorMessaging.instance.ShowError("Not enough AP");
            return;
        }

        _emitter.Play();
        PotionManager.instance.AddIngredientToPotion(_selectedIngredient);
        GameManager.instance.Player.RemoveAP(_selectedIngredient.APCost);
        //_selectedIngredient.AddAmount(-1);
        _selectedIngredient = null;
        ListOutPotionIngredients();
    }
}
