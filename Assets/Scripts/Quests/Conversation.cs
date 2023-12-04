using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Conversation : MonoBehaviour
{
    public TMP_Text displayText;
    public TMP_Text guardText;
    public TMP_Text chefText;


    //private string[] conversationTexts;
    private string[] conversationAlchemistTexts;
    private string[] conversationAlchemistFinalTexts;
    private string[] conversationTextReturn;
    private string[] conversationTextComplete;


   
    private string[] conversationGuardTexts;
    private string[] conversationChefTexts;

    private int currentTextIndex = 0;
    private int currentGuardTextIndex = 0;
    private int currentChefTextIndex = 0;


    public GameObject Conversation1;
    public GameObject alchemist;

    public GameObject ConversationGuard;
    public GameObject Guard;
    public Transform targetPosition;
    public float speed = 5f;


    public GameObject ChefConversation;
    public GameObject Chef;
    public GameObject chefsTable;

    public QuestGiver questGiver;
    public Quest quest;

    public bool questOneComplete;

    public bool textOneActive;
    public bool textTwoActive;
    public bool textThreeActive;
    public bool textFourActive;

    public bool questFinished;

    public bool GuardConvoActive;
    public int guardConvoCount;
    public  int ChefConvoCount;


    public bool potionDrank;
    public UI_Inventory uiInv;

    public GameObject chefBubble;
    public GameObject alchemistBubble;

    public bool firstTalkAlchemist;
    void Start()
    {
        Conversation1.SetActive(false);
        ChefConversation.SetActive(false);
        textOneActive = true;
        textTwoActive = false;
        textThreeActive = false;
        textFourActive = false;

        questFinished = false;
        guardConvoCount = 0;
        ChefConvoCount = 0;

        alchemistBubble.SetActive(true);
        chefBubble.SetActive(false);
        firstTalkAlchemist = false;
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
                alchemistBubble.SetActive(false);
                chefBubble.SetActive(true);
                Conversation1.SetActive(true);
                firstTalkAlchemist = true;
                Debug.Log("collision");

                // conversation texts
                conversationAlchemistTexts = new string[]
                {
                "Ah, Prince, I've been expecting you. In search of the Elixir of Remembrance, aren't you?",
                "To unlock the secrets of the mind, you must embark on a quest for ingredients rare and profound for me.",
                "Bring these to me and only then shall I create what you seek."
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
            else if (quest.isActive && questFinished && potionDrank == false)
            {
                // text that'll show up when you've accepted the quest but haven't completed it yet
                Conversation1.SetActive(true);
                alchemistBubble.SetActive(false);
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
            else if (quest.isActive && questFinished && potionDrank )
            {
                Debug.Log("potion drank in Conversation");
                // Alchemist last words pop up
                Conversation1.SetActive(true);
                AudioController.Instance.PlaySoundGameplayNoRepeat("alchemist_convo_1");

                Debug.Log("collision");

                // conversation texts
                conversationAlchemistFinalTexts = new string[]
                {
                        "By mixing the essence of celestial energies and earthly elements,",
                        "I've crafted a portal to lead you to your destiny – the throne awaits your rightful claim"
                };


                // Display the initial text
                UpdateText(conversationAlchemistTexts);
            }
        }

        if(other.gameObject == Guard && guardConvoCount == 0)
        {
            GuardConvoActive = true;

            ConversationGuard.SetActive(true);
            conversationGuardTexts = new string[]
            {
                "Your Royal Highness, something terrible has happened.",
                "I'm afraid you've lost your memories.",
                "The town alchemist might hold the key to restoring them.",
                "The coronation is tomorrow and without your memories you've been deemed unfit for king!",
                "You must go north and find the alchemist before sunset!",
                "I must be getting back to my post now, so I won't be of much help",
                "I wish you the best of luck!"

            };

            // Display the initial text
            UpdateGuardText(conversationGuardTexts);
        }
       
        if(other.gameObject == Chef && firstTalkAlchemist)
        {
            ChefConversation.SetActive(true);
            chefBubble.SetActive(false);

            conversationChefTexts = new string[]
            {
                "Your Highness, take any tool you need from my stash.",
                "But fair warning, they're old and may might not survive more than one use.",
                "I hope they will serve you well, even just for a moment."
            };

            UpdateChefText(conversationChefTexts);
        }

    }

    public void OnButtonClick()
    {
        // when the button is clicked, it increments the index to display the next text
        if (guardConvoCount == 1 && Conversation1.activeInHierarchy)
        {
            currentTextIndex++;
            if (textOneActive)
            {
                //currentTextIndex = 0;

                if (currentTextIndex >= conversationAlchemistTexts.Length)
                {
                   
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
                    Conversation1.SetActive(false);

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
                        textThreeActive = false;
                        textFourActive = false;
                        
                    }

                }
                UpdateText(conversationTextComplete);
            }
            else if (textFourActive)
            {
                if(currentTextIndex >= conversationAlchemistFinalTexts.Length)
                {
                    Conversation1.SetActive(false);

                }
                UpdateText(conversationAlchemistFinalTexts);
            }

        }
        // if we reach the end of the conversation, disable the conversation
        else if (guardConvoCount == 0)
        {
            currentGuardTextIndex++;

            if (currentGuardTextIndex >= conversationGuardTexts.Length)
            {
                ConversationGuard.SetActive(false);
                GuardConvoActive = false;
                Animator guardWalk = Guard.GetComponent<Animator>();
                guardConvoCount = 1;
                guardWalk.Play("Guard-walk-anim");
                
            }
            UpdateGuardText(conversationGuardTexts);
        }

        else if (guardConvoCount == 1 && ChefConversation.activeInHierarchy)
        {
            currentChefTextIndex++;
            if(currentChefTextIndex >= conversationChefTexts.Length)
            {
                ChefConversation.SetActive(false);
                ChefConvoCount = 1;
                chefsTable.GetComponent<BoxCollider2D>().size = new Vector2(4.4f, 1.1f);
                chefsTable.GetComponent<BoxCollider2D>().offset = new Vector2(0.04f, 0.96f);
            }
            UpdateChefText(conversationChefTexts);
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

        potionDrank = uiInv.potionDrankStatus;
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

    void UpdateChefText(string[] currentChefText)
    {
        if (currentChefTextIndex < currentChefText.Length)
        {
            // Change the text of the Text element based on the current conversation text
            chefText.text = currentChefText[currentChefTextIndex];
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

        else if (collision.gameObject == Chef)
        {
            ChefConversation.SetActive(false);
        }
    }


    private IEnumerator DisableGuard()
    {
        yield return new WaitForSeconds(3f);

        Guard.SetActive(false);
    }
}
