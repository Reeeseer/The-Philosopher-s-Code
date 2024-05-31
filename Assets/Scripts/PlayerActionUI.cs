// This script is the manager for the UI the player uses to take their turn
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActionUI : MonoBehaviour
{
    [SerializeField] Image _brewPanel;
    [SerializeField] Image _ifPanel;
    [SerializeField] IngredientsPanel _ingredientsPanel;


    void Awake()
    {
        BattleTurnManager.instance.OnPlayerTurnStart += PlayerTurnStart;
    }

    public void PlayerAttack()
    {
        GameManager.instance.Player.Attack();
    }

    void PlayerTurnStart(PlayerAvatar obj)
    {
        if (GameManager.instance.GameOver) { return; }
        _brewPanel.gameObject.SetActive(true);
        _ifPanel.gameObject.SetActive(false);
        _ingredientsPanel.gameObject.SetActive(false);
    }

    /// <summary>
    /// this function calls when pressing the BREW!! button during player turn
    /// </summary>
    public void ShowIfPanel()
    {
        _brewPanel.gameObject.SetActive(false);
        _ifPanel.gameObject.SetActive(true);
        _ingredientsPanel.gameObject.SetActive(false);
    }

    /// <summary>
    /// this function calls after selecting any if() statement for targeting during player turn
    /// </summary>
    public void ShowIngredientsPanel()
    {
        _brewPanel.gameObject.SetActive(false);
        _ifPanel.gameObject.SetActive(false);
        _ingredientsPanel.gameObject.SetActive(true);
        _ingredientsPanel.ListOutIngredients(IngredientDataOptions.IngredientType.Potion);
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
        _ingredientsPanel.gameObject.SetActive(true);
        _ingredientsPanel.ListOutIngredients(IngredientDataOptions.IngredientType.Code);

    }

    /// <summary>
    /// Disables the UI for a time
    /// </summary>
    public void DisableActionUi()
    {
        _brewPanel.gameObject.SetActive(false);
        _ifPanel.gameObject.SetActive(false);
        _ingredientsPanel.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        BattleTurnManager.instance.OnPlayerTurnStart -= PlayerTurnStart;
    }
}
