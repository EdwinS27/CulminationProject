using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RangedAutoAttack : MonoBehaviour   {
    [Header("Ranged Auto Attack Stats")]
    private float armorPen;
    private float attackDamage;
    private float missileSpeed = 5f;
    private bool dealMixedDamage = false;
    private bool targetSet = false;
    private GameObject sentFrom;
    private GameObject target;
    Quaternion objRotation;
    void Update()   {
        if(targetSet)  {
            if(target == null) // removing this kills the player ???????????
                Destroy(this.gameObject);
            else{
                // var lookAtTarget = new Vector3(
                //     target.transform.position.x - transform.position.x,
                //     0,
                //     target.transform.position.z - transform.position.z
                // );
                // objRotation = Quaternion.LookRotation(lookAtTarget);
                // if(transform.rotation != objRotation){
                //     transform.rotation = Quaternion.Slerp(
                //         transform.rotation,
                //         objRotation,
                //         missileSpeed * Time.deltaTime
                //     );
                // }
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, missileSpeed * Time.deltaTime);
            }
        }
    }
    private void decideDamage() {
        if(!dealMixedDamage){
            if(armorPen > 0)    target.GetComponent<Stats>().takeBasicDamage(this.sentFrom, this.attackDamage, this.armorPen);
            else    target.GetComponent<Stats>().takeBasicDamage(this.sentFrom, this.attackDamage);
        }
        else{
            // we will do the deal mixed damage here
        }
    }
    // add missile speed
    public void SetTarget(GameObject sentFrom, GameObject targetTransferedFromHeroCombat, bool boolIn, float damage, float armorPen, float missileSpeed){
        this.sentFrom = sentFrom;
        this.targetSet = boolIn;
        this.target = targetTransferedFromHeroCombat;
        this.armorPen = armorPen;
        this.attackDamage = damage;
        this.missileSpeed = missileSpeed;
    }
    public void SetTargetSpecialDamage()    {
        dealMixedDamage = true;
    }
    private void OnTriggerEnter(Collider collidedWith) {
        // Debug.Log("Has entered trigger");
        if(GameObject.ReferenceEquals(collidedWith.gameObject, this.target)){
            decideDamage();
            Destroy(this.gameObject);
        }   
    }
}
