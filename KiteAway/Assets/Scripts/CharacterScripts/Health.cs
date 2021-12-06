using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {
    Stats statsScript;
    private Image hpbar;
    void Start() {
        statsScript = GetComponent<Stats>();
        hpbar = gameObject.GetComponentsInChildren<Image>()[1];
        statsScript.SetHealth(statsScript.GetMaxHealth());
        statsScript.SetMana(statsScript.GetMaxMana());
    }
    void Update() {
        float convertedHealth = UtilScript.RemapRange(statsScript.GetHealth(),0,statsScript.GetMaxHealth());
        hpbar.fillAmount = convertedHealth;
    }
}
