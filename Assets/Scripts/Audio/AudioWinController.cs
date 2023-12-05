using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioWinController : MonoBehaviour
{
    public static AudioWinController Instance { get; private set; }

    [System.Serializable]
    public class AudioWinEntry
    {
        public AudioSource sourceWin;
    }

    [SerializeField] private List<AudioWinEntry> audioSources = new List<AudioWinEntry>();
    private Dictionary<string, AudioSource> audioSourceDictionaryWin = new Dictionary<string, AudioSource>();

    [SerializeField] private GameObject audioClipsWin;

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

            if (!isInitialized && audioClipsWin != null)
            {
                InitializeWinAudioSourcesDynamically();
                isInitialized = true;
            }
            else if (audioClipsWin == null)
            {
                Debug.LogError("AudioClipsWin reference not set in the AudioController script.");
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Initialize the list of audio clips so they are accessible
    private void InitializeWinAudioSourcesDynamically()
    {
        Transform audioClipsWinTransform = audioClipsWin.transform;
        int childCountWin = audioClipsWinTransform.childCount;

        for (int i = 0; i < childCountWin; i++)
        {
            Transform childWin = audioClipsWinTransform.GetChild(i);
            AudioSource audioSourceWin = childWin.GetComponent<AudioSource>();

            if (audioSourceWin != null)
            {
                audioSources.Add(new AudioWinEntry { sourceWin = audioSourceWin });
                audioSourceDictionaryWin.Add(childWin.name, audioSourceWin);
            }
            else
            {
                Debug.LogWarning("AudioSource component not found in child object: " + childWin.name);
            }
        }
    }

 

    // Method for playing sound
    public void PlaySoundWin(string sourceNameWin)
    {
        if (audioSourceDictionaryWin.ContainsKey(sourceNameWin))
        {
            audioSourceDictionaryWin[sourceNameWin].Play();
        }
        else
        {
            Debug.LogWarning("Audio source not found: " + sourceNameWin);
        }
    }


      // Method to stop all playing audio sources
    public void StopAllAudioWin()
    {
        foreach (var audioSourceWin in audioSourceDictionaryWin.Values)
        {
             audioSourceWin.Stop();
        }

        // foreach (var entry in audioSourceDictionaryIntro.Values)
        // {
        //     if (entry.isPlaying)
        //     {
        //         entry.Stop();
        //     }
        // }
 
    }

    public void setVolume(string sourceNameWin, float newVolumeWin)
    {
        if (AudioWinController.Instance.audioSourceDictionaryWin.TryGetValue(sourceNameWin, out AudioSource audioSourceWin))
        {
            // Adjust the volume
            audioSourceWin.volume = newVolumeWin;

            Debug.Log("Volume set for " + sourceNameWin);
        }
        else
        {
            Debug.LogWarning("Audio source not found: " + sourceNameWin);
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
public void PlayDelayedSoundWin(string sourceNameWin, float delayWin)
{
    StartCoroutine(PlayDelayedSoundWinCoroutine(sourceNameWin, delayWin));
}

    private IEnumerator PlayDelayedSoundWinCoroutine(string sourceNameWin, float delayWin)
    {
        yield return new WaitForSeconds(delayWin);

        if (audioSourceDictionaryWin.ContainsKey(sourceNameWin))
        {
            AudioSource audioSourceWin = audioSourceDictionaryWin[sourceNameWin];

            if (!audioSourceWin.isPlaying)
            {
                audioSourceWin.Play();
            }
            else
            {
                Debug.LogWarning("Audio source is already playing: " + sourceNameWin);
            }
        }
        else
        {
            Debug.LogWarning("Audio source not found: " + sourceNameWin);
        }
    }

}
