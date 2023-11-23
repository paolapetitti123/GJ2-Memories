using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Quest 
{
    public bool isActive;

    public string title = "Ingredient Gathering";
    public string description = "If you want the first of 3 potions, I will need from you a Mushroom, an herb and a flower.";
    public int potionReward = 1;

    public QuestGoal goal;

   
}
