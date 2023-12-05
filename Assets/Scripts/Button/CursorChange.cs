using UnityEngine;
using UnityEngine.EventSystems;

public class CursorChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Texture2D pointerCursor; // Assign your pointer cursor texture in the inspector
    public Texture2D defaultCursor; // Assign your default cursor texture in the inspector


    private void Start()
    {
        // Set the default cursor initially
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }


    // Called when the mouse pointer enters the UI element
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Change cursor to pointer when hovering
        Cursor.SetCursor(pointerCursor, Vector2.zero, CursorMode.Auto);
    }

    // Called when the mouse pointer exits the UI element
    public void OnPointerExit(PointerEventData eventData)
    {
        // Change cursor back to default when not hovering
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }
}
