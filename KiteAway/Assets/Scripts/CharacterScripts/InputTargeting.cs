using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTargeting : MonoBehaviour {
    RaycastHit hit;
    public bool heroPlayer;
    Vector3 targetDestination;
    private GameObject selectedHero;
    void Start() {  selectedHero = this.gameObject;}
    void Update() { characterController();}
    void characterController() {
        if (Input.GetMouseButtonDown(1)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000)) {
                if(hit.collider.GetComponent<Targetable>() != null ) {
                    if(hit.collider.GetComponent<Targetable>().GetEnemyType() == Targetable.EnemyType.MINION) {
                        selectedHero.GetComponent<HeroCombat>().setTargetedEnemy(hit.collider.gameObject); // targeting the enemy
                    }
                }
                else{
                    selectedHero.GetComponent<HeroCombat>().setTargetedEnemy(null);
                    if(hit.collider.gameObject.tag == "Ground")  {
                        targetDestination = hit.point;
                        selectedHero.GetComponent<Movement>().SetMovementFromInputTarget(targetDestination);
                    }
                }
            } // end if physics
        } // end input mouse down
    } // end method
}