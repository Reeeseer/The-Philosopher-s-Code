using System;
using System.Collections;
using System.Collections.Generic;
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
        while (GameManager.instance == null)
            yield return null;
        GameManager.instance.Player.OnAPChange += UpdateAP;
    }

    private void UpdateAP(int ap)
    {
        Text.text = ap.ToString();
    }

    private void OnDisable()
    {
        GameManager.instance.Player.OnAPChange -= UpdateAP;
    }
}
