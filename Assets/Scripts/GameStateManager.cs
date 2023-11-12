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
        GamePlay,
        GameWin,
        GameOver,
        GameCredits
    }

   
    public void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
