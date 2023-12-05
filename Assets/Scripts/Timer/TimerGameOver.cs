using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TimerGameOver : MonoBehaviour
{

    private void OnEnable()
    {
        TimeManager.OnMinuteChanged += TimeCheck;
        TimeManager.OnHourChanged += TimeCheck;
    }

    private void OnDisable()
    {
        TimeManager.OnMinuteChanged -= TimeCheck;
        TimeManager.OnHourChanged -= TimeCheck;
    }


    private void TimeCheck()
    {
     

        if(TimeManager.Hour == 15 && TimeManager.Minute == 59)
        {
            GameStateManager.Instance.LoadScene(GameStateManager.Scene.GameLose);
        }
    }


}
