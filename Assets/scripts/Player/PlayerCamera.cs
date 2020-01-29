using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    public new Camera camera;

    public float CameraRotationSpeedY = 2f;
    public float CameraRotationSpeedX = 2f;
    public float CameraSmooth = 1f;
    public float CameraZoomSpeed = 3f;
    public float CameraZoomMax = 15f;
    public float CameraZoomMin = 3f;

    private Rigidbody rb;

    void Start() {
        rb = gameObject.GetComponent<Rigidbody>();
        camera = gameObject.GetComponentInChildren<Camera>();
    }

    void Update() {
        if (Input.GetAxis("Fire2") > 0) {
            RotateCamera();
        }
        ClampAngles();
        ZoomCamera();
        ClampZoom();
    }

    private void RotateCamera() {
        float cameraMoveY = Input.GetAxis("Mouse X") * CameraRotationSpeedX;
        float cameraMoveX = Input.GetAxis("Mouse Y")* CameraRotationSpeedY;

        rb.AddTorque(-cameraMoveX, cameraMoveY, 0);
    }

    private void ZoomCamera() {
        var cameraZoom = -Input.GetAxis("Mouse ScrollWheel");

        if(cameraZoom != 0) {
            camera.fieldOfView += cameraZoom * CameraZoomSpeed;
        }
    }

    private void ClampAngles() {

        var rotationX = transform.eulerAngles.x;
        var rotationZ = 0;

        if (transform.eulerAngles.x > 355
            || transform.eulerAngles.x < 120) {
            rotationX = 355;
        }

        if (transform.eulerAngles.x < 320
            && transform.eulerAngles.x < 200) {
            rotationX = 320;
        }

        Vector3 rotationAngles = new Vector3(rotationX, transform.eulerAngles.y, rotationZ);

        transform.eulerAngles = rotationAngles;
    }

    private void ClampZoom() {
        //if (camera.transform.localPosition.y > CameraZoomMax
        //    || camera.transform.localPosition.z > CameraZoomMax) {
        //    camera.transform.localPosition = new Vector3(0, CameraZoomMax - 1, CameraZoomMax - 1);
        //}

        //if (camera.transform.localPosition.y < CameraZoomMin
        //    || camera.transform.localPosition.z < CameraZoomMin) {
        //    camera.transform.localPosition = new Vector3(0, CameraZoomMin + 1, CameraZoomMin + 1);
        //}
    }
}
