using UnityEngine;

/// <summary>
/// Class for player camera movement and controls.
/// </summary>
public class PlayerCamera : MonoBehaviour {
    public float CameraRotationSpeedY = 2f;
    public float CameraRotationSpeedX = 2f;
    public float CameraSmooth = 1f;
    public float CameraZoomSpeed = 3f;
    public float CameraZoomMax = 30f;
    public float CameraZoomMin = 3f;

    private new Camera camera;
    private Rigidbody rb;

    private float CameraZoomVelocity = 0;
    private float CameraZoomTime = 0;

    /// <summary>
    /// Called on startup.
    /// </summary>
    void Start() {
        rb = gameObject.GetComponent<Rigidbody>();
        camera = gameObject.GetComponentInChildren<Camera>();
    }

    /// <summary>
    /// Called every frame.
    /// </summary>
    void FixedUpdate() {
        if (Input.GetAxis("Fire2") > 0) {
            RotateCamera();
        }
        ClampAngles();
        ZoomCamera();
        ClampZoom();
    }

    /// <summary>
    /// Uses mouse movement to rotate player view.
    /// </summary>
    private void RotateCamera() {
        float cameraMoveY = Input.GetAxis("Mouse X") * CameraRotationSpeedX;
        float cameraMoveX = Input.GetAxis("Mouse Y")* CameraRotationSpeedY;

        rb.AddTorque(-cameraMoveX, cameraMoveY, 0);
    }

    /// <summary>
    /// Changes camera fov based on mouse scroll wheel.
    /// </summary>
    private void ZoomCamera() {
        var cameraZoom = -Input.GetAxis("Mouse ScrollWheel");

        InertialZoom(cameraZoom);
    }

    /// <summary>
    /// Changes the camera fov with inerta.
    /// </summary>
    /// <param name="movement">The amount of scroll movement.</param>
    private void InertialZoom(float movement) {
        if (movement == 0) {
            CameraZoomTime -= (CameraZoomTime <= 0) ? 0 : 0.02f;
            CameraZoomVelocity = Mathf.Lerp(0, CameraZoomVelocity, CameraZoomTime);
        } else {
            CameraZoomTime += (CameraZoomTime >= 1) ? 0 : 0.1f;
            CameraZoomVelocity += movement * CameraZoomSpeed;
        }

        camera.fieldOfView += CameraZoomVelocity;
    }

    /// <summary>
    /// Clamps the player view from moving outside of bounds.
    /// </summary>
    private void ClampAngles() {

        var rotationX = transform.eulerAngles.x;
        var rotationZ = 0;

        if (transform.eulerAngles.x > 355
            || transform.eulerAngles.x < 120) {
            rotationX = 355;
        }

        if (transform.eulerAngles.x < 320
            && transform.eulerAngles.x > 200) {
            rotationX = 320;
        }

        Vector3 rotationAngles = new Vector3(rotationX, transform.eulerAngles.y, rotationZ);

        transform.eulerAngles = rotationAngles;
    }

    /// <summary>
    /// Clamps the camera fov.
    /// </summary>
    private void ClampZoom() {
        camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, CameraZoomMin, CameraZoomMax);
    }
}
