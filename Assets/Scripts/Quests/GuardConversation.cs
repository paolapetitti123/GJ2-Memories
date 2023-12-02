using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuardConversation : MonoBehaviour
{
    public GameObject ConversationGuard;
    public GameObject player;

    public TMP_Text displayText;
    private string[] conversationTexts;
    private int currentTextIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        ConversationGuard.SetActive(false);
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            ConversationGuard.SetActive(true);
            conversationTexts = new string[]
            {
                "Your Highness, something's terrible has happened.",
                "I'm afraid you've lost your memories.",
                "The town alchemist might hold the key to restoring them.",
                "You must go find them!"
            };

            // Display the initial text
            UpdateText(conversationTexts);
        }
    }


    public void OnButtonClick()
    {
        // when the button is clicked, it increments the index to display the next text
        currentTextIndex++;

        if (currentTextIndex >= conversationTexts.Length)
        {
            ConversationGuard.SetActive(false);
        }
        UpdateText(conversationTexts);

    }

    void UpdateText(string[] conversationTexts)
    {
        if (currentTextIndex < conversationTexts.Length)
        {
            // Change the text of the Text element based on the current conversation text
            displayText.text = conversationTexts[currentTextIndex];
        }

    }


    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            ConversationGuard.SetActive(false);

        }



    }
}
