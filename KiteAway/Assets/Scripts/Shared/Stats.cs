using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Stats : MonoBehaviour  {
    [SerializeField]    int xpWorth;
    // Important Variables
    [SerializeField]    float rotationSpeed;
    [SerializeField]    float health;
    [SerializeField]    float maxHealth;
    [SerializeField]    int currentLevel;
    [SerializeField]    int goldWorth;
    [SerializeField]    float moveSpeed;
    [SerializeField]    float abilityDamage;
    [SerializeField]    float cooldownReduction;
    // Attack Variables
    [SerializeField]    float attackRange;
    private float totalAttackSpeed;
    [SerializeField]    float attackSpeed;
    [SerializeField]    float bonusAttackSpeed;
    [SerializeField]    float attackTime;
    [SerializeField]    float attackDamage;
    [SerializeField]    float bonusAttackDamage; // gets added to with items... PENDING
    float totalAttackDamage; // the value of base attackDamage and bonusAttackDamage
    [SerializeField]    float criticalAttackDamage; // as a percentage
    // Mana / Resource variables
    [SerializeField]    float mana;
    [SerializeField]    float maxMana;
    [SerializeField]    float manaRegen;
    // Armor Variables
    [SerializeField]    float armorPen;
    [SerializeField]    float armor;
    [SerializeField]    float bonusArmor;
    // Magic Resist Variables
    [SerializeField]    float magicPen;
    [SerializeField]    float magicResist;
    [SerializeField]    float bonusMagicResist;
    [SerializeField]    float healthRegen;
    private GameObject sendsTheirRegards;
    void Start(){this.health = this.maxHealth;}
    // Update is called once per frame
    void Update(){
        if (health <= 0)
            death();
    }
    void death(){
        if(gameObject.GetComponent<Targetable>() != null)   {
            if(gameObject.tag != "Player"){
                sendsTheirRegards.GetComponent<GenericChampion>().AddXP(GetXPWorth());
                sendsTheirRegards.GetComponent<GenericChampion>().AddGold(GetGold());
                sendsTheirRegards.GetComponent<HeroCombat>().setTargetedEnemy(null);
                Destroy(this.gameObject); // Kill any object
            }
        }
    }
    // effective health formula : E = ( 1 + (resistance / 100)) * normal health
    public void takeBasicDamage(GameObject sentFrom, float damage)   {
        this.sendsTheirRegards = sentFrom;
        float effectiveHealthAndArmor = (1 + (this.armor / 100)) * this.health;
        float formulaForReducedDamage = this.health / effectiveHealthAndArmor;
        float damageTaken = damage * formulaForReducedDamage;
        // Debug.Log("Object: " + gameObject.name + "\tDamage: " + damage  + "\t DamageTaken: " + damageTaken);
        this.health -= damageTaken;
    }
    public void takeBasicDamage(GameObject sentFrom, float damage, float armorPenPercent)    {
        this.sendsTheirRegards = sentFrom;
        float armorAfterArmorPen = (this.armor * armorPenPercent) + (this.bonusArmor * armorPenPercent);
        float effectiveHealthAndArmor = (1 + (armorAfterArmorPen / 100)) * this.health;
        float formulaForReducedDamage = this.health / effectiveHealthAndArmor;
        float damageTaken = damage * formulaForReducedDamage;
        this.health -= damageTaken;
    }
    public void takeBasicDamage(GameObject sentFrom, float damage, float armorPen, float magicPen)  {
        this.sendsTheirRegards = sentFrom;
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
        // Debug.Log("Total Damage Taken: " + totalDamageTaken);
        this.health -= totalDamageTaken;
    }
    // Use for Magic Damage
    // use for auto attacks : Two ways to take basic attack damage so two constructors
    // armorPen and magicPen need to arrive in < 1. : example: 80 % armor pen = .8
    public void takeMixedDamage(GameObject sentFrom, float baseMagicDamage, float bonusAD, float ap)   {
        this.sendsTheirRegards = sentFrom;
        // stuff
    }
    public void takeMagicDamage(GameObject sentFrom, float damage)    {
        this.sendsTheirRegards = sentFrom;
        float effectiveHealthAndMagicResist = (1 + (this.magicResist / 100)) * health;
        float formulaForReducedDamage = health / effectiveHealthAndMagicResist;
        float damageTaken = damage * formulaForReducedDamage;
        // Debug.Log("damageTaken: " + damageTaken);
        this.health -= damage;
    }
    public void takeMagicDamage(GameObject sentFrom, float damage, float magicPenPercent)    {
        this.sendsTheirRegards = sentFrom;
        float magicResistAfterPen = (this.magicResist * magicPenPercent) + (bonusMagicResist * magicPenPercent);
        float effectiveHealthAndArmor = effectiveHealthAndArmor = (1 + (magicResistAfterPen / 100)) * health;
        float formulaForReducedDamage = health / effectiveHealthAndArmor;
        float damageTaken = damage * formulaForReducedDamage;
        this.health -= damage;
    }
    // mixed damage would go here
    public void takeMagicDamage(GameObject sentFrom, float totalDamage, float magicPenPercent, float magicPen)    {
        this.sendsTheirRegards = sentFrom;
        float magicResistAfterPen = (this.magicResist * magicPenPercent) + (bonusMagicResist * magicPenPercent);
        float effectiveHealthAndArmor = effectiveHealthAndArmor = (1 + (magicResistAfterPen / 100)) * health;
        float formulaForReducedDamage = health / effectiveHealthAndArmor;
        float damageTaken = totalDamage * formulaForReducedDamage;
        this.health -= totalDamage;
        // mixedDamage goes here
    }
    // For taking basic damage : 
    public void takePhysicalDamage(GameObject sentFrom, float damage)    {
        this.sendsTheirRegards = sentFrom;
        float effectiveHealthAndArmor = (1 + (armor / 100)) * health;
        float formulaForReducedDamage = health / effectiveHealthAndArmor;
        float damageTaken = damage * formulaForReducedDamage;
        this.health -= damage;
    }
    public void takePhysicalDamage(GameObject sentFrom, float damage, float armorPenPercent)    {
        this.sendsTheirRegards = sentFrom;
        float armorAfterArmorPen = (armor * armorPenPercent) + (bonusArmor * armorPenPercent);
        float effectiveHealthAndArmor = effectiveHealthAndArmor = (1 + (armorAfterArmorPen / 100)) * health;
        float formulaForReducedDamage = health / effectiveHealthAndArmor;
        float damageTaken = damage * formulaForReducedDamage;
        this.health -= damage;
    }
    // mixed damage would go here
    public void takePhysicalDamage(GameObject sentFrom, float totalDamage, float armorPenPercent, float magicPen)    {
        this.sendsTheirRegards = sentFrom;
        float armorAfterArmorPen = (armor * armorPenPercent) + (bonusArmor * armorPenPercent);
        float effectiveHealthAndArmor = effectiveHealthAndArmor = (1 + (armorAfterArmorPen / 100)) * health;
        // mixedDamage goes here
    }
    public void AddBonusAttackSpeed(float attackSpeedAdd){this.bonusAttackSpeed+= attackSpeedAdd;  this.totalAttackSpeed += attackSpeedAdd;}
    public float GetRotationSpeed() {   return this.rotationSpeed;}
    public void SetAttackRange(float newAttackRange){this.attackRange = newAttackRange;}
    // Attack Speed Getters and Setters and Other Methods
    public float GetAttackRange(){return this.attackRange;}
    public float GetAttackSpeed(){return this.attackSpeed;}
    public void SetAttackSpeed(float newAttackSpeed){this.attackSpeed = newAttackSpeed;}
    // Mana Getters and Setters and Other Methods
    public float GetMana(){return this.mana;}
    public void SetMana(float newMana){this.mana = newMana;}
    public float GetMaxMana(){return this.maxMana;}
    public void AddMana(float moreMana){this.mana += moreMana;}
    public void DeductMana(float manaCost){this.mana -= manaCost;}
    public void SetMaxMana(float moreMana){this.maxMana += moreMana;}
    // Health Getters and Setters and Other Methods
    public void AddHealth(float add){ this.health += add;}
    public float GetHealth(){return health;}
    public void SetHealth(float newHP){this.health = newHP;}
    public float GetMaxHealth(){return this.maxHealth;}
    public void SetMaxHealth(float newHP){this.maxHealth = newHP;}
    public float GetCooldownReduction(){return this.cooldownReduction;}
    public float GetAbilityDamage(){return this.abilityDamage;}
    public float GetMagicPen(){return this.magicPen;}
    public float GetAttackDamage(){return this.attackDamage;}
    public float GetBonusAttackDamage(){return this.bonusAttackDamage;}
    public float GetTotalAttackDamage(){return this.totalAttackDamage;}
    public float GetAttackTime(){return this.attackTime;}
    public float GetArmorPen(){return this.armorPen;}
    public float GetArmor(){return this.armor;}
    public float GetBonusArmor(){return this.bonusArmor;}
    public float GetMagicResist(){return this.magicResist;}
    public void AddMagicResist(float increase){ this.bonusMagicResist += increase; }
    public float GetBonusMagicResist(){return this.bonusMagicResist;}
    public float GetHealthRegen(){return this.healthRegen;}
    public float GetManaRegen(){return this.manaRegen;}
    public float GetMoveSpeed(){return this.moveSpeed;}
    public float GetCriticalAttackDamage()  {return this.criticalAttackDamage;}
    public int GetGold(){return this.goldWorth;}
    public int GetXPWorth(){ return this.xpWorth;}
    public void AddMoveSpeed(float add){this.moveSpeed += add;}
    public void SetMoveSpeed(float moveSpeed){this.moveSpeed = moveSpeed;}
    public void ReduceMoveSpeed(float decrease){this.moveSpeed -= decrease;}
    public void AffectMoveSpeed(float amount, float timeAffected){
        float originalMoveSpeed = GetMoveSpeed();
        float tempNewMoveSpeed = GetMoveSpeed() * amount;
        Debug.Log("MoveSpeeds before change: " + GetMoveSpeed() + " after should be: " + tempNewMoveSpeed);
        SetMoveSpeed(tempNewMoveSpeed);
        Debug.Log("Get MoveSpeed: " + GetMoveSpeed());
        while(timeAffected > 0){
            timeAffected -= Time.deltaTime;
        }
        Debug.Log("MoveSpeeds before change: " + GetMoveSpeed() + " after should be: "+ originalMoveSpeed);
        SetMoveSpeed(originalMoveSpeed);
        Debug.Log("Get MoveSpeed: " + GetMoveSpeed());
    }
    public void AddGrowth(float healthGrowth, float manaGrowth, float healthRegenGrowth, float manaRegenGrowth, float armorGrowth, float magicResistGrowth, float attackDamageGrowth) {
        this.health += healthGrowth;
        this.maxHealth += healthGrowth;
        this.mana += manaGrowth;
        this.maxMana += manaGrowth;
        this.healthRegen += healthRegenGrowth;
        this.manaRegen += manaRegenGrowth;
        this.armor += armorGrowth;
        this.magicResist += magicResistGrowth;
        this.attackDamage += attackDamageGrowth;
    }
}