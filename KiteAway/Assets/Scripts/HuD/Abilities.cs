using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// This is a script for HUD.
public class Abilities : MonoBehaviour {
    public GenericChampion champion;
    public Image abilityImage1;
    float durationAbility1;
    float cooldownDurationAbility1;
    bool ability1OnCooldown = false;
    public KeyCode abilityKeyCode1;
    // public Canvas ability1Canvas;
    //public Image skillShotIndicatorAbility1;
    public Image abilityImage2;
    float cooldownDurationAbility2;
    float durationAbility2;
    bool ability2OnCooldown = false;
    public KeyCode abilityKeyCode2;
    public Image abilityImage3;
    float cooldownDurationAbility3;
    float durationAbility3;
    bool ability3OnCooldown = false;
    public KeyCode abilityKeyCode3;
    public Image abilityImage4;
    float cooldownDurationAbility4;
    float durationAbility4;
    bool ability4OnCooldown = false;
    public KeyCode abilityKeyCode4;
    // Used for champions that require skill shots
    void Start() {
        abilityImage1.fillAmount = 1;
        abilityImage2.fillAmount = 1;
        abilityImage3.fillAmount = 1;
        abilityImage4.fillAmount = 1;
        champion = GameObject.FindGameObjectWithTag("Player").GetComponent<GenericChampion>();
        //skillShotIndicatorAbility1.GetComp1nt<Image>().enabled = false;
    }
    void Update() {
        if(champion == null){
            champion = GameObject.FindGameObjectWithTag("Player").GetComponent<GenericChampion>();
        }
        AbilitiesCast();
        //Debug.Log("Q DURATION = " + durationAbility1); 
    }
    void AbilitiesCast() {
        Ability1();
        Ability2();
        Ability3();
        Ability4();
    }
    void Ability1() {
        if (    Input.GetKeyDown(abilityKeyCode1) && ability1OnCooldown == false
                && (champion.statsScript.GetMana() - champion.GetAbility1Cost() >= 0)
                && (champion.GetAbility1Points() > -1)
        ) {
            // Debug.Log("Abilities Script: Pressed Q");
            champion.UseAbility1();
            abilityImage1.fillAmount = 1;
            ability1OnCooldown = true;
            durationAbility1 = cooldownDurationAbility1;
        }
        if (ability1OnCooldown && durationAbility1 > 0) {
            durationAbility1 -= Time.deltaTime;
            abilityImage1.fillAmount -= 1 / durationAbility1 * Time.deltaTime;
            if (durationAbility1 < 1) {
                abilityImage1.fillAmount = 0;
                durationAbility1 = 0;
                ability1OnCooldown = false;
            }
        }
    }
    void Ability2() {
        if (Input.GetKey(abilityKeyCode2) && ability2OnCooldown == false
            && (champion.statsScript.GetMana() - champion.GetAbility2Cost() >= 0)
            && (champion.GetAbility2Points() > -1)
        )   {
            champion.UseAbility2();
            abilityImage2.fillAmount = 1;
            ability2OnCooldown = true;
            durationAbility2 = cooldownDurationAbility2;
        }
        if (ability2OnCooldown && durationAbility2 > 0) {
            durationAbility2 -= Time.deltaTime;
            abilityImage2.fillAmount -= 1 / durationAbility2 * Time.deltaTime;
            if (durationAbility2 < 1) {
                abilityImage2.fillAmount = 0;
                durationAbility2 = 0;
                ability2OnCooldown = false;
            }
        }
    }
    void Ability3() {
        if (Input.GetKey(abilityKeyCode3) && ability3OnCooldown == false
            && (champion.statsScript.GetMana() - champion.GetAbility3Cost() >= 0)
            && (champion.GetAbility3Points() > -1)
        )   {
            champion.UseAbility3();
            abilityImage3.fillAmount = 1;
            ability3OnCooldown = true;
            durationAbility3 = cooldownDurationAbility3;
        }
        if (ability3OnCooldown && durationAbility3 > 0) {
            durationAbility3 -= Time.deltaTime;
            abilityImage3.fillAmount -= 1 / durationAbility3 * Time.deltaTime;
            if (durationAbility3 < 1) {
                abilityImage3.fillAmount = 0;
                durationAbility3 = 0;
                ability3OnCooldown = false;
            }
        }
    }
    void Ability4() {
        if (Input.GetKey(abilityKeyCode4) && ability4OnCooldown == false
            && (champion.statsScript.GetMana() - champion.GetAbility4Cost() >= 0)
            && (champion.GetAbility4Points() > -1)
        )   {
            champion.UseAbility4();
            abilityImage4.fillAmount = 1;
            ability4OnCooldown = true;
            durationAbility4 = cooldownDurationAbility4;
        }
        if (ability4OnCooldown && durationAbility4 > 0) {
            durationAbility4 -= Time.deltaTime;
            abilityImage4.fillAmount -= 1 / durationAbility4 * Time.deltaTime;
            if (durationAbility4 < 1) {
                abilityImage4.fillAmount = 0;
                durationAbility4 = 0;
                ability4OnCooldown = false;
            }
        }
    }
    public void LowerAllCooldowns() {
        durationAbility1 -= 1.5f;
        durationAbility2 -= 1.5f;
        durationAbility3 -= 1.5f;
        durationAbility4 -= 1.5f;
    }
    public void SetCooldownDurationAbility1() {
        cooldownDurationAbility1 = champion.GetAbility1Duration();
        if(champion.GetAbility1Points() == 0)
            abilityImage1.fillAmount = 0;
            // Debug.Log("You Can Use Ability Rank 1 => Points in: " + champion.GetAbility1Points() + "Duration: " + champion.GetAbility1Duration());
            
    }
    public void SetCooldownDurationAbility2() {
        cooldownDurationAbility2 = champion.GetAbility2Duration();
        if(champion.GetAbility2Points() == 0)
            abilityImage2.fillAmount = 0;
            // Debug.Log("You Can Use Ability Rank 2 ");
    }
    public void SetCooldownDurationAbility3() {
        cooldownDurationAbility3 = champion.GetAbility3Duration();
        if(champion.GetAbility3Points() == 0)
            abilityImage3.fillAmount = 0;
            // Debug.Log("You Can Use Ability Rank 3 ");
    }
    public void SetCooldownDurationAbility4() {
        cooldownDurationAbility4 = champion.GetAbility4Duration();
        if(champion.GetAbility4Points() == 0)
            abilityImage4.fillAmount = 0;
            // Debug.Log("You Can Use Ability Rank 4 ");
    }
}