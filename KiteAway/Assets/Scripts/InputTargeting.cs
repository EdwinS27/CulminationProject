using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTargeting : MonoBehaviour {

    public GameObject selectedHero;
    public bool heroPlayer;
    RaycastHit hit;


    // Start is called before the first frame update
    void Start() {
        selectedHero = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetMouseButtonDown(1)) {
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))  {
                // If the character has no target to follow.
                if(hit.collider.GetComponent<Targetable>() != null) {
                    if(hit.collider.gameObject.GetComponent<Targetable>().enemyType == Targetable.EnemyType.MINION) {
                        selectedHero.GetComponent<HeroCombat>().targetedEnemy = hit.collider.gameObject;
                        Debug.Log("The Player Has Targeted the minion InputTargetting.");
                    }
                }
                else{
                    // If the character has a Target AND they click the ground, then we will lose the targetting.
                    if(hit.collider.gameObject.tag == "Ground")  {
                        //Debug.Log("The Player Has Untargeted.");
                        selectedHero.GetComponent<HeroCombat>().targetedEnemy = null;
                    }
                    // If the character has a target, it will swap to the new target
                    else if(hit.collider.gameObject.GetComponent<Targetable>().enemyType == Targetable.EnemyType.MINION) {
                        selectedHero.GetComponent<HeroCombat>().targetedEnemy = hit.collider.gameObject;
                        Debug.Log("The Player Has Targeted the minion InputTargetting.");
                    }
                }
            }
        }
    }
}
