using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingArtillery : SkillShots    {
    float damage;
    float magicPen;
    // it falls from the sky
    void Update()   {
        fallFromSky();
    }
    new private void HitSomething() {
        shotsConnectedIncrement();
        targetHit.GetComponent<Stats>().takeMagicDamage(getSender(), this.damage);
    }
    public void setLivingArtilleryDamage(float damageInput, float magicPen, float missileSpeed)  {
        this.damage = damageInput;
        this.magicPen = magicPen;
        setMissileSpeed(missileSpeed);
    }
    private void OnTriggerEnter(Collider collidedWith) {
        if(collidedWith.tag == "Enemy")    {
            targetHit = collidedWith.gameObject;
            HitSomething();
        }
    }
    private void OnTriggerExit(Collider collidedWith) {
        if(collidedWith.tag == "Ground")    {
            Destroy(this.gameObject);
        }
    }
}
