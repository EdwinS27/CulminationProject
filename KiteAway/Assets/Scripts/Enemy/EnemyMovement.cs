using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour   {
    public GameObject character;
    Vector3 playerCharactersLocation;
    Stats statsScript;
    private bool moveToEnemy = true;
    private bool enemyAttack = false;
    void Start(){statsScript = GetComponent<Stats>();}
    void Update(){if(moveToEnemy) MoveTowardsEnemy();}
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
        else{character = GameObject.FindGameObjectWithTag("Player");}
    }
    public bool GetMoveToEnemy(){return this.moveToEnemy;}
    public bool GetEnemyAttack(){return this.enemyAttack;}
    public void SetMoveToEnemy(bool move){this.moveToEnemy = move;}
    public void SetEnemyAttack(bool attack){this.enemyAttack = attack;}
    public void SetEnemyTarget(GameObject character){this.character = character;}
}
