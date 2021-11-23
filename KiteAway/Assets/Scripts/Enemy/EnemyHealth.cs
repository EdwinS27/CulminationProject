using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {
    Stats statsScript;
    private Image hpbar;
    float health, maxHealth;
    void Start() {
        hpbar = GetComponentsInChildren<Image>()[1]; // get 2nd image
        statsScript = GetComponent<Stats>();
        maxHealth = statsScript.GetMaxHealth();
    }
    void Update() {
        health = statsScript.GetHealth();
        float convertedHealth = UtilScript.RemapRange(health,0,maxHealth);
        hpbar.fillAmount = convertedHealth;
    }
}
