using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GenericChampion : MonoBehaviour    {
    int gold = 0;
    int currentXP = 0;
    int currentLevel = 1;
    int levelUpPoints = 1;
    int pointsAvailableForUltimate = 1;
    float ability1Cost;
    float ability2Cost;
    float ability3Cost;
    float ability4Cost;
    float ability1Damage;   // damage of ability1
    float ability2Damage;   // damage of ability2
    float ability3Damage;   // damage of ability3
    float ability4Damage;   // damage of ability4
    int ability1Points = -1;   // starts at 0, and at most can go to 5 in most cases
    int ability2Points = -1;   // starts at 0, and at most can go to 5 in most cases
    int ability3Points = -1;   // starts at 0, and at most can go to 5 in most cases
    int ability4Points = -1;   // starts at 0, and at most can go to 5 in most cases
    float ability1Duration = 0;
    float ability2Duration = 0;
    float ability3Duration = 0;
    float ability4Duration = 0;
    [SerializeField]    float[] ability1ResourceCosts = {28f, 31f, 34f, 37f, 40f};
    [SerializeField]    float[] ability1CoolDowns = {5.5f, 5.25f, 5f, 4.75f, 4.5f}; // Base Cooldown for Each Level Rank in the ability
    [SerializeField]    float[] ability1Damages = {20f, 45f, 75f, 95f, 120f}; // Base Damage for each Level Rank in the ability
    // Variables for Ability 2 Base Damage and Base Cooldown
    [SerializeField]    float[] ability2ResourceCosts = {50f, 50f, 50f, 50f, 50f};
    [SerializeField]    float[] ability2CoolDowns = {12f, 12f, 12f, 12f, 12f}; // Base Cooldown for Each Level Rank in the ability ability2CoolDown
    [SerializeField]    float[] ability2Damages = {80f, 135f, 190f, 245f, 300f}; // Base Damage for each Level Rank in the ability
    // Variables for Ability 3 Base Damage and Base Cooldown
    [SerializeField]    float[] ability3ResourceCosts = {90f, 90f, 90f, 90f, 90f};
    [SerializeField]    float[] ability3CoolDowns = {28f, 25f, 22f, 19f, 16f}; // Base Cooldown for Each Level Rank in the ability ability2Damage
    [SerializeField]    float[] ability3Damages = {80f, 130f, 180f, 230f, 280f}; // Base Damage for each Level Rank in the ability
    // Variables for Ability 4 Base Damage and Base Cooldown
    [SerializeField]    float[] ability4ResourceCosts = {100f, 100f, 100f};
    [SerializeField]    float[] ability4CoolDowns = {120f, 120f, 120f}; // Base Cooldown for Each Level Rank in the ability ability2Damage
    [SerializeField]    float[] ability4Damages = {350f, 500f, 650f}; // Base Damage for each Level Rank in the ability
    [SerializeField]    float attackDamageGrowth;
    [SerializeField]    float manaGrowth;
    [SerializeField]    float manaRegenGrowth;
    [SerializeField]    float healthGrowth;
    [SerializeField]    float healthRegenGrowth;
    [SerializeField]    float armorGrowth;
    [SerializeField]    float magicResistGrowth;
    [SerializeField]    float missileSpeed;
    int[] xpRequiredToLevelUp = {
        0, 280, 380, 480, 580, 680,
        780, 880, 980, 1080, 1180, 1280,
        1380, 1480, 1580, 1680, 1780, 1880
    };
    public Stats statsScript;
    // public Sprite championPortrait;
    public Sprite abilityImage1;
    public Sprite abilityImage2;
    public Sprite abilityImage3;
    public Sprite abilityImage4;
    // Methods I can override for new champions I implement
    public virtual void UseAbility1()     {}
    public virtual void UseAbility2()     {}
    public virtual void UseAbility3()   {}
    public virtual void UseAbility4()    {}
    // Resource Cost of Ability
    public void SetAbility1Cost(float resourceCost)     { ability1Cost = resourceCost; }
    public void SetAbility2Cost(float resourceCost)     { ability2Cost = resourceCost; }
    public void SetAbility3Cost(float resourceCost)   { ability3Cost = resourceCost; }
    public void SetAbility4Cost(float resourceCost)    { ability4Cost = resourceCost; }
    // Set New Cooldown for these Abilities 1 - 4
    public void SetAbility1Cd(float newCooldown)  {     ability1Duration = newCooldown;}
    public void SetAbility2Cd(float newCooldown)  {     ability2Duration = newCooldown;}
    public void SetAbility3Cd(float newCooldown)  {     ability3Duration = newCooldown;}
    public void SetAbility4Cd(float newCooldown)  {     ability4Duration = newCooldown;}
    // Set current damage for Abilities 1 through 4
    public void SetDamageAbility1(float newDamage)  {  ability1Damage = newDamage;}
    public void SetDamageAbility2(float newDamage)  {  ability2Damage = newDamage;}
    public void SetDamageAbility3(float newDamage)  {  ability3Damage = newDamage;}
    public void SetDamageAbility4(float newDamage)  {  ability4Damage = newDamage;}
    // Get current damage for Abilities 1 through 4
    public float GetAbility1Damage()  { return ability1Damage;}
    public float GetAbility2Damage()  { return ability2Damage;}
    public float GetAbility3Damage()  { return ability3Damage;}
    public float GetAbility4Damage()  { return ability4Damage;}
    // Get current cooldowns for Abilities 1 through 4
    public float GetAbility1Duration()  {  return ability1Duration;}
    public float GetAbility2Duration()  {  return ability2Duration;}
    public float GetAbility3Duration()  {  return ability3Duration;}
    public float GetAbility4Duration()  {  return ability4Duration;}
    // Get current Rank for an ability
    public int GetAbility1Points()  {  return ability1Points;}
    public int GetAbility2Points()  {  return ability2Points;}
    public int GetAbility3Points()  {  return ability3Points;}
    public int GetAbility4Points()  {  return ability4Points;}
    // Get current damage for Abilities 1 through 4
    public float GetAbility1Cost()  { return ability1Cost;}
    public float GetAbility2Cost()  { return ability2Cost;}
    public float GetAbility3Cost()  { return ability3Cost;}
    public float GetAbility4Cost()  { return ability4Cost;}
    public void leveledAbility1()   {   this.ability1Points++;  }
    public void leveledAbility2()   {   this.ability2Points++;  }
    public void leveledAbility3()   {   this.ability3Points++;  }
    public void leveledAbility4()   {   this.ability4Points++;  }
    public void LevelAbility1()  {
        leveledAbility1();
        ability1Damage = ability1Damages[ability1Points];
        ability1Duration = ability1CoolDowns[ability1Points];
        ability1Cost = ability1ResourceCosts[ability1Points];
        leveledAnAbility();
    }
    public void LevelAbility2()  {
        leveledAbility2();
        ability2Damage = ability2Damages[ability2Points];
        ability2Duration = ability2CoolDowns[ability2Points];
        ability2Cost = ability2ResourceCosts[ability2Points];
        leveledAnAbility();
    }
    public void LevelAbility3()  {
        leveledAbility3();
        ability3Damage = ability3Damages[ability3Points];
        ability3Duration = ability3CoolDowns[ability3Points];
        ability3Cost = ability3ResourceCosts[ability3Points];
        leveledAnAbility();
    }
    public void LevelAbility4()  {
        leveledAbility4();
        ability4Damage = ability4Damages[ability4Points];
        ability4Duration = ability4CoolDowns[ability4Points];
        ability4Cost = ability4ResourceCosts[ability4Points];
        pointsAvailableForUltimate--;
        leveledAnAbility();
    }
    public void checkXP()   {
        // Debug.Log("Current XP: " + currentXP + "\tNextXP: " + xpRequiredToLevelUp[currentLevel] + "\tCurrent Level: " + currentLevel);
        if(currentLevel != 18 && (currentXP >= xpRequiredToLevelUp[currentLevel]) )   {
            if(currentXP > xpRequiredToLevelUp[currentLevel])  {
                int remainder = currentXP - xpRequiredToLevelUp[currentLevel];
                LeveledUp();
                SetXP(remainder);
            }
            else{
                LeveledUp();
            }
        }
    }
    public void LeveledUp()   {
        levelUpPoints++;
        currentXP = 0;
        currentLevel++;
        statsScript.AddGrowth(healthGrowth, manaGrowth, healthRegenGrowth, manaRegenGrowth, armorGrowth, magicResistGrowth, attackDamageGrowth);
        if(currentLevel == 6 || currentLevel == 11 || currentLevel == 16){
            pointsAvailableForUltimate++;
        }
        
    }
    public int GetNextXPLevel(){
        if(currentLevel != 18){
            return xpRequiredToLevelUp[currentLevel + 1];
        }
        else
            return 0;
    }
    public void AddGold(int moreGold){this.gold += moreGold;}
    public int GetXP(){return currentXP;}
    public void SetXP(int xp){currentXP = xp;}
    public void AddXP(int addThisXP){currentXP += addThisXP;}
    public int GetCurrentLevel(){return currentLevel;}
    public void leveledAnAbility(){this.levelUpPoints--;}
    public int GetSkillablePoints(){return this.levelUpPoints; }
    public int GetPointsAvailableForUltimate(){return this.pointsAvailableForUltimate;}
    public float getMissileSpeed(){return this.missileSpeed;}
    // Get the Sprites of this champion
    // public Sprite GetChampionPortrait()  {   return this.championPortrait; }
    public Sprite GetAbilityImage1(){return abilityImage1;}
    public Sprite GetAbilityImage2(){return abilityImage2;}
    public Sprite GetAbilityImage3(){return abilityImage3;}
    public Sprite GetAbilityImage4(){return abilityImage4;}
    public T GenericMethod<T>(T param){return param;}
}