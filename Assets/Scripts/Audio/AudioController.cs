using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance { get; private set; }

    [SerializeField] private GameObject mainCharacterMovement;

    [System.Serializable]
    public class AudioEntry
    {
        public AudioSource source;
    }

    [SerializeField] private List<AudioEntry> audioSources = new List<AudioEntry>();
    private Dictionary<string, AudioSource> audioSourceDictionaryGameplay = new Dictionary<string, AudioSource>();

    [SerializeField] private GameObject audioClipsGameplay;

    // Check if our scene is initialized
    private bool isInitialized = false;
    // Store the current scene name
    private string currentSceneName; 

    private void Awake()
    {
        // Verify for nulls
        if (Instance == null)
        {
            Instance = this;

            if (!isInitialized && audioClipsGameplay != null)
            {
                InitializeGameplayAudioSourcesDynamically();
                isInitialized = true;
            }
            else if (audioClipsGameplay == null)
            {
                Debug.LogError("AudioClipsGameplay reference not set in the AudioController script.");
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Initialize the list of audio clips so they are accessible
    private void InitializeGameplayAudioSourcesDynamically()
    {
        Transform audioClipsTransform = audioClipsGameplay.transform;
        int childCount = audioClipsTransform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Transform child = audioClipsTransform.GetChild(i);
            AudioSource audioSourceGameplay = child.GetComponent<AudioSource>();

            if (audioSourceGameplay != null)
            {
                audioSources.Add(new AudioEntry { source = audioSourceGameplay });
                audioSourceDictionaryGameplay.Add(child.name, audioSourceGameplay);
            }
            else
            {
                Debug.LogWarning("AudioSource component not found in child object: " + child.name);
            }
        }
    }

    // private void Update()
    // {
    //     // Check if the scene has changed
    //     string newSceneName = SceneManager.GetActiveScene().name;
    //     if (newSceneName != currentSceneName)
    //     {
    //         currentSceneName = newSceneName;

    //         // Use this switch setup to play sounds in specifics scenes from the audio controller.
    //         switch (currentSceneName)
    //         {
    //             case "MainMenu":
                   
    //                 break;

    //             case "GamePlay":
           
    //                 break;

    //             case "GameWin":
            
    //                 break;

    //             case "GameOver":
          
    //                 break;

    //             case "GameCredits":
                
    //                 break;

    //             default:
    //                 Debug.LogWarning("Unknown scene: " + currentSceneName);
    //                 break;
    //         }
    //     }
    // }

    // Method for playing sound
    public void PlaySoundGameplay(string sourceName)
    {
        if (audioSourceDictionaryGameplay.ContainsKey(sourceName))
        {
            audioSourceDictionaryGameplay[sourceName].Play();
        }
        else
        {
            Debug.LogWarning("Audio source not found: " + sourceName);
        }
    }

    // Method for playing sounds with delay
    public void PlayDelayedSoundGameplay(string sourceName, float delay)
    {
        StartCoroutine(PlayDelayedSoundCoroutine(sourceName, delay));
    }

    private IEnumerator PlayDelayedSoundCoroutine(string sourceName, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (audioSourceDictionaryGameplay.ContainsKey(sourceName))
        {
            audioSourceDictionaryGameplay[sourceName].Play();
        }
        else
        {
            Debug.LogWarning("Audio source not found: " + sourceName);
        }
    }
}
