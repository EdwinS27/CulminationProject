using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour   {
    public GameObject character;
    Vector3 playerCharactersLocation;
    Stats statsScript;
    bool moveToEnemy = false;
    void Start()    {
        character = GameObject.FindGameObjectWithTag("Player");
        statsScript = GetComponent<Stats>();
    }
    void Update()   {   if(moveToEnemy) MoveTowardsEnemy();}
    public void MoveTowardsEnemy(){
        if(character != null){
            playerCharactersLocation = character.GetComponent<Transform>().position;
            transform.position = Vector3.MoveTowards(transform.position, playerCharactersLocation, statsScript.GetMoveSpeed() * Time.deltaTime);
            var lookAtTarget = new Vector3(
                playerCharactersLocation.x - transform.position.x,
                0,
                playerCharactersLocation.z - transform.position.z
            );
            var angle = Vector3.Angle(lookAtTarget, transform.position);
            if(angle > 1){
                // transform.rotation = Quaternion.LookRotation(playerCharactersLocation);
                var charRotation = Quaternion.LookRotation(lookAtTarget);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    charRotation,
                    statsScript.GetRotationSpeed() * Time.deltaTime);
            }
        }
        else{
            if(GameObject.FindGameObjectWithTag("Player") == true){
                character = GameObject.FindGameObjectWithTag("Player");
            }
        }
    }
    public GameObject GetTargetedEnemy(){return this.character;}
    public void SetMoveToEnemy(bool move){  this.moveToEnemy = move;}
    public void SetTargetEnemy(GameObject character){this.character = character;}
}
