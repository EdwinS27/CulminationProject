using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CausticSpittle : SkillShots {
    float damage;
    float magicPen;
    private float timeToBeAlive = 4f;
    new private void HitSomething() {
        shotsConnectedIncrement();
        decideDamage();
    }
    private void Update() {
        moveTowardsTargetLocation();
        if(timeToBeAlive > 0)
            timeToBeAlive -= Time.deltaTime;
        else
            Destroy(this.gameObject);
    }
    public void setCausticSpittleDamage(float damageInput, float magicPen, float missileSpeed)  {
        this.damage = damageInput;
        this.magicPen = magicPen;
        setMissileSpeed(missileSpeed);
    }
    private void OnTriggerEnter(Collider collidedWith){
        if(collidedWith.tag == "Enemy"){
            targetHit = collidedWith.gameObject;
            HitSomething();
            Destroy(this.gameObject); // IF WE HIT SOMETHING DESTROY SELF AFTER
        }   
    }
    private void decideDamage() {
        if(magicPen > 0)
            targetHit.GetComponent<Stats>().takeMagicDamage(getSender(), this.damage, this.magicPen);
        else
            targetHit.GetComponent<Stats>().takeMagicDamage(getSender(), this.damage);
    }
}
