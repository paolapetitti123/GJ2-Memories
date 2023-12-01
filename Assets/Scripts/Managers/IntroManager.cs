using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public GameObject[] introCanvases; // Array to hold all intro canvases
    public Button nextButton;
    public Button backButton;
    public Button homeButton;

    private int currentCanvasIndex = 0;

    void Start()
    {
        // Disable all canvases except the first one
        for (int i = 1; i < introCanvases.Length; i++)
        {
            introCanvases[i].SetActive(false);
        }

        // Disable back button on the first canvas
        backButton.interactable = false;

        // Add click listeners to buttons
        nextButton.onClick.AddListener(NextCanvas);
        backButton.onClick.AddListener(PreviousCanvas);
        homeButton.onClick.AddListener(GoToMenu);
    }

    void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");

        if (currentCanvasIndex == 4) 
        {
            SceneManager.LoadScene("MainMenu");
        }
    }


    void NextCanvas()
    {
        if (currentCanvasIndex < introCanvases.Length - 1)
        {
            // Disable all canvases
            foreach (GameObject canvas in introCanvases)
            {
                canvas.SetActive(false);
            }

            // Enable next canvas
            currentCanvasIndex++;
            introCanvases[currentCanvasIndex].SetActive(true);

            // Enable back button after moving to the second canvas
            if (currentCanvasIndex == 1)
            {
                backButton.interactable = true;
            }

            // Check if we are in scene5 and load the gameplay scene
            if (currentCanvasIndex == 5) // scene5
            {
                LoadGameplayScene();
            }
        }
    }

    void PreviousCanvas()
    {
        if (currentCanvasIndex > 0)
        {
            // Disable all canvases
            foreach (GameObject canvas in introCanvases)
            {
                canvas.SetActive(false);
            }

            // Enable previous canvas
            currentCanvasIndex--;
            introCanvases[currentCanvasIndex].SetActive(true);

            // Disable back button on the first canvas
            if (currentCanvasIndex == 0)
            {
                backButton.interactable = false;
            }
        }
    }

  

    void LoadGameplayScene()
    {
        SceneManager.LoadScene("GamePlay"); 
    }
}
