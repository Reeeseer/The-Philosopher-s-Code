using FMODUnity;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContinuePrompt : MonoBehaviour
{
    [SerializeField] Transform _children;
    [SerializeField] Button _returnToMenu;
    [SerializeField] Button _continue;

    [SerializeField] TMP_Text _text;
    StudioEventEmitter _emitter;

    void OnEnable()
    {
        StartCoroutine(Load());
    }

    public IEnumerator Load()
    {
        while (GameManager.instance == null) { yield return null; }
        _children.gameObject.SetActive(true);
        _emitter = GetComponent<StudioEventEmitter>();
        _text = GetComponentInChildren<TMP_Text>();
        GameManager.instance.OnGameOver += Activate;
        GameManager.instance.OnGameOver += Activate;
        _children.gameObject.SetActive(false);

    }

    public void Activate(bool win)
    {
        _children.gameObject.SetActive(true);
        if (win)
        {
            _text.text = "You Won, Next Battle?";
            _continue.onClick.AddListener(NextFight);
            _continue.GetComponentInChildren<TMP_Text>().text = "Next Battle";
        }
        else
        {
            _text.text = "You lost, Try Again?";
            _continue.onClick.AddListener(Restart);
            _continue.GetComponentInChildren<TMP_Text>().text = "Restart?";
        }
        _emitter.Play();
        FindObjectOfType<BMG>().gameObject.SetActive(false);
    }

    public void NextFight()
    {
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadSceneAsync(scene.buildIndex + 1);
        _continue.onClick.RemoveListener(NextFight);
    }

    public void Restart()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
