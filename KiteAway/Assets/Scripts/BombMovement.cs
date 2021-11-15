using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombMovement : MonoBehaviour   {
    GameObject playerCharacter;
    Vector3 playerCharactersLocation;
    Stats statsScript;
    // Start is called before the first frame update
    void Awake()    {
        playerCharacter = GameObject.FindGameObjectWithTag("Player");
        statsScript = GetComponent<Stats>();
    }

    // Update is called once per frame
    void Update()   {
        playerCharactersLocation = playerCharacter.GetComponent<Transform>().position;
        transform.position = Vector3.MoveTowards(transform.position, playerCharactersLocation, statsScript.moveSpeed * Time.deltaTime);
    }

    // collider will go here, where it will be decided if I want it to be an instant kill, or a huge portion of damage.
}
