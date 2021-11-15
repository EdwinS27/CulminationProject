using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysticShot : MonoBehaviour {
    static int shotsFired = 0;
    static int shotsMissed = 0;
    static int shotsConnected = 0;
    // distance travel variables
    float maxDistance;
    Vector3 TargetDestination;
    bool startMovingToPreventCrash = false;
    bool startCoroutineDestroy = false;
    // values needed to compute the damage
    float damage;
    float magicPen;
    float armorPen;
    float lethality; // ?
    public float mysticShotSpeed;
    void Update() {
        if(startMovingToPreventCrash){
            //Debug.Log("Move");
            //transform.position = Vector3.MoveTowards(transform.position, TargetDestination, mysticShotSpeed * Time.deltaTime); // no longer needed now that I have a valid path for skill shots
            transform.position -= TargetDestination * mysticShotSpeed * Time.deltaTime;
            if(startCoroutineDestroy == false)  {
                // It Works!
                StartCoroutine(DestroyObject());
                startCoroutineDestroy = true;
            }
        }
    }
    private void OnTriggerEnter(Collider collidedWith) {
        if(collidedWith.tag == "Enemy")    {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Ezreal>().InteractWithStacks();
            GameObject.FindGameObjectWithTag("Player").GetComponent<Abilities>().LowerAllCooldowns();
            //Debug.Log(GameObject.FindGameObjectWithTag("Player").GetComponent<Abilities>().coolDownDurationAbilityOne);
            //collidedWith.gameObject.GetComponent<Stats>().health -= damage;
            collidedWith.gameObject.GetComponent<Stats>().takeBasicDamage(damage, armorPen, magicPen);
            Destroy(this.gameObject);
        }   
    }
    // public void takePhysicalDamage(float totalDamage, float baseDamage, float bonusDamage, float armorPen)    {
    //     float effectiveHealthArmor = (1 + (armor / 100)) * health;

    // Setters & Getters for TargetDestination
    public Vector3 GetTargetDestination()  {
        return TargetDestination;
    }
    public void SetTargetDestination(Vector3 targetLocation) {
        TargetDestination = targetLocation;
        startMovingToPreventCrash = true;
        shotsFiredIncrement();
        Debug.Log("Number of shots fired: " + shotsFired);
        TargetDestination.y = 0;
    }
    public static void shotsFiredIncrement() {
        // increment shotsFired
        shotsFired++;
    }

    public static void shotsConnectedIncrement() {
        // increment shots that connected with an enemy of any type
        shotsConnected++;

    }

    public static void shotsMissedIncrement() {
        // incremenet shots that do not connect with any enemy of any type
        shotsMissed++;
    }
    public void setMysticShotDamage(float damageInput, float armorPen, float magicPen)  {
        this.damage = damageInput;
        this.armorPen = armorPen;
        this.magicPen = magicPen;
    }
    IEnumerator DestroyObject(){
        yield return new WaitForSeconds(3);
        // Do something
        Destroy(this.gameObject);
    }

    private void Awake() {
        
    }


}
