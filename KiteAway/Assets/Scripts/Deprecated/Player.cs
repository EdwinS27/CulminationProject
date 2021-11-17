using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

    //Rigidbody playerBody;

    //// need to experiment with movement speed !!!
    //private float charMovementSpeed = 10.0f;
    //[SerializeField]
    //private float charRotationSpeed = 10f;
    //private float offSet;
    //private float teleportationDistanceX = 1f;
    //private float teleportationDistanceZ = 1f;



    //[SerializeField]
    //private GameObject MysticShot;
    //[SerializeField]
    //private float offSetX;
    //private bool characterUsedQ = false;



    //int[] direction = { 1, 1 };
    //// Location of the character
    //Vector3 charLocation;
    //Vector3 targetDestination;
    //Vector3 lookAtTarget;
    //// Rotation of the character
    //Quaternion charRotation;

    ////
    //public float distanceToStop;

    //bool walking = false;

    //private float timeRemainingOnCharacterQ;
    ////private float timeElapsed;
    //[SerializeField]
    //private float coolDownDurationOnCharacterQ = 6f;


    //void Start() {
    //    playerBody = GetComponent<Rigidbody>();

    //    timeRemainingOnCharacterQ = 0;
    //}

    //void Update() {
    //    // We want to be checking the player movement

    //    //if (Input.GetMouseButtonDown(0)) {
    //    //    printLocation();
    //    //}
    //    playerControl();
    //    /*
    //     private void manageTimers() {
    //        if(
    //     }
    //     */

    //    if (walking)
    //        Movement();
    //    // Manage Timers
    //    manageTimers();
    //}
    //// End of Update
    //void playerControl() {

    //    // If the player presses S we want the character to not move anymore
    //    if (Input.GetKey("s")) {
    //        //Debug.Log("STOP MOVING !");
    //        //offSet = 0f;
    //        walking = false;
    //    }
    //    // else if player has not pressed S to stop  do :
    //    else {
    //        playerAbilities();
    //        // check if the right mouse button has been clicked
    //        if (Input.GetMouseButtonDown(0)) {

    //            // Cast a ray at the location of the mouse
    //            RaycastHit hit;

    //            // My Ray is not working
    //            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    //            //int[] getDirection = directionXZHandler(targetDestination);


    //            //direction[0] = getDirection[0];
    //            //direction[1] = getDirection[1];
    //            // SO I will check is the direction of the player going + - on the x and the z
    //            if (Physics.Raycast(ray, out hit, 10000)) {
    //                // this accesses whether the tag of the location / object of the mouse click
    //                //Debug.Log("What did I click on: " + hit.transform.tag);

    //            }

    //        }
    //        if (Input.GetMouseButtonDown(1)) {

    //            // if the mouse button is clicked do the following code
    //            // Debug.Log("Right Mouse Click");


    //            // Cast a ray at the location of the mouse
    //            RaycastHit hit;
    //            // My Ray is not working
    //            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    //            //int[] getDirection = directionXZHandler(targetDestination);


    //            //direction[0] = getDirection[0];
    //            //direction[1] = getDirection[1];
    //            // SO I will check is the direction of the player going + - on the x and the z
    //            if (Physics.Raycast(ray, out hit, 1000)) {
    //                // this accesses whether the tag of the location / object of the mouse click
    //                //Debug.Log("What did I click on: " + hit.transform.tag);

    //                if (hit.transform.tag == "Ground") {
    //                    walking = true;
    //                    targetDestination = hit.point;
    //                    // Debug.Log("Player Location: " + transform.position + "\nMouse Location: " + targetDestination);

    //                    SetTargetDestination(targetDestination);
    //                }
    //            }
    //        }
    //    }

    //}
    ///************* Tutorial I used for the movement code if I forget *************/
    ///* https://www.youtube.com/watch?v=MAbei7eMlXg  */

    //void playerAbilities() {

    //    CharacterQability();


    //    // bool ready for TP

    //    // AND the
    //    if (Input.GetKey("e")) {
    //        // This is the teleportation ability
    //        if (Input.GetMouseButtonDown(0)) {
    //            // Debug.Log("I should be teleporting");
    //            // Cast a ray at the location of the mouse
    //            RaycastHit hit;

    //            // My Ray is not working
    //            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    //            if (Physics.Raycast(ray, out hit, 1000)) {
    //                var locationToTeleportTo = hit.point;

    //                SetTargetDestination(locationToTeleportTo);
    //                // teleport


    //                // Get the correct teleportation direction
    //                directionXZHandler(locationToTeleportTo);
    //                // Debug.Log("Player Location: " + transform.position + "\tTeleport Location: " + locationToTeleportTo);



    //                transform.position = new Vector3(
    //                    transform.position.x + teleportationDistanceX,
    //                    transform.position.y,
    //                    transform.position.z + teleportationDistanceZ
    //                    );
    //            }

    //        }

    //        teleportationDistanceX = 1f;
    //        teleportationDistanceZ = 1f;
    //    }
    //}

    //void Movement() {

    //    transform.rotation = Quaternion.Slerp(
    //        transform.rotation,
    //        charRotation,
    //        charRotationSpeed * Time.deltaTime
    //        );


    //    // needed
    //    Vector3 adjustedTargetDestination = new Vector3(targetDestination.x, transform.position.y, targetDestination.z);

    //    transform.position = Vector3.MoveTowards(
    //        transform.position,
    //        adjustedTargetDestination,
    //        (charMovementSpeed * Time.deltaTime));
    //    // I need to be checking this when my character is moving
    //    if (Vector3.Distance(transform.position, adjustedTargetDestination) < distanceToStop) {
    //        transform.position = adjustedTargetDestination;


    //        walking = false;
    //        //Debug.Log("Character has reached their destination and should stop moving!");
    //    }
    //}

    //private void manageTimers() {
    //    if (timeRemainingOnCharacterQ > 0) {
    //        timeRemainingOnCharacterQ -= Time.deltaTime;
    //        //  ready = false
    //    }
    //    else {
    //        // ready = true;
    //        if (characterUsedQ == true) {
    //            print("Q Ability is now ready!");
    //            characterUsedQ = false;
    //        }
    //    }
    //}

    //// attack range & auto attack

    //    // set timer : for


    //void directionXZHandler(Vector3 destinationInput) {
    //    // is char.x > or < destinationInput.x by atleast 5 or so
    //    if (transform.position.x != destinationInput.x) {
    //        if (transform.position.x > destinationInput.x)
    //            teleportationDistanceX *= -1;
    //    }
    //    else
    //        teleportationDistanceX = 0;

    //    if (transform.position.z != destinationInput.z) {
    //        if (transform.position.z > destinationInput.z)
    //            teleportationDistanceZ *= -1;
    //    }
    //    else
    //        teleportationDistanceZ = 0;
    //}
    //// **********************************************************************
    //// **********************************************************************
    //// **********************************************************************
    //// **********************************************************************
    //void SetTargetDestination(Vector3 target) {
    //    // This might be the place I need to look at aim the Q
    //    lookAtTarget = new Vector3(
    //        target.x - transform.position.x,
    //        transform.position.y,
    //        target.z - transform.position.z
    //        );
    //    charRotation = Quaternion.LookRotation(lookAtTarget);
    //}

    //private void CharacterQability() {
    //    /* --------------------------- Q Ability ------------------------------- */
    //    /*
    //     When the player presses : q, I want the character to fire an ability
    //     in his forward direction.
    //     Q must fire forward.
    //     Has limited range, After the range has been reached. It will dissipate.
    //     It will only damage the first target infront then it will dissipate.
    //     It will have a cool down. ***** Completed *****
    //     Cooldown will go down, if character ability hits a valid target
    //    */
    //    if (Input.GetKeyUp("q") && timeRemainingOnCharacterQ < 1 && characterUsedQ == false) {
    //        Debug.Log("Player Shot Q");
    //        // Will spawn an object infront of character : Instantiate (Object, Vector3, Quaternion)

    //        // I want an offSet for the character's X
    //        var offSetXForQAbility = offSetX;

    //        // I want to set a Vector3 for the offSetX
    //        // *********************************************************** TO DO **************************************************************************************************************
    //        // ********************************************************************************************************************************************************************************
    //        // ********************************************************************************************************************************************************************************
    //        // ******************************************** NEED TO FIND THE FRONT OF THE CHARACTER *******************************************************************************************
    //        // ******************************************** THEN THAT WILL BE THE DIRECTION OF THE Q ******************************************************************************************
    //        // ********************************************************************************************************************************************************************************
    //        // ********************************************************************************************************************************************************************************
    //        // ********************************************************************************************************************************************************************************
    //        Vector3 adjustedSpawnForMysticShot = new Vector3(transform.position.x + offSetXForQAbility, transform.position.y, transform.position.z);
    //        Instantiate(MysticShot, adjustedSpawnForMysticShot, Quaternion.identity);
    //        timeRemainingOnCharacterQ = coolDownDurationOnCharacterQ;
    //        characterUsedQ = true;
    //    }
    //}


    /**************************************** Previous Movement Code *************************************** */

    //void move() {
    //    offSet = movement * (playerMovementSpeed * (Time.deltaTime));

    //    // Should adjust the direction the character is going based on destination
    //    playerBody.velocity = new Vector3(offSet * direction[0], playerBody.velocity.y, offSet * direction[1]);
    //}
}
