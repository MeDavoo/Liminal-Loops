using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 moveDirection;
    public CharacterController controller;

    public float speed;
    public float sprintSpeed;
    public float slideSlopeSpeed;
    public float gravity;
    public float jumpHeight;

    Vector3 velocity;
    public bool isGrounded, isSprinting;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    float smoothTurnVelocity;
    public float smoothTurnTime = 0.1f;

    private float groundRayDistance = 1;
    private RaycastHit slopeHit;

    void Start()
    {
        isSprinting = false;
    }

    // Update is called once per frame
    void Update()
    {
        //jump
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -20f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        //sprint
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
        }

        //gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //walk
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        if (moveDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothTurnVelocity, smoothTurnTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            if (!isSprinting)
            {
                controller.Move(moveDirection.normalized * speed * Time.deltaTime);
            }
            else if (isSprinting)
            {
                controller.Move(moveDirection.normalized * Mathf.Lerp(speed, sprintSpeed, 10) * Time.deltaTime);
            }
        }

        if (OnSteepSlope())
        {
            SteepSloveMovement();
        }
    }

    private bool OnSteepSlope()
    {
        if (!isGrounded) return false;
    
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, (controller.height / 2) + groundRayDistance))
        {
            float slopeAngle = Vector3.Angle(slopeHit.normal, Vector3.up);
            if (slopeAngle > controller.slopeLimit) return true;
        }
        return false;

    }

    private void SteepSloveMovement()
    {
        Vector3 slopeDirection = Vector3.up - slopeHit.normal * Vector3.Dot(Vector3.up, slopeHit.normal);
        float slideSpeed = speed + slideSlopeSpeed + Time.deltaTime;

        moveDirection = slopeDirection * -slideSpeed;
        moveDirection.y = moveDirection.y - slopeHit.point.y; 
    }
}
