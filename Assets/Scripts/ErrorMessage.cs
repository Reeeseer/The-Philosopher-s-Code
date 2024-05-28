using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ErrorMessage : MonoBehaviour
{
    public Image _image;
    TMP_Text _text;
    StudioEventEmitter _emitter;

    private void OnEnable()
    {
        StartCoroutine(Load());
    }

    private IEnumerator Load()
    {
        _text = GetComponentInChildren<TMP_Text>();
        _image.gameObject.SetActive(false);
        _emitter = GetComponent<StudioEventEmitter>();
        while (ErrorMessaging.instance == null) { yield return null; }
        ErrorMessaging.instance.OnFailedAttempt += DisplayMessage;
    }

    public void DisplayMessage(string message)
    {
        StartCoroutine(Message(message));
    }

    public IEnumerator Message(string message)
    {
        _image.gameObject.SetActive(true);
        _emitter.Play();
        _text.text = message;
        yield return new WaitForSeconds(2);
        _image.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        ErrorMessaging.instance.OnFailedAttempt -= DisplayMessage;
    }
}
