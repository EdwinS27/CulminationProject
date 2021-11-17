using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAutoAttack : MonoBehaviour   {
    Vector3 initialPos, targetPos;
    
    [Header("Ranged Auto Attack Stats")]
    private float armorPen;
    private float attackDamage;
    public float MissileSpeed = 3f;
    public bool stopProjectile = false;
    private bool targetSet = false;
    // think I can replace this
    private GameObject target;
    private string targetType;
    void Update()   {
        if(targetSet)  {
            // Debug.Log(target);
            if(target == null){
                Destroy(this.gameObject);
            }
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, MissileSpeed * Time.deltaTime);
            if(!stopProjectile) {
                if(Vector3.Distance(transform.position, target.transform.position) < 0.5f)  {
                    if(targetType == "Minion")  {
                        target.GetComponent<Stats>().takeBasicDamage(attackDamage, armorPen);
                        stopProjectile = true;
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
    public void enterDamageAndArmorPen(float damage, float armorPen)    {
        this.attackDamage = damage;
        this.armorPen = armorPen;
    }
    public void setTargetsPosition(Vector3 targetPosition) {
        targetPos = targetPosition;
    }
    public void isTargetSet(bool input)    {
        targetSet = input;
    }
    public string getTargetType(){
        return targetType;
    }
    public void setTarget(GameObject targetTransferedFromHeroCombat){
        target = targetTransferedFromHeroCombat;
    }
    public void setTargetType(string input) {
        targetType = input;
    }
    private void onCollisionEnter(Collider collidedWith) {
        if(collidedWith.tag == "Enemy")    {
            // they take damage here
            //Destroy(this.gameObject); // destroyu this object
        }
    }
}
