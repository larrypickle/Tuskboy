using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //8 direction character controller 
    //https://www.youtube.com/watch?v=cVy-NTjqZR8

    float charSpeed = 5;
    public float turnSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public bool isSprinting;
    public bool canSprint;
    public float jumpHeight;




    //for collision / slopes
    public float height = 0.5f;
    public float heightPadding = 0.05f;
    public LayerMask ground; //so we know what is ground or not
    public float maxGroundAngle = 120;
    public float gravity = -12;
    //public float gravity;

    CharacterController controller;

    Animator anim;

    Vector2 playerInput;
    float angle; //to know how to rotate player
    float groundAngle; //check if our current groundAngle is less than maxGroundAngle

    Vector3 forward;
    RaycastHit hitInfo; //to check for ground to see if we can continue moving
    bool grounded;
    float velocityY;

    Quaternion targetRotation; //quaternion used to represent rotations
    Transform cam; // to keep track of camera angles


    // Start is called before the first frame update
    void Start()
    {

        cam = Camera.main.transform; //this accesses the main camera
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        GetInput();//get input every frame, so the thing stops when

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        CalculateDirection();
        CalculateForward();
        //CalculateGroundAngle();
        //CheckGround();
        ApplyGravity();
        //DrawDebugLines();

        if (Mathf.Abs(playerInput.x) < 1 && Mathf.Abs(playerInput.y) < 1)
        { //this stops the character from moving when no input
            anim.SetInteger("condition", 0);

            return;
        }

        else
        {
            anim.SetInteger("condition", 1);
        }
        Rotate();
        Move();


    }

    void Move()
    {

        if (Input.GetKey(KeyCode.LeftShift) && canSprint == true)
        {
            isSprinting = true;
        }

        else
        {
            isSprinting = false;
        }
        if (groundAngle >= maxGroundAngle)
        {
            return;
        }

        if (isSprinting == true)
        {
            charSpeed = sprintSpeed;
            //Debug.Log("debug: " + sprintSpeed + "velocity: " + velocity);

        }

        else
        {
            charSpeed = walkSpeed;
        }
        anim.SetFloat("velocity", (charSpeed / 10) - (float)0.5);

        Vector3 velocity = transform.forward * charSpeed;
        controller.Move(velocity * Time.deltaTime);
    }
    void GetInput()
    {
        playerInput.x = Input.GetAxisRaw("Horizontal");
        playerInput.y = Input.GetAxisRaw("Vertical");
    }

    void CalculateDirection()
    {
        angle = Mathf.Atan2(playerInput.x, playerInput.y); //give us radians, tangent between up and horizontal
        angle = Mathf.Rad2Deg * angle; // to convert to degrees
        angle += cam.eulerAngles.y;
    }

    void Rotate()
    {
        targetRotation = Quaternion.Euler(0, angle, 0); // convert euler angles to quaternions
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    

    void CalculateForward()
    {
        if (!grounded)
        {
            forward = (transform.forward).normalized;
            return;
        }

        forward = Vector3.Cross(hitInfo.normal, -transform.right);
    }

    void CalculateGroundAngle()
    {
        if (!grounded)
        {
            groundAngle = 90;
            return;
        }
        groundAngle = Vector3.Angle(hitInfo.normal, transform.forward);

    }

    //use raycast of length height to determine whether or not player is grounded
    void CheckGround()
    {
        if (Physics.Raycast(transform.position, -Vector3.up, out hitInfo, height + heightPadding, ground))
        {
            if (Vector3.Distance(transform.position, hitInfo.point) > height)
            {
                transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.up * height, 5 * Time.deltaTime);
            }
            grounded = true;
        }

        else
        {
            grounded = false;
        }
    }

    void ApplyGravity()
    {
        if (!grounded)
        {
            transform.position += Physics.gravity * Time.deltaTime;
        }
    }

    
    void Jump()
    {
        if (controller.isGrounded)
        {
            float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
            velocityY = jumpVelocity;
            //Debug.Log("jump work");

        }
    }
}
