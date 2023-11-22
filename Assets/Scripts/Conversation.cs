using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Conversation : MonoBehaviour
{
    public TMP_Text displayText;
    private string[] conversationTexts;
    private string[] conversationTextReturn;
    private string[] conversationTextComplete;
    private int currentTextIndex = 0;
    public GameObject Conversation1;
    public GameObject guard;

    public QuestGiver questGiver;

    public Quest quest;

    public bool questOneComplete = false;

    public bool textOneActive;
    public bool textTwoActive;
    public bool textThreeActive;

    void Start()
    {
        Conversation1.SetActive(false);
        textOneActive = true;
        textTwoActive = false;
        textThreeActive = false;
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collision is with a specific tag or layer if needed
        if (other.gameObject == guard && !quest.isActive)
        {
            Conversation1.SetActive(true);
            Debug.Log("collision");

            // conversation texts
            conversationTexts = new string[]
            {
                "Good day. What have you come here searching for?",
                "Ahhhh I see. You desire to recover your memories.",
                "Potions you will need... Not 1, 2 but 3!",
                "To unlock the secrets of the mind, you must embark on a quest for ingredients rare and profound.",
            "First you need the elixir of remembrance!",
        "The ingredients I need are: a mushroom, an herb, a flower...",
            "Please bring me the ingredients to complete the elixir of remembrance ."
            };


            // Display the initial text
            UpdateText(conversationTexts);
        }
        
        if (other.gameObject == guard && quest.isActive && textTwoActive)
        {
            // text that'll show up when you've accepted the quest but haven't completed it yet

            Conversation1.SetActive(true);
            Debug.Log("collision 2nd text");

            // conversation texts
            conversationTextReturn = new string[]
            {
                "Well what are you waiting for..?", 
                "I'll be waiting here for those ingredients"
            };


            // Display the initial text
            UpdateText(conversationTextReturn);

        }
        
        if (other.gameObject == guard && quest.isActive && textThreeActive)
        {
            // text that'll show up when you've accepted the quest but haven't completed it yet
            questOneComplete = true;
            Conversation1.SetActive(true);
            Debug.Log("collision 3rd text");

            // conversation texts
            conversationTextComplete = new string[]
            {
                "I see you have brought me the ingredients for the exlir of rememberance.",
                "Here you go, one elixir of rememberance, as promised.", 
                "Come see me once you're ready to gather ingredients for the next elixir."
            };
            // Display the initial text
            UpdateText(conversationTextComplete);
        }

    }

    public void OnButtonClick()
    {
        // when the button is clicked, it increments the index to display the next text
        currentTextIndex++;
        
        // if we reach the end of the conversation, disable the conversation
        if (textOneActive)
        {
            if (currentTextIndex >= conversationTexts.Length)
            {
                Conversation1.SetActive(false);
                if (!quest.isActive)
                {
                    questGiver.OpenQuestWindow();
                    currentTextIndex = 0;
                    quest.isActive = true;
                    textTwoActive = true;
                    textOneActive = false;
                }
                else if (quest.isActive)
                {
                    textTwoActive = true;
                    textOneActive = false;
                    questGiver.questWindow.SetActive(false);
                }
                
            }
            UpdateText(conversationTexts);
        }
        else if (textTwoActive)
        {
            if (currentTextIndex >= conversationTextReturn.Length)
            {
                Conversation1.SetActive(false);
                if (quest.isActive && !quest.goal.IsReached())
                {
                    questGiver.questWindow.SetActive(false);
                    currentTextIndex = 0;
                }
                else if (quest.isActive && quest.goal.IsReached())
                {
                    currentTextIndex = 0;
                    textThreeActive = true;
                    textTwoActive = false;
                    questGiver.questWindow.SetActive(false);
                }

            }
            UpdateText(conversationTextReturn);
        }
        else if (textThreeActive && !textOneActive && !textTwoActive)
        {
            Conversation1.SetActive(false);

            if (currentTextIndex >= conversationTextReturn.Length)
            {
                if (quest.isActive && !quest.goal.IsReached())
                {
                    questGiver.questWindow.SetActive(false);

                }
                else if (quest.isActive && quest.goal.IsReached())
                {
                    questGiver.questWindow.SetActive(false);
                    questOneComplete = true;
                }
               
            }
            UpdateText(conversationTextComplete);
        }

        Debug.Log(currentTextIndex);
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
        if (collision.gameObject == guard)
        {
            Conversation1.SetActive(false);
          
        }
    }
}
