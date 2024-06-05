using System.Collections;
using TMPro;
using UnityEngine;

public class APCounter : MonoBehaviour
{
    public TMP_Text Text;

    private void OnEnable()
    {
        StartCoroutine(Load());
    }

    private IEnumerator Load()
    {
        while (GameManager.Instance == null)
            yield return null;
        GameManager.Instance.Player.OnApChange += UpdateAP;
    }

    private void UpdateAP(int ap)
    {
        Text.text = ap.ToString();
    }

    private void OnDisable()
    {
        GameManager.Instance.Player.OnApChange -= UpdateAP;
    }
}
