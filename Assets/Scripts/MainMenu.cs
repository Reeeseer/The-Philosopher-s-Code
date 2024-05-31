 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Image _creditsPanel;
    [SerializeField] Image _controlsPanel;

    public void PlayCampaign()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayEndlessMode()
    {
        SceneManager.LoadScene("Endless Mode");
    }

    public void OpenSettings()
    {

    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenCredits()
    {
        _creditsPanel.gameObject.SetActive(true);
    }

    public void ShowControls()
    {
        _controlsPanel.gameObject.SetActive(true);
    }
}
