using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public List<Canvas> canvases;  // List of all canvases in the game
    private Canvas currentCanvas;   // The currently active canvas

    // Singleton instance for easy access
    private static CanvasManager _instance;
    public static CanvasManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("CanvasManager is not initialized.");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        // No need to check for _instance or use DontDestroyOnLoad
        _instance = this;
    }

    void Start()
    {
        // Initialize the canvas manager with the first canvas
        SwitchCanvas(canvases[0]);
    }

    public void SwitchCanvas(Canvas newCanvas)
    {
        // Deactivate the current canvas
        if (currentCanvas != null)
        {
            currentCanvas.gameObject.SetActive(false);
        }

        // Activate the new canvas
        newCanvas.gameObject.SetActive(true);

        // Update the current canvas reference
        currentCanvas = newCanvas;

        // Log Canvas everytime it switches
        Debug.Log("The current canvas is " + currentCanvas);
    }
}
