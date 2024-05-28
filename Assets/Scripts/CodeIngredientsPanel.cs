using FMODUnity;
using System.Collections.Generic;
using UnityEngine;

public class CodeIngredientsPanel : MonoBehaviour
{
    [SerializeField] CodeIngredientCard _prefab;
    [SerializeField] IngredientCardData _data;
    [SerializeField] Transform _selectionParent;

    CodeIngredient _selectedIngredient;
    List<CodeIngredientCard> _cards = new();
    StudioEventEmitter _emitter;

    void OnEnable()
    {
        _emitter = GetComponent<StudioEventEmitter>();
    }

    public void ListOutCodeIngredients()
    {
        foreach (var c in _cards)
        {
            Destroy(c.gameObject);
        }

        _cards.Clear();

        foreach (var i in IngredientsManager.Instance.CodeIngredientsList)
        {
            var card = Instantiate(_prefab, _selectionParent);
            //card.Intialize(_data, i);
            _cards.Add(card);
        }
    }

    internal void SetSelectedIngredient(CodeIngredient ingredient)
    {
        _selectedIngredient = ingredient;
    }

    public void AddIngredientToPotion()
    {

        if (_selectedIngredient == null)
        {
            Debug.Log("No Ingredient Selected");
            return;
        }

        //if (_selectedIngredient.Amount == 0)
        //{
        //    Debug.Log("No Ingreidents Left");
        //    ErrorMessaging.instance.SendMessage("No Ingreidents Left");
        //    return;
        //}

        //else
        //{
        //    _emitter.Play();
        //    _selectedIngredient.AddAmount(-1);
        //    PotionManager.instance.CurrentPotion.AddCodeIngredient(_selectedIngredient);
        //    _selectedIngredient = null;
        //}
    }
}
