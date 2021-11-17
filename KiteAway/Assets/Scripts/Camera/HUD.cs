using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour    {
    public Text manaText;
    public Slider sliderManaHUD;
    public Text healthText;
    public Slider sliderHealthHUD;
    Stats statsScript;
    void Start()    {
        statsScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        sliderHealthHUD.maxValue = statsScript.maxHealth;
        sliderManaHUD.maxValue = statsScript.maxMana;
    }
    void Update()   {
        sliderHealthHUD.value = statsScript.health;
        sliderManaHUD.value = statsScript.mana;
        manaText.text = statsScript.mana + " / " + statsScript.maxMana;
        healthText.text = statsScript.health + " / " + statsScript.maxHealth;
    }
}
