using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HUD : MonoBehaviour    {
    float health, maxHealth;
    float mana, maxMana;
    private GameObject player;
    private Image hpBar;
    private Image manaBar;
    public Text manaText;
    public Text healthText;
    public Image ability1;
    public Image ability2;
    public Image ability3;
    public Image ability4;
    Stats statsScript;
    void Start()    {
        hpBar = gameObject.GetComponentsInChildren<Image>()[1];
        // Debug.Log(hpBar);
        manaBar = gameObject.GetComponentsInChildren<Image>()[3]; 
        // Debug.Log(manaBar);
        player = GameObject.FindGameObjectWithTag("Player");
        statsScript = player.GetComponent<Stats>();
        ability1.sprite = player.GetComponent<GenericChampion>().GetAbilityImage1();
        ability2.sprite = player.GetComponent<GenericChampion>().GetAbilityImage2();
        ability3.sprite = player.GetComponent<GenericChampion>().GetAbilityImage3();
        ability4.sprite = player.GetComponent<GenericChampion>().GetAbilityImage4();
    }
    void Update()   {
        mana = statsScript.GetMana();
        health = statsScript.GetHealth();
        manaText.text = mana + " / " + statsScript.GetMaxMana();
        healthText.text = health + " / " + statsScript.GetMaxHealth();
        float convertedHealth = UtilScript.RemapRange(health,0,maxHealth);
        float convertedMana = UtilScript.RemapRange(mana, 0, statsScript.GetMaxMana());
        hpBar.fillAmount = convertedHealth;
        manaBar.fillAmount = convertedMana;
    }
    public void updateValues()   {
        this.maxHealth = statsScript.GetMaxHealth();
        this.maxMana = statsScript.GetMaxMana();
    }
}
