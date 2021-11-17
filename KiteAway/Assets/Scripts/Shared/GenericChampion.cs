using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GenericChampion : MonoBehaviour    {
    int currentLevel = 0;
    private int currentXP = 1;
    private int[] xpRequiredToLevelUp = {
        1, 280, 380, 480, 580, 680,
        780, 880, 980, 1080, 1180, 1280,
        1380, 1480, 1580, 1680, 1780, 1880
    };
    public Stats statsScript;
    // cost of Mana
    float currentAbilityOneCost;
    float currentAbilityTwoCost;
    float currentAbilityThreeCost;
    float currentAbilityFourCost;
    float currentAbilityDamageOne;
    float currentAbilityDamageTwo;
    float currentAbilityDamageThree;
    float currentAbilityDamageFour;
    int pointsInAbilityOne = 0;   // starts at 0, and at most can go to 5 in most cases
    int pointsInAbilityTwo = 0;   // starts at 0, and at most can go to 5 in most cases
    int pointsInAbilityThree = 0;   // starts at 0, and at most can go to 5 in most cases
    int pointsInAbilityFour = 0;   // starts at 0, and at most can go to 5 in most cases
    float currentAbilityCoolDownOne = 0;
    float currentAbilityCoolDownTwo = 0;
    float currentAbilityCoolDownThree = 0;
    float currentAbilityCoolDownFour = 0;
    // Methods I can override for new champions I implement
    public virtual void useAbilityOne()     {}
    public virtual void useAbilityTwo()     {}
    public virtual void useAbilityThree()   {}
    public virtual void useAbilityFour()    {}
    public virtual void onLevelUp(string skill) {}
    public void SetCostOfAbilityOne(float resourceCost)     { currentAbilityOneCost = resourceCost; }
    public void SetCostOfAbilityTwo(float resourceCost)     { currentAbilityTwoCost = resourceCost; }
    public void SetCostOfAbilityThree(float resourceCost)   { currentAbilityThreeCost = resourceCost; }
    public void SetCostOfAbilityFour(float resourceCost)    { currentAbilityFourCost = resourceCost; }
    // Set New Cooldown for these Abilities 1 - 4
    public void SetCurrentAbilityCooldownOne(float newCooldown)  {  this.currentAbilityCoolDownOne = newCooldown;  }
    public void SetCurrentAbilityCooldownTwo(float newCooldown)  {  this.currentAbilityCoolDownTwo = newCooldown;  }
    public void SetCurrentAbilityCooldownThree(float newCooldown)  {    this.currentAbilityCoolDownThree = newCooldown;    }
    public void SetCurrentAbilityCooldownFour(float newCooldown)  { this.currentAbilityCoolDownFour = newCooldown; }
    public T GenericMethod<T>(T param)
    {
        return param;
    }
    // Set current damage for Abilities 1 through 4
    public void SetCurrentAbilityDamageOne(float newDamage)  {  this.currentAbilityDamageOne = newDamage;  }
    public void SetCurrentAbilityDamageTwo(float newDamage)  {  this.currentAbilityDamageTwo = newDamage;  }
    public void SetCurrentAbilityDamageThree(float newDamage)  {    this.currentAbilityDamageThree = newDamage;    }
    public void SetCurrentAbilityDamageFour(float newDamage)  { this.currentAbilityDamageFour = newDamage; }
    // Get current damage for Abilities 1 through 4
    public float GetCurrentAbilityDamageOne()  {  return this.currentAbilityDamageOne;  }
    public float GetCurrentAbilityDamageTwo()  {  return this.currentAbilityDamageTwo;  }
    public float GetCurrentAbilityDamageThree()  {    return this.currentAbilityDamageThree;    }
    public float GetCurrentAbilityDamageFour()  { return this.currentAbilityDamageFour; }
    // Get current cooldowns for Abilities 1 through 4
    public float GetCurrentAbilityCooldownOne()  {  return this.currentAbilityCoolDownOne;  }
    public float GetCurrentAbilityCooldownTwo()  {  return this.currentAbilityCoolDownTwo;  }
    public float GetCurrentAbilityCooldownThree()  {    return this.currentAbilityCoolDownThree;    }
    public float GetCurrentAbilityCooldownFour()  { return this.currentAbilityCoolDownFour; }
    // Get current Rank for an ability
    public int GetCurrentPointsInAbilityOne()  {  return this.pointsInAbilityOne; }
    public int GetCurrentPointsInAbilityTwo()  {  return this.pointsInAbilityTwo; }
    public int GetCurrentPointsInAbilityThree()  {    return this.pointsInAbilityThree;   }
    public int GetCurrentPointsInAbilityFour()  { return this.pointsInAbilityFour;    }
    // Get Current Resource Cost of Abilities 1 through 4
    public float GetLevelAbilityOneCost()  {    return this.currentAbilityOneCost;   }
    public float GetLevelAbilityTwoCost()  {    return this.currentAbilityTwoCost;   }
    public float GetLevelAbilityThreeCost()  {  return this.currentAbilityThreeCost; }
    public float GetLevelAbilityFourCost()  {   return this.currentAbilityFourCost;  }
    // Level an ability as well as level the champion
    public void LevelAbilityOne()  {
        this.pointsInAbilityOne++;
        this.currentLevel++;
    }
    public void LevelAbilityTwo()  {
        this.pointsInAbilityTwo++;
        this.currentLevel++;
    }
    public void LevelAbilityThree()  {
        this.pointsInAbilityThree++;
        this.currentLevel++;
    }
    public void LevelAbilityFour()  {
        this.pointsInAbilityFour++;
        this.currentLevel++;
    }
    public void checkXP()   {
        if(currentXP == xpRequiredToLevelUp[currentLevel])   {
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
    public void GiveStatsScript(Stats script)   { statsScript = script; }
    public void LeveledUp() { this.currentXP = 0; this.currentLevel++; }
    public float GetXP() {   return this.currentXP;  }
    public void SetXP(int xp) {   this.currentXP = xp;    }
    public void AddXP(int addThisXP) {    this.currentXP += addThisXP;    }
    private void Update() { checkXP();  }
}
