using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image _filler;
    [SerializeField] Fighter _owner;
    [SerializeField] string _id;

    private void OnEnable()
    {
        if (_id == "Player")
            _owner = GameManager.instance.Player;
        else if(_id == "Enemy")
            _owner = GameManager.instance.Enemy;

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
