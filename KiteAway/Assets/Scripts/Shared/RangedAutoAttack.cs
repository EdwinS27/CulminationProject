using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RangedAutoAttack : MonoBehaviour   {
    [Header("Ranged Auto Attack Stats")]
    private GameObject sentFrom;
    private float armorPen;
    private float attackDamage;
    private float missileSpeed = 5f;
    private bool dealMixedDamage = false;
    private bool targetSet = false;
    // think I can replace this
    public GameObject target;
    private string targetType;
    void Update()   {
        if(targetSet)  {
            if(target == null) // removing this kills the player ???????????
                Destroy(this.gameObject);
            else{
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, missileSpeed * Time.deltaTime);
            }
        }
    }
    private void decideDamage() {
        if(armorPen > 0)    {
            // do advanced basic damage
            target.GetComponent<Stats>().takeBasicDamage(this.sentFrom, this.attackDamage, this.armorPen);
        }
        else{
            // Debug.Log("No armor");
            target.GetComponent<Stats>().takeBasicDamage(this.sentFrom, this.attackDamage);
        }
    }
    // add missile speed
    public void SetTarget(GameObject sentFrom, GameObject targetTransferedFromHeroCombat, bool boolIn, string targetType, float damage, float armorPen){
        this.sentFrom = sentFrom;
        this.targetSet = boolIn;
        this.target = targetTransferedFromHeroCombat;
        this.targetType = targetType;
        this.armorPen = armorPen;
        this.attackDamage = damage;
        // this.missileSpeed = missileSpeed;
    }
    public void SetTargetSpecialDamage()    {
        dealMixedDamage = true;
    }
    private void OnTriggerEnter(Collider collidedWith) {
        Debug.Log("Has entered trigger");
        if(GameObject.ReferenceEquals(collidedWith.gameObject, this.target)){
            decideDamage();
            Destroy(this.gameObject);
        }   
    }
}
