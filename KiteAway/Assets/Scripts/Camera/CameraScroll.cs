using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour {
    public Camera mainCam;
    private float camFOV;
    public float zoomSpeed;

    private float mouseScrollInput;

    // Start is called before the first frame update
    void Start() {
        camFOV = mainCam.fieldOfView;
    }

    // Update is called once per frame
    void Update() {
        mouseScrollInput = Input.GetAxis("Mouse ScrollWheel");
        camFOV -= mouseScrollInput * zoomSpeed;
        camFOV = Mathf.Clamp(camFOV, 15, 45);

        mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, camFOV, zoomSpeed);
    }
}
