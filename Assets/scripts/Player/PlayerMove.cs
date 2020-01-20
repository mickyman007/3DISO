using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public float CameraMoveSpeedX = 50f;
    public float CameraMoveSpeedY = 50f;
       
    private Rigidbody rb;

    void Start() {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update() {
        Move();
    }


    private void Move() {
        var movementX = Input.GetAxis("Horizontal") * CameraMoveSpeedX;
        var movementY = Input.GetAxis("Vertical") * CameraMoveSpeedY;
        rb.AddRelativeForce(movementX, 0, movementY, ForceMode.Force);
    }
}
