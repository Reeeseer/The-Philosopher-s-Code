using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Image _image;

    private void OnEnable()
    {
        Continue();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) { Open(); }
    }

    private void Open()
    {
        _image.gameObject.SetActive(true);
    }

    public void Continue()
    {
        _image.gameObject.SetActive(false);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
