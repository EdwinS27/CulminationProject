using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour   {
    public Transform playerCharacter;
    private Vector3 cameraOffset;
    [Range(0.01f, 1.0f)]
    public float smoothness = 0.5f;

    // Start is called before the first frame update
    void Start()    {
        if(playerCharacter == null)
            playerCharacter = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        cameraOffset = transform.position - playerCharacter.transform.position;
    }

    // Update is called once per frame
    void Update()   {
        Vector3 newPos = playerCharacter.position + cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPos, smoothness);
    }
}
