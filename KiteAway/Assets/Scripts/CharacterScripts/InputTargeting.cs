using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InputTargeting : MonoBehaviour {
    private GameObject selectedHero;
    void Start(){selectedHero = this.gameObject;}
    void Update(){
        if (Input.GetMouseButtonDown(1) && selectedHero.GetComponent<Stats>().GetHealth() > 0) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000)){
                if(hit.collider.GetComponent<Targetable>() != null) { if(hit.collider.GetComponent<Targetable>().GetEnemyType() == Targetable.EnemyType.MINION){ selectedHero.GetComponent<HeroCombat>().setTargetedEnemy(hit.collider.gameObject); } }
                else{
                    selectedHero.GetComponent<HeroCombat>().setTargetedEnemy(null);
                    if (hit.collider.gameObject.tag == "Ground")
                        selectedHero.GetComponent<Movement>().SetMovementFromInputTarget(hit.point);
                }
            }
        }
    }
}