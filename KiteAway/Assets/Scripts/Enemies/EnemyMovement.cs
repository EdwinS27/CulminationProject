using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour  {
    Stats statsScript;
    GameObject playerCharacter;
    Vector3 playerCharactersLocation;
    void Start() {
        statsScript = GetComponent<Stats>();
        playerCharacter = GameObject.FindGameObjectWithTag("Player");
        Debug.Log("Found Player");
    }
    void Update()   {
        playerCharactersLocation = playerCharacter.GetComponent<Transform>().position;
        transform.position = Vector3.MoveTowards(transform.position, playerCharactersLocation, statsScript.moveSpeed * Time.deltaTime);
    }
}
