using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HUD : MonoBehaviour    {
    public GameObject manager;
    private GameObject player;
    private Image hpBar;
    private Image manaBar;
    private Image xPBar;
    public Text manaText;
    public Text healthText;
    public Image ability1;
    public Image ability2;
    public Image ability3;
    public Image ability4;
    GenericChampion champion;
    void Start()    {
        hpBar = gameObject.GetComponentsInChildren<Image>()[1];
        // Debug.Log(hpBar);
        manaBar = gameObject.GetComponentsInChildren<Image>()[3]; 
        xPBar = gameObject.GetComponentsInChildren<Image>()[5]; 
        champion = manager.GetComponent<GameManager>().getChosenChampion();
        // Debug.Log(champion);
        ability1.sprite = champion.GetAbilityImage1();
        ability2.sprite = champion.GetAbilityImage2();
        ability3.sprite = champion.GetAbilityImage3();
        ability4.sprite = champion.GetAbilityImage4();
    }
    void Update()   {
        updateHealthInHud();
        updateManaInHud();
        updateXp();
    }
    private void updateValues()   {
    }
    private void updateHealthInHud(){
        float maxHealth = champion.statsScript.GetMaxHealth();
        float health = champion.statsScript.GetHealth();
        float convertedHealth = UtilScript.RemapRange(health,0,maxHealth);
        healthText.text = health + " / " + maxHealth;
        hpBar.fillAmount = convertedHealth;
    }
    private void updateManaInHud(){
        float maxMana = champion.statsScript.GetMaxMana();
        float mana = champion.statsScript.GetMana();
        float convertedMana = UtilScript.RemapRange(mana, 0, maxMana);
        manaBar.fillAmount = convertedMana;
        manaText.text = mana + " / " + champion.statsScript.GetMaxMana();
    }
    private void updateXp(){
        if(champion.GetCurrentLevel() != 18){
            int currentXp = champion.GetXP();
            int nextXP = champion.GetNextXPLevel();
            float convertedXP = UtilScript.RemapRange(currentXp,0,nextXP);
            xPBar.fillAmount = convertedXP;
        }
        else{
            xPBar.fillAmount = 1;
        }
    }
}
