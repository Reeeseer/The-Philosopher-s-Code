using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActionUI : MonoBehaviour
{
    [SerializeField] Image _brewPanel;
    [SerializeField] Image _ifPanel;
    [SerializeField] PotionIngredientsPanel _ingredientsPanel;
    [SerializeField] CodeIngredientsPanel _codePanel;

    void Start()
    {
        StartCoroutine(Load());
    }

    IEnumerator Load()
    {
        while(TurnManager.instance == null)
            yield return null;

        TurnManager.instance.OnPlayerTurnStart += PlayerTurnStart;
    }

    void PlayerTurnStart(PlayerAvatar obj)
    {
        if (GameManager.instance.GameOver) { return; }
        _brewPanel.gameObject.SetActive(true);
        _ifPanel.gameObject.SetActive(false);
        _ingredientsPanel.gameObject.SetActive(false);
        _codePanel.gameObject.SetActive(false);
    }

    /// <summary>
    /// this function calls when pressing the BREW!! button during player turn
    /// </summary>
    public void ShowIfPanel()
    {
        _brewPanel.gameObject.SetActive(false);
        _ifPanel.gameObject.SetActive(true);
        _ingredientsPanel.gameObject.SetActive(false);
        _codePanel.gameObject.SetActive(false);
    }

    /// <summary>
    /// this function calls after selecting any if() statement for targeting during player turn
    /// </summary>
    public void ShowIngredientsPanel()
    {
        _brewPanel.gameObject.SetActive(false);
        _ifPanel.gameObject.SetActive(false);
        _ingredientsPanel.gameObject.SetActive(true);
        _ingredientsPanel.ListOutPotionIngredients();
        _codePanel.gameObject.SetActive(false);
    }

    /// <summary>
    /// this function calls after confirming ingredients during player turn
    /// </summary>
    public void ShowCodePanel()
    {
        if (PotionManager.instance.CurrentPotion == null)
        {
            ErrorMessaging.instance.ShowError("No ingredients added to potion");
            return;
        }

        _brewPanel.gameObject.SetActive(false);
        _ifPanel.gameObject.SetActive(false);
        _ingredientsPanel.gameObject.SetActive(false);
        _codePanel.gameObject.SetActive(true);
        _codePanel.ListOutCodeIngredients();
    }

    /// <summary>
    /// Disables the UI for a time
    /// </summary>
    public void DisableActionUi()
    {
        _brewPanel.gameObject.SetActive(false);
        _ifPanel.gameObject.SetActive(false);
        _ingredientsPanel.gameObject.SetActive(false);
        _codePanel.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        TurnManager.instance.OnPlayerTurnStart -= PlayerTurnStart;
    }
}
