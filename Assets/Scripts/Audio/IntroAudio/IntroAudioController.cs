using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroAudioController : MonoBehaviour
{
    public static IntroAudioController Instance { get; private set; }

    [System.Serializable]
    public class IntroAudioEntry
    {
        public AudioSource sourceIntro;
    }

    [SerializeField] private List<IntroAudioEntry> audioSources = new List<IntroAudioEntry>();
    private Dictionary<string, AudioSource> audioSourceDictionaryIntro = new Dictionary<string, AudioSource>();

    [SerializeField] private GameObject audioClipsIntro;

    // // Check if our scene is initialized
    private bool isInitialized = false;
    // // Store the current scene name
    // private string currentSceneName; 


    private void Awake()
    {
        // Verify for nulls
        if (Instance == null)
        {
            Instance = this;

            if (!isInitialized && audioClipsIntro != null)
            {
                InitializeIntroAudioSourcesDynamically();
                isInitialized = true;
            }
            else if (audioClipsIntro == null)
            {
                Debug.LogError("AudioClipsIntro reference not set in the AudioController script.");
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Initialize the list of audio clips so they are accessible
    private void InitializeIntroAudioSourcesDynamically()
    {
        Transform audioClipsIntroTransform = audioClipsIntro.transform;
        int childCountIntro = audioClipsIntroTransform.childCount;

        for (int i = 0; i < childCountIntro; i++)
        {
            Transform childIntro = audioClipsIntroTransform.GetChild(i);
            AudioSource audioSourceIntro = childIntro.GetComponent<AudioSource>();

            if (audioSourceIntro != null)
            {
                audioSources.Add(new IntroAudioEntry { sourceIntro = audioSourceIntro });
                audioSourceDictionaryIntro.Add(childIntro.name, audioSourceIntro);
            }
            else
            {
                Debug.LogWarning("AudioSource component not found in child object: " + childIntro.name);
            }
        }
    }

 

    // Method for playing sound
    public void PlaySoundIntro(string sourceNameIntro)
    {
        if (audioSourceDictionaryIntro.ContainsKey(sourceNameIntro))
        {
            audioSourceDictionaryIntro[sourceNameIntro].Play();
        }
        else
        {
            Debug.LogWarning("Audio source not found: " + sourceNameIntro);
        }
    }


      // Method to stop all playing audio sources
    public void StopAllAudioIntro()
    {
        foreach (var audioSourceIntro in audioSourceDictionaryIntro.Values)
        {
             audioSourceIntro.Stop();
        }

        // foreach (var entry in audioSourceDictionaryIntro.Values)
        // {
        //     if (entry.isPlaying)
        //     {
        //         entry.Stop();
        //     }
        // }
    }



    public void setVolume(string sourceNameIntro, float newVolume)
    {
        if (IntroAudioController.Instance.audioSourceDictionaryIntro.TryGetValue(sourceNameIntro, out AudioSource audioSourceIntro))
        {
            // Adjust the volume
            audioSourceIntro.volume = newVolume;

            Debug.Log("Volume set for " + sourceNameIntro);
        }
        else
        {
            Debug.LogWarning("Audio source not found: " + sourceNameIntro);
        }
    }



    // Method for playing sound which does not repeat
// public void PlaySoundGameplayNoRepeat(string sourceName)
// {
//     if (audioSourceDictionaryGameplay.ContainsKey(sourceName))
//     {
//         AudioSource audioSource = audioSourceDictionaryGameplay[sourceName];

//         StartCoroutine(CheckAndPlay(audioSource));
//     }
//     else
//     {
//         Debug.LogWarning("Audio source not found: " + sourceName);
//     }
// }

// private System.Collections.IEnumerator CheckAndPlay(AudioSource audioSource)
// {
//     yield return new WaitForSeconds(0.1f); // Adjust this delay as needed

//     // Check if the audio is not already playing
//     if (!audioSource.isPlaying)
//     {
//         audioSource.Play();
//     }
//     else
//     {
//         Debug.Log("Sound is already playing: " + audioSource.clip.name);
//     }
// }




// Method for playing sounds with delay
public void PlayDelayedSoundIntro(string sourceName, float delay)
{
    StartCoroutine(PlayDelayedSoundCoroutine(sourceName, delay));
}

    private IEnumerator PlayDelayedSoundCoroutine(string sourceName, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (audioSourceDictionaryIntro.ContainsKey(sourceName))
        {
            AudioSource audioSourceIntro = audioSourceDictionaryIntro[sourceName];

            if (!audioSourceIntro.isPlaying)
            {
                audioSourceIntro.Play();
            }
            else
            {
                Debug.LogWarning("Audio source is already playing: " + sourceName);
            }
        }
        else
        {
            Debug.LogWarning("Audio source not found: " + sourceName);
        }
    }

}
