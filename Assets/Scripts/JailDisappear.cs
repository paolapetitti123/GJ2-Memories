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
        StartCoroutine(TurnImageOff());

        // trigger jail sound
    }

    public IEnumerator TurnImageOff()
    {
        yield return new WaitForSeconds(5f);

        jailScene.GetComponent<Image>().enabled = false;
        jailBars.GetComponent<Image>().enabled = false;

        // Start Win Scene background music

        // turn off jail sound (if needed) 

    }
}
