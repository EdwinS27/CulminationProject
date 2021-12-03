using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssenceFlux : MonoBehaviour   {
    float maxDistance;
    Vector3 TargetDestination;
    bool startMovingToPreventCrash = false;
    bool startCoroutineDestroy = false;
    float damage;
    bool bonusDamage = false;
    public float fluxWaveSpeed;
    GameObject enemy;
    void Update() {
        // watching for bonus damage to acvitate
        if(bonusDamage == true) {

        }
        if(startMovingToPreventCrash && bonusDamage == false){
            transform.position -= TargetDestination * fluxWaveSpeed * Time.deltaTime;
            if(startCoroutineDestroy == false)  {
                StartCoroutine(DestroyObject());
                startCoroutineDestroy = true;
            }
        }

    }
    private void OnTriggerEnter(Collider collidedWith) {
        if(collidedWith.tag == "Enemy")    {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Ezreal>().InteractWithStacks();
            this.gameObject.GetComponent<Renderer>().enabled = false;
            enemy = collidedWith.GetComponent<GameObject>();
        }
    }
    public void takesBonusDamage(float damage, float bonusDamage)  {
        //enemy.GetComponent<Stats>().health -= damage * bonusDamage;
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
        yield return new WaitForSeconds(4);
        Destroy(this.gameObject);
    }
    void ActivateBonusDamage()  {
        bonusDamage = true;
        DestroyObject();
    }
}
