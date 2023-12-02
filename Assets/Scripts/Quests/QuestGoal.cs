using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuestGoal 
{
    public GoalType goalType;

    public int requiredAmount = 7;
    public int currentAmount = 0;



    public bool itemsCollected;

    public int count = 0;
    public bool hasSpokenFinish;


    public bool IsReached()
    {
        if (currentAmount >= requiredAmount)
            return itemsCollected = true;
        else return itemsCollected= false;
    }

    public bool HasSpoken()
    {
        if (count >= 1)
            return hasSpokenFinish = true;
        else return hasSpokenFinish = false;
    }

    public void TalkingCounter()
    {
        count++;
    }

    public void IngredientGathered()
    {
        currentAmount++;
    }


}

public enum GoalType
{
    Gathering
}