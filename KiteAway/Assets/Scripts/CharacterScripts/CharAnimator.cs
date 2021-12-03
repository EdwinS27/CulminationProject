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
        _anim.SetFloat("Speed", speed, motionSmoothTime, Time.deltaTime);
    }
    void movementAnimation(){
        if(moveScript.GetWalking()){
            if(speed < 1){
                speed+= .001f;
            }
            else
                speed = 1f;
        }
        else{
            if(speed > .5)
                speed -= .001f;
            else
                speed = 0;
        }
    }
}
