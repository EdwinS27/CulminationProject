using System.Collections;
using System.Collections.Generic;
using Unity;
using UnityEngine;
using UnityEditor.UI;
public class GameManager : MonoBehaviour    {
    public GameObject enemyMinion;
    public GameObject enemyBombMinion;
    private float time = 0; // seconds
    private float timeMin = 0;  // mins
    private float timeHour = 0; // ???????????????? why are you still playing this long
    private int intensity = 1;
    public Text gameTimer; // Format: 00 minutes : 00 seconds
    public Text playerScore; // Format : Kills / Deaths
    public GenericChampion champion;
    
    void Start() {
    }

    void Update()   {
        incrementTime();
        setTime();
    }
    void incrementTime()    {
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
        if(time % 3 == 0){
            Debug.Log("Player should get more Mana");
            monitorChampionResource();
        }
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
    void monitorChampionResource()  {
        if(champion.statsScript.GetMana() < champion.statsScript.GetMaxMana()) {
            Debug.Log(champion.statsScript.GetMana() + " " + champion.statsScript.GetMaxMana());
            champion.statsScript.AddMana(8.5f);
        }
    }

    void adjustEnemyXPGift()    {
        
    }
}