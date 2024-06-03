using UnityEngine;

[CreateAssetMenu(fileName = "new Enemy Data", menuName = "Create new Enemy Data", order = 1)]
public class EnemyData : ScriptableObject
{
    public Sprite Icon;
    public EnemyAvatar EnemyPrefab;
}
