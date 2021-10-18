using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {


    public Slider playerHealthSlider3D;
    Slider playerHealthSlider2D;

    Stats statsScript;

    // Start is called before the first frame update
    void Start() {
        statsScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        playerHealthSlider2D = GetComponent<Slider>();

        playerHealthSlider2D.maxValue = statsScript.maxHealth;
        playerHealthSlider3D.maxValue = statsScript.maxHealth;
        //statsScript.health = statsScript.maxHealth;
    }

    // Update is called once per frame
    void Update() {
        playerHealthSlider2D.value = statsScript.health;
        playerHealthSlider3D.value = playerHealthSlider2D.value;
    }
}
