using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScript : MonoBehaviour
{
    public Rigidbody playerBall;

    private InputAction inputAction;

    private BallControls ballControls;
    private Vector3 offset;

    void Awake()
    {
        ballControls = new BallControls();
        ballControls.Enable();
    }

    void Update()
    {
        transform.position = playerBall.position;
        

        Vector2 inputVector = ballControls.Ball.Camera_Move.ReadValue<Vector2>();

        transform.Rotate(Vector3.up , inputVector.x);
        transform.Rotate(Vector3.right , inputVector.y, Space.World);

        transform.Translate(new Vector3(0,2,-10), Space.Self);

    }

}
