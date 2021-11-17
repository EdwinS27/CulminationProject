using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ezreal : GenericChampion {
    [SerializeField]    GameObject prefabMysticShot;
    [SerializeField]    GameObject prefabEssenceFlux;
    [SerializeField]    GameObject prefabArcaneBarrage;
    public float maxShiftRange;
    // Variables for Ability One Base Damage and Base Cooldown and base Mana Cost
    float[] baseAbilityOneManaCost = {28f, 45f, 70f, 95f, 120f};
    float[] baseAbilityOneCoolDown = {5.5f, 5.25f, 5f, 4.75f, 4.5f}; // Base Cooldown for Each Level Rank in the ability
    float[] baseAbilityOneDamage = {20f, 45f, 75f, 95f, 120f}; // Base Damage for each Level Rank in the ability
    float[] baseAbilityTwoManaCost = {50f, 50f, 50f, 50f, 50f};
    // Variables for Ability Two Base Damage and Base Cooldown
    float[] baseAbilityTwoCoolDown = {12f, 12f, 12f, 12f, 12f}; // Base Cooldown for Each Level Rank in the ability baseAbilityTwoCoolDown
    float[] baseAbilityTwoDamage = {80f, 135f, 190f, 245f, 300f}; // Base Damage for each Level Rank in the ability
    // Variables for Ability Three Base Damage and Base Cooldown
    float[] baseAbilityThreeCoolDown = {28f, 25f, 22f, 19f, 16f}; // Base Cooldown for Each Level Rank in the ability baseAbilityTwoDamage
    float[] baseAbilityThreeDamage = {80f, 130f, 180f, 230f, 280f}; // Base Damage for each Level Rank in the ability
    // Variables for Ability Four Base Damage and Base Cooldown
    float[] baseAbilityFourCoolDown = {120f, 120f, 120f}; // Base Cooldown for Each Level Rank in the ability baseAbilityTwoDamage
    float[] baseAbilityFourDamage = {350f, 500f, 650f}; // Base Damage for each Level Rank in the ability
    // Variables for passive :
    int stacks = 0; // amount of stacks , limit is 5
    float stacksDuration; // duration of stack, interacts with Time.DeltaTime
    float bonusAttackSpeed;
    float originalAttackSpeed;
    Vector3 skillShotTargetLocation;
    private void Start() {
        GiveStatsScript(GetComponent<Stats>());
    }
    void Update()   {
        passive();
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
            if(bonusAttackSpeed == -1)   {
                statsScript.SetAttackSpeed(originalAttackSpeed);
            }
        }
        if(stacksDuration < 0)
            bonusAttackSpeed = -1;
    }

    public float getStacks()    {   return this.stacks; }
    public float getStackDuration() {   return this.stacksDuration;    }
    public void setStacks(int stacksIn)   { this.stacks = stacksIn;  }
    public void resetStackDuration()   {    this.stacksDuration = 6;    }
    public Vector3 GetTargetLocation<Vector3>(Vector3 param)    {   return param;   }
    public void InteractWithStacks() {
        if(stacks == 0) {
            originalAttackSpeed = statsScript.GetAttackSpeed();
            stacks++;
        }
        else if(stacks < 5)
            stacks++;
        else
            stacks = 5;
        // after we check these conditions
        bonusAttackSpeed = originalAttackSpeed * stacks;
        statsScript.SetAttackSpeed(bonusAttackSpeed);
        Debug.Log("Original Attack Speed: " + originalAttackSpeed + "\t" + "Bonus Attack Speed" + bonusAttackSpeed);
    }
    // overrides the generic champion abilityOne
    public override void useAbilityOne() {
        if(statsScript.GetMana() - GetLevelAbilityOneCost() >= 0 && GetCurrentPointsInAbilityOne() > 0){
            statsScript.DeductMana(GetLevelAbilityOneCost());
            GetMousePositionForSkillShot(); // if we can use the ability then, and only then do we get the mouse position
            SpawnMysticShot();
        }
    }
    // overrides the generic champion abilityTwo
    public override void useAbilityTwo() {
        if(statsScript.GetMana() - GetLevelAbilityTwoCost() >= 0 && GetCurrentPointsInAbilityTwo() > 0){
            statsScript.DeductMana(GetLevelAbilityTwoCost());
            GetMousePositionForSkillShot(); // if we can use the ability then, and only then do we get the mouse position
            SpawnEssenceFlux();
        }
    }
    // overrides the generic champion abilityTwo
    public override void useAbilityThree() {
        Debug.Log("You Can Use Ability Rank Three ");
        if(statsScript.GetMana() - GetLevelAbilityThreeCost() >= 0 && GetCurrentPointsInAbilityThree() > 0)  {
            statsScript.DeductMana(GetLevelAbilityThreeCost());
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000)) {
                Debug.Log("Character position: " + transform.position + "\tE destination: " + hit.point);
                float movedX = ChangeInXDuringShift(transform.position, hit.point);
                float movedZ = ChangeInZDuringShift(transform.position, hit.point);
                Debug.Log("Position of Player before shift: " + transform.position);
                Shift(movedX, movedZ);
                Debug.Log("Position of Player after shift: " + transform.position);
            }
        }
    }
    // overrides the generic champion abilityTwo
    public override void useAbilityFour() {
        if(statsScript.GetMana() - GetLevelAbilityFourCost() >= 0 && GetCurrentPointsInAbilityFour() > 0){
            statsScript.DeductMana(GetLevelAbilityFourCost());
            GetMousePositionForSkillShot(); // if we can use the ability then, and only then do we get the mouse position
            SpawnArcaneBarrage();
        }
    }
    public void SpawnMysticShot() {
        // useSkillShot = true;
        var createdMysticShot = Instantiate(prefabMysticShot, transform.position, transform.rotation);
        Debug.Log(createdMysticShot);
        createdMysticShot.transform.rotation.SetLookRotation(transform.position);
        createdMysticShot.GetComponent<MysticShot>().SetTargetDestination(skillShotTargetLocation);
        // formula for bonus mystic shot damage is: bonus = abilityDamage + (totalAttackDamage * 130% or 1.3) + (15 % totalAbilityDamage)
        float bonusMysticShotDamage = GetCurrentAbilityDamageOne() + (statsScript.attackDamage * 1.3f) + (statsScript.abilityDamage * .15f);
        createdMysticShot.GetComponent<MysticShot>().setMysticShotDamage(bonusMysticShotDamage, statsScript.armorPen, statsScript.magicPen);
    }
    public void SpawnEssenceFlux()  {
        // useSkillShot = true;
        var createdEssenceFlux = Instantiate(prefabEssenceFlux, transform.position, transform.rotation);
        createdEssenceFlux.GetComponent<EssenceFlux>().SetTargetDestination(skillShotTargetLocation);
    }
    public void SpawnArcaneBarrage()  {
        // useSkillShot = true;
        var createdArcaneBarrage = Instantiate(prefabArcaneBarrage, transform.position, transform.rotation);
        createdArcaneBarrage.GetComponent<ArcaneBarrage>().SetTargetDestination(skillShotTargetLocation);
    }
    void GetMousePositionForSkillShot() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000)) {
            skillShotTargetLocation = (transform.position - hit.point).normalized;
        }
    }
    // Update is called once per frame
    
    // Does the actual change itself.
    void Shift(float changeInX, float changeInZ)    {
        // In shift I need to ask a function to determine if we are adding or subtracting to our transform.position
        // two parameters I will need are, changeInX and changeInZ
        // each changeWill then need to be asked if it will be
        if(changeInX > 0){
            transform.position = new Vector3(
                transform.position.x + changeInX,
                transform.position.y,
                transform.position.z
            );
        }
        else    {
            transform.position = new Vector3(
                transform.position.x + changeInX,
                transform.position.y,
                transform.position.z
            );
        }
        // if the change in transform.position.z is greater than 0 we are going towards
        if(changeInZ > 0){
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                transform.position.z - ( changeInZ * - 1)
            );
        }
        else    {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                transform.position.z + changeInZ
            );
        }
    }
    // The following Methods are for using Shift
    public float ChangeInXDuringShift(Vector3 characterLocation, Vector3 shiftDestination) {
        float rangeOfShiftX = characterLocation.x - shiftDestination.x;
        if(characterLocation.x > shiftDestination.x){
            //Debug.Log("We are going Right or forwards" + rangeOfShiftX);
            return Mathf.Abs(rangeOfShiftX) < maxShiftRange ? Mathf.Abs(rangeOfShiftX) * -1: maxShiftRange * -1;
        }
        else{
            //Debug.Log("We are going Left or backwards" + Mathf.Abs(rangeOfShiftX) + " " + maxShiftRange);
            return Mathf.Abs(rangeOfShiftX) < maxShiftRange ? Mathf.Abs(rangeOfShiftX): maxShiftRange;
        }
    }
    // Calculates the change in X and Z to move the character correctly based on player's desired location
    public float ChangeInZDuringShift(Vector3 characterLocation, Vector3 shiftDestination) {
        float rangeOfShiftZ = characterLocation.z - shiftDestination.z;
        if(characterLocation.z > shiftDestination.z){
            //Debug.Log("We are going Up or upwards " + rangeOfShiftZ);
            return Mathf.Abs(rangeOfShiftZ) < maxShiftRange ? rangeOfShiftZ: maxShiftRange * -1;
        }
        else{
            //Debug.Log("We are going down or downwards " + Mathf.Abs(rangeOfShiftZ) + " " + maxShiftRange);
            return Mathf.Abs(rangeOfShiftZ) < maxShiftRange ? Mathf.Abs(rangeOfShiftZ): maxShiftRange;
        }
    }
    
    public override void onLevelUp(string skill) {
        Debug.Log("Skill being leveled up: " + skill);
        if(skill == "Q" || skill == "q")    {
            this.LevelAbilityOne();
            SetCurrentAbilityDamageOne(baseAbilityOneDamage[GetCurrentPointsInAbilityOne()]);
            SetCurrentAbilityCooldownOne(baseAbilityOneCoolDown[GetCurrentPointsInAbilityOne()]);
            SetCostOfAbilityOne(baseAbilityOneManaCost[GetCurrentPointsInAbilityOne()]);
        }
        else if(skill == "W" || skill == "w"){
            this.LevelAbilityTwo();
            SetCurrentAbilityDamageTwo(baseAbilityTwoDamage[GetCurrentPointsInAbilityTwo()]);
            SetCurrentAbilityCooldownTwo(baseAbilityTwoCoolDown[GetCurrentPointsInAbilityTwo()]);
            SetCostOfAbilityOne(60);
        }
        else if(skill == "E" || skill == "e"){
            this.LevelAbilityThree();
            SetCurrentAbilityDamageThree(baseAbilityThreeDamage[GetCurrentPointsInAbilityThree()]);
            SetCurrentAbilityCooldownThree(baseAbilityThreeCoolDown[GetCurrentPointsInAbilityThree()]);
            SetCostOfAbilityThree(90);
        }
        else{
            this.LevelAbilityFour();
            SetCurrentAbilityDamageFour(baseAbilityFourDamage[GetCurrentPointsInAbilityFour()]);
            SetCurrentAbilityCooldownFour(baseAbilityFourCoolDown[GetCurrentPointsInAbilityFour()]);
            SetCostOfAbilityFour(100);
        }
        Debug.Log("Skill being leveled up: " + skill);
    }
}
