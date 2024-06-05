using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image _filler;
    [SerializeField] Fighter _owner;
    [SerializeField] string _id;

    private void OnEnable()
    {
        SetOwner();
        GameManager.Instance.OnEnemyChange += ChangeOwner;
    }

    private void SetOwner()
    {
        if (_id == "Player")
            _owner = GameManager.Instance.Player;
        else if (_id == "Enemy")
            _owner = GameManager.Instance.Enemy;

        _owner.OnHealthChanged += UpdateHealth;
    }

    public void ChangeOwner(EnemyAvatar enemy, EnemyData data)
    {
        _owner.OnHealthChanged -= UpdateHealth;

        if (_id == "Enemy") _owner = enemy;

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
