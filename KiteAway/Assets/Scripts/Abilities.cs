using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour {

    // Ability 1
    [Header("Ability 1 : Q")]
    public Image abilityImage1;
    public float coolDownDurationAbilityOne = 5f;
    bool isAbilityOneOnCooldown = false;
    public KeyCode keyCodeForAbilityOne;

    Vector3 position;
    public Canvas ability1Canvas;
    //public Image skillShotIndicatorAbilityOne;
    public Transform character;

    // Ability 2
    [Header("Ability 2 : W")]
    public Image abilityImage2;
    public float coolDownDurationAbilityTwo = 5;
    bool isAbilityTwoOnCooldown = false;
    public KeyCode keyCodeForAbilityTwo;

    // Ability 3
    [Header("Ability 3 : E")]
    public Image abilityImage3;
    public float coolDownDurationAbilityThree = 5;
    bool isAbilityThreeOnCooldown = false;
    public KeyCode keyCodeForAbilityThree;

    // Ability 4
    [Header("Ability 4 : R")]
    public Image abilityImage4;
    public float coolDownDurationAbilityFour = 5;
    bool isAbilityFourOnCooldown = false;
    public KeyCode keyCodeForAbilityFour;



    // Start is called before the first frame update
    void Start() {
        abilityImage1.fillAmount = 0;
        abilityImage2.fillAmount = 0;
        abilityImage3.fillAmount = 0;
        abilityImage4.fillAmount = 0;

        //skillShotIndicatorAbilityOne.GetComponent<Image>().enabled = false;
    }

    // Update is called once per frame
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
        if (Input.GetKey(keyCodeForAbilityOne) && isAbilityOneOnCooldown == false) {
            Debug.Log("Player used Q");
            isAbilityOneOnCooldown = true;
            abilityImage1.fillAmount = 1;
        }

        if (isAbilityOneOnCooldown) {
            abilityImage1.fillAmount -= 1 / coolDownDurationAbilityOne * Time.deltaTime;
            //Debug.Log(abilityImage1.fillAmount);

            if (abilityImage1.fillAmount <= 0) {
                abilityImage1.fillAmount = 0;
                isAbilityOneOnCooldown = false;
            }
        }
    }

    void AbilityTwo() {
        if (Input.GetKey(keyCodeForAbilityTwo) && isAbilityTwoOnCooldown == false) {
            Debug.Log("Player used W");
            isAbilityTwoOnCooldown = true;
            abilityImage2.fillAmount = 1;
        }

        if (isAbilityTwoOnCooldown) {
            abilityImage2.fillAmount -= 1 / coolDownDurationAbilityTwo * Time.deltaTime;
            //Debug.Log(abilityImage1.fillAmount);

            if (abilityImage2.fillAmount <= 0) {
                abilityImage2.fillAmount = 0;
                isAbilityTwoOnCooldown = false;
            }
        }

    }

    void AbilityThree() {
        if (Input.GetKey(keyCodeForAbilityThree) && isAbilityThreeOnCooldown == false) {
            Debug.Log("Player used W");
            isAbilityThreeOnCooldown = true;
            abilityImage3.fillAmount = 1;
        }

        if (isAbilityThreeOnCooldown) {
            abilityImage3.fillAmount -= 1 / coolDownDurationAbilityThree * Time.deltaTime;
            //Debug.Log(abilityImage1.fillAmount);

            if (abilityImage3.fillAmount <= 0) {
                abilityImage3.fillAmount = 0;
                isAbilityThreeOnCooldown = false;
            }
        }

    }

    void AbilityFour() {
        if (Input.GetKey(keyCodeForAbilityFour) && isAbilityFourOnCooldown == false) {
            Debug.Log("Player used W");
            isAbilityFourOnCooldown = true;
            abilityImage4.fillAmount = 1;
        }

        if (isAbilityFourOnCooldown) {
            abilityImage4.fillAmount -= 1 / coolDownDurationAbilityFour * Time.deltaTime;
            //Debug.Log(abilityImage1.fillAmount);

            if (abilityImage4.fillAmount <= 0) {
                abilityImage4.fillAmount = 0;
                isAbilityFourOnCooldown = false;
            }
        }

    }

    public void spawnMysticShot() {
        // useSkillShot = true;
    }
}
