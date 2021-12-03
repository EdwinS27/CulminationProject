using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : SelectCharacter {
    // Camera Settings
    private Transform playerCharacter;
    private Vector3 cameraOffset;
    [Range(0.01f, 1.0f)]
    private float smoothness = 0.5f;
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
    private GameObject createChampion;
    public GameObject[] champions;
    private GenericChampion champion;
    public GameObject levelQ;
    public GameObject levelW;
    public GameObject levelE;
    public GameObject levelR;
    private  Vector3 startLocation = new Vector3(0,1,0);
    private void Start() {
        spawnCharacter();
        cameraOffset = transform.position - playerCharacter.transform.position;
        var minion1 = Instantiate(enemyMinion, new Vector3(10f,0f,10f), Quaternion.identity);
        minion1.GetComponent<EnemyMovement>().SetTargetEnemy(createChampion);
        var minion2 = Instantiate(enemyMinion, new Vector3(-10f,0f,-10f), Quaternion.identity);
        minion2.GetComponent<EnemyMovement>().SetTargetEnemy(createChampion);
    }
    void spawnCharacter(){
        int option = getSelectedCharacter();
        createChampion = Instantiate(champions[option], startLocation, Quaternion.identity);
        playerCharacter = createChampion.GetComponent<Transform>();
        champion = createChampion.GetComponent<GenericChampion>();
    }
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
        Vector3 newPos = playerCharacter.position + cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPos, smoothness);
        monitorChampionResource();
        EnemySpawner();
        incrementTime();
        setTime();
        checkForUpgradableSkills();
        checkGameState();
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
        minion.GetComponent<EnemyMovement>().SetTargetEnemy(createChampion);
        if(intensity > 1){
            minion.GetComponent<Stats>().AddMoveSpeed(intensity * speed);
            minion.GetComponent<Stats>().SetMaxHealth(intensity * maxHealth);
            minion.GetComponent<Stats>().AddMagicResist(intensity * magicResist);
        }
    }
    void manageGame(){
        if(champion.statsScript.GetHealth() <= 0){
            // the game is over so we end the game and show the stats
        }
    }
    public void LevelAbility1(){champion.LevelAbility1();}
    public void LevelAbility2(){champion.LevelAbility2();}
    public void LevelAbility3(){champion.LevelAbility3();}
    public void LevelAbility4(){champion.LevelAbility4();}
    public void checkForUpgradableSkills()    {
        // print(champion.GetSkillablePoints());
        if(champion.GetSkillablePoints() > 0){
            if(champion.GetAbility1Points() < 4)
                levelQ.SetActive(true);
            else
                levelQ.SetActive(false);
            if(champion.GetAbility2Points() < 4)
                levelW.SetActive(true);
            else
                levelW.SetActive(false);
            if(champion.GetAbility3Points() < 4)
                levelE.SetActive(true);
            else
                levelE.SetActive(false);
            if(champion.GetPointsAvailableForUltimate() > 0)
                levelR.SetActive(true);
            else
                levelR.SetActive(false);
            
        }
        else{
            levelQ.SetActive(false);
            levelW.SetActive(false);
            levelE.SetActive(false);
            levelR.SetActive(false);
        }
    }
    public void checkGameState(){
        if(champion.statsScript.GetHealth() <= 0){
            // print("Dead");
            GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("Death", true);
            WaitToSeeDeathAnimationThenGoToGameOver();
        }
    }
    public GenericChampion getChosenChampion(){
        return this.champion;
    }
    IEnumerator WaitToSeeDeathAnimationThenGoToGameOver(){
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("GameOver");
    }
}