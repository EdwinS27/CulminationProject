using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HeroCombat : MonoBehaviour {
    public enum HeroAttackType {
        Melee, Ranged
    };
    [Header ("Animation Variables")]
    public HeroAttackType heroAttackType;
    public bool isHeroAlive;
    public bool basicAtttackIdle = false;
    [Header ("Attack Stats")]
    public float attackRange;
    public float rotateSpeedForAttack;
    [Header ("Ranged Attack")]
    public GameObject targetedEnemy;
    [SerializeField]
    private GameObject _projectileAuto;
    public bool performRangedAttack = true;
    private Movement moveScript;
    [Header ("Melee Attack")]
    public bool performMeleeAttack = false;
    private Stats statScript;
    // Start is called before the first frame update
    void Start() {
        moveScript = GetComponent<Movement>();
        statScript = GetComponent<Stats>();
    }
    // Update is called once per frame
    void Update() {
        if(targetedEnemy != null) {;
            // If the distance between the character and the enemy is greater than the character's attack range
            if(Vector3.Distance(transform.position, targetedEnemy.transform.position) > attackRange) {
                Debug.Log("The player is farther than the attack range: " + attackRange);
                // Characters Target Destination is set here
                moveScript.setTargetDestination(targetedEnemy.transform.position);
                // Character Rotation
                moveScript.SetTargetRotation(targetedEnemy.transform.position);
            }
            else {
                //Debug.Log("The player is within attack range!");
                // If the distance is not greater. Attack !
                if(heroAttackType == HeroAttackType.Melee) {
                    if (performMeleeAttack) {
                        //Debug.Log("Attack the minion!");
                        StartCoroutine(MeleeAttackInterval());
                    }
                }
                // If the distance is not greater. Attack !
                else if(heroAttackType == HeroAttackType.Ranged) {
                    if (performRangedAttack) {
                        //Debug.Log("Attack the minion!");
                        //Debug.Log("Ranged Auto Attack method called");
                        RangedAttack();
                        StartCoroutine(RangedAttackInterval());
                    }
                }
            }
        }
    }

    IEnumerator MeleeAttackInterval() {
        performMeleeAttack = false;
        //anim.setBool("BasicAttack", false);

        yield return new WaitForSeconds(statScript.attackTime / (100 + statScript.attackTime) * .01f);
    }

    public void MeleeAttack() {
        if(targetedEnemy != null) {
            if (targetedEnemy.GetComponent<Targetable>().enemyType == Targetable.EnemyType.MINION) {
                targetedEnemy.GetComponent<Stats>().health -= statScript.attackDamage;
            }
        }

        performMeleeAttack = true;
    }

    IEnumerator RangedAttackInterval() {
        performRangedAttack = false;
        //anim.setBool("Basic Attack", true);
        yield return new WaitForSeconds(statScript.attackTime);
        Debug.Log(statScript.attackTime);
        if(targetedEnemy == null)   {
            //anim.setBool("Basic Attack", false);
        }
        performRangedAttack = true;
    }

    public void RangedAttack() {
        Debug.Log("Targeted Enemy: " + targetedEnemy);
        if(targetedEnemy != null) {
            if (targetedEnemy.GetComponent<Targetable>().enemyType == Targetable.EnemyType.MINION) {
                Debug.Log("Player has targeted an enemy HeroCombatScript!");
                SpawnRangedProjectile("Minion", targetedEnemy);
                targetedEnemy.GetComponent<Stats>().health -= statScript.attackDamage;
            }
        }

        performRangedAttack = true;
    }

    void SpawnRangedProjectile(string typeOfEnemy, GameObject targetedEnemyObj) {
        float damage = statScript.attackDamage;
        GameObject auto = Instantiate(_projectileAuto, transform.position, Quaternion.identity);

        if(typeOfEnemy == "Minion") {
            Debug.Log("Type of Enemy is Minion");
            //auto.GetComponent<RangedAutoAttack>().setTargetsPosition(targetedEnemy.transform.position);
            auto.GetComponent<RangedAutoAttack>().setTargetType("Minion");
            auto.GetComponent<RangedAutoAttack>().setTarget(targetedEnemy);
            auto.GetComponent<RangedAutoAttack>().isTargetSet(true);
        }
    }
}