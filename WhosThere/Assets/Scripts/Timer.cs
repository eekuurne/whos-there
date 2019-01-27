using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    //5 hrs - 5 min
    //300 min - 300 sec


    [SerializeField] Text timerText;
    readonly int secondsLimit = 300;
    readonly int startingHour = 17;

    float startingTime;
    float secondsSinceStart;

    bool timerStarted;
    bool timerEnded;

   // Game  gameState;

    void Start()
    {
        timerStarted = false;
        timerEnded = false;
      //  gameState = FindObjectOfType<GameState>();
    }

    void Update()
    {
        if (!timerEnded)
        {
            secondsSinceStart = Time.time - startingTime; 

            timerText.text = GetTimestamp(secondsSinceStart + (startingHour * 60));
            timerEnded |= secondsSinceStart >= secondsLimit;

            if (secondsSinceStart > (secondsLimit - 60))
            {
                // Change color of timer text to red when 60 seconds left
                timerText.color = UnityEngine.Color.red;
            }
        }
    }

    public void StartTimer()
    {
        if (!timerStarted)
        {
            timerText.gameObject.SetActive(true);
            startingTime = Time.time;
            timerStarted = true;
        }
    }

    public void EndTimer()
    {
        if (!timerEnded)
        {
            timerEnded = true;
            timerText.text = "Congratulations! Time remaining: " + GetTimestamp(secondsLimit - secondsSinceStart);
        }
    }

    private string GetTimestamp(float s) {

        int minutes = (int)(Math.Truncate((double)(s / 60)));
        int seconds = (int)(s % 60);

        return minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}