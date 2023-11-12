using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonManager : MonoBehaviour
{

    [SerializeField] Button _replayButton;
    [SerializeField] Button _creditsButton;
    [SerializeField] Button _exitButton;

    // Start is called before the first frame update
    void Start()
    {
        _replayButton.onClick.AddListener(Replay);
        _creditsButton.onClick.AddListener(Credits);
        _exitButton.onClick.AddListener(ExitGame);
    }

    public void Replay()
    {
        GameStateManager.Instance.LoadScene(GameStateManager.Scene.GamePlay);
    }

    public void Credits()
    {
        GameStateManager.Instance.LoadScene(GameStateManager.Scene.GameCredits);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

