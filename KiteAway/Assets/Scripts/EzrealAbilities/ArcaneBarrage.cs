using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneBarrage : MonoBehaviour  {
    float ap;
    float damage;
    float bonusAD;
    private GameObject sendsTheirRegards;
    Vector3 TargetDestination;
    public float arcaneBarrageSpeed;
    void Update() {
        // transform.position = Vector3.MoveTowards(transform.position, TargetDestination, arcaneBarrageSpeed * Time.deltaTime);
        transform.position -= TargetDestination * arcaneBarrageSpeed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider collidedWith) {
        if(collidedWith.tag == "Enemy")    {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Ezreal>().InteractWithStacks();
            collidedWith.gameObject.GetComponent<Stats>().takeMixedDamage(this.sendsTheirRegards, this.damage, this.bonusAD, this.ap);
        }
        else if(collidedWith.tag == "Boundary")    {    Destroy(this.gameObject);   }
    }

    // Setters & Getters for TargetDestination
    public void gameObjectSentFrom(GameObject sentFrom) {   this.sendsTheirRegards = sentFrom;  }
    public Vector3 GetTargetDestination()  {    return TargetDestination;   }
    public void SetTargetDestination(Vector3 targetLocation) {  TargetDestination = targetLocation; }
    public void SetArcaneBarrageDamage(float damageInput, float bonusAD, float ap)  {
        this.damage = damageInput;
        this.bonusAD = bonusAD;
        this.ap = ap;
    }
}
