// This script was created to handle the behavior for the target selection part of the player's turn

using UnityEngine;

public class IfPanel : MonoBehaviour
{
    public void SetTarget(Fighter target, Fighter additionalTarget = null)
    {
        GameManager.Instance.Targets.Clear();
        GameManager.Instance.Targets.Add(target);
        if (additionalTarget != null)
            GameManager.Instance.Targets.Add(additionalTarget);

        Debug.Log(GameManager.Instance.Targets.Count);

        //TODO: fix this:
        GetComponentInParent<PlayerActionUI>().ShowIngredientsPanel();
    }

    public void SetTargetAsPlayer()
    {
        SetTarget(GameManager.Instance.Player);
    }

    public void SetTargetAsEnemy()
    {
        SetTarget(GameManager.Instance.Enemy);
    }

    public void SetTargetAsBoth()
    {
        SetTarget(GameManager.Instance.Player, GameManager.Instance.Enemy);
    }
}
