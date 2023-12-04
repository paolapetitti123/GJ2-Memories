using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CursorChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Texture2D pointerCursor; // Assign your pointer cursor texture in the inspector
    public Texture2D defaultCursor; // Assign your default cursor texture in the inspector

    public GameObject[] itemSlots;

    private void Start()
    {
        // Set the default cursor initially
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }

    private void Update()
    {
        // Iterate through item slots and check for changes
        CheckForItemSlotChanges();
    }

    // Called when the mouse pointer enters the UI element
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (IsHoveringOverValidItemSlot(eventData.pointerEnter))
        {
            Cursor.SetCursor(pointerCursor, Vector2.zero, CursorMode.Auto);
        }
        
        
    }

    // Called when the mouse pointer exits the UI element
    public void OnPointerExit(PointerEventData eventData)
    {
        // Change cursor back to default when not hovering
            Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }

    private void CheckForItemSlotChanges()
    {
        foreach (GameObject itemSlot in itemSlots)
        {
            if (IsHoveringOverValidItemSlot(itemSlot))
            {
                // Change cursor to pointer when hovering over a valid item slot
                Cursor.SetCursor(pointerCursor, Vector2.zero, CursorMode.Auto);
                return; // Exit the loop early if a valid item slot is found
            }
        }

        // Change cursor back to default if not hovering over a valid item slot
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }

    private bool IsHoveringOverValidItemSlot(GameObject hoveredObject)
    {
        if(hoveredObject == null)
        {
            return false;
        }

        Image imageComponent = hoveredObject.GetComponent<Image>();
        if(imageComponent == null)
        { 
            return false; 
        }

        // Check if the image name is either "Axe" or "Square"
        string imageName = imageComponent.sprite ? imageComponent.sprite.name : "";
        return imageName == "Axe" || imageName == "Square";
    }
}
