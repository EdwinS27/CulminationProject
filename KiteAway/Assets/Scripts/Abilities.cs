using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour {
    public GenericChampion champion;
    [Header("Ability 1 : Q")] // Variables for Ability One In HUD
    public Image abilityImage1;
    private float coolDownDurationAbilityOne;
    private float remainingCoolDownDurationAbilityOne;
    private bool isAbilityOneOnCooldown = false;
    public KeyCode keyCodeForAbilityOne;

    Vector3 position;
    public Canvas ability1Canvas;
    //public Image skillShotIndicatorAbilityOne;
    public Transform character;
    public GameObject prefabMysticShot;

    [Header("Ability 2 : W")]
    public Image abilityImage2;
    private float coolDownDurationAbilityTwo;
    private float remainingCoolDownDurationAbilityTwo;
    private bool isAbilityTwoOnCooldown = false;
    public KeyCode keyCodeForAbilityTwo;


    [Header("Ability 3 : E")]
    public Image abilityImage3;
    private float coolDownDurationAbilityThree;
    private float remainingCoolDownDurationAbilityThree;
    private bool isAbilityThreeOnCooldown = false;
    public KeyCode keyCodeForAbilityThree;


    [Header("Ability 4 : R")]
    public Image abilityImage4;
    private float coolDownDurationAbilityFour;
    float remainingCoolDownDurationAbilityFour;
    bool isAbilityFourOnCooldown = false;
    public KeyCode keyCodeForAbilityFour;
    // Used for champions that require skill shots
    Vector3 skillShotTargetLocation;
    Stats statScript;
    void Start() {
        coolDownDurationAbilityThree = 10f; 
        statScript = GetComponent<Stats>();
        abilityImage1.fillAmount = 0;
        abilityImage2.fillAmount = 0;
        abilityImage3.fillAmount = 0;
        abilityImage4.fillAmount = 0;
        //skillShotIndicatorAbilityOne.GetComponent<Image>().enabled = false;
    }
    void Update() {
        AbilitiesCast();
        //Debug.Log("Q DURATION = " + remainingCoolDownDurationAbilityOne); 
    }
    void AbilitiesCast() {
        AbilityOne();
        AbilityTwo();
        AbilityThree();
        AbilityFour();
    }
    void AbilityOne() {
        if (Input.GetKey(keyCodeForAbilityOne) && isAbilityOneOnCooldown == false && champion.GetCurrentAbilityRankOne() > 0) {
            isAbilityOneOnCooldown = true;
            champion.useAbilityOne();
            remainingCoolDownDurationAbilityOne = coolDownDurationAbilityOne;
            abilityImage1.fillAmount = 1;
        }
        if (isAbilityOneOnCooldown && remainingCoolDownDurationAbilityOne > 0) {
            remainingCoolDownDurationAbilityOne -= Time.deltaTime;
            abilityImage1.fillAmount -= 1 / remainingCoolDownDurationAbilityOne * Time.deltaTime;
            if (remainingCoolDownDurationAbilityOne < 1) {
                abilityImage1.fillAmount = 0;
                remainingCoolDownDurationAbilityOne = 0;
                isAbilityOneOnCooldown = false;
            }
        }
    }
    void AbilityTwo() {
        if (Input.GetKey(keyCodeForAbilityTwo) && isAbilityTwoOnCooldown == false && champion.GetCurrentAbilityRankTwo() > 0) {
            // reduce mana here
            isAbilityTwoOnCooldown = true;
            champion.useAbilityTwo();
            abilityImage2.fillAmount = 1;
            remainingCoolDownDurationAbilityTwo = coolDownDurationAbilityTwo;
            abilityImage1.fillAmount = 1;
        }
        if (isAbilityTwoOnCooldown) {
            remainingCoolDownDurationAbilityTwo -= Time.deltaTime;
            abilityImage2.fillAmount -= 1 / remainingCoolDownDurationAbilityTwo * Time.deltaTime;
            if (remainingCoolDownDurationAbilityTwo < 1) {
                abilityImage2.fillAmount = 0;
                isAbilityTwoOnCooldown = false;
            }
        }
    }
    void AbilityThree() {
        // where you click get vector, get the direction between origin and point click
        // subtract the vectors = direction
        // normalize the vector
        // vector * magnitude of the range = how far it goes in that direction
        if (Input.GetKey(keyCodeForAbilityThree) && isAbilityThreeOnCooldown == false && champion.GetCurrentAbilityRankThree() > 0) {
            Debug.Log("Player used E");
            champion.useAbilityThree();
            isAbilityThreeOnCooldown = true;
            abilityImage3.fillAmount = 1;
            remainingCoolDownDurationAbilityThree = coolDownDurationAbilityThree;
        }
        if (isAbilityThreeOnCooldown) {
            abilityImage3.fillAmount -= 1 / coolDownDurationAbilityThree * Time.deltaTime;
            if (abilityImage3.fillAmount <= 0) {
                abilityImage3.fillAmount = 0;
                isAbilityThreeOnCooldown = false;
            }
        }
    }
    void AbilityFour() {
        if (Input.GetKey(keyCodeForAbilityFour) && isAbilityFourOnCooldown == false  && champion.GetCurrentAbilityRankFour() > 0) {
            isAbilityFourOnCooldown = true;
            abilityImage4.fillAmount = 1;
        }
        if (isAbilityFourOnCooldown) {
            abilityImage4.fillAmount -= 1 / coolDownDurationAbilityFour * Time.deltaTime;
            if (abilityImage4.fillAmount <= 0) {
                abilityImage4.fillAmount = 0;
                isAbilityFourOnCooldown = false;
            }
        }
    }
    public void LowerAllCooldowns() {
        remainingCoolDownDurationAbilityOne -= 1.5f;
        remainingCoolDownDurationAbilityTwo -= 1.5f;
        remainingCoolDownDurationAbilityThree -= 1.5f;
        remainingCoolDownDurationAbilityFour -= 1.5f;
    }
    public void SetCoolDownDurationAbilityOne(float coolDownDuration) {
        coolDownDurationAbilityOne = coolDownDuration;
    }
    public void SetCoolDownDurationAbilityTwo(float coolDownDuration) {
        coolDownDurationAbilityOne = coolDownDuration;
    }
    public void SetCoolDownDurationAbilityThree(float coolDownDuration) {
        coolDownDurationAbilityOne = coolDownDuration;
    }
    public void SetCoolDownDurationAbilityFour(float coolDownDuration) {
        coolDownDurationAbilityOne = coolDownDuration;
    }
}