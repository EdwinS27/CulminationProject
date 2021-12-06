using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraManager : MonoBehaviour{
    Camera mainCam;
    private float camFOV;
    private float zoomSpeed = 5f;
    float cameraSpeed = 20f;
    float screenHeightMax = 25f;
    float screenSizeThickness = 25f;
    bool cameraLocked = false;
    public Transform playerCharacter;
    private Vector3 cameraOffset;
    private float smoothness = 0.5f;
    private Vector3 setCameraPosition = new Vector3(3f,7f,20f);
    private void Start() {
        mainCam = GetComponent<Camera>();
        camFOV = mainCam.fieldOfView;
    }
    void Update(){
        if(playerCharacter == null){
            playerCharacter = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
        scrollCamera();
        manageCamera();
    }
    void scrollCamera(){
        float mouseScrollInput = Input.GetAxis("Mouse ScrollWheel");
        // print(mouseScrollInput);
        camFOV -= mouseScrollInput * zoomSpeed;
        camFOV = Mathf.Clamp(camFOV, 5, 40);
        mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, camFOV, zoomSpeed);
    }
    void manageCamera(){
        if(Input.GetKeyDown(KeyCode.Y))
            cameraLocked = !cameraLocked;
        if(!cameraLocked) roamCamera();
        else lockCamera();
    }
    void roamCamera(){
        Vector3 pos = transform.position;
        if(Input.mousePosition.y >= Screen.height - screenHeightMax){
            pos.z -= cameraSpeed * Time.deltaTime;
        }
        if(Input.mousePosition.y <= screenHeightMax){
            pos.z += cameraSpeed * Time.deltaTime;
        }
        if(Input.mousePosition.x >= Screen.width - screenSizeThickness){
            pos.x -= cameraSpeed * Time.deltaTime;
        }
        if(Input.mousePosition.x <= screenSizeThickness){
            pos.x += cameraSpeed * Time.deltaTime;
        }
        transform.position = pos;
    }
    void lockCamera(){
        Vector3 camPosition = new Vector3(
            playerCharacter.position.x + 4f,
            15f,
            playerCharacter.position.z + 22f
        );
        transform.position = camPosition;
    }
}
