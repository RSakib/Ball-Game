using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestBallControl : MonoBehaviour
{
    static float DEFAULT_SPEED = 2;
    static float DEFAULT_JUMP_H = 30;
    static float DEFAULT_STOMP_V = 50;

    [Header("Cameras")]
    [Tooltip("This Object is designed for use with Cinemachine, so put your main brain here")]
    public Camera Main_Camera_Brain;
    [Tooltip("Attach the virtual camera that will be following this object. Note that it will deactive in anticipation of a second Speed Camera")]
    public GameObject Player_Cam;

    [Header("Attribute Values")]
    [Tooltip("Speed coefficient for how the ball travels")]
    public float speed = DEFAULT_SPEED;
    [Tooltip("How much force the ball will jump with")]
    public float jump_h = DEFAULT_JUMP_H;
    [Tooltip("How much force the ball will slam to the ground with")]
    public float stomp_v = DEFAULT_STOMP_V;

    // Rigidbody component of ball
    private Rigidbody ballRigidBody;
    // New User Input System object
    private BallControls ballControls;
    // Raycast hit beneath the ball
    private RaycastHit downRay;
    // A rendered trail behind the ball
    private TrailRenderer tRender;

    private void Awake()
    {
        // Set up all private variables on awake of Ball
        ballRigidBody = GetComponent<Rigidbody>();
        tRender = GetComponent<TrailRenderer>();
        ballControls = new BallControls();

        // Set the Initial states of Trail Render and the main virtual cam
        tRender.emitting = false;
        Player_Cam.SetActive(true);

        // Enable the Input System and enable the actions within it
        ballControls.Ball.Enable();
        ballControls.Ball.Jump.performed += ballJump;
        ballControls.Ball.Stomp.performed += ballStomp;
        // Do movement in fixedUpdate to allow for continuous hold on input
    }

    private void FixedUpdate()
    {
        // If the Ball is going a above a certain speed, the speed
        //camera will engage and the ball trail will start to draw
        if(ballRigidBody.velocity.magnitude > 50)
        {
            tRender.emitting = true;
            Player_Cam.SetActive(false);
        }
        // Else, just turn the speed camera off
        else
        {
            // Turn off the trail render from before if the ball is not fast enough
            if (ballRigidBody.velocity.magnitude < 20)
            {
                tRender.emitting = false;
            }
            Player_Cam.SetActive(true);
        }

        // Get the orientation Quaternion for the current camera
        Quaternion CharacterRotation = Main_Camera_Brain.transform.rotation;

        // Cast a raycast downwards starting from the ball's position
        Physics.Raycast(origin:ballRigidBody.transform.position, direction:Vector3.down,hitInfo: out downRay);

        // Read the input for movement from the Input System
        Vector2 inputVector = ballControls.Ball.Movement.ReadValue<Vector2>();
        
        // Using the downray's normal and distance, determine if the ground is flat
        //and is touching the Ball's rigidbody
        if (downRay.normal.y == 1 && downRay.distance < 1)
        {
            // Movement is applied in the direction of the Camera, not the Ball
            ballRigidBody.AddForce((CharacterRotation*new Vector3(inputVector.x, 0 ,inputVector.y))*speed, ForceMode.Impulse);
            // Decay any movement done on flat ground
            momentum(0.7f);
        }
        //Else If it is ON a slope, either allow forward movement or gradually ramp up momentum 
        else if(downRay.distance < 1.2)
        {
            // If the player is going forward, simply do basic movement
            if (inputVector.y > 0)
            {
                ballRigidBody.AddForce((CharacterRotation*new Vector3(inputVector.x, 0 ,inputVector.y))*speed, ForceMode.Impulse);
            }
            // Else, gradually accelerate movement
            else
            {
                // TODO: Make this a switch statement
                if (ballRigidBody.velocity.magnitude < 30)
                {
                    momentum(4f);
                }
                else if(ballRigidBody.velocity.magnitude < 40)
                {
                    momentum(6f);
                }
                else
                {
                    momentum(8f);
                }
            }
        }
        // Else (which means this ball is in the air), limit movement and push on momentum for a dynamic falling ball
        else
        {
            ballRigidBody.AddForce((CharacterRotation*new Vector3(inputVector.x, 0 ,inputVector.y))*speed/2, ForceMode.Impulse);
            momentum(2.5f);
        }
        

    }

    // Push the Ball faster in the direction it was already going
    private void momentum(float multiplier)
    {
        // We get the direction by velocity instead of local position
        // This is because the local position will be messed up due to how the ball is always rotating
        Vector3 momentum_direction = ballRigidBody.velocity.normalized;
        Debug.DrawRay(ballRigidBody.position, Quaternion.Euler(90,0,0)*momentum_direction*100, Color.green, Time.deltaTime);
        ballRigidBody.AddForce(ballRigidBody.velocity*multiplier, ForceMode.Acceleration);
    }

    // Apply a Force Downward Force to ideally bounce the ball off the ground
    private void ballStomp(InputAction.CallbackContext context)
    {
        // Only Stomp if a raycast down is >= 1.5 (which probably means its in the air and accounts for slopes)
        if (downRay.distance >= 1.5)
        {
            ballRigidBody.AddForce(Vector3.down*stomp_v, ForceMode.Impulse);
        }
    }

    // Apply a Force Upward to ideally make the ball jump off the ground
    private void ballJump(InputAction.CallbackContext context)
    {
        // Only Jump if a raycast down is is < 1 (Which means it is on stable ground, but also allows jumps on inclines)
        if(downRay.normal != new Vector3(0,0,0) && downRay.distance < 1)
        {
            ballRigidBody.AddForce(Vector3.up*jump_h, ForceMode.VelocityChange);
        }
    }

}
