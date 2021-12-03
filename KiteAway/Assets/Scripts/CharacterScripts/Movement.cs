using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    private Vector3 targetDestination; // Destination
    private Vector3 lookAtTarget;    // Rotation of the character
    private Quaternion charRotation;
    private float distanceToStop = 0.01f;
    bool walking = false;
    bool characterIsAttacking = false;
    private Stats statsScript;
    private HeroCombat heroCombatScript;
    // Start is called before the first frame update
    void Start() {
        statsScript = GetComponent<Stats>();
        heroCombatScript = GetComponent<HeroCombat>();
    }
    void Update() {
        if (walking)    Move();
    }
    // fix rotation
    void Move() {
        if(!characterIsAttacking){
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                charRotation,
                statsScript.GetRotationSpeed() * Time.deltaTime
            );
        }
        Vector3 adjustedtargetdestination = new Vector3(targetDestination.x, transform.position.y, targetDestination.z);
        transform.position = Vector3.MoveTowards(
            transform.position,
            adjustedtargetdestination,
            (statsScript.GetMoveSpeed() * Time.deltaTime));
        if (Vector3.Distance(transform.position, adjustedtargetdestination) <= distanceToStop) {
            //Debug.Log("character has reached their destination and should stop moving!\nAlso resetting the value of distance to stop");
            distanceToStop = 0.01f;
            walking = false;
        }
    }
    public void SetMovementFromInputTarget(Vector3 target)    {
        lookAtTarget = new Vector3(
            target.x - transform.position.x,
            target.y - transform.position.y,
            target.z - transform.position.z
        );
        charRotation = Quaternion.LookRotation(lookAtTarget);
        targetDestination = target;
        walking = true;
        distanceToStop = 0.01f;
    }
    // This would be based on attack range
    public void SetMovementFromHeroCombat(Vector3 target, float stoppingDistance)    {
        lookAtTarget = new Vector3(
            target.x - transform.position.x,
            0,
            target.z - transform.position.z
        );
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
        var angle = Vector3.Angle(lookAtTarget, transform.position);
        if(angle > 1){
            charRotation = Quaternion.LookRotation(lookAtTarget);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                charRotation,
                statsScript.GetRotationSpeed() * Time.deltaTime
            );
        }
    }
    public void AdjustRotationToTarget(Vector3 target){
        lookAtTarget = new Vector3(
            target.x - transform.position.x,
            0,
            target.z - transform.position.z
        );
        charRotation = Quaternion.LookRotation(lookAtTarget);
        if(transform.rotation != charRotation){
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                charRotation,
                statsScript.GetRotationSpeed() * Time.deltaTime
            );
        }
    }
    public bool GetWalking(){   return this.walking;}
    public void SetWalking(bool walk){   this.walking = walk;}
    public bool GetAttacking(){ return this.characterIsAttacking;}
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