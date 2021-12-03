using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTargeting : MonoBehaviour {
    RaycastHit hit;
    private GameObject selectedHero;
    void Start() {  selectedHero = this.gameObject;}
    void Update() { characterController();}
    void characterController() {
        if (Input.GetMouseButtonDown(1) && selectedHero.GetComponent<Stats>().GetHealth() > 0) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000)) {
                // print(hit.collider.gameObject.name);
                if(hit.collider.GetComponent<Targetable>() != null ) {
                    if(hit.collider.GetComponent<Targetable>().GetEnemyType() == Targetable.EnemyType.MINION) {
                        // Debug.Log("Targetting an Enemy!");
                        selectedHero.GetComponent<HeroCombat>().setTargetedEnemy(hit.collider.gameObject); // assign a target to heroscript
                    }
                }
                else{
                    selectedHero.GetComponent<HeroCombat>().setTargetedEnemy(null); // assign no target enemy
                    if(hit.collider.gameObject.tag == "Ground")  
                        selectedHero.GetComponent<Movement>().SetMovementFromInputTarget(hit.point);
                }
            } // end if physics
        } // end input mouse down
    } // end method
}