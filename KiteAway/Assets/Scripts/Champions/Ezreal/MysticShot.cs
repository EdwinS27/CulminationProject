using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MysticShot : SkillShots {
    private float timeToBeAlive = 4f;
    private float damage;
    private float armorPen;
    private void Start() {
        transform.rotation *= Quaternion.Euler(90, 0, 0);
    }
    new private void HitSomething() {
        decideDamage();
        shotsConnectedIncrement();
        GameObject ezreal = getSender();
        ezreal.GetComponent<Ezreal>().InteractWithStacks();
        GameObject.FindGameObjectWithTag("Abilities").GetComponent<Abilities>().LowerAllCooldowns();
    }
    public void setMysticShotDamage(float totalDamage, float armorPen)  {
        this.damage = Mathf.Ceil(totalDamage);
        this.armorPen = armorPen;
        // setMissileSpeed(missileSpeed);
    }
    private void OnTriggerEnter(Collider collidedWith) {
        if(collidedWith.tag == "Enemy")    {
            targetHit = collidedWith.gameObject;
            HitSomething();
            // Debug.Log("targetHit: " + targetHit);
            Destroy(this.gameObject); // IF WE HIT SOMETHING DESTROY SELF AFTER
        }   
    }
    private void Update() {
        moveTowardsTargetLocation();
        if(timeToBeAlive > 0)
            timeToBeAlive -= Time.deltaTime;
        else
            Destroy(this.gameObject);
    }
    private void decideDamage() {
        if(armorPen > 0)    {
            // do advanced basic damage
            targetHit.GetComponent<Stats>().takeBasicDamage(getSender(), this.damage, this.armorPen);
        }
        else{
            // Debug.Log("No armor");
            targetHit.GetComponent<Stats>().takeBasicDamage(getSender(), this.damage);
        }
    }
}
