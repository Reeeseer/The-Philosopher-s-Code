using FMODUnity;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinuePrompt : MonoBehaviour
{
    [SerializeField] Transform _children;
    StudioEventEmitter _emitter;

    void OnEnable()
    {
        _children.gameObject.SetActive(false);    
        _emitter = GetComponent<StudioEventEmitter>();
    }

    public void Activate()
    {
        _children.gameObject.SetActive(true);
        _emitter.Play();
        FindObjectOfType<BMG>().gameObject.SetActive(false);
    }

    public void GoToLevel(int levelIndex)
    {
        var operation = SceneManager.LoadSceneAsync(levelIndex);
    }

    public void ReturnToMenu()
    {
        var operation = SceneManager.LoadSceneAsync(0);
    }
}
