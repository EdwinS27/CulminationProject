using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HeroCombat : MonoBehaviour {
    public enum HeroAttackType {Melee, Ranged};
    [Header ("Animation Variables")]
    public HeroAttackType heroAttackType; // ENUM
    private Animator _anim;
    private Stats statScript;
    private Movement moveScript;
    public GameObject offset;
    private bool isHeroAlive;
    private GameObject targetedEnemy;
    [SerializeField]    private GameObject _projectileAuto;
    public bool canPerformRangedAttack = true;
    private bool canPerformMeleeAttack = false;
    void Start() {
        moveScript = GetComponent<Movement>();
        statScript = GetComponent<Stats>();
        _anim = GetComponent<Animator>();
    }
    void Update() {
        if(targetedEnemy != null) {
            moveScript.AdjustRotationToTarget(targetedEnemy);
            if(Vector3.Distance(transform.position, targetedEnemy.transform.position) > statScript.GetAttackRange())
                moveScript.SetMovementFromHeroCombat(targetedEnemy.transform.position, statScript.GetAttackRange());
            else {
                if(heroAttackType == HeroAttackType.Melee && canPerformMeleeAttack){
                    StartCoroutine(MeleeAttackInterval());
                }
                else if(heroAttackType == HeroAttackType.Ranged && canPerformRangedAttack){
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
    public void MeleeAttack() {
        if(targetedEnemy != null) {
            if (targetedEnemy.GetComponent<Targetable>().GetEnemyType() == Targetable.EnemyType.MINION) {
                // take damage
            }
        }
        canPerformMeleeAttack = true;
        _anim.SetBool("Basic Attack", false);
    }
    IEnumerator RangedAttackInterval() {
        canPerformRangedAttack = false;
        moveScript.SetWalking(false);
        _anim.SetBool("Basic Attack", true);
        if(targetedEnemy == null)   {
            _anim.SetBool("Basic Attack", false);
            canPerformRangedAttack = true;
        }
        float newAttackTime =  statScript.GetAttackTime() / (100 + statScript.GetAttackTime()) * .01f;
        yield return new WaitForSeconds(newAttackTime); // wait until we can attack again
    }
    public void RangedAttack() {
        if(targetedEnemy != null) {
            if (targetedEnemy.GetComponent<Targetable>().GetEnemyType() == Targetable.EnemyType.MINION){
                SpawnRangedProjectile(targetedEnemy);
                canPerformRangedAttack = true;
                _anim.SetBool("Basic Attack", false);
            }
        }
        else{
            canPerformRangedAttack = true;
            _anim.SetBool("Basic Attack", false);
        }
    }
    void SpawnRangedProjectile(GameObject targetedEnemyObj) {
        // Debug.Log("Type of Enemy is: " + targetedEnemyObj);
        float damage = statScript.GetAttackDamage();
        float armorPen = statScript.GetArmorPen();
        float missileSpeed = this.gameObject.GetComponent<GenericChampion>().getMissileSpeed();
        GameObject auto = Instantiate(_projectileAuto, offset.transform.position, Quaternion.LookRotation(targetedEnemyObj.transform.position));
        // Debug.Log("The current champion: " + this.gameObject + "'s missile speed is: " + missileSpeed);
        auto.GetComponent<RangedAutoAttack>().SetTarget(this.gameObject, targetedEnemy, true, damage, armorPen, missileSpeed);
    }
    // getters and setters
    public void setTargetedEnemy(GameObject targetedEnemy) {    this.targetedEnemy = targetedEnemy;}
    public GameObject getTargetedEnemy() {    return this.targetedEnemy;}
}