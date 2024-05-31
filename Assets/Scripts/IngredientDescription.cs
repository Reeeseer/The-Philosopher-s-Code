
using TMPro;
using UnityEngine;

public class IngredientDescription : MonoBehaviour
{
    private TMP_Text _text;

    private void OnEnable()
    {
        _text = GetComponent<TMP_Text>();
    }
    internal void SetText(string description)
    {
        _text.text = description;
    }
}
