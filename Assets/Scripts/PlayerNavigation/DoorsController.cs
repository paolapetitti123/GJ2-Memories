using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsController : MonoBehaviour
{
    private bool playerInTriggerZone = false;
    private float triggerCooldown = 1f;  // Adjust the cooldown time as needed
    private float lastTriggerTime;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !playerInTriggerZone && Time.time - lastTriggerTime > triggerCooldown)
        {
            playerInTriggerZone = true;
            lastTriggerTime = Time.time;

            // Player has entered the trigger zone and cooldown has passed
            HandleDoorCollision();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTriggerZone = false;
        }
    }

    private void HandleDoorCollision()
    {
        // Differentiate between doors based on their tags
        if (gameObject.CompareTag("DoorOne"))
        {
            // Handle collision with DoorOne
            CanvasManager.Instance.SwitchCanvas(CanvasManager.Instance.canvases[1]);
        }
        else if (gameObject.CompareTag("DoorTwo"))
        {
            // Handle collision with DoorTwo
            CanvasManager.Instance.SwitchCanvas(CanvasManager.Instance.canvases[0]);
        }
        else if (gameObject.CompareTag("DoorThree"))
        {
            // Handle collision with DoorTwo
            CanvasManager.Instance.SwitchCanvas(CanvasManager.Instance.canvases[2]);
        }
        else if (gameObject.CompareTag("DoorFour"))
        {
            // Handle collision with DoorTwo
            CanvasManager.Instance.SwitchCanvas(CanvasManager.Instance.canvases[1]);
        }
        else if (gameObject.CompareTag("DoorFive"))
        {
           // Trigger next scene
        }
    }
}
