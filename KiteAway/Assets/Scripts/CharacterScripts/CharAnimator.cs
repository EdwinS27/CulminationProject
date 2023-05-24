using UnityEngine;
public class CharAnimator : MonoBehaviour   {
    Animator _anim;
    Stats statsScript;
    Movement moveScript;
    float speed;
    float motionSmoothTime = .1f;
    void Start()    {
        _anim = GetComponent<Animator>();
        statsScript = GetComponent<Stats>();
        moveScript = GetComponent<Movement>();
    }
    void Update()   {
        movementAnimation();
    }
    void movementAnimation(){
        if(moveScript.GetWalking()){
            if(speed < 1){
                speed += (statsScript.GetMoveSpeed() / 100);
            }
            else
                speed = 1f;
        }
        else{
            speed = 0;
        }
        _anim.SetFloat("Speed", speed, motionSmoothTime, Time.deltaTime);
    }
}
