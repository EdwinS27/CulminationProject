using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {
    Stats statsScript;
    private Image hpbar;
    float health, maxHealth;
    void Start() {
        // Debug.Log("Object: " + gameObject.name);
        statsScript = GetComponent<Stats>();
        hpbar = gameObject.GetComponentsInChildren<Image>()[1]; // get 2nd image
        // Debug.Log("hpBar: " + hpbar);
        maxHealth = statsScript.GetMaxHealth();
        // Debug.Log("maxHealth: " + maxHealth);
        statsScript.SetHealth(statsScript.GetMaxHealth());
        // Debug.Log("Health: " + statsScript.GetHealth());
        statsScript.SetMana(statsScript.GetMaxMana());
        // Debug.Log("Mana: " + statsScript.GetMana());
    }
    void Update() {
        health = statsScript.GetHealth();
        float convertedHealth = UtilScript.RemapRange(health,0,maxHealth);
        hpbar.fillAmount = convertedHealth;
    }
    public void LeveledUp(float newHealthMax) {
        float convertedHealth = UtilScript.RemapRange(health,0,maxHealth);
        hpbar.fillAmount = convertedHealth;
    }
}
