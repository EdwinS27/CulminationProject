using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HeroCombat : MonoBehaviour {
    public enum HeroAttackType {Melee, Ranged};
    [Header ("Animation Variables")]
    public HeroAttackType heroAttackType;
    public GameObject offset;
    private bool isHeroAlive;
    public bool basicAtttackIdle = false;
    public float rotateSpeedForAttack;
    float attackRange;
    private GameObject targetedEnemy;
    [SerializeField]
    private GameObject _projectileAuto;
    public bool canPerformRangedAttack = true;
    private Movement moveScript;
    public bool performMeleeAttack = false;
    private Stats statScript;
    // Start is called before the first frame update
    void Start() {
        moveScript = GetComponent<Movement>();
        statScript = GetComponent<Stats>();
        attackRange = statScript.GetAttackRange();
        // Debug.Log(attackRange);
    }
    // Update is called once per frame
    void Update() {
        if(targetedEnemy != null) { // if we have a target
            if(Vector3.Distance(transform.position, targetedEnemy.transform.position) > attackRange) {  // If the distance between the character and the enemy is greater than the character's attack range
                // Debug.Log("The player is farther than the attack range: " + attackRange);
                // Characters Target Destination is set here && Character Rotation
                moveScript.SetMovementFromInputTarget(targetedEnemy.transform.position, attackRange);
            }
            else {
                moveScript.StopMovement();
                // Debug.Log("The player is within attack range!");
                // If the distance is not greater. Attack !
                if(heroAttackType == HeroAttackType.Melee) {
                    if (performMeleeAttack) {
                        //Debug.Log("Attack the minion!");
                        StartCoroutine(MeleeAttackInterval());
                    }
                }
                // If the distance is not greater. Attack !
                else if(heroAttackType == HeroAttackType.Ranged) {
                    if (canPerformRangedAttack) {
                        StartCoroutine(RangedAttackInterval());
                        //Debug.Log("Attack the minion!");
                    }
                }
            }
        }
        else    {

        }
    }

    IEnumerator MeleeAttackInterval() {
        performMeleeAttack = false;
        //anim.setBool("BasicAttack", false);

        yield return new WaitForSeconds(statScript.GetAttackTime() / (100 + statScript.GetAttackTime()) * .01f);
    }

    public void MeleeAttack() {
        performMeleeAttack = true;
        // anim.setBool("Basic Attack", true);
        if(targetedEnemy != null) {
            if (targetedEnemy.GetComponent<Targetable>().GetEnemyType() == Targetable.EnemyType.MINION) {
                // take damage
            }
        }
    }
    public void setTargetedEnemy(GameObject targetedEnemy) {    this.targetedEnemy = targetedEnemy;}
    public GameObject getTargetedEnemy() {    return this.targetedEnemy;}
    IEnumerator RangedAttackInterval() {
        canPerformRangedAttack = false;
        //anim.setBool("Basic Attack", true);
        float newAttackTime =  statScript.GetAttackTime() - statScript.GetAttackSpeed(); // statScript.GetAttackTime() / (100 + statScript.GetAttackSpeed()) * .01f
        //Debug.Log("canPerformRangedAttack: " + canPerformRangedAttack + "\tnewAttackTime" + newAttackTime);
        if(targetedEnemy != null)   {
            // Debug.Log("Ranged Auto Attack method called");
            RangedAttack();
            //anim.setBool("Basic Attack", false);
            // canPerformRangedAttack = true;
        }
        yield return new WaitForSeconds(newAttackTime); // wait until we can attack again
        canPerformRangedAttack = true;
    }
    public void RangedAttack() {
        // Debug.Log("Targeted Enemy: " + targetedEnemy);
        if(targetedEnemy != null) {
            if (targetedEnemy.GetComponent<Targetable>().GetEnemyType() == Targetable.EnemyType.MINION) {
                // Debug.Log("Player has targeted an enemy HeroCombatScript!");
                SpawnRangedProjectile("Minion", targetedEnemy);
            }
        }
    }

    void SpawnRangedProjectile(string typeOfEnemy, GameObject targetedEnemyObj) {
        float damage = statScript.GetAttackDamage();
        float armorPen = statScript.GetArmorPen();
        GameObject auto = Instantiate(_projectileAuto, offset.transform.position, Quaternion.identity);
        if(typeOfEnemy == "Minion") {
            // Debug.Log("Type of Enemy is Minion");
            auto.GetComponent<RangedAutoAttack>().SetTarget(this.gameObject, targetedEnemy, true, "Minion", damage, armorPen);
            //auto.GetComponent<RangedAutoAttack>().SetTarget(targetedEnemy, true, "Minion", damage, armorPen);
        }
    }
}