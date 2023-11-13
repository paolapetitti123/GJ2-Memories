using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioBackgroundController : MonoBehaviour
{
    [SerializeField] private GameObject audioClipsBackground; // Reference to the AudioClips for background audio GameObject

    private Dictionary<string, AudioSource> audioSourceDictionaryBackground = new Dictionary<string, AudioSource>();
    private string currentSceneName; // Store the current scene name

    private void Awake()
    {
        if (audioClipsBackground != null)
        {
            InitializeBackgroundAudioSourcesDynamically();
        }
        else
        {
            Debug.LogError("AudioClipsBackground reference not set in the AudioBackgroundController script.");
        }
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
        // Check if the scene has changed
        string newSceneName = SceneManager.GetActiveScene().name;
        if (newSceneName != currentSceneName)
        {
            currentSceneName = newSceneName;

            // Use switch or any logic to perform actions based on the current scene
            switch (currentSceneName)
            {
                case "MainMenu":
                    PlayBackgroundSound("main_menu_bg_music_1", 0.5f, 0.3f);
    
                  
                    break;

                case "GamePlay":
                    PlayBackgroundSound("gameplay_bg_music_1", 0.3f, 2.0f);
         
                  
                    break;

                case "GameWin":
                    PlayBackgroundSound("gameplay_bg_music_1", 1.0f, 2.0f);
           
                   
                    break;

                case "GameOver":
                    PlayBackgroundSound("gameplay_bg_music_1", 1.0f, 2.0f);
             

                    break;

                case "GameCredits":
                    PlayBackgroundSound("gameplay_bg_music_1", 1.0f, 2.0f);


                    break;

                default:
                    Debug.LogWarning("Unknown scene: " + currentSceneName);
                    break;
            }
        }
    }

        // Method for triggering background music where we want it to trigger.
        public void PlayBackgroundSound(string sourceName, float fadeOutDuration, float fadeInDuration)
        {
            if (audioSourceDictionaryBackground.ContainsKey(sourceName))
            {
                foreach (var audioSource in audioSourceDictionaryBackground.Values)
                {
                    StartCoroutine(FadeOut(audioSource, fadeOutDuration));
                }

                // Play the selected background audio with a delay for the fade-out effect
                StartCoroutine(PlayWithDelay(sourceName, fadeOutDuration, fadeInDuration));
            }
            else
            {
                Debug.LogWarning("Background audio source not found: " + sourceName);
            }
        }

        // Coroutine to fade out the audio over a specified duration
        private IEnumerator FadeOut(AudioSource audioSource, float duration)
        {
            float startVolume = audioSource.volume;

            while (audioSource.volume > 0)
            {
                audioSource.volume -= startVolume * Time.deltaTime / duration;
                yield return null;
            }

            audioSource.Stop();
            audioSource.volume = startVolume;
        }

        // Coroutine to play the new background audio with a delay and fade-in
        private IEnumerator PlayWithDelay(string sourceName, float fadeOutDuration, float fadeInDuration)
        {
            yield return new WaitForSeconds(fadeOutDuration);

            if (audioSourceDictionaryBackground.ContainsKey(sourceName))
            {
                AudioSource newAudioSource = audioSourceDictionaryBackground[sourceName];
                newAudioSource.volume = 0.0f; // Start with volume at 0

                newAudioSource.Play();

                while (newAudioSource.volume < 1.0f)
                {
                    newAudioSource.volume += Time.deltaTime / fadeInDuration;
                    yield return null;
                }
            }
        }


}
