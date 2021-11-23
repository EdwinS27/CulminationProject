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
    public void setLivingArtilleryDamage(float damageInput, float magicPen, float missileSpeed)  {
        this.damage = damageInput;
        this.magicPen = magicPen;
        setMissileSpeed(missileSpeed);
    }
    private void OnTriggerEnter(Collider collidedWith) {
        if(collidedWith.tag == "Enemy")    {
            targetHit = collidedWith.gameObject;
            HitSomething();
            targetHit.GetComponent<Stats>().takeMagicDamage(getSender(), damage);
        }   
        if(collidedWith.tag == "ground")    {
            Destroy(this.gameObject); // IF WE HIT SOMETHING DESTROY SELF AFTER
        }
    }
}
