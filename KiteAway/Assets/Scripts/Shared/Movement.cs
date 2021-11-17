using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    // need to experiment with movement speed !!!
    private float charMovementSpeed = 10.0f;
    [SerializeField]
    private float charRotationSpeed = 10f;
    private float offSet;

    // Location of the character
    Vector3 charLocation;
    Vector3 targetDestination;
    Vector3 lookAtTarget;
    // Rotation of the character
    Quaternion charRotation;

    public float rotateMovementSpeed = 0.075f;
    float rotateVelocity;

    public float distanceToStop;

    HeroCombat heroCombatScript;
    bool walking = false;
    // Start is called before the first frame update
    void Start() {
        heroCombatScript = GetComponent<HeroCombat>();
    }

    // Update is called once per frame
    void Update() {
        characterController();
        if (walking)
            Move();
    }

    void characterController() {
        if (Input.GetMouseButtonDown(1)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000)) {
                targetDestination = hit.point;
                SetTargetRotation(targetDestination);
                distanceToStop = 0.01f;
            }
        }
    }

    void Move() {

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            charRotation,
            charRotationSpeed * Time.deltaTime
            );


        // needed
        Vector3 adjustedtargetdestination = new Vector3(targetDestination.x, transform.position.y, targetDestination.z);

        transform.position = Vector3.MoveTowards(
            transform.position,
            adjustedtargetdestination,
            (charMovementSpeed * Time.deltaTime));

        //float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
        //rotationToLookAt.eulerAngles.y,
        //ref rotateVelocity,
        //rotateSpeedMovement * (Time.deltaTime * 5));

        //tranform.eulerAngles = new Vector3(0, rotationY, 0);

        // i need to be checking this when my character is moving
        if (Vector3.Distance(transform.position, adjustedtargetdestination) < distanceToStop) {
            transform.position = adjustedtargetdestination;
            //Debug.Log("character has reached their destination and should stop moving!\nAlso resetting the value of distance to stop");
            distanceToStop = 0.01f;
            walking = false;
        }
    }


    public void SetTargetRotation(Vector3 target) {
        // This might be the place I need to look at aim the Q
        lookAtTarget = new Vector3(
            target.x - transform.position.x,
            0,
            target.z - transform.position.z
            );
        charRotation = Quaternion.LookRotation(lookAtTarget);
        // setTargetDestination should activate this boolean
        walking = true;
    }


    // setters and getters
    public void setTargetDestination(Vector3 target) {
        targetDestination = target;
    }

    public Vector3 getTargetDestination() {
        return targetDestination;
    }

    public void setCharacterStoppingDistance(float attackRange) {
        distanceToStop = attackRange;
    }

    public void check() {
        if(heroCombatScript.targetedEnemy != null) {
            if (heroCombatScript.targetedEnemy.GetComponent<HeroCombat>() != null) {
                if (heroCombatScript.targetedEnemy.GetComponent<HeroCombat>().isHeroAlive) {
                    heroCombatScript.targetedEnemy = null;
                }
            }
        }
    }
}
