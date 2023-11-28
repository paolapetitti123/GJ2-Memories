using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioBackgroundController : MonoBehaviour
{
    private bool isFadingOut = false;

    public static AudioBackgroundController Instance { get; private set; }

    [SerializeField] private GameObject audioClipsBackground;

    private Dictionary<string, AudioSource> audioSourceDictionaryBackground = new Dictionary<string, AudioSource>();
    private string currentSceneName;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            if (audioClipsBackground != null)
            {
                InitializeBackgroundAudioSourcesDynamically();
            }
            else
            {
                Debug.LogError("AudioClipsBackground reference not set in the AudioBackgroundController script.");
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

      void Start()
    {

            // Trigger the outside river background music
            // AudioBackgroundController.Instance.PlayBgAudio("outside_river_bg_1");

            }
            
    private void InitializeBackgroundAudioSourcesDynamically()
    {
        Transform audioClipsTransform = audioClipsBackground.transform;
        int childCount = audioClipsTransform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Transform child = audioClipsTransform.GetChild(i);
            AudioSource audioSourceBackground = child.GetComponent<AudioSource>();

            if (audioSourceBackground != null)
            {
                audioSourceDictionaryBackground.Add(child.name, audioSourceBackground);
            }
            else
            {
                Debug.LogWarning("AudioSource component not found in child object: " + child.name);
            }
        }
    }

    void Update()
    {
    //     // Check if the scene has changed
    //     string newSceneName = SceneManager.GetActiveScene().name;
    //     if (newSceneName != currentSceneName)
    //     {
    //         currentSceneName = newSceneName;

    //         // Use switch or any logic to perform actions based on the current scene
    //         switch (currentSceneName)
    //         {
    //             case "MainMenu":
    //                 PlayBackgroundSound("main_menu_bg_music_1", 1f, 1f, 0.5f);
    
    //             // Debug.Log("The currentSceneName " + currentSceneName + " and the music that should be playing is main_menu_bg_music_1");
                  

    //                 break;

    //             case "GamePlay":       
    //                 // PlayBackgroundSound("gameplay_bg_music_1", 1f, 1f, 0.5f);
    //                 // StopBackgroundSound("main_menu_bg_music_1");
    //                 break;

    //             case "GameWin":
    //                 PlayBackgroundSound("game_win_bg_music_1", 1.0f, 2.0f, 1f);
           
                   
    //                 break;

    //             case "GameOver":
    //                 PlayBackgroundSound("game_over_bg_music_1", 1.0f, 2.0f, 1f);
             

    //                 break;

    //             case "GameCredits":
    //                 PlayBackgroundSound("credits_bg_music_1", 1.0f, 2.0f, 1f);

    // // Debug.Log("The currentSceneName " + currentSceneName + " and the music that should be playing is credits_bg_music_1");

    //                 break;

    //             default:
    //                 Debug.LogWarning("Unknown scene: " + currentSceneName);
    //                 break;
    //         }
    //     }
    }



    // Method to play background audio with overlap support
    public void PlayBgAudio(params string[] sourceNames)
    {
        // Stop all audio sources before playing the new ones
        foreach (var audioSource in audioSourceDictionaryBackground.Values)
        {
            audioSource.Stop();
        }

        // Play the specified background audio sources
        foreach (var sourceName in sourceNames)
        {
            if (audioSourceDictionaryBackground.TryGetValue(sourceName, out var audioSource))
            {
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning("Background audio source not found: " + sourceName);
            }
        }
    }








        // Simpler Method for playing audio
        // public void PlayBgAudio(string sourceName)
        // {
        //     // Stop all audio sources before playing the new one
        //     foreach (var backgroundAudioSource in audioSourceDictionaryBackground.Values)
        //     {
        //         backgroundAudioSource.Stop();
        //     }

        //     // Check if the audio clip with the given name exists in the dictionary
        //     if (audioSourceDictionaryBackground.TryGetValue(sourceName, out var audioSource))
        //     {
        //         // Play the audio clip with the specified name
        //         audioSource.Play();
        //     }
        //     else
        //     {
        //         // Log a warning if the specified audio clip is not found
        //         Debug.LogWarning("Background audio source not found: " + sourceName);
        //     }
        // }




        // Method for triggering background music where we want it to trigger.
        public void PlayBackgroundSound(string sourceName, float fadeOutDuration, float fadeInDuration, float targetVolume)
        { 

           if (audioSourceDictionaryBackground.ContainsKey(sourceName))
            {
        // Stop any ongoing coroutines to prevent overlapping audio
        StopAllCoroutines();
                foreach (var audioSource in audioSourceDictionaryBackground.Values)
                {
                    StartCoroutine(FadeOut(audioSource, fadeOutDuration));
                }

                // Play the selected background audio with a delay for the fade-out effect
                StartCoroutine(PlayWithDelay(sourceName, fadeOutDuration, fadeInDuration, targetVolume));
            }
            else
            {
                Debug.LogWarning("Background audio source not found: " + sourceName);
            }
        }

       // Coroutine to fade out the audio over a specified duration
        private IEnumerator FadeOut(AudioSource audioSource, float duration)
        {
            isFadingOut = true; // Set the flag to true to indicate the fade-out is in progress

            float startVolume = audioSource.volume;

            while (audioSource.volume > 0 && isFadingOut)
            {
                audioSource.volume -= startVolume * Time.deltaTime / duration;
                yield return null;
            }

            if (isFadingOut)
            {
                audioSource.Stop();
            }

            audioSource.volume = startVolume;

            isFadingOut = false; // Reset the flag
        }

        // Coroutine to play the new background audio with a delay and fade-in
        private IEnumerator PlayWithDelay(string sourceName, float fadeOutDuration, float fadeInDuration, float targetVolume)
        {
            yield return new WaitForSeconds(fadeOutDuration);

            if (audioSourceDictionaryBackground.ContainsKey(sourceName))
            {
                AudioSource newAudioSource = audioSourceDictionaryBackground[sourceName];
                newAudioSource.volume = 0.0f; // Start with volume at 0

                newAudioSource.Play();

                if (fadeInDuration <= 0.0f)
                {
                    Debug.LogWarning("Fade-in duration should be greater than 0. Setting to a small value.");
                    fadeInDuration = 0.01f;
                }

                float deltaVolume = Time.deltaTime / fadeInDuration;

                while (newAudioSource.volume < targetVolume)
                {
                    newAudioSource.volume += deltaVolume;

                    // Clamp the volume to ensure it stays within the valid range [0, 1]
                    newAudioSource.volume = Mathf.Clamp01(newAudioSource.volume);

                    yield return null;
                }
            }
        }


       // Method to stop the audio immediately
        public void StopBackgroundSound(string sourceName)
        {
            if (audioSourceDictionaryBackground.ContainsKey(sourceName))
            {
                isFadingOut = false; // Set the flag to stop the fade-out coroutine immediately
                StopAllCoroutines(); // Stop all coroutines to prevent overlapping

                // Check if the source to be stopped is currently playing
                if (audioSourceDictionaryBackground.TryGetValue(sourceName, out var activeAudioSource))
                {
                    StartCoroutine(FadeOut(activeAudioSource, 0.5f)); // Fade out the active audio source
                }
            }
            else
            {
                Debug.LogWarning("Background audio source not found: " + sourceName);
            }
        }


        // Method to stop all background audio
        public void StopAllBackgroundAudio()
        {
            foreach (var audioSource in audioSourceDictionaryBackground.Values)
            {
                audioSource.Stop();
            }
        }


}
