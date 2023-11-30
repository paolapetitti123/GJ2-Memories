using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsController : MonoBehaviour
{
    //private bool playerInTriggerZone = false;
    //private float triggerCooldown = 1f;  // Adjust the cooldown time as needed
    //private float lastTriggerTime;
    public GameObject DoorOne;
    public GameObject DoorTwo;
    public GameObject DoorThree;
    public GameObject DoorFour;
    public GameObject DoorFive;
    public GameObject DoorSix;



    public GameObject outside;
    public GameObject alchemist;
    public GameObject kitchen;

    public GameObject mainCamera;
    public GameObject kitchenCamera;
    public GameObject alchemistCamera;
    public GameObject outsideLeftCamera;

    private void Start()
    {
        outside.SetActive(true);
        alchemist.SetActive(true);
        kitchen.SetActive(true);

        mainCamera.SetActive(true);
        kitchenCamera.SetActive(false);
        alchemistCamera.SetActive(false);
        outsideLeftCamera.SetActive(false);
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        // Differentiate between doors based on their tags
        if (other.gameObject == DoorOne)
        {
            Debug.Log("hit door one");
            // Handle collision with DoorOne
            //outside.SetActive(false);
           // kitchen.SetActive(false);
           // alchemist.SetActive(true);

            mainCamera.SetActive(false);
            kitchenCamera.SetActive(false);
            alchemistCamera.SetActive(true);
            outsideLeftCamera.SetActive(false);

            // Assuming your player's GameObject has a Transform component (if not, adjust accordingly)
            Transform playerTransform = gameObject.GetComponent<Transform>();

            // Set the player's y-position to its negative value
            playerTransform.position = new Vector3(playerTransform.position.x, 22.51f, playerTransform.position.z);
            Debug.Log(playerTransform.position);
            //CanvasManager.Instance.SwitchCanvas(CanvasManager.Instance.canvases[1]);

            // Trigger the alchemist background sounds
            AudioBackgroundController.Instance.PlayBgAudio("alchemist_bg_1", "bubbling_cauldron_1");

        }
        else if (other.gameObject == DoorTwo)
        {
            Debug.Log("hit door two");
            // Handle collision with DoorOne
           // outside.SetActive(false);
           // kitchen.SetActive(true);
            //alchemist.SetActive(false);
            Transform playerTransform = gameObject.GetComponent<Transform>();

            mainCamera.SetActive(false);
            kitchenCamera.SetActive(true);
            alchemistCamera.SetActive(false);
            outsideLeftCamera.SetActive(false);

            // Set the player's y-position to its negative value
            playerTransform.position = new Vector3(playerTransform.position.x, -23.63f, playerTransform.position.z);
            Debug.Log(playerTransform.position);

            //CanvasManager.Instance.SwitchCanvas(CanvasManager.Instance.canvases[0]);

            // Stop background audio
            AudioBackgroundController.Instance.StopAllBackgroundAudio();
            // Trigger door opening sound
            AudioController.Instance.PlaySoundGameplay("door_open_1");

        }
        else if (other.gameObject == DoorThree)
        {
            Debug.Log("hit door three");
            // Handle collision with DoorOne
           // outside.SetActive(true);
          //  kitchen.SetActive(false);
          //  alchemist.SetActive(false);
            Transform playerTransform = gameObject.GetComponent<Transform>();

            mainCamera.SetActive(true);
            kitchenCamera.SetActive(false);
            alchemistCamera.SetActive(false);
            outsideLeftCamera.SetActive(false);

            // Set the player's y-position to its negative value
            playerTransform.position = new Vector3(playerTransform.position.x, 3.16f, playerTransform.position.z);

            Debug.Log(playerTransform.position);
            //CanvasManager.Instance.SwitchCanvas(CanvasManager.Instance.canvases[2]);

            // Trigger the outside river background audio
            AudioBackgroundController.Instance.PlayBgAudio("wind_bg_1", "river_stream_bg_1");
        }
        else if (other.gameObject == DoorFour)
        {
            Debug.Log("hit door four");
            // Handle collision with DoorOne
            //outside.SetActive(true);
           // kitchen.SetActive(false);
           // alchemist.SetActive(false);
            Transform playerTransform = gameObject.GetComponent<Transform>();

            mainCamera.SetActive(true);
            kitchenCamera.SetActive(false);
            alchemistCamera.SetActive(false);
            outsideLeftCamera.SetActive(false);

            // Set the player's y-position to its negative value
            playerTransform.position = new Vector3(playerTransform.position.x, -4.79f, playerTransform.position.z);
            Debug.Log(playerTransform.position);

            //CanvasManager.Instance.SwitchCanvas(CanvasManager.Instance.canvases[2]);

            // Trigger the outside river background audio
            AudioBackgroundController.Instance.PlayBgAudio("wind_bg_1", "river_stream_bg_1");
        }
        else if (other.gameObject == DoorFive)
        {
            Transform playerTransform = gameObject.GetComponent<Transform>();


            mainCamera.SetActive(false);
            kitchenCamera.SetActive(false);
            alchemistCamera.SetActive(false);
            outsideLeftCamera.SetActive(true);

            // Set the player's y-position to its negative value
            playerTransform.position = new Vector3(-29.26f, playerTransform.position.y, playerTransform.position.z);
            Debug.Log(playerTransform.position);

            //CanvasManager.Instance.SwitchCanvas(CanvasManager.Instance.canvases[2]);

            // Trigger the outside river background audio
            AudioBackgroundController.Instance.PlayBgAudio("wind_bg_1", "river_stream_bg_1");
        }
        else if (other.gameObject == DoorSix)
        {
            Transform playerTransform = gameObject.GetComponent<Transform>();


            mainCamera.SetActive(true);
            kitchenCamera.SetActive(false);
            alchemistCamera.SetActive(false);
            outsideLeftCamera.SetActive(false);

            // Set the player's y-position to its negative value
            playerTransform.position = new Vector3(-8.71f, playerTransform.position.y, playerTransform.position.z);
            Debug.Log(playerTransform.position);

            //CanvasManager.Instance.SwitchCanvas(CanvasManager.Instance.canvases[2]);

            // Trigger the outside river background audio
            AudioBackgroundController.Instance.PlayBgAudio("wind_bg_1", "river_stream_bg_1");
        }
    }
}
