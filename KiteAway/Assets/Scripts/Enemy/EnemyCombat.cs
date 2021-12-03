using System.Collections;
using UnityEngine;
public class EnemyCombat : MonoBehaviour {
    public enum EnemyAttackType {Melee, Ranged};
    [Header ("Animation Variables")]
    public EnemyAttackType enemyAttackType;
    public GameObject offset;
    private GameObject targetedEnemy;
    private bool canPerformRangedAttack = true;
    private EnemyMovement enemyMoveScript;
    private bool performMeleeAttack = true;
    private Animator _anim;
    private Stats statScript;
    [SerializeField] GameObject _projectileAuto;
    void Start() {
        enemyMoveScript = GetComponent<EnemyMovement>();
        _anim = GetComponent<Animator>();
        statScript = GetComponent<Stats>();
    }
    // Update is called once per frame
    void Update() {
        if(targetedEnemy != null) { // if we have a target
            if(Vector3.Distance(transform.position, targetedEnemy.transform.position) > statScript.GetAttackRange()){
                _anim.SetBool("Moving", true);
                enemyMoveScript.SetMoveToEnemy(true);
            }
            else {
                _anim.SetBool("Moving", false);
                if(enemyAttackType == EnemyAttackType.Melee)
                    if (performMeleeAttack){
                        enemyMoveScript.SetMoveToEnemy(false);
                        StartCoroutine(MeleeAttackInterval());
                    }
                else if(enemyAttackType == EnemyAttackType.Ranged){
                        enemyMoveScript.SetMoveToEnemy(false);
                        if (canPerformRangedAttack)
                            StartCoroutine(RangedAttackInterval());
                }
            }
        }
        else{
            targetedEnemy = enemyMoveScript.GetTargetedEnemy();
        }

    }
    IEnumerator MeleeAttackInterval() {
        performMeleeAttack = false;
        _anim.SetBool("Basic Attack", true);
        yield return new WaitForSeconds(statScript.GetAttackTime() / (100 + statScript.GetAttackTime()) * .01f);
    }
    public void MeleeAttack() {
        if(targetedEnemy != null) {
            if (targetedEnemy.GetComponent<Targetable>().GetEnemyType() == Targetable.EnemyType.CHARACTER)  {
                targetedEnemy.GetComponent<Stats>().takeBasicDamage(this.gameObject, statScript.GetAttackDamage());
                _anim.SetBool("Basic Attack", false);
                performMeleeAttack = true;
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
                SpawnRangedProjectile(targetedEnemy);
            }
        }
    }

    void SpawnRangedProjectile(GameObject targetedEnemyObj) {
        float damage = statScript.GetAttackDamage();
        float armorPen = statScript.GetArmorPen();
        float missileSpeed = this.gameObject.GetComponent<GenericChampion>().getMissileSpeed();
        GameObject auto = Instantiate(_projectileAuto, offset.transform.position, Quaternion.identity);
        // Debug.Log("The current champion: " + this.gameObject + "'s missile speed is: " + missileSpeed);
        auto.GetComponent<RangedAutoAttack>().SetTarget(this.gameObject, targetedEnemy, true, damage, armorPen, missileSpeed);
        _anim.SetBool("Basic Attack", false);
    }
}