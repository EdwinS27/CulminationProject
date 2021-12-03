using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneBarrage : SkillShots  {
    float ap;
    float damage;
    float bonusAD;
    Vector3 TargetDestination;
    private void Start() {
        transform.rotation *= Quaternion.Euler(90, 0, 0);
    }
    new private void HitSomething() {
        shotsConnectedIncrement();
        GameObject ezreal = getSender();
        ezreal.GetComponent<Ezreal>().InteractWithStacks();
        targetHit.GetComponent<Stats>().takePhysicalDamage(getSender(), this.bonusAD);
        targetHit.GetComponent<Stats>().takeMagicDamage(getSender(), this.damage);
    }
    private void Update() {
        moveTowardsTargetLocation();
    }
    private void OnTriggerEnter(Collider collidedWith) {
        if(collidedWith.tag == "Enemy")    {
            // Debug.Log(collidedWith);
            targetHit = collidedWith.gameObject;
            HitSomething();
        }
        else if(collidedWith.tag == "Boundary"){Destroy(this.gameObject);}
    }

    // Setters & Getters for TargetDestination
    public Vector3 GetTargetDestination(){return TargetDestination;}
    public void SetTargetDestination(Vector3 targetLocation){TargetDestination = targetLocation;}
    public void SetArcaneBarrageDamage(float damageInput, float bonusAD, float ap)  {
        this.damage = damageInput;
        this.bonusAD = bonusAD;
        this.ap = ap;
    }
}
