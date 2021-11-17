using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
public class LevelUp : MonoBehaviour    {
    public GameObject levelUpButtonQ;
    public GameObject levelUpButtonW;
    public GameObject levelUpButtonE;
    public GameObject levelUpButtonR;
    void Start()    {
        levelUpButtonR.SetActive(false);
    }
    public void LeveledUpSkill()    {
        levelUpButtonQ.SetActive(false);
        levelUpButtonW.SetActive(false);
        levelUpButtonE.SetActive(false);
        levelUpButtonR.SetActive(false);
    }
    public void LevelUpSkill()   {
        levelUpButtonQ.SetActive(true);
        levelUpButtonW.SetActive(true);
        levelUpButtonE.SetActive(true);
        levelUpButtonR.SetActive(true);
    }
    public void LevelUpSkill(string notThisOne)   {
        if(notThisOne == "Q" || notThisOne == "q"){
            levelUpButtonQ.SetActive(false);
            levelUpButtonW.SetActive(true);
            levelUpButtonE.SetActive(true);
            levelUpButtonR.SetActive(false);
        }
        else if(notThisOne == "W" || notThisOne == "w"){
            levelUpButtonQ.SetActive(true);
            levelUpButtonW.SetActive(false);
            levelUpButtonE.SetActive(true);
            levelUpButtonR.SetActive(false);
        }
        else    {
            levelUpButtonQ.SetActive(true);
            levelUpButtonW.SetActive(true);
            levelUpButtonE.SetActive(false);
            levelUpButtonR.SetActive(false);
        }
        LeveledUpSkill();
    }
}
