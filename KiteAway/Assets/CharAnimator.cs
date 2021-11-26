using UnityEngine;
public class CharAnimator : MonoBehaviour   {
    Animator _anim;
    Movement moveScript;
    float speed;
    float motionSmoothTime = .1f;
    void Start()    {
        moveScript =GetComponent<Movement>();
        _anim = GetComponent<Animator>();
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
