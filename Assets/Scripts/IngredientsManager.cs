using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class IngredientsManager : MonoBehaviour
{
    public static IngredientsManager Instance;

    public List<PotionIngredient> PotionIngredientsList;
    public List<CodeIngredient> CodeIngredientsList;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void ResetIngredients()
    {
        foreach (var i in PotionIngredientsList)
        {
            i.Restart();
        }

        foreach (var i in CodeIngredientsList)
        {
            i.Restart();
        }
    }
}
