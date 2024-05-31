using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInfoUI : MonoBehaviour
{
    [SerializeField] Image _enemyIcon;

    private void OnEnable()
    {
        StartCoroutine(Load());
    }

    public IEnumerator Load()
    {
        while (GameManager.instance == null)
        {
            yield return null;
        }

        _enemyIcon.sprite = GameManager.instance.EnemyData.Icon;
    }
}
