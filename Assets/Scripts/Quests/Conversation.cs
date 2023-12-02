using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Conversation : MonoBehaviour
{
    public TMP_Text displayText;
    public TMP_Text guardText;
    private string[] conversationTexts;
    private string[] conversationAlchemistTexts;
    private string[] conversationGuardTexts;
    private string[] conversationTextReturn;
    private string[] conversationTextComplete;
    private int currentTextIndex = 0;
    private int currentGuardTextIndex = 0;


    public GameObject Conversation1;
    public GameObject alchemist;

    public GameObject ConversationGuard;
    public GameObject Guard;
    public Transform targetPosition;
    public float speed = 5f;

    public QuestGiver questGiver;
    public Quest quest;

    public bool questOneComplete;

    public bool textOneActive;
    public bool textTwoActive;
    public bool textThreeActive;

    public bool questFinished;

    public bool GuardConvoActive;
    public int guardConvoCount;

    void Start()
    {
        Conversation1.SetActive(false);
        textOneActive = true;
        textTwoActive = false;
        textThreeActive = false;

        questFinished = false;
        guardConvoCount = 0;
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject == alchemist)
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
                // Trigger convo audio
                AudioController.Instance.PlaySoundGameplayNoRepeat("alchemist_convo_1");

                Conversation1.SetActive(true);
                Debug.Log("collision");

                // conversation texts
                conversationAlchemistTexts = new string[]
                {
                "Good day. I see you desire to recover your memories.",
                "To unlock the secrets of the mind, you must embark on a quest for ingredients rare and profound.",
                "You will need these ingredients so I can craft my potion... Good luck!"
                };

                Debug.Log(conversationAlchemistTexts.ToString());

                // Display the initial text
                UpdateText(conversationAlchemistTexts);
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
                "I see you have brought me the ingredients for the elixir of rememberance.",
                "Here you go, one elixir of rememberance, as promised.",
                "Drink up"
                };


                // "Come see me once you're ready to gather ingredients for the next elixir."
                // Display the initial text
                UpdateText(conversationTextComplete);
            }
        }

        if(other.gameObject == Guard && guardConvoCount == 0)
        {
            GuardConvoActive = true;

            ConversationGuard.SetActive(true);
            conversationGuardTexts = new string[]
            {
                "Your Highness, something's terrible has happened.",
                "I'm afraid you've lost your memories.",
                "The town alchemist might hold the key to restoring them.",
                "You must go north and find them!"
            };

            // Display the initial text
            UpdateGuardText(conversationGuardTexts);
        }
       

    }

    public void OnButtonClick()
    {
        // when the button is clicked, it increments the index to display the next text
        if (guardConvoCount == 1)
        {
            currentTextIndex++;
            if (textOneActive)
            {
                //currentTextIndex = 0;

                if (currentTextIndex >= conversationAlchemistTexts.Length)
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
                UpdateText(conversationAlchemistTexts);
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
        // if we reach the end of the conversation, disable the conversation
        else if (guardConvoCount == 0)
        {
            currentGuardTextIndex++;

            if (currentGuardTextIndex >= conversationGuardTexts.Length)
            {
                ConversationGuard.SetActive(false);
                Animator guardWalk = Guard.GetComponent<Animator>();
                guardConvoCount = 1;
                guardWalk.Play("Guard-walk-anim");
                
            }
            UpdateGuardText(conversationGuardTexts);
        }

    }

    private void Update()
    {
        if(guardConvoCount == 1)
        {
            float distance = Vector3.Distance(Guard.transform.position, targetPosition.position);

            if (distance > 0.1f)
            {
                // Calculate the direction towards the target position
                Vector3 direction = (targetPosition.position - Guard.transform.position).normalized;

                // Move the object towards the target position
                Guard.transform.Translate(direction * speed * Time.deltaTime);
                StartCoroutine(DisableGuard());
            }

        }
    }

    void UpdateText(string[] conversationTexts)
    {
        Debug.Log(conversationTexts.ToString());
        if (currentTextIndex < conversationTexts.Length)
        {
            // Change the text of the Text element based on the current conversation text
            displayText.text = conversationTexts[currentTextIndex];
        }
    }


    void UpdateGuardText(string[] currentGuardText)
    {
        if (currentGuardTextIndex < currentGuardText.Length)
        {
            // Change the text of the Text element based on the current conversation text
            guardText.text = currentGuardText[currentGuardTextIndex];
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == alchemist)
        {
            Conversation1.SetActive(false);
        }

        else if (collision.gameObject == Guard)
        {
            ConversationGuard.SetActive(false);
        }
    }


    private IEnumerator DisableGuard()
    {
        yield return new WaitForSeconds(3f);

        Guard.SetActive(false);
    }
}
