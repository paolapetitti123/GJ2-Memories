using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    public PlayerMovement player;
    public GameObject questIcon;
    public GameObject questWindow;
    //public TMP_Text titleText;
    //public TMP_Text descriptionText;
    //public TMP_Text rewardText;
    public Button exitButton;
    public Texture2D defaultCursor;

    private void Start()
    {

        questWindow.SetActive(false);
        questIcon.SetActive(false);
        questIcon.GetComponent<Button>().onClick.AddListener(OnQuestIconClick);


    }

    public void OnQuestIconClick()
    {
        Debug.Log("Quest Icon Clicked");

        questWindow.SetActive(true);
        //titleText.text = quest.title;
        //descriptionText.text = quest.description;
        //rewardText.text = "Reward: " + quest.potionReward.ToString() + " Elixir of Rememberance";
        exitButton.onClick.AddListener(AcceptQuest);
    }

    public void OpenQuestWindow()
    {
        if (!quest.isActive)
        {
            questWindow.SetActive(true);
            questIcon.SetActive(true);
            //titleText.text = quest.title;
            //descriptionText.text = quest.description;
            //rewardText.text = "Reward: " + quest.potionReward.ToString() + " Elixir of Rememberance";
            exitButton.onClick.AddListener(AcceptQuest);
        }
        else if(quest.isActive)
        {
            questWindow.SetActive(false);
            Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
        }
        
    }

    public void AcceptQuest()
    {
        questWindow.SetActive(false);
        quest.isActive = true;

        // give to player
        player.quest = quest;
    }

}
