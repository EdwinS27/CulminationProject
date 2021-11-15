using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour    {
    float time = 0; // seconds
    float timeMin = 0;  // mins
    float timeHour = 0; // ????????????????
    public Text gameTimer; // Format: 00 minutes : 00 seconds
    public Text playerScore; // Format : Kills / Deaths
    
    void Start() {
        
    }

    void Update()   {
        if(time > 59)   {
            time = 0;
            if(timeMin < 59)
                timeMin++;
            else{
                timeHour++;
                timeMin = 0;
            }
        }
        else
            time += Time.deltaTime;
        setTime();
    }

    void setTime()  {
        string timeFormat = "";
        // minutes
        if(timeMin < 1)
            timeFormat += "00 : ";
        else if(timeMin < 9)
            timeFormat += "0 "+ timeMin + " : ";
        else
            timeFormat += timeMin + " : ";
        // seconds
        if(time < 9)
            timeFormat += "0" + Mathf.Round(time);
        else
            timeFormat += Mathf.Round(time);
        gameTimer.text = timeFormat;
    }
}