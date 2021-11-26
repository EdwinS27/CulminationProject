using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    private Vector3 charLocation;   // Location of the character
    private Vector3 targetDestination; // Destination
    private Vector3 lookAtTarget;    // Rotation of the character
    private Quaternion charRotation;
    private float rotateVelocity;
    private float distanceToStop = 0.01f;
    Stats statsScript;
    HeroCombat heroCombatScript;
    bool walking = false;
    // Start is called before the first frame update
    void Start() {
        statsScript = GetComponent<Stats>();
        heroCombatScript = GetComponent<HeroCombat>();
    }
    void Update() {
        if (walking)    Move();
    }
    void Move() {
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            charRotation,
            statsScript.GetRotationSpeed() * Time.deltaTime
        );
        Vector3 adjustedtargetdestination = new Vector3(targetDestination.x, transform.position.y, targetDestination.z);
        transform.position = Vector3.MoveTowards(
            transform.position,
            adjustedtargetdestination,
            (statsScript.GetMoveSpeed() * Time.deltaTime));
        // i need to be checking this when my character is moving
        if (Vector3.Distance(transform.position, adjustedtargetdestination) <= distanceToStop) {
            // transform.position = adjustedtargetdestination;
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
    public void SetMovementFromInputTarget(Vector3 target, float stoppingDistance)    {
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
    public bool GetWalking(){   return this.walking;}
    public void StopMovement()  { walking = false;}
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