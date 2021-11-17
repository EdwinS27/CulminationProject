using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {
    public Slider playerHealthSlider3D;
    Stats statsScript;
    void Start() {
        // Debug.Log("Give me the statsScript!");
        statsScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        // Debug.Log("Give the slider the correct max health!");
        playerHealthSlider3D.maxValue = statsScript.maxHealth;
        // Debug.Log("Give the player his max health!");
        statsScript.health = statsScript.maxHealth;
        statsScript.mana = statsScript.maxMana;
    }
        void Update() {
        playerHealthSlider3D.value = statsScript.health;
    }
}
