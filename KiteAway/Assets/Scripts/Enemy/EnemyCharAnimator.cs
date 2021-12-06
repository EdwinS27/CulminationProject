using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharAnimator : MonoBehaviour{
    float speed;
    float motionSmoothTime = .1f;
    private Animator _anim;
    private EnemyMovement enemyMoveScript;
    void Start()    {
        _anim = this.gameObject.GetComponent<Animator>();
        enemyMoveScript = this.gameObject.GetComponent<EnemyMovement>();
    }
    void Update()   {
        movementAnimation();
    }
    void movementAnimation(){
        if(!enemyMoveScript.GetEnemyAttack()){
            if(enemyMoveScript.GetMoveToEnemy()){
                if(speed < 1){
                    speed+= .01f;
                }
                else
                    speed = 1f;
            }
            else{
                if(speed > 0)
                    speed -= .001f;
                else
                    speed = 0;
            }
            _anim.SetFloat("Speed", speed, motionSmoothTime, Time.deltaTime);
        }
    }
}
