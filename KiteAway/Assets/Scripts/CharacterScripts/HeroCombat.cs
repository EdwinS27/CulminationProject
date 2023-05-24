using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HeroCombat : MonoBehaviour {
    public enum HeroAttackType { Melee, Ranged };
    [Header("Animation Variables")]
    public HeroAttackType heroAttackType; // ENUM
    public GameObject offset;
    private bool isHeroAlive;
    private GameObject targetedEnemy;
    [SerializeField] private GameObject _projectileAuto;
    public bool canPerformRangedAttack = true;
    private bool canPerformMeleeAttack = true;
    // Scripts and Other Components
    private Animator _anim;
    private Stats statScript;
    private Movement moveScript;
    void Start() {
        moveScript = GetComponent<Movement>();
        statScript = GetComponent<Stats>();
        _anim = GetComponent<Animator>();
    }
    void Update() {
        if (targetedEnemy != null) {
            if (Vector3.Distance(transform.position, targetedEnemy.transform.position) > statScript.GetAttackRange())
                moveScript.SetMovementFromHeroCombat(targetedEnemy.transform.position, statScript.GetAttackRange());
            else {
                if (heroAttackType == HeroAttackType.Melee && canPerformMeleeAttack) {
                    StartCoroutine(MeleeAttackInterval());
                }
                else if (heroAttackType == HeroAttackType.Ranged && canPerformRangedAttack) {
                    StartCoroutine(RangedAttackInterval());
                }
            }
        }
    }
    IEnumerator MeleeAttackInterval() {
        canPerformMeleeAttack = false;
        _anim.SetBool("Basic Attack", true);
        yield return new WaitForSeconds(statScript.GetAttackTime() / (100 + statScript.GetAttackTime()) * .01f);
    }
    void MeleeAttack() {
        if (targetedEnemy != null) {
            if (targetedEnemy.GetComponent<Targetable>().GetEnemyType() == Targetable.EnemyType.MINION) {
                // take damage
            }
        }
        canPerformMeleeAttack = true;
        _anim.SetBool("Basic Attack", false);
    }

    public void notAttacking() {
        _anim.SetBool("Basic Attack", false);
        canPerformMeleeAttack = true;
        canPerformRangedAttack = true;
    }

    IEnumerator RangedAttackInterval() {
        canPerformRangedAttack = false;
        // moveScript.LockMovement(true);
        _anim.SetBool("Basic Attack", true);
        float newAttackTime = statScript.GetAttackTime() / (100 + statScript.GetAttackTime()) * .01f;
        yield return new WaitForSeconds(newAttackTime); // wait until we can attack again
        if (targetedEnemy == null) {
            _anim.SetBool("Basic Attack", false);
            canPerformRangedAttack = true;
        }
    }
    void RangedAttack() {
        if (targetedEnemy != null) {
            if (targetedEnemy.GetComponent<Targetable>().GetEnemyType() == Targetable.EnemyType.MINION)
                SpawnRangedProjectile(targetedEnemy);
        }
        else {
            canPerformRangedAttack = true;
            _anim.SetBool("Basic Attack", false);
            // moveScript.LockMovement(false);
        }
    }
    void SpawnRangedProjectile(GameObject targetedEnemyObj) {
        // Debug.Log("Type of Enemy is: " + targetedEnemyObj);
        float damage = statScript.GetAttackDamage();
        float armorPen = statScript.GetArmorPen();
        float missileSpeed = this.gameObject.GetComponent<GenericChampion>().getMissileSpeed();
        GameObject auto = Instantiate(_projectileAuto, offset.transform.position, Quaternion.identity);
        auto.transform.rotation = Quaternion.LookRotation(targetedEnemy.transform.position);
        auto.GetComponent<RangedAutoAttack>().SetTarget(this.gameObject, targetedEnemy, true, damage, armorPen, missileSpeed);
        canPerformRangedAttack = true;
        _anim.SetBool("Basic Attack", false);
        moveScript.LockMovement(false);
    }
    // getters and setters
    public GameObject getTargetedEnemy() { return this.targetedEnemy; }
    public void setTargetedEnemy(GameObject targetedEnemy) => this.targetedEnemy = targetedEnemy;
}