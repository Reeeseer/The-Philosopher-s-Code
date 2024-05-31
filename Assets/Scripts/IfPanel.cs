// This script was created to handle the behavior for the target selection part of the player's turn

using UnityEngine;

public class IfPanel : MonoBehaviour
{
    public void SetTarget(Fighter target, Fighter additionalTarget = null)
    {
        GameManager.instance.targets.Clear();
        GameManager.instance.targets.Add(target);
        if (additionalTarget != null)
            GameManager.instance.targets.Add(additionalTarget);

        Debug.Log(GameManager.instance.targets.Count);

        //TODO: fix this:
        GetComponentInParent<PlayerActionUI>().ShowIngredientsPanel();
    }

    public void SetTargetAsPlayer()
    {
        SetTarget(GameManager.instance.Player);
    }

    public void SetTargetAsEnemy()
    {
        SetTarget(GameManager.instance.Enemy);
    }

    public void SetTargetAsBoth()
    {
        SetTarget(GameManager.instance.Player, GameManager.instance.Enemy);
    }
}
