using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HeroCombat : MonoBehaviour {
    public enum HeroAttackType {Melee, Ranged};
    [Header ("Animation Variables")]
    public HeroAttackType heroAttackType;
    public GameObject offset;
    private bool isHeroAlive;
    private float rotateSpeedForAttack;
    float attackRange;
    private GameObject targetedEnemy;
    [SerializeField]
    private GameObject _projectileAuto;
    public bool canPerformRangedAttack = true;
    private Movement moveScript;
    private bool performMeleeAttack = false;
    private Stats statScript;
    private Animator _anim;
    // Start is called before the first frame update
    void Start() {
        moveScript = GetComponent<Movement>();
        statScript = GetComponent<Stats>();
        attackRange = statScript.GetAttackRange();
        _anim = GetComponent<Animator>();
        // Debug.Log(_anim);
    }
    // Update is called once per frame
    void Update() {
        if(targetedEnemy != null) { // if we have a target
            if(Vector3.Distance(transform.position, targetedEnemy.transform.position) > attackRange) {  // If the distance between the character and the enemy is greater than the character's attack range
                // Debug.Log("The player is farther than the attack range: " + attackRange);
                // Characters Target Destination is set here && Character Rotation
                moveScript.SetMovementFromHeroCombat(targetedEnemy.transform.position, attackRange);
                // moveScript.FaceTarget();
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
                        // moveScript.FaceTarget();
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
        _anim.SetBool("Basic Attack", true);
        float newAttackTime =  statScript.GetAttackTime() - statScript.GetAttackSpeed(); // statScript.GetAttackTime() / (100 + statScript.GetAttackSpeed()) * .01f
        // Debug.Log("newAttackTime" + newAttackTime);
        yield return new WaitForSeconds(newAttackTime); // wait until we can attack again
        if(targetedEnemy == null)   {
            // Debug.Log("Ranged Auto Attack method called");
            // Debug.Log(_anim.GetBool("Basic Attack"));
            // Debug.Log(_anim.GetBool("Basic Attack"));
            _anim.SetBool("Basic Attack", false);
            canPerformRangedAttack = true;
        }
    }
    public void RangedAttack() {
        // Debug.Log("Targeted Enemy: " + targetedEnemy);
        if(targetedEnemy != null) {
            if (targetedEnemy.GetComponent<Targetable>().GetEnemyType() == Targetable.EnemyType.MINION) {
                // Debug.Log("Player has targeted an enemy HeroCombatScript!");
                // Debug.Log(_anim.GetBool("Basic Attack"));
                SpawnRangedProjectile("Minion", targetedEnemy);
            }
        }
    }

    void SpawnRangedProjectile(string typeOfEnemy, GameObject targetedEnemyObj) {
        if (typeOfEnemy == "Minion") {
            // Debug.Log("Type of Enemy is Minion");
            float damage = statScript.GetAttackDamage();
            float armorPen = statScript.GetArmorPen();
            float missileSpeed = this.gameObject.GetComponent<GenericChampion>().getMissileSpeed();
            GameObject auto = Instantiate(_projectileAuto, offset.transform.position, Quaternion.identity);
            // Debug.Log("The current champion: " + this.gameObject + "'s missile speed is: " + missileSpeed);
            auto.GetComponent<RangedAutoAttack>().SetTarget(this.gameObject, targetedEnemy, true, "Minion", damage, armorPen, missileSpeed);
        }
        _anim.SetBool("Basic Attack", false);
    }
}