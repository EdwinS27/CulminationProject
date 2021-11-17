using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Abilities : MonoBehaviour {
    public GenericChampion champion;
    [Header("Ability 1 : Q")] // Variables for Ability One In HUD
    public Image abilityImage1;
    private float cdDurationAbility1;
    private float timeLeftOnAbility1;
    private bool onCooldownAbility1 = false;
    public KeyCode keyCodeForAbilityOne;
    public Canvas ability1Canvas;
    //private Image skillShotIndicatorAbilityOne;
    public Transform character;

    [Header("Ability 2 : W")]
    public Image abilityImage2;
    private float cdDurationAbility2;
    private float timeLeftOnAbility2;
    private bool onCooldownAbility2 = false;
    public KeyCode keyCodeForAbilityTwo;


    [Header("Ability 3 : E")]
    public Image abilityImage3;
    private float cdDurationAbility3;
    private float timeLeftOnAbility3;
    private bool onCooldownAbility3 = false;
    public KeyCode keyCodeForAbilityThree;
    [Header("Ability 4 : R")]
    public Image abilityImage4;
    private float cdDurationAbility4;
    float timeLeftOnAbility4;
    bool onCooldownAbility4 = false;
    public KeyCode keyCodeForAbilityFour;
    void Start() {
        // abilityImage1.fillAmount = 1;
        // abilityImage2.fillAmount = 1;
        // abilityImage3.fillAmount = 1;
        // abilityImage4.fillAmount = 1;
        //skillShotIndicatorAbilityOne.GetComponent<Image>().enabled = false;
    }
    void Update() {
        AbilitiesCast();
    }
    void AbilitiesCast() {
        AbilityOne();
        AbilityTwo();
        AbilityThree();
        AbilityFour();
    }
    void AbilityOne() {
        if (Input.GetKey(keyCodeForAbilityOne) && onCooldownAbility1 == false && champion.GetCurrentPointsInAbilityOne() > 0 ) {
            champion.useAbilityOne();
            onCooldownAbility1 = true;
            abilityImage1.fillAmount = 1;
            timeLeftOnAbility1 = cdDurationAbility1;
        }
        if (onCooldownAbility1 && timeLeftOnAbility1 > 1) {
            timeLeftOnAbility1 -= Time.deltaTime;
            abilityImage1.fillAmount -= 1 / timeLeftOnAbility1 * Time.deltaTime;
            if (timeLeftOnAbility1 < 1) {
                onCooldownAbility1 = false;
                abilityImage1.fillAmount = 0;
            }
        }
    }
    void AbilityTwo() {
        if (Input.GetKey(keyCodeForAbilityTwo) && onCooldownAbility2 == false && champion.GetCurrentPointsInAbilityTwo() > 0) {
            champion.useAbilityTwo();
            onCooldownAbility2 = true;
            abilityImage2.fillAmount = 1;
            timeLeftOnAbility2 = cdDurationAbility2;
        }
        if (onCooldownAbility2 && timeLeftOnAbility2 > 0) {
            timeLeftOnAbility2 -= Time.deltaTime;
            abilityImage2.fillAmount -= 1 / timeLeftOnAbility2 * Time.deltaTime;
            if (timeLeftOnAbility2 < 1) {
                onCooldownAbility2 = false;
                abilityImage2.fillAmount = 0;
            }
        }
    }
    void AbilityThree() {
        if (Input.GetKey(keyCodeForAbilityThree) && onCooldownAbility3 == false && champion.GetCurrentPointsInAbilityThree() > 0) {
            champion.useAbilityThree();
            onCooldownAbility3 = true;
            abilityImage3.fillAmount = 1;
            timeLeftOnAbility3 = cdDurationAbility3;
        }
        if (onCooldownAbility3 && timeLeftOnAbility3 > 0) {
            timeLeftOnAbility3 -= Time.deltaTime;
            abilityImage3.fillAmount -= 1 / timeLeftOnAbility3 * Time.deltaTime;
            if (abilityImage3.fillAmount <= 0) {
                onCooldownAbility3 = false;
                abilityImage3.fillAmount = 0;
            }
        }
        else    {
            onCooldownAbility3 = false;
            abilityImage3.fillAmount = 0;
        }
    }
    void AbilityFour() {
        if (Input.GetKey(keyCodeForAbilityFour) && onCooldownAbility4 == false  && champion.GetCurrentPointsInAbilityFour() > 0) {
            champion.useAbilityFour();
            onCooldownAbility4 = true;
            abilityImage4.fillAmount = 1;
            timeLeftOnAbility4 = cdDurationAbility4;
        }
        if (onCooldownAbility4) {
            timeLeftOnAbility4 -= Time.deltaTime;
            abilityImage4.fillAmount -= 1 / timeLeftOnAbility4 * Time.deltaTime;
            if (abilityImage4.fillAmount <= 0) {
                abilityImage4.fillAmount = 0;
                onCooldownAbility4 = false;
                cdDurationAbility4 = 0;
            }
        }
    }
    public void LowerAllCooldowns() {
        timeLeftOnAbility1 -= 1.5f;
        timeLeftOnAbility2 -= 1.5f;
        timeLeftOnAbility3 -= 1.5f;
        timeLeftOnAbility4 -= 1.5f;
    }
    public void LowerAllCooldowns(float timeToReduce) {
        timeLeftOnAbility1 -= timeToReduce;
        timeLeftOnAbility2 -= timeToReduce;
        timeLeftOnAbility3 -= timeToReduce;
        timeLeftOnAbility4 -= timeToReduce;
    }
    public void SetCdDurationAbility1(float coolDownDuration) { cdDurationAbility1 = coolDownDuration;}
    public void SetCdDurationAbility2(float coolDownDuration) { cdDurationAbility2 = coolDownDuration;}
    public void SetCdDurationAbility3(float coolDownDuration) {   cdDurationAbility3 = coolDownDuration;    }
    public void SetCdDurationAbility4(float coolDownDuration) {    cdDurationAbility4 = coolDownDuration; }
    public void canUseAbilityOne()  {
        abilityImage1.fillAmount = 0;
        SetCdDurationAbility1(champion.GetCurrentAbilityCooldownOne());
    }
    public void canUseAbilityTwo()  {
        abilityImage2.fillAmount = 0;
        SetCdDurationAbility2(champion.GetCurrentAbilityCooldownTwo());
    }
    public void canUseAbilityThree()  {
        abilityImage3.fillAmount = 0;
        SetCdDurationAbility3(champion.GetCurrentAbilityCooldownThree());
    }
    public void canUseAbilityFour()  {
        abilityImage4.fillAmount = 0;
        SetCdDurationAbility4(champion.GetCurrentAbilityCooldownFour());
    }
}