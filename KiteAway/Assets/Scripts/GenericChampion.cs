using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericChampion : MonoBehaviour    {
    float currentLevel = 1;
    float abilityRankOne = 0;   // starts at 0, and at most can go to 5 in most cases
    float abilityRankTwo = 0;   // starts at 0, and at most can go to 5 in most cases
    float abilityRankThree = 1;   // starts at 0, and at most can go to 5 in most cases
    float abilityRankFour = 0;   // starts at 0, and at most can go to 5 in most cases
    float currentAbilityCoolDownOne = 0;
    float currentAbilityCoolDownTwo = 0;
    float currentAbilityCoolDownThree = 0;
    float currentAbilityCoolDownFour = 0;
    public virtual void useAbilityOne()     {}
    public virtual void useAbilityTwo()     {}
    public virtual void useAbilityThree()   {}
    public virtual void useAbilityFour()    {}
    public T GenericMethod<T>(T param)
    {
        return param;
    }
    public float GetCurrentAbilityCooldownOne()  {
        return this.currentAbilityCoolDownOne;
    }
    public float GetCurrentAbilityCooldownTwo()  {
        return this.currentAbilityCoolDownTwo;
    }
    public float GetCurrentAbilityCooldownThree()  {
        return this.currentAbilityCoolDownThree;
    }
    public float GetCurrentAbilityCooldownFour()  {
        return this.currentAbilityCoolDownFour;
    }
    public float GetCurrentAbilityRankOne()  {
        return this.abilityRankOne;
    }
    public float GetCurrentAbilityRankTwo()  {
        return this.abilityRankTwo;
    }
    public float GetCurrentAbilityRankThree()  {
        return this.abilityRankThree;
    }
    public float GetCurrentAbilityRankFour()  {
        return this.abilityRankFour;
    }
    public void GetCurrentAbilityRankOne(float newRank)  {
        this.abilityRankOne = newRank;
    }
    public void GetCurrentAbilityRankTwo(float newRank)  {
        this.abilityRankTwo = newRank;
    }
    public void GetCurrentAbilityRankThree(float newRank)  {
        this.abilityRankThree = newRank;
    }
    public void GetCurrentAbilityRankFour(float newRank)  {
        this.abilityRankFour = newRank;
    }
}
