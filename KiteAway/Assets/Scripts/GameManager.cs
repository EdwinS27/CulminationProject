using System.Collections;
using System.Collections.Generic;
using Unity;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour    {
    public GameObject location1;
    public GameObject location2;
    public GameObject location3;
    public GameObject location4;
    public GameObject enemyMinion;
    public GameObject enemyBombMinion;
    // float armor = 10;
    float speed = .5f;
    float maxHealth = 25;
    float magicResist = 25;
    private float timeUntilRegen = 5f;
    private float timeUntilSpawnMinions = 0;
    private float timeUntilSpawnBombers = 0;
    private float spawnDelayMinions  = 30f;
    private float spawnDelayBombers  = 45f;
    private float time = 0; // seconds
    private float timeMin = 0;  // mins
    private float timeHour = 0; // ???????????????? why are you still playing this long
    private int intensity = 1;
    public Text gameTimer; // Format: 00 minutes : 00 seconds
    public Text playerScore; // Format : Kills / Deaths
    public GenericChampion champion;
    void EnemySpawner(){
        if (timeUntilSpawnMinions > 0)
            timeUntilSpawnMinions -= Time.deltaTime;
        else{
            spawnEnemyMinions();
            timeUntilSpawnMinions = spawnDelayMinions;
        }
        if (timeUntilSpawnBombers > 0)
            timeUntilSpawnBombers -= Time.deltaTime;
        else{
            spawnEnemyBombers();
            timeUntilSpawnBombers = spawnDelayBombers;
        }
    }
    void Update()   {
        monitorChampionResource();
        EnemySpawner();
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
        else    {
            time += Time.deltaTime;
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
        if(time < 9)    {
            timeFormat += "0" + Mathf.Round(time);
        }
        else                                        
            timeFormat += Mathf.Round(time);
        gameTimer.text = timeFormat;
    }
    void monitorChampionResource()  {
        if (timeUntilRegen > 0)
            timeUntilRegen -= Time.deltaTime;
        else{
            if(champion.statsScript.GetMana() < champion.statsScript.GetMaxMana()) {
                // Debug.Log(champion.statsScript.GetMana() + " " + champion.statsScript.GetMaxMana());
                champion.statsScript.AddMana(champion.statsScript.GetManaRegen());
            }
            if(champion.statsScript.GetHealth() < champion.statsScript.GetMaxHealth()) {
                // Debug.Log(champion.statsScript.GetHealth() + " " + champion.statsScript.GetMaxHealth());
                champion.statsScript.AddHealth(champion.statsScript.GetHealthRegen());
            }
            timeUntilRegen = 5f;
        }
    }
    private Vector3 PickLocation() {
        int randomNum = Random.Range(0, 4);
        if(randomNum == 0)   return location1.transform.position;
        else if(randomNum == 1)   return location2.transform.position;
        else if(randomNum == 2)   return location3.transform.position;
        else if(randomNum == 3)   return location4.transform.position;
        else    return location2.transform.position;
    }
    void spawnEnemyBombers()    {
        Vector3 location = PickLocation();
        var minion = Instantiate(enemyBombMinion, location, Quaternion.identity);
        if(intensity > 1){
            minion.GetComponent<Stats>().AddMoveSpeed(intensity * speed);
            minion.GetComponent<Stats>().SetMaxHealth(intensity * maxHealth);
        }
    }
    void spawnEnemyMinions()    {
        Vector3 location = PickLocation();
        var minion = Instantiate(enemyMinion, location, Quaternion.identity);
        if(intensity > 1){
            minion.GetComponent<Stats>().AddMoveSpeed(intensity * speed);
            minion.GetComponent<Stats>().SetMaxHealth(intensity * maxHealth);
            minion.GetComponent<Stats>().AddMagicResist(intensity * magicResist);
        }
    }
}