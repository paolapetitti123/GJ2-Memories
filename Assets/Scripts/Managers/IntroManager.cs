using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public GameObject[] introCanvases; // Array to hold all intro canvases
    public Button nextButton;
    public Button backButton;
    public Button homeButton;

    public GameObject[] sceneTexts;


    private int currentCanvasIndex = 0;
    private int currentSceneTextIndex = 0;

    private int currentCanvasTrackAudio = 0;

    void Start()
    {
        // Disable all canvases except the first one
        for (int i = 1; i < introCanvases.Length; i++)
        {
            introCanvases[i].SetActive(false);
            sceneTexts[i].SetActive(false);
        }

        // Disable back button on the first canvas
        backButton.interactable = false;

        // Add click listeners to buttons
        nextButton.onClick.AddListener(NextCanvas);
        backButton.onClick.AddListener(PreviousCanvas);
        homeButton.onClick.AddListener(GoToMenu);


        // Starting audio
        IntroAudioController.Instance.PlaySoundIntro("wind");
        IntroAudioController.Instance.PlaySoundIntro("woman-cry");
        IntroAudioController.Instance.PlaySoundIntro("prince-cry");
        Debug.Log("currentCanvasTrackAudio = " + currentCanvasTrackAudio);

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
        currentCanvasTrackAudio++;
        Debug.Log("currentCanvasTrackAudio = " + currentCanvasTrackAudio);
        // Audio trigger
        TriggerIntroAudio();


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


            foreach (GameObject text in sceneTexts)
                    {
                        text.gameObject.SetActive(false);
                    }

            currentSceneTextIndex++;
            sceneTexts[currentSceneTextIndex].SetActive(true);

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
        //Track caanvas for audio
         currentCanvasTrackAudio--;
        Debug.Log("currentCanvasTrackAudio = " + currentCanvasTrackAudio);
        // Audio trigger
        TriggerIntroAudio();
        
        if (currentCanvasIndex > 0)
        {
            // Disable all canvases
            foreach (GameObject canvas in introCanvases)
            {
                canvas.SetActive(false);
            }

             // Disable all canvases
            foreach (GameObject canvas in introCanvases)
            {
                canvas.SetActive(false);
            }

            // Enable previous canvas
            currentCanvasIndex--;
            introCanvases[currentCanvasIndex].SetActive(true);
   

            // Same for text

            foreach (GameObject text in sceneTexts)
            {
                text.gameObject.SetActive(false);
            }

            currentSceneTextIndex--;
            sceneTexts[currentSceneTextIndex].SetActive(true);

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


    void TriggerIntroAudio()
    {    

        // Stop audio from continuing between intro sections
        IntroAudioController.Instance.StopAllAudioIntro(); 
           
              // Conditional for tracking the intro section and playing sound accordingly
            if ( currentCanvasTrackAudio == 0)
            {

                //Reset the volume
                IntroAudioController.Instance.setVolume("woman-cry", 0.3f);
                IntroAudioController.Instance.setVolume("prince-cry", 0.3f);

                IntroAudioController.Instance.PlaySoundIntro("wind");
                IntroAudioController.Instance.PlaySoundIntro("woman-cry");
                IntroAudioController.Instance.PlaySoundIntro("prince-cry");
            } else if ( currentCanvasTrackAudio == 1)
            
            {  
                IntroAudioController.Instance.PlaySoundIntro("scroll-audio");
                // Intro audio
                IntroAudioController.Instance.setVolume("woman-cry", 0.07f);
                IntroAudioController.Instance.setVolume("prince-cry", 0.07f);
                IntroAudioController.Instance.PlaySoundIntro("woman-cry");
                IntroAudioController.Instance.PlaySoundIntro("prince-cry");

            } else if (currentCanvasTrackAudio == 2) {
                       
                IntroAudioController.Instance.PlaySoundIntro("wind");
                IntroAudioController.Instance.PlaySoundIntro("princess-think");
                IntroAudioController.Instance.PlaySoundIntro("prince-cry");

            } else if (currentCanvasTrackAudio == 3) {

                IntroAudioController.Instance.PlaySoundIntro("bubbling-potion");
                IntroAudioController.Instance.PlayDelayedSoundIntro("princess-laugh", 1.2f);
            } else if (currentCanvasTrackAudio == 4) {

               IntroAudioController.Instance.PlayDelayedSoundIntro("gulp-sound", 0.9f);
                IntroAudioController.Instance.PlayDelayedSoundIntro("spell-cast", 2.5f);
                IntroAudioController.Instance.PlayDelayedSoundIntro("princess-laugh", 4.0f);
            }
    

    }

}
