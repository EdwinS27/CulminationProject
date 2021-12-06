// using UnityEngine;
// public class CameraScroll : MonoBehaviour {
//     public Camera mainCam;
//     private float camFOV;
//     private float zoomSpeed = 10f;
//     private float mouseScrollInput;
//     void Start(){camFOV = mainCam.fieldOfView;}
//     void Update() {
//         mouseScrollInput = Input.GetAxis("Mouse ScrollWheel");
//         camFOV -= mouseScrollInput * zoomSpeed;
//         camFOV = Mathf.Clamp(camFOV, 30, 50);
//         mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, camFOV, zoomSpeed);
//     }
// }
