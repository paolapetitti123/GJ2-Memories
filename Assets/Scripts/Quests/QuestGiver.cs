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

    private void Start()
    {
       // titleText = GameObject.Find("QuestTitle").GetComponent<TextMeshPro>();
        //descriptionText = GameObject.Find("QuestDescription").GetComponent<TextMeshPro>();
        // rewardText = GameObject.Find("QuestReward").GetComponent<TextMeshPro>();

        questWindow.SetActive(false);

    }


    public void OpenQuestWindow()
    {
        questWindow.SetActive(true);
        titleText.text = quest.title;
        descriptionText.text = quest.description;
        rewardText.text =  quest.potionReward.ToString();
    }
}
