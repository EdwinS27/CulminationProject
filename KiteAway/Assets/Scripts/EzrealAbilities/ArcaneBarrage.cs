using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneBarrage : MonoBehaviour  {
    float maxDistance;
    Vector3 TargetDestination;
    bool startMovingToPreventCrash = false;
    bool startCoroutineDestroy = false;
    float damage;
    public float arcaneBarrageSpeed;
    void Update() {
        if(startMovingToPreventCrash){
            // transform.position = Vector3.MoveTowards(transform.position, TargetDestination, arcaneBarrageSpeed * Time.deltaTime);
            transform.position -= TargetDestination * arcaneBarrageSpeed * Time.deltaTime;
            if(startCoroutineDestroy == false)  {
                StartCoroutine(DestroyObject());
                startCoroutineDestroy = true;
            }
        }

    }
    private void OnTriggerEnter(Collider collidedWith) {
        if(collidedWith.tag == "Enemy")    {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Ezreal>().InteractWithStacks();
            collidedWith.gameObject.GetComponent<Stats>().health -= damage;
        }   
    }

    // Setters & Getters for TargetDestination
    public Vector3 GetTargetDestination()  {
        return TargetDestination;
    }
    public void SetTargetDestination(Vector3 targetLocation) {
        TargetDestination = targetLocation;
        startMovingToPreventCrash = true;
    }
    public void setMysticShotDamage(float damageInput)  {
        damage = damageInput;
    }
    IEnumerator DestroyObject(){
        yield return new WaitForSeconds(3);
        // Do something
        Destroy(this.gameObject);
    }
}
