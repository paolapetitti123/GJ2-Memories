using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuPlay : MonoBehaviour
{
    void Awake()
    {
       
    }

   public void CheckSceneAndDontDestroy()
    {
        string activeSceneName = SceneManager.GetActiveScene().name;

        if (activeSceneName == "MainMenu" || activeSceneName == "Intro")
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update() {
        CheckSceneAndDontDestroy();
    }

}
