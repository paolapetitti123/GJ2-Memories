using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    public PlayerMovement player;

    public GameObject questWindow;
    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public TMP_Text rewardText;
    public Button acceptButton;

    private void Start()
    {

        questWindow.SetActive(false);

    }


    public void OpenQuestWindow()
    {
        if (!quest.isActive)
        {
            questWindow.SetActive(true);
            titleText.text = quest.title;
            descriptionText.text = quest.description;
            rewardText.text = "Reward: " + quest.potionReward.ToString() + " Elixir of Rememberance";
            acceptButton.onClick.AddListener(AcceptQuest);
        }
        else if(quest.isActive)
        {
            questWindow.SetActive(false);
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
