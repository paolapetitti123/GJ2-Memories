using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Serializable class to hold information about each door
[System.Serializable]
public class DoorInfo
{
    public Vector2 position;        // Position of the door
    public Vector2 size;            // Size of the door
    public Canvas targetRoomCanvas; // Canvas of the room the door leads to
    public Sprite doorSprite;       // Sprite for the door
}

public class RoomsManager : MonoBehaviour
{
    // Door sprites for different doors
    public Sprite doorSprite1;
    public Sprite doorSprite2;
    public Sprite doorSprite3;

    // Prefab for the door GameObject
    public GameObject doorPrefab;

    // Canvases representing different rooms
    public Canvas destinationCanvas1;
    public Canvas destinationCanvas2;
    public Canvas destinationCanvas3;

    // List to hold door information
    private List<DoorInfo> doorInfos = new List<DoorInfo>();

    // List to keep track of instantiated doors
    private List<GameObject> instantiatedDoors = new List<GameObject>();

    // Static instance variable for easy access
    public static RoomsManager Instance;

    // Variable to track the current canvas
    private Canvas currentCanvas;

    private void Awake()
    {
        Instance = this;            // Assign the instance on awake
        currentCanvas = destinationCanvas1; // Set the initial canvas
    }

    void Start()
    {
        SetupDoors();               // Call a method to set up door information
        InstantiateDoorsForCurrentCanvas(); // Call a method to instantiate all doors when the room is initialized
    }

    void SetupDoors()
    {
        // Define door information for each room
        doorInfos.Add(new DoorInfo { position = new Vector2(-17f, 5f), size = new Vector2(1f, 1.5f), targetRoomCanvas = destinationCanvas1, doorSprite = doorSprite1 });
        doorInfos.Add(new DoorInfo { position = new Vector2(17f, 5f), size = new Vector2(1f, 1f), targetRoomCanvas = destinationCanvas2, doorSprite = doorSprite2 });
        doorInfos.Add(new DoorInfo { position = new Vector2(17f, -1f), size = new Vector2(1f, 1f), targetRoomCanvas = destinationCanvas2, doorSprite = doorSprite3 });
        doorInfos.Add(new DoorInfo { position = new Vector2(-17f, 2f), size = new Vector2(1f, 1f), targetRoomCanvas = destinationCanvas3, doorSprite = doorSprite3 });
    }

    public void InstantiateDoorsForCurrentCanvas()
    {
        Debug.Log($"Instantiating doors for {currentCanvas.name}");

        DestroyExistingDoors(); // Destroy existing doors before instantiating new ones

        foreach (var doorInfo in doorInfos)
        {
            // Only instantiate doors if the condition is met
            if (doorInfo.targetRoomCanvas == currentCanvas)
            {
                GameObject newDoor = InstantiateDoor(doorInfo.position, doorInfo.size, doorInfo.targetRoomCanvas, doorInfo.doorSprite);
                instantiatedDoors.Add(newDoor);
            }
        }
    }

    GameObject InstantiateDoor(Vector2 position, Vector2 size, Canvas targetRoomCanvas, Sprite doorSprite)
    {
        Debug.Log($"Instantiating door on canvas: {targetRoomCanvas.name}");

        // Instantiate the door prefab at the specified position
        GameObject newDoor = Instantiate(doorPrefab, new Vector3(position.x, position.y, 0f), Quaternion.identity);

        // Set the size of the door
        newDoor.transform.localScale = new Vector3(size.x, size.y, 1f);

        // Attach the DoorsController script to the instantiated door
        DoorsController doorsController = newDoor.GetComponent<DoorsController>();

        // Check if the DoorsController component is attached
        if (doorsController != null)
        {
            // Set the destination room canvas for the door
            doorsController.targetRoomCanvas = targetRoomCanvas;

            // Get the SpriteRenderer component from the DoorsController
            SpriteRenderer doorSpriteRenderer = doorsController.GetComponent<SpriteRenderer>();

            // Check if the SpriteRenderer component is attached
            if (doorSpriteRenderer != null)
            {
                // Set the sprite for the door
                doorSpriteRenderer.sprite = doorSprite;
            }
            else
            {
                Debug.LogError("SpriteRenderer component not found on DoorsController.");
            }
        }
        else
        {
            Debug.LogError("DoorsController component not found on the instantiated door prefab.");
        }

        return newDoor;
    }

    void DestroyExistingDoors()
    {
        foreach (var door in instantiatedDoors)
        {
            // Log the door being destroyed
            Debug.Log($"Destroying door on canvas: {door.GetComponent<DoorsController>().targetRoomCanvas.name}");
            Destroy(door);
        }

        instantiatedDoors.Clear(); // Clear the list after destroying doors
    }

    public void SwitchCanvas()
    {
        Debug.Log($"Switching canvas from {currentCanvas.name}");

        SetCanvasActive(currentCanvas, false); // Disable the current canvas
        DestroyExistingDoors(); // Destroy existing doors before switching canvas

        // Switch to the next canvas
        if (currentCanvas == destinationCanvas1)
        {
            currentCanvas = destinationCanvas2;
        }
        else if (currentCanvas == destinationCanvas2)
        {
            currentCanvas = destinationCanvas3;
        }
        else
        {
            currentCanvas = destinationCanvas1;
        }

        SetCanvasActive(currentCanvas, true); // Enable the new current canvas
        InstantiateDoorsForCurrentCanvas(); // Instantiate new doors for the current canvas

        Debug.Log($"Switched to {currentCanvas.name}");
    }

    private void SetCanvasActive(Canvas canvas, bool isActive)
    {
        if (canvas != null)
        {
            canvas.gameObject.SetActive(isActive);
        }
    }
}
