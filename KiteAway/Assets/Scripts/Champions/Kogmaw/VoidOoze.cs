using UnityEngine;
public class VoidOoze : SkillShots  {
    float damage;
    float magicPen;
    float percentAffectMoveSpeed;
    private float timeToBeAlive = 4f;
    void Start(){
        
    }
    void Update(){
        if(timeToBeAlive > 0)
            timeToBeAlive -= Time.deltaTime;
        else
            Destroy(this.gameObject);
    }
    new private void HitSomething() {
        shotsConnectedIncrement();
        decideDamage();
    }
    public void setVoidOozeDamage(float damageInput, float magicPen)  {
        this.damage = damageInput;
        this.magicPen = magicPen;
    }
    public void setVoidOozeRules(float percentToAffectChange){this.percentAffectMoveSpeed = percentToAffectChange;}
    private void OnCollisionEnter(Collision collidedWith) {
        if(collidedWith.gameObject.tag == "Enemy"){
            targetHit = collidedWith.gameObject;
            HitSomething();
        }
    }
    private void decideDamage(){
        if(magicPen > 0)
            targetHit.GetComponent<Stats>().takeMagicDamage(getSender(), this.damage, this.magicPen);
        else
            targetHit.GetComponent<Stats>().takeMagicDamage(getSender(), this.damage);
    }
    private void OnTriggerStay(Collider collidedWith) {
        if(collidedWith.tag == "Enemy")    {
            collidedWith.GetComponent<Stats>().AffectMoveSpeed(percentAffectMoveSpeed, 1);
        }
    }
    private void OnTriggerExit(Collider collidedWith) {
        if(collidedWith.tag == "Enemy")    {
            
        }
    }
}
