using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Conversation : MonoBehaviour
{
    public TMP_Text displayText;
    private string[] conversationTexts;
    private int currentTextIndex = 0;
    public GameObject Conversation1;
    public GameObject guard;

    void Start()
    {

        Conversation1.SetActive(false);
     
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collision is with a specific tag or layer if needed
        if (other.gameObject == guard)
        {
            Conversation1.SetActive(true);
            Debug.Log("collision");

            // conversation texts
            conversationTexts = new string[]
            {
            "Hello! First text Azmatttt hiiii",
            "Second text option",
             "Second text option siukeeeeee",
            "Third text option lalalalala."
            };


            // Display the initial text
            UpdateText();
        }
    }

    public void OnButtonClick()
    {
        // when the button is clicked, it increments the index to display the next text
        currentTextIndex++;

        // if we reach the end of the conversation, disable the conversation
        if (currentTextIndex >= conversationTexts.Length)
        {
            Conversation1.SetActive(false);
        }

        // Update the text
        UpdateText();
    }

    void UpdateText()
    {
        if (currentTextIndex < conversationTexts.Length)
        {
            // Change the text of the Text element based on the current conversation text
            displayText.text = conversationTexts[currentTextIndex];
        }
       
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == guard)
        {
            Conversation1.SetActive(false);
        }
    }
}
