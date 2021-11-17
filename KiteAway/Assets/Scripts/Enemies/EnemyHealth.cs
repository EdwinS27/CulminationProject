using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {
    Slider enemyHealthSlider;
    Stats statsScript;
    void Start() {
        statsScript = GetComponent<Stats>();
        statsScript.health = statsScript.maxHealth;
        if (GameObject.FindGameObjectWithTag ("EnemyCanvas")) {
            enemyHealthSlider = (Slider) FindObjectOfType(typeof (Slider));
        }
        enemyHealthSlider.maxValue = statsScript.maxHealth;
    }
    void Update() {
        enemyHealthSlider.value = statsScript.health;
    }
}
