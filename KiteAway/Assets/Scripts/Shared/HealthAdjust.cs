using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAdjust : MonoBehaviour {
    // Update is called once per frame
    void LateUpdate()   {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0,0,0);
    }
}