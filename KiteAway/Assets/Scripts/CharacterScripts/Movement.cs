using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    private Vector3 targetDestination;
    private Vector3 lookAtTarget;
    private Quaternion charRotation;
    private float distanceToStop = 0.1f;
    private bool lockedOutOfMovement = false;
    private bool walking = false;
    bool characterIsAttacking = false;
    private Stats statsScript;
    private HeroCombat heroCombatScript;
    // Start is called before the first frame update
    void Start() {
        statsScript = GetComponent<Stats>();
        heroCombatScript = GetComponent<HeroCombat>();
    }
    void Update() {
        if (walking) characterMovement();
        else characterAdjustment();

    }
    void characterMovement() {
        transform.rotation = Quaternion.Slerp(
            transform.rotation, charRotation,
            statsScript.GetRotationSpeed() * Time.deltaTime);

        transform.position = Vector3.MoveTowards(
            transform.position,
             targetDestination,
            ((statsScript.GetMoveSpeed() / 2) * Time.deltaTime));

        if (Vector3.Distance(transform.position, targetDestination) <= distanceToStop) {
            distanceToStop = 1f; // can test this out
            walking = false;
        }
    }

    void characterAdjustment() {
        transform.rotation = Quaternion.Slerp(
            transform.rotation, charRotation,
            statsScript.GetRotationSpeed() * Time.deltaTime);
    }

    public void SetMovementFromInputTarget(Vector3 target) {
        heroCombatScript.notAttacking();
        lookAtTarget = new Vector3(
            target.x - transform.position.x,
            0,
            target.z - transform.position.z );
        charRotation = Quaternion.LookRotation(lookAtTarget);
        target.y = 0;
        targetDestination = target;
        
        walking = true;
        distanceToStop = 1f;
    }
    // This would be based on attack range
    public void SetMovementFromHeroCombat(Vector3 target, float stoppingDistance)    {
        lookAtTarget = new Vector3(
            target.x - transform.position.x,
            0,
            target.z - transform.position.z );
        charRotation = Quaternion.LookRotation(lookAtTarget);
        targetDestination = target;
        walking = true;
        distanceToStop = stoppingDistance;
    }
    public void AdjustRotationToTarget(GameObject target){
        lookAtTarget = new Vector3(
            target.transform.position.x - transform.position.x,
            0,
            target.transform.position.z - transform.position.z
        );

        charRotation = Quaternion.LookRotation(lookAtTarget);
    }

    public void AdjustRotationToTarget(Vector3 target){
        lookAtTarget = new Vector3(
            target.x - transform.position.x,
            0,
            target.z - transform.position.z);
        charRotation = Quaternion.LookRotation(lookAtTarget);
    }


    public bool GetWalking(){   return this.walking;}
    public void SetWalking(bool walk){   this.walking = walk;}
    public bool GetAttacking(){ return this.characterIsAttacking;}
    public void LockMovement(bool locked){ this.walking = locked;}
}

/*

    // Deprecated 

    //float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
    //rotationToLookAt.eulerAngles.y,
    //ref rotateVelocity,
    //rotateSpeedMovement * (Time.deltaTime * 5));

    //tranform.eulerAngles = new Vector3(0, rotationY, 0);
    // public void check() {
    //     if(heroCombatScript.targetedEnemy != null) {
    //         if (heroCombatScript.targetedEnemy.GetComponent<HeroCombat>() != null) {
    //             if (heroCombatScript.targetedEnemy.GetComponent<HeroCombat>().isHeroAlive) {
    //                 heroCombatScript.targetedEnemy = null;
    //             }
    //         }
    //     }
    // }
*/