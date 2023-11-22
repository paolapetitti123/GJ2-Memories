using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuestGoal 
{
    public GoalType goalType;

    public int requiredAmount;


    public int currentAmount;


    public bool IsReached()
    {
        
        return (currentAmount >= requiredAmount);
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