using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image _filler;
    [SerializeField] Fighter _owner;

    private void OnEnable()
    {
        _owner.OnHealthChanged += UpdateHealth;
    }

    private void UpdateHealth(int currHealth, int maxHealth)
    {
        _filler.fillAmount = (float)currHealth / (float)maxHealth;
    }

    private void OnDisable()
    {
        _owner.OnHealthChanged -= UpdateHealth;
    }
}
