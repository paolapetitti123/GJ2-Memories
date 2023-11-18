using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsController : MonoBehaviour
{
    public Canvas targetRoomCanvas;  // Only one target room for simplicity

    private SpriteRenderer doorSpriteRenderer;

    private bool playerInTriggerZone = false;
    private float triggerCooldown = 1f;  // Adjust the cooldown time as needed
    private float lastTriggerTime;

    private void Start()
    {
        // Get the SpriteRenderer component
        doorSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetDoorSprite(Sprite doorSprite)
    {
        // Set the sprite for the door
        doorSpriteRenderer.sprite = doorSprite;
    }

   private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.CompareTag("Player") && !playerInTriggerZone && Time.time - lastTriggerTime > triggerCooldown)
    {
        playerInTriggerZone = true;
        lastTriggerTime = Time.time;

        if (RoomsManager.Instance != null)
        {
            RoomsManager.Instance.SwitchCanvas();
        }
        else
        {
            Debug.LogError("RoomsManager.Instance is null!");
        }
    }
}


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInTriggerZone = false;
        }
    }
}
