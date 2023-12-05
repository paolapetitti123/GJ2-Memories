using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class JailDisappear : MonoBehaviour
{
    public GameObject jailScene;
    public GameObject jailBars;

    // Start is called before the first frame update
    void Start()
    {
       

        // Play audio for jail scene
        AudioWinController.Instance.PlaySoundWin("jail-close-1");
        AudioWinController.Instance.PlaySoundWin("princess-crying-1");
        AudioWinController.Instance.PlaySoundWin("prison-torches-1");
        StartCoroutine(TurnImageOff());
    }

    public IEnumerator TurnImageOff()
    {   

 
        
        yield return new WaitForSeconds(5f);

        AudioWinController.Instance.StopAllAudioWin();
        jailScene.GetComponent<Image>().enabled = false;
        jailBars.GetComponent<Image>().enabled = false;


        // Play audio for jail scene
        AudioWinController.Instance.PlaySoundWin("win-bg-music-1");
        AudioWinController.Instance.PlaySoundWin("crowd-cheer-1");
        AudioWinController.Instance.PlaySoundWin("birds-chirp-1");
        AudioWinController.Instance.PlaySoundWin("wind-win-1");


        // Start Win Scene background music

        // turn off jail sound (if needed) 

    }
}
