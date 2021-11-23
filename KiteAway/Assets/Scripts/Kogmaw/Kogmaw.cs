using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kogmaw : GenericChampion {
    public GameObject offset;
    [SerializeField]    GameObject causticSpittle;
    [SerializeField]    GameObject voidOoze;
    [SerializeField]    GameObject livingArtillery;
    // Variables Excluse to Kogmaw
    
    // Variables for passive :
    int stacks = 0; // amount of stacks , limit is 5
    float stacksDuration; // duration of stack, interacts with Time.DeltaTime
    float bonusAttackSpeed;
    float originalAttackSpeed;
    private bool activateAttackRangeIncrease = false;
    private float timer;
    private float[] gainBonusAttackSpeed = {1.5f, 2f, 2.5f, 3f, 3.5f};
    private float[] gainBonusAttackRange = {1.3f, 1.5f, 1.7f, 1.9f, 2.1f};
    private float attackRangeIncreaseTimer = 8f;
    private float originalAttackRange;
    private float newAttackRange; // needed for attack range increase
    Vector3 skillShotTargetLocation;
    void Update()   {
        if(activateAttackRangeIncrease){
            if(timer > 0)
                timer -= Time.deltaTime;
            else{
                activateAttackRangeIncrease = false;
                statsScript.SetAttackRange(originalAttackRange);
            }
        }
        passive();
        checkXP();
    }
    void passive()  {
        if(stacksDuration > 0)   {
            //Debug.Log("Stacks: " + stacks + " Stack Duration: " + stackDuration);
            stacksDuration -= Time.deltaTime;
            //Debug.Log("Attack Speed: " + statScript.GetAttackSpeed() + " Stacks: " + stacks);
        }
        else    {
            stacksDuration = 0;
            stacks = 0;
        }
        if(stacksDuration < 0)
            bonusAttackSpeed = -1;
    }

    public float getStacks()    {   return this.stacks; }
    public float getStackDuration() {   return this.stacksDuration;    }
    public void setStacks(int stacksIn)   { this.stacks = stacksIn;  }
    public void resetStackDuration()   {    this.stacksDuration = 8;    }
    public Vector3 GetTargetLocation<Vector3>(Vector3 param)    {   return param;   }
    public void InteractWithStacks() {
        if(stacks == 0) {
            stacks++;
            SetAbility4Cost((stacks) * 40);
        }
        else if(stacks < 10)
            stacks++;
        else
            stacks = 10;
        // after we check these conditions
        SetAbility4Cost((stacks) * 40);
        // Debug.Log("Original Attack Speed: " + originalAttackSpeed + "\t" + "Bonus Attack Speed" + bonusAttackSpeed);
    }
    // overrides the generic champion abilityOne
    public override void UseAbility1() {
        // Debug.Log("Ezreal used Ability 1");
        statsScript.DeductMana(GetAbility1Cost());
        GetMoUsePositionForSkillShot(); // if we can Use the ability then, and only then do we get the moUse position
        SpawnCausticSpittle();
    }
    // overrides the generic champion abilityTwo
    public override void UseAbility2() {
        if((statsScript.GetMana() - GetAbility2Cost() >= 0) && (GetAbility2Points() > -1)){
            statsScript.DeductMana(GetAbility2Cost());
            ActivateBioArcaneBarrage();
        }
    }
    // overrides the generic champion abilityTwo
    public override void UseAbility3() {
        if(statsScript.GetMana() - GetAbility3Cost() >= 0 && GetAbility3Points() > -1)  {
            statsScript.DeductMana(GetAbility3Cost());
            GetMoUsePositionForSkillShot(); // if we can Use the ability then, and only then do we get the moUse position
            SpawnVoidOoze();
        }
    }
    // overrides the generic champion abilityTwo
    public override void UseAbility4() {
        if( (statsScript.GetMana() - GetAbility4Cost() >= 0) && (GetAbility4Points() > -1) ){
            statsScript.DeductMana(GetAbility4Cost());
            SpawnLivingArtillery();
        }
    }
    public void SpawnCausticSpittle() {
        // UseSkillShot = true;
        var causticSpittleShot = Instantiate(causticSpittle, offset.transform.position, transform.rotation);
        float totalMagicDamage = statsScript.GetAbilityDamage() * 1.7f;
        causticSpittleShot.GetComponent<CausticSpittle>().setCausticSpittleDamage(totalMagicDamage, statsScript.GetMagicPen(), getMissileSpeed()); // damageInput, float magicPen, float missileSpeed
    }
    public void ActivateBioArcaneBarrage()  {
        timer = attackRangeIncreaseTimer;
        originalAttackRange = statsScript.GetAttackRange();
        activateAttackRangeIncrease = true;
        newAttackRange = originalAttackRange + gainBonusAttackRange[GetAbility2Points()];
        Debug.Log(newAttackRange);
        statsScript.SetAttackRange(newAttackRange);
    }
    public void SpawnVoidOoze()  {
        // UseSkillShot = true;
        var voidOozeShot = Instantiate(voidOoze, offset.transform.position, transform.rotation);
    }
    public void SpawnLivingArtillery()  {
        // UseSkillShot = true;
        stacks++;
        var livingArtilleryShot = Instantiate(livingArtillery, offset.transform.position, transform.rotation);
    }
    void GetMoUsePositionForSkillShot() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000)) {
            skillShotTargetLocation = (transform.position - hit.point).normalized;
        }
    }
    new public void LevelAbility1()  {
        leveledAbility1();
        SetDamageAbility1(90 + (GetAbility1Points() * 50));
        SetAbility1Cd(8);
        SetAbility1Cost(40);
        statsScript.AddBonusAttackSpeed(gainBonusAttackSpeed[GetAbility1Points()]);
        // Debug.Log("Cost: " + ability1Cost + "\tDuration: " + ability1Duration);
    }
    new public void LevelAbility2()  {
        leveledAbility2();
        SetDamageAbility1(90 + (GetAbility1Points() * 50));
        SetAbility2Cd(17);
        SetAbility2Cost(40);
        // Debug.Log("Cost: " + ability1Cost + "\tDuration: " + ability1Duration);
    }
}
