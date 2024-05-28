using Unity.VisualScripting;
using UnityEngine;

public class IfPanel : MonoBehaviour
{
    public void SetTarget(IAmTarget target, IAmTarget additionalTarget = null)
    {
        GameManager.instance.targets.Add(target);
        if (additionalTarget != null)
            GameManager.instance.targets.Add(additionalTarget);

        Debug.Log(GameManager.instance.targets.Count);

        GameManager.instance.PlayerActionUi.ShowIngredientsPanel();
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
