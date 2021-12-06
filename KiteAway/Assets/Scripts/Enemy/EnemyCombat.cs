using System.Collections;
using UnityEngine;
public class EnemyCombat : MonoBehaviour {
    public enum EnemyAttackType {Melee, Ranged};
    [Header ("Animation Variables")]
    public EnemyAttackType enemyAttackType;
    public GameObject offset;
    public GameObject targetedEnemy;
    private bool canPerformRangedAttack = true;
    public bool canPerformMeleeAttack = true;
    private Animator _anim;
    private Stats statScript;
    private EnemyMovement enemyMoveScript;
    [SerializeField] GameObject _projectileAuto;
    void Start() {
        _anim = GetComponent<Animator>();
        statScript = GetComponent<Stats>();
        enemyMoveScript = GetComponent<EnemyMovement>();
    }
    // Update is called once per frame
    void Update() {
        if(targetedEnemy != null && targetedEnemy.GetComponent<Stats>().GetHealth() > 0){
            if(Vector3.Distance(transform.position, targetedEnemy.transform.position) > statScript.GetAttackRange()){
                _anim.SetBool("Moving", true);
                _anim.SetBool("Basic Attack", false);
                enemyMoveScript.SetMoveToEnemy(true);
            }
            else {
                _anim.SetBool("Moving", false);
                enemyMoveScript.SetMoveToEnemy(false);
                if(enemyAttackType == EnemyAttackType.Melee)
                    if (canPerformMeleeAttack)
                        StartCoroutine(MeleeAttackInterval());
                else if(enemyAttackType == EnemyAttackType.Ranged){
                        if (canPerformRangedAttack){
                            enemyMoveScript.SetMoveToEnemy(false);
                            StartCoroutine(RangedAttackInterval());
                        }
                }
            }
        }
        else{
            targetedEnemy = GameObject.FindGameObjectWithTag("Player");
        }
    }
    IEnumerator MeleeAttackInterval() {
        _anim.SetBool("Basic Attack", true);
        canPerformMeleeAttack = false;
        yield return new WaitForSeconds(statScript.GetAttackTime() / (100 + statScript.GetAttackTime()) * .01f);
    }
    public void MeleeAttack() {
        targetedEnemy.GetComponent<Stats>().takeBasicDamage(this.gameObject, statScript.GetAttackDamage());
        enableAttacks();
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
    void enableAttacks(){
        canPerformMeleeAttack = true;
        canPerformRangedAttack = true;
        this._anim.SetBool("Basic Attack", false);
    }
}