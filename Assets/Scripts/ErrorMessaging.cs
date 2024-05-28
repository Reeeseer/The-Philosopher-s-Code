using System;
using UnityEngine;

public class ErrorMessaging : MonoBehaviour
{
    public static ErrorMessaging instance;
    public Action<string> OnFailedAttempt;

    private void Awake()
    {
        if (instance == null)
        { instance = this; }
        else
            Destroy(gameObject);
    }

    public void ShowError(string message)
    {
        OnFailedAttempt?.Invoke(message);
    }
}
