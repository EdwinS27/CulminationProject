using UnityEngine;
public class Ezreal : GenericChampion {
    private Animator _anim;
    public GameObject offset;
    [SerializeField]    GameObject prefabMysticShot;
    [SerializeField]    GameObject prefabEssenceFlux;
    [SerializeField]    GameObject prefabArcaneBarrage;
    // Variables Excluse to Ezreal
    public float maxShiftRange;
    // Variables for passive :
    int stacks = 0; // amount of stacks , limit is 5
    float stacksDuration; // duration of stack, interacts with Time.DeltaTime
    float bonusAttackSpeed;
    float originalAttackSpeed;
    Vector3 skillShotTargetLocation;
    private void Start() {
        statsScript = GetComponent<Stats>();
        _anim = GetComponent<Animator>();
    }
    void Update()   {
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
        float s = stacks * .1f;
        // Debug.Log("float " + s);
        var n = s + originalAttackSpeed;
        // Debug.Log(n);
        bonusAttackSpeed = originalAttackSpeed + n;
        statsScript.SetAttackSpeed(bonusAttackSpeed);
        
        // Debug.Log("Original Attack Speed: " + originalAttackSpeed + "\tStacks: " + stacks + "\tNew Attack Speed: " + bonusAttackSpeed);
        // Debug.Log("Original Attack Speed: " + originalAttackSpeed + "\t" + "Bonus Attack Speed" + bonusAttackSpeed);
        resetStackDuration();
    }
    // overrides the generic champion abilityOne
    public override void UseAbility1() {
        GetMousePositionForSkillShot(); // if we can Use the ability then, and only then do we get the moUse position
        _anim.SetBool("ShootAbility", true);
        // Debug.Log("ShootAbility: " + _anim.GetBool("ShootAbility"));
        statsScript.DeductMana(GetAbility1Cost());
        SpawnMysticShot();
        // _anim.SetBool("Basic Attack", false);
        _anim.SetBool("ShootAbility", false);
    }
    // overrides the generic champion abilityTwo
    public override void UseAbility2() {
        if((statsScript.GetMana() - GetAbility2Cost() >= 0) && (GetAbility2Points() > -1)){
            statsScript.DeductMana(GetAbility2Cost());
            GetMousePositionForSkillShot(); // if we can Use the ability then, and only then do we get the moUse position
            SpawnEssenceFlux();
        }
    }
    // overrides the generic champion abilityTwo
    public override void UseAbility3() {
        if(statsScript.GetMana() - GetAbility3Cost() >= 0 && GetAbility3Points() > -1)  {
            statsScript.DeductMana(GetAbility3Cost());
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000)) {
                // Debug.Log("Character position: " + transform.position + "\tE destination: " + hit.point);
                float movedX = ChangeInXDuringShift(transform.position, hit.point);
                float movedZ = ChangeInZDuringShift(transform.position, hit.point);
                // Debug.Log("Position of Player before shift: " + transform.position);
                Shift(movedX, movedZ);
                // Debug.Log("Position of Player after shift: " + transform.position);
            }
        }
    }
    // overrides the generic champion abilityTwo
    public override void UseAbility4() {
        if( (statsScript.GetMana() - GetAbility4Cost() >= 0) && (GetAbility4Points() > -1) ){
            statsScript.DeductMana(GetAbility4Cost());
            GetMousePositionForSkillShot(); // if we can Use the ability then, and only then do we get the moUse position
            SpawnArcaneBarrage();
        }
    }
    public void SpawnMysticShot() {
        // UseSkillShot = true;
        var createdMysticShot = Instantiate(prefabMysticShot, offset.transform.position, transform.rotation);
        // Debug.Log(createdMysticShot);
        createdMysticShot.transform.rotation.SetLookRotation(skillShotTargetLocation);
        createdMysticShot.GetComponent<MysticShot>().setGameObjectSentFrom(this.gameObject);
        createdMysticShot.GetComponent<MysticShot>().setMissileSpeed(getMissileSpeed());
        createdMysticShot.GetComponent<MysticShot>().setTargetDestination(skillShotTargetLocation);
        // formula for bonus mystic shot damage is: bonus = abilityDamage + (totalAttackDamage * 130% or 1.3) + (15 % totalAbilityDamage)
        float totalDamage = GetAbility1Damage() + (statsScript.GetAttackDamage() * 1.3f) + (statsScript.GetAbilityDamage() * .15f);
        createdMysticShot.GetComponent<MysticShot>().setMysticShotDamage(totalDamage, statsScript.GetArmorPen());
    }
    public void SpawnEssenceFlux()  {
        // UseSkillShot = true;
        var createdEssenceFlux = Instantiate(prefabEssenceFlux, offset.transform.position, transform.rotation);
        createdEssenceFlux.GetComponent<EssenceFlux>().SetTargetDestination(skillShotTargetLocation);
    }
    public void SpawnArcaneBarrage()  {
        // UseSkillShot = true;
        var createdArcaneBarrage = Instantiate(prefabArcaneBarrage, offset.transform.position, transform.rotation);
        createdArcaneBarrage.GetComponent<ArcaneBarrage>().SetTargetDestination(skillShotTargetLocation);
        createdArcaneBarrage.GetComponent<MysticShot>().setGameObjectSentFrom(this.gameObject);
        createdArcaneBarrage.GetComponent<ArcaneBarrage>().SetArcaneBarrageDamage(GetAbility4Damage(), statsScript.GetBonusAttackDamage(), statsScript.GetAbilityDamage());
    }
    void GetMousePositionForSkillShot() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000)) {
            skillShotTargetLocation = (transform.position - hit.point).normalized;
        }
    }
    // Does the actual change in movement.
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
}