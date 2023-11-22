using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuestGoal 
{
    public GoalType goalType;

    private int requiredAmount = 3;


    public int currentAmount;

    public bool questCompletion = false;

    public bool itemsCollected;

    Conversation convo;


    public bool IsReached()
    {
        if(currentAmount >= requiredAmount)
        {
            itemsCollected = true;
        }
        else
        {
            itemsCollected = false;
        }
        return itemsCollected;
    }

    public bool hasSpokenToAlchemist()
    {
        if (convo.questOneComplete)
        {
            return questCompletion = true;
        }
        else
        {
            return questCompletion = false;
        }
        
    }

    public void IngredientGathered()
    {
        
        
        if (goalType == GoalType.Gathering)
        {
          currentAmount++;
        }
        
    }
}

public enum GoalType
{
    Gathering
}