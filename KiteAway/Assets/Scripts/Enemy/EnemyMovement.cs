using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour   {
    GameObject playerCharacter;
    Vector3 playerCharactersLocation;
    Stats statsScript;
    // Start is called before the first frame update
    void Start()    {
        playerCharacter = GameObject.FindGameObjectWithTag("Player");
        statsScript = GetComponent<Stats>();
    }
    // Update is called once per frame
    void Update()   {
        playerCharactersLocation = playerCharacter.GetComponent<Transform>().position;
        transform.position = Vector3.MoveTowards(transform.position, playerCharactersLocation, statsScript.GetMoveSpeed() * Time.deltaTime);
    }
    // collider will go here, where it will be decided if I want it to be an instant kill, or a huge portion of damage.
    private void OnTriggerEnter(Collider c) {
        if(c.gameObject.tag == "Player")    {
            Debug.Log("Do something: => Bomber");
        }
    }
}
