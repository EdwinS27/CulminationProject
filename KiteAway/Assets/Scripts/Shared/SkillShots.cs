using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillShots : MonoBehaviour {
    private bool activateDestruct = false;
    private float missileSpeed = 2f;
    private static int shotsFired = 0;
    private static int shotsMissed = 0;
    private static int shotsConnected = 0;
    public GameObject targetHit;
    private GameObject skillShotSentFrom;
    private Vector3 targetDestination;
    public void HitSomething() {
        shotsConnectedIncrement();
    }
    public static void resetGame(){
        shotsFired = 0;
        shotsMissed = 0;
        shotsConnected = 0;
    }
    public static int getShotsFired(){ return shotsFired;}
    public static int getShotsMissed(){ return shotsMissed;}
    public static int getShotsConnected(){ return shotsConnected;}
    public static void shotsFiredIncrement() {shotsFired++;}   // increment shotsFired
    public static void shotsConnectedIncrement() {shotsConnected++;}   // increment shots that connected with an enemy of any type
    public static void shotsMissedIncrement() {shotsMissed++;}
    public void setMissileSpeed(float missileSpeed) {this.missileSpeed = missileSpeed;}
    public Vector3 getTargetDestination()  {return this.targetDestination;}
    public GameObject getSender()   {return this.skillShotSentFrom;} // get the game object that sent the skill shot
    public void setGameObjectSentFrom(GameObject sentFrom) {this.skillShotSentFrom = sentFrom;}   // set the game object that sent the skill shot
    public void setTargetSpawn(Vector3 targetLocation)  {this.targetDestination = targetLocation; shotsFiredIncrement();}
    public void setTargetDestination(Vector3 targetLocation)  {
        shotsFiredIncrement();
        this.targetDestination = targetLocation;
        targetDestination.y = 0;
    }
    // Movement Options
    public void moveTowardsTargetLocation(){
        transform.position -= this.targetDestination * this.missileSpeed * Time.deltaTime;
    }
    public void fallFromSky()   {
        var fallingY = transform.position.y - (this.missileSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, fallingY, transform.position.z);
    }
    public void setDestructBool(bool b){ this.activateDestruct = b;}
    public bool getDestructBool(){ return this.activateDestruct;}
}
