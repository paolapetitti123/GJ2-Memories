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



    public GameObject outside;
    public GameObject alchemist;
    public GameObject kitchen;


    private void Start()
    {
        outside.SetActive(true);
        alchemist.SetActive(false);
        kitchen.SetActive(false);


    }
    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player") && !playerInTriggerZone && Time.time - lastTriggerTime > triggerCooldown)
    //    {
    //        playerInTriggerZone = true;
    //        lastTriggerTime = Time.time;

    //        // Player has entered the trigger zone and cooldown has passed
    //        HandleDoorCollision();
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        playerInTriggerZone = false;
    //    }
    //}

    public void OnTriggerEnter2D(Collider2D other)
    {
        // Differentiate between doors based on their tags
        if (other.gameObject == DoorOne)
        {
            Debug.Log("hit door one");
            // Handle collision with DoorOne
            outside.SetActive(false);
            kitchen.SetActive(false);
            alchemist.SetActive(true);
            // Assuming your player's GameObject has a Transform component (if not, adjust accordingly)
            Transform playerTransform = gameObject.GetComponent<Transform>();

            // Set the player's y-position to its negative value
            playerTransform.position = new Vector3(playerTransform.position.x, -Mathf.Abs(playerTransform.position.y), playerTransform.position.z);

            //CanvasManager.Instance.SwitchCanvas(CanvasManager.Instance.canvases[1]);
        }
        else if (other.gameObject == DoorTwo)
        {
            Debug.Log("hit door two");
            // Handle collision with DoorOne
            outside.SetActive(false);
            kitchen.SetActive(true);
            alchemist.SetActive(false);
            Transform playerTransform = gameObject.GetComponent<Transform>();

            // Set the player's y-position to its negative value
            playerTransform.position = new Vector3(playerTransform.position.x, Mathf.Abs(playerTransform.position.y + 2f), playerTransform.position.z);


            //CanvasManager.Instance.SwitchCanvas(CanvasManager.Instance.canvases[0]);
        }
        else if (other.gameObject == DoorThree)
        {
            Debug.Log("hit door three");
            // Handle collision with DoorOne
            outside.SetActive(true);
            kitchen.SetActive(false);
            alchemist.SetActive(false);
            Transform playerTransform = gameObject.GetComponent<Transform>();

            // Set the player's y-position to its negative value
            playerTransform.position = new Vector3(playerTransform.position.x, Mathf.Abs(playerTransform.position.y + 2f), playerTransform.position.z);

            //CanvasManager.Instance.SwitchCanvas(CanvasManager.Instance.canvases[2]);
        }
        else if (other.gameObject == DoorFour)
        {
            Debug.Log("hit door four");
            // Handle collision with DoorOne
            outside.SetActive(true);
            kitchen.SetActive(false);
            alchemist.SetActive(false);
            Transform playerTransform = gameObject.GetComponent<Transform>();

            // Set the player's y-position to its negative value
            playerTransform.position = new Vector3(playerTransform.position.x, -Mathf.Abs(playerTransform.position.y -2f), playerTransform.position.z);

            //CanvasManager.Instance.SwitchCanvas(CanvasManager.Instance.canvases[2]);
        }
        else if (gameObject.CompareTag("DoorFive"))
        {
           // Trigger next scene
        }
    }
}
