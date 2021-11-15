using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Stats : MonoBehaviour  {
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
    public float GetAttackSpeed()   {   return this.attackSpeed;    }
    public void SetAttackSpeed(float newAttackSpeed)   {   this.attackSpeed = newAttackSpeed;    }
    public float GetMana()  {    return this.mana;  }
    // effective health formula : E = ( 1 + (resistance / 100)) * normal health
    // Use for Attack Damage
    public void takePhysicalDamage(float totalDamage, float baseDamage, float bonusDamage, float armorPen)    {
        float effectiveHealthArmor = (1 + (armor / 100)) * health;
    }
    // Use for Magic Damage
    public void takeMagicDamage(string typeOfDamage, float damage)    {
        float effectiveHealthMagicResist = (1 + (magicResist / 100)) * health;
    }
    // use for auto attacks : Two ways to take basic attack damage so two constructors
    // armorPen and magicPen need to arrive in < 1. : example: 80 % armor pen = .8
    public void takeBasicDamage(float damage)    {
        float effectiveHealthAndArmor = (1 + (armor / 100)) * health;
        float formulaForReducedDamage = health / effectiveHealthAndArmor;
        float damageTaken = damage * formulaForReducedDamage;
        this.health -= damage;
    }
    public void takeBasicDamage(float damage, float armorPenPercent)    {
        float armorAfterArmorPen = (armor * armorPenPercent) + (bonusArmor * armorPenPercent);
        float effectiveHealthAndArmor = effectiveHealthAndArmor = (1 + (armorAfterArmorPen / 100)) * health;
        float formulaForReducedDamage = health / effectiveHealthAndArmor;
        float damageTaken = damage * formulaForReducedDamage;
        this.health -= damage;
    }
    // mixed damage would go here
    public void takeBasicDamage(float totalDamage, float armorPenPercent, float magicPen)    {
        float armorAfterArmorPen = (armor * armorPenPercent) + (bonusArmor * armorPenPercent);
        float effectiveHealthAndArmor = effectiveHealthAndArmor = (1 + (armorAfterArmorPen / 100)) * health;
        // mixedDamage goes here
    }
}