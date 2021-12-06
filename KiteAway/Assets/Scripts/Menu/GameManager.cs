using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : SelectCharacter {
    // Camera Settings
    private Transform playerCharacter;
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
    private int intensity = 1;
    private GameObject createChampion;
    public GameObject[] champions;
    public GameObject levelQ;
    public GameObject levelW;
    public GameObject levelE;
    public GameObject levelR;
    private  Vector3 startLocation = new Vector3(0,1,0);
    private bool championIsAlive = true;
    private GenericChampion champion;
    // public Abilities abilityScript;
    bool prevActivateDeath = true;
    float timeDeathDelay = 8f;
    private void Start() {
        spawnCharacter();
        // var minion1 = Instantiate(enemyMinion, new Vector3(10f,0f,10f), Quaternion.identity);
        // minion1.GetComponent<EnemyCombat>().setTargetedEnemy(createChampion);
        // var minion2 = Instantiate(enemyMinion, new Vector3(-10f,0f,-10f), Quaternion.identity);
        // minion2.GetComponent<EnemyCombat>().setTargetedEnemy(createChampion);
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
        if(championIsAlive){
            // EnemySpawner();
            monitorChampionResource();
            checkForUpgradableSkills();
            levelAbilitiesWithKeys();
        }
        checkGameState();
        if(!prevActivateDeath){
            WaitToSeeDeathAnimationThenGoToGameOver();
        }
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
            minion.GetComponent<EnemyCombat>().setTargetedEnemy(createChampion);
        }
    }
    void spawnEnemyMinions()    {
        Vector3 location = PickLocation();
        var minion = Instantiate(enemyMinion, location, Quaternion.identity);
        minion.GetComponent<EnemyCombat>().setTargetedEnemy(createChampion);
        if(intensity > 1){
            minion.GetComponent<Stats>().AddMoveSpeed(intensity * speed);
            minion.GetComponent<Stats>().SetMaxHealth(intensity * maxHealth);
            minion.GetComponent<Stats>().AddMagicResist(intensity * magicResist);
        }
    }
    public void levelAbilitiesWithKeys(){
        if(champion.GetSkillablePoints() > 0){
            if(Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.LeftAlt)){
                champion.LevelAbility1();
                // abilityScript.SetCooldownDurationAbility1();
                }
            else if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftAlt)){
                champion.LevelAbility2();
                // abilityScript.SetCooldownDurationAbility2();
                }
            else if(Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.LeftAlt)){
                champion.LevelAbility3();
                // abilityScript.SetCooldownDurationAbility3();
                }
        }
        else if(champion.GetPointsAvailableForUltimate() > 0)
            if(Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.LeftAlt)){
                champion.LevelAbility4();
                // abilityScript.SetCooldownDurationAbility4();
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
            // print("end");
            championIsAlive = false;
            createChampion.GetComponent<Animator>().SetBool("Death", true);
            if(prevActivateDeath){
                prevActivateDeath = false;
            }
        }
    }
    public GenericChampion getChosenChampion(){
        return this.champion;
    }
    void swapScene(){
        SceneManager.LoadScene("GameOverStats");
    }
    public void WaitToSeeDeathAnimationThenGoToGameOver(){
        if(timeDeathDelay > 0)
            timeDeathDelay -= Time.deltaTime;
        else
            swapScene();
    }
}