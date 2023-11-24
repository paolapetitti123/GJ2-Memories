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

    public bool questOneComplete;

    public bool textOneActive;
    public bool textTwoActive;
    public bool textThreeActive;

    public bool questFinished;


    void Start()
    {
        Conversation1.SetActive(false);
        textOneActive = true;
        textTwoActive = false;
        textThreeActive = false;

        questFinished = false;
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject == guard)
        {
            if (questFinished)
            {
                textThreeActive = true;
                textTwoActive = false;
                textOneActive = false;
                currentTextIndex = 0;
            }
            // Check if the collision is with a specific tag or layer if needed
            if (!quest.isActive)
            {
                Conversation1.SetActive(true);
                Debug.Log("collision");

                // conversation texts
                conversationTexts = new string[]
                {
                "Good day. I see you desire to recover your memories.",
                "To unlock the secrets of the mind, you must embark on a quest for ingredients rare and profound.",
                "You will need these ingredients to complete 3 potions.",
                "Please bring me the ingredients... Good luck!"
                };


                // Display the initial text
                UpdateText(conversationTexts);
            }
            else if (quest.isActive && !questFinished)
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
            else if (quest.isActive && questFinished)
            {
                // text that'll show up when you've accepted the quest but haven't completed it yet
                Conversation1.SetActive(true);
                Debug.Log("collision 3rd text");
                currentTextIndex = 0;
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
                if (quest.isActive && !questFinished)
                {
                    currentTextIndex = 0;
                    textThreeActive = true;
                    textTwoActive = false;
                    questGiver.questWindow.SetActive(false);
                }
                currentTextIndex = 0;
            }
            UpdateText(conversationTextReturn);
        }
        else if (textThreeActive)
        {
            if (currentTextIndex >= conversationTextComplete.Length)
            {
                Conversation1.SetActive(false);
                questOneComplete = true;
                quest.goal.hasSpokenFinish = true;
                if (quest.isActive && questFinished)
                {
                    questOneComplete = true;
                    questGiver.questWindow.SetActive(false);
                    quest.goal.hasSpokenFinish = true;
                }
               
            }
            UpdateText(conversationTextComplete);
        }

    
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
