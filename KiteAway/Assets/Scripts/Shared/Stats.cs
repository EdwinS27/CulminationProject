using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Stats : MonoBehaviour  {
    [SerializeField]    int xp;
    public float health;
    public float maxHealth;
    public float cooldownReduction;
    public float abilityDamage;
    public float magicPen;
    public float attackDamage;
    public float bonusAttackDamage;
    public float totalAttackDamage;
    public float attackSpeed;
    public float attackTime;
    public float maxMana;
    public float armorPen;
    public float mana;
    public float armor;

    public float bonusArmor;
    public float magicResist;
    public float bonusMagicResist;
    public float healthRegen;
    public float moveSpeed;
    public float criticalAttackDamage;
    public float gold;
    public float exprerienceProvided;
    HeroCombat heroCombatScript;
    void Start()    {
        heroCombatScript = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroCombat>();
    }
    // Update is called once per frame
    void Update()   {
        if (health <= 0)    {
            Destroy(gameObject);
            heroCombatScript.targetedEnemy = null;
            heroCombatScript.performRangedAttack = false;
        }
    }
    // effective health formula : E = ( 1 + (resistance / 100)) * normal health
    // Use for Attack Damage
    public void takeBasicDamage(float damage, float armorPenPercent)    {
        float armorAfterArmorPen = (this.armor * armorPenPercent) + (this.bonusArmor * armorPenPercent);
        float effectiveHealthAndArmor = (1 + (armorAfterArmorPen / 100)) * this.health;
        float formulaForReducedDamage = this.health / effectiveHealthAndArmor;
        float damageTaken = damage * formulaForReducedDamage;
        this.health -= damageTaken;
    }
    public void takeBasicDamage(float damage, float armorPen, float magicPen)  {
        // damage take via armor
        float armorAfterArmorPen = (this.armor * armorPen) + (this.bonusArmor * armorPen);
        float effectiveHealthAndArmor = (1 + (armorAfterArmorPen / 100)) * this.health;
        float formulaForReducedDamageForArmor = this.health / effectiveHealthAndArmor;
        float damageTakenArmor = damage * formulaForReducedDamageForArmor;
        // damage take via armor
        float magicAfterMagicPen = (this.armor * armorPen) + (this.bonusArmor * armorPen);
        float effectiveHealthAndMagicResist = (1 + (magicAfterMagicPen / 100)) * this.health;
        float formulaForReducedDamageForMagicResist = this.health / effectiveHealthAndMagicResist;
        float damageTakenMagicResist = damage * formulaForReducedDamageForMagicResist;
        float totalDamageTaken = damageTakenArmor + damageTakenMagicResist;
        Debug.Log(totalDamageTaken);
        this.health -= totalDamageTaken;
    }
    // Use for Magic Damage
    // use for auto attacks : Two ways to take basic attack damage so two constructors
    // armorPen and magicPen need to arrive in < 1. : example: 80 % armor pen = .8
    public void takeMagicDamage(float damage)    {
        float effectiveHealthAndMagicResist = (1 + (this.magicResist / 100)) * health;
        float formulaForReducedDamage = health / effectiveHealthAndMagicResist;
        float damageTaken = damage * formulaForReducedDamage;
        this.health -= damage;
    }
    public void takeMagicDamage(float damage, float magicPenPercent)    {
        float magicResistAfterPen = (this.magicResist * magicPenPercent) + (bonusMagicResist * magicPenPercent);
        float effectiveHealthAndArmor = effectiveHealthAndArmor = (1 + (magicResistAfterPen / 100)) * health;
        float formulaForReducedDamage = health / effectiveHealthAndArmor;
        float damageTaken = damage * formulaForReducedDamage;
        this.health -= damage;
    }
    // mixed damage would go here
    public void takeMagicDamage(float totalDamage, float magicPenPercent, float magicPen)    {
        float armorAfterArmorPen = (this.magicResist * magicPenPercent) + (bonusArmor * magicPenPercent);
        float effectiveHealthAndArmor = effectiveHealthAndArmor = (1 + (armorAfterArmorPen / 100)) * this.health;
        // mixedDamage goes here
    }
    // For taking basic damage : 
    public void takePhysicalDamage(float damage)    {
        float effectiveHealthAndArmor = (1 + (armor / 100)) * health;
        float formulaForReducedDamage = health / effectiveHealthAndArmor;
        float damageTaken = damage * formulaForReducedDamage;
        this.health -= damage;
    }
    public void takePhysicalDamage(float damage, float armorPenPercent)    {
        float armorAfterArmorPen = (armor * armorPenPercent) + (bonusArmor * armorPenPercent);
        float effectiveHealthAndArmor = effectiveHealthAndArmor = (1 + (armorAfterArmorPen / 100)) * health;
        float formulaForReducedDamage = health / effectiveHealthAndArmor;
        float damageTaken = damage * formulaForReducedDamage;
        this.health -= damage;
    }
    // mixed damage would go here
    public void takePhysicalDamage(float totalDamage, float armorPenPercent, float magicPen)    {
        float armorAfterArmorPen = (armor * armorPenPercent) + (bonusArmor * armorPenPercent);
        float effectiveHealthAndArmor = effectiveHealthAndArmor = (1 + (armorAfterArmorPen / 100)) * health;
        // mixedDamage goes here
    }
    public void DeductMana(float manaCost)   {
        this.mana -= manaCost;
    }
    
    public float GetAttackSpeed()   {   return this.attackSpeed;    }
    public void SetAttackSpeed(float newAttackSpeed)   {   this.attackSpeed = newAttackSpeed;    }
    public float GetMana()  {    return this.mana;  }
    public float GetMaxMana()   {   return this.maxMana;    }
    public void AddMana(float moreMana)  {    this.mana += moreMana;  }
    public void SetMaxMana(float moreMana)   {   this.maxMana += moreMana;    }
    public int GetXP()  {   return this.xp; }
}