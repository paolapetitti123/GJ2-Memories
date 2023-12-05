using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Conversation conversation;
    public Texture2D pointerCursor; // Assign your pointer cursor texture in the inspector
    public Texture2D defaultCursor;

    public void OnButtonClick()
    {
        conversation.OnButtonClick();
    }

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
}