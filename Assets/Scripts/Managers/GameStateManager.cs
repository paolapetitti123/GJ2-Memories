using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    private void Awake()
    {
        Instance = this;
    }

  
    public enum Scene
    {
        MainMenu,
        Intro,
        GamePlay,
        GameWin,
        GameLose,
        GameCredits
    }

   
    public void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

}
