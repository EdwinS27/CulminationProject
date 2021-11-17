using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour   {
    public AudioSource explosionSound;
    float damage = 100;
    float magicDamage = 15;
    float magicPen = 10;
    // Start is called before the first frame update
    void Update()   {
    }
    // collider will go here, where it will be decided if I want it to be an instant kill, or a huge portion of damage.
    public float GetDamage()    {   return this.damage; }
    public float GetMagicDamage()   {   return this.magicDamage; }
    public float GetMagicPenetration()  {   return this.magicPen;   }
    public void SetDamage( float value)    {   this.damage = value; }
    public void SetMagicDamage( float value)   {   this.magicDamage = value; }
    public void SetMagicPenetration( float value)  {   this.magicPen = value;   }
    private void OnTriggerEnter(Collider champion) {
        if(champion.tag == "Player") {
            explosionSound.Play();
            champion.gameObject.GetComponent<Stats>().takeMagicDamage(this.damage, this.magicDamage, this.magicPen);
            Destroy(this.gameObject);
        }
    }
}
