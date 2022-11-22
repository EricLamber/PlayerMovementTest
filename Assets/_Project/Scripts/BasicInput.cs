using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicInput : MonoBehaviour
{
    [Header("Player Parametrs")]
    [SerializeField] private float walkSpeed = 2;
    [SerializeField] private float sprintSpeed = 5;
    [SerializeField] private float rotationSpeed = 5;
    [SerializeField] private float jumpPower = 20;
    [SerializeField] private float rollPower = 20;

    [Header("Ground Check")]
    [SerializeField] private float groundCheckSphereRadius;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded;

    private const string animatorIsGrounded = "IsGrounded";
    private const string animatorRolling = "Roll";
    private const string animatorSpeed = "Speed";
    private const string animatorJump = "Jump";

    private float moveSpeed;
    private Animator animator;
    private Rigidbody rigidBody;

    private Vector3 moveInput;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rigidBody.freezeRotation = true;
    }

    private void Update()
    {
        InputHandler();
        GroundCheckHandler();
        SpeedChangeHandler();
    }

    private void FixedUpdate()
    {
        MoveHandler();
        RotationHandler();
        JumpAndRollHandler();
    }

    private void MoveHandler()
    {
        if (!isGrounded)
            return;

        rigidBody.velocity = moveSpeed * moveInput;

        animator.SetFloat(animatorSpeed, rigidBody.velocity.magnitude);
    }

    private void RotationHandler()
    {
        if (moveInput == Vector3.zero)
            return;

        var targetRot = Quaternion.LookRotation(moveInput, Vector3.up);
        var resultRot = Quaternion.Slerp(rigidBody.rotation, targetRot, rotationSpeed * Time.deltaTime);
        rigidBody.MoveRotation(resultRot);
    }

    private void JumpAndRollHandler()
    {
        if (!isGrounded) 
            return;

        if (Input.GetButtonDown("Jump"))
        {
            rigidBody.AddForce(transform.up * jumpPower);
            animator.SetTrigger(animatorJump);
        }
        
        if (Input.GetButtonDown("Roll"))
        {
            rigidBody.AddForce(transform.forward * rollPower);
            animator.SetTrigger(animatorRolling);
        }
    }

    private void InputHandler()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");

        moveInput = new Vector3(-vertical, 0, horizontal).normalized;
    }

    private void GroundCheckHandler()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckSphereRadius, whatIsGround);
        animator.SetBool(animatorIsGrounded, isGrounded);
    }

    private void SpeedChangeHandler()
    {
        if (!isGrounded)
            return;

        if (Input.GetButton("Sprint"))
        {
            moveSpeed = sprintSpeed;
        }
        else
        {
            moveSpeed = walkSpeed;

            animator.SetTrigger(animatorIsGrounded);
        }
    }

}
