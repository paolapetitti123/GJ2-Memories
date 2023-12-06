using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    public static Action OnMinuteChanged;
    public static Action OnHourChanged;

    public static int Minute { get; private set; }
    public static int Hour { get; private set; }

    private float minuteToRealTime = 1f;
    private float timer;

    public GameObject guardConvo;


    // Start is called before the first frame update
    void Start()
    {
        Minute = 0;
        Hour = 10;
        timer = minuteToRealTime;

        
   
    }

    // Update is called once per frame
    void Update()
    {
        if (!guardConvo.activeInHierarchy)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                Minute++;
                OnMinuteChanged?.Invoke();
                if (Minute >= 60)
                {
                    Hour++;
                    Minute = 0;
                    OnHourChanged?.Invoke();
                    
                }

                timer = minuteToRealTime;
            }
        }
        
    }
}
