using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuCredits : MonoBehaviour
{
    void Awake()
    {
       
    }

   public void CheckSceneAndDontDestroy()
    {
        string activeSceneName = SceneManager.GetActiveScene().name;

        if (activeSceneName == "MainMenu" || activeSceneName == "GameCredits")
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
