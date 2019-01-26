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

    float startingTime;
    float endingTime;
    float timeSinceStart;
    float secondsLeft;

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
        if (  !timerEnded)
        {
            timeSinceStart = Time.time - startingTime; 
            secondsLeft = 300 - timeSinceStart;


            timerText.text = GetTimestamp(secondsLeft);
            if (300 - timeSinceStart < 0)
            {
                timerEnded = true;
                // gameState.BuildingBlowsUp();
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
            timerText.text = "Congratulations! Time remaining: " + GetTimestamp(secondsLeft);
        }
    }

    private string GetTimestamp(float s) {

        int minutes = (int)(Math.Truncate((double)(s / 60)));
        int seconds = (int)(s % 60);

        return minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}