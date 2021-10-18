using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCombat : MonoBehaviour {

    public enum HeroAttackType {
        Melee, Ranged
    };

    public HeroAttackType heroAttackType;


    public GameObject targetedEnemy;
    public float attackRange;
    public float rotateSpeedForAttack;

    private Movement moveScript;
    public bool basicAtttackIdle = false;
    public bool isHeroAlive;
    public bool performMeleeAttack = false;

    private Stats statScript;
    // Start is called before the first frame update
    void Start() {
        moveScript = GetComponent<Movement>();
        statScript = GetComponent<Stats>();
    }

    // Update is called once per frame
    void Update() {
        if(targetedEnemy != null) {
            // If the distance between the character and the enemy is greater than the character's attack range
            if(Vector3.Distance(gameObject.transform.position, targetedEnemy.transform.position) > attackRange) {
                // Characters Target Destination is set here
                moveScript.setTargetDestination(targetedEnemy.transform.position);
                // Character Rotation
                moveScript.SetTargetRotation(targetedEnemy.transform.position);
            }
            else {
                // If the distance is not greater. Attack !
                if(heroAttackType == HeroAttackType.Melee) {
                    if (performMeleeAttack) {
                        Debug.Log("Attack the minion!");

                        StartCoroutine(MeleeAttackInterval());
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
}