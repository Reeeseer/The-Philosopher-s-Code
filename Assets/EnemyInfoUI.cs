using System.Collections;
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
        while (GameManager.Instance == null)
        {
            yield return null;
        }

        _enemyIcon.sprite = GameManager.Instance.EnemyData.Icon;
    }
}
