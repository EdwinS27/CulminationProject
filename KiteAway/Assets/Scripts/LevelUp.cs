using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
public class LevelUp : MonoBehaviour    {
    public GameObject levelQ;
    public GameObject levelW;
    public GameObject levelE;
    public GameObject levelR;
    void Start()    {
        // levelQ.SetActive(false);
        // levelW.SetActive(false);
        // levelE.SetActive(false);
        levelR.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LeveledUpSkill()    {
        levelQ.SetActive(false);
        levelW.SetActive(false);
        levelE.SetActive(false);
        levelR.SetActive(false);
    }
    public void LevelUpSkill()   {
        levelQ.SetActive(true);
        levelW.SetActive(true);
        levelE.SetActive(true);
        levelR.SetActive(true);
    }
    public void LevelUpSkill(string notThisOne)   {
        if(notThisOne == "Q" || notThisOne == "q"){
            levelQ.SetActive(false);
            levelW.SetActive(true);
            levelE.SetActive(true);
            levelR.SetActive(false);
        }
        else if(notThisOne == "W" || notThisOne == "w"){
            levelQ.SetActive(true);
            levelW.SetActive(false);
            levelE.SetActive(true);
            levelR.SetActive(false);
        }
        else    {
            levelQ.SetActive(true);
            levelW.SetActive(true);
            levelE.SetActive(false);
            levelR.SetActive(false);
        }
    }
}
