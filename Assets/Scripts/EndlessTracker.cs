// This script was created to handle the behavior for the target selection part of the player's turn

using UnityEngine;

public class EndlessTracker : MonoBehaviour
{
    public static EndlessTracker instance;
    public int EnemiesKilledThisRun;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
}