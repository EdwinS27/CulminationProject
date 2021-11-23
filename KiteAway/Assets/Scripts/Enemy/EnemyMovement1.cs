using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement1 : MonoBehaviour  {
    Stats statsScript;
    GameObject character;
    Vector3 charactersLocation;
    private void Awake()   {
        character = GameObject.FindGameObjectWithTag("Player");
        statsScript = GetComponent<Stats>();
    }
    void Update()   {
        charactersLocation = character.GetComponent<Transform>().position;
        this.transform.position = Vector3.MoveTowards(this.transform.position, charactersLocation, statsScript.GetMoveSpeed() * Time.deltaTime);
    }
}
