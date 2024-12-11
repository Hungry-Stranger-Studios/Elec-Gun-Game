using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerMovement : MonoBehaviour
{
    //Note that these are being serialized to utilize Gizmos
    [Header("Components")]
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerInputActions playerInputActions;
    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private BoxCollider2D playerCollider;

    [Header("Movement Curves")]
    [SerializeField] private AnimationCurve accelerationCurve;
    [SerializeField] private AnimationCurve decelerationCurve;
    private float time;
    private float bufferTime;

    [Header("Walk/Sprint Values")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float jumpMult;
    [SerializeField] private float speedModifier;

    [Header("Grounding Values")]
    [SerializeField] private Vector2 groundCheckOffset = new Vector2(0,0);
    [SerializeField] private float groundCheckRadius = 0.2f;

    private float moveSpeed;
    private float checkSpeed;
    private bool moveCancelled;
    private bool isSprinting;
    private float movementDirectionX;
    private bool isGrounded;
    private bool isDecelerating;


    private void Awake()
    {
        //getting components
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerTransform = GetComponent<Transform>();
        playerCollider = GetComponent<BoxCollider2D>();

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        //If an event only wants to happen on a certain phase this is a helpful way to cause that.
        //Note: ONLY works if the action is not in the evoke unity events part of the input actions.
        //playerInputActions.Player.Jump.performed += Jump; 

        //initialize internal values and states
        isGrounded = false;
        isDecelerating = true;
        checkSpeed = walkSpeed;
        moveCancelled = false;

    }

    private void Update()
    {
        CheckIfGrounded();
        

        //this will give you the vector2 containing the movement input (already normalized)
        movementDirectionX = playerInputActions.Player.Move.ReadValue<Vector2>().x;

        if (moveCancelled && bufferTime <= 0)
        {
            isDecelerating = true;
            walkSpeed = checkSpeed;
            if (speedModifier != 1)
            {
                speedModifier = 1;
                isSprinting = false;
            }
            bufferTime = 0;
            moveCancelled = false;
        }
        else if (moveCancelled && bufferTime > 0)
        {
            bufferTime -= Time.deltaTime;
        }

        
    }

    private void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
        //regular forward movement
        if (!isDecelerating)
        {
            if (isSprinting)
            {
                speedModifier -= 9 * Time.deltaTime;
            }
            if (speedModifier < 1)
            {
                speedModifier = 1;
                isSprinting = false;
            }
            if (time > 1f && walkSpeed == checkSpeed)
            {
                walkSpeed += 7;
            }
                moveSpeed = movementDirectionX * accelerationCurve.Evaluate(time) * walkSpeed * speedModifier;
            
        }
        else
        {
            //to decelerate, move the player a little more in the same direction they were going, at a little less of the same speed they were going
            moveSpeed = decelerationCurve.Evaluate(time) * playerRigidbody.velocity.x;
        }
        //if the player ends their jump early, start decreasing the upward velocity

        //applying player velocity
        playerRigidbody.velocity = new Vector2(moveSpeed, playerRigidbody.velocity.y);
    }

    public void Move(InputAction.CallbackContext context)
    {
        //if the player JUST put inputted movement
        if (context.started)
        {
            time = 0f;
            isDecelerating = false;
            moveCancelled = false;
        }
        //if the player changes their movement without cancelling it
        if (context.performed)
        {
            //do something
        }
        //if the player releases movement input
        else if (context.canceled)
        {
            time = 0f;
            bufferTime = 0.25f;
            moveCancelled = true;
        }
    }

    public void Jump(InputAction.CallbackContext context) //This is an EVENT method. This means that the below will happen ONLY on Jump.performed
    {
        //perform jumping
        if (context.performed)
        {
            if (isGrounded)
            {
                //"Uppercut" the player, sending them up for a jump
                playerRigidbody.AddForce(Vector2.up * jumpMult, ForceMode2D.Impulse);
                isGrounded = false;
            }
        }
        //If the player releases the jump velocity before the apex of their jump
        if (context.canceled && playerRigidbody.velocity.y > 0f)
        {
            //"Push the player back down. Reducing their velocity to 0 quicker than otherwise"
            playerRigidbody.AddForce(Vector2.down * playerRigidbody.velocity.y * 0.5f, ForceMode2D.Impulse);
        }
    }

    public void Sprint(InputAction.CallbackContext context)
    {
        //If the player presses sprint 
        if (context.started)
        {
            if (!isDecelerating && !isSprinting)
            {
                speedModifier += 3f;
                isSprinting = true;
            }
        }   
    }

    private void CheckIfGrounded()
    {
        // Cast a ray downwards from the player's ground check position
        isGrounded = Physics2D.OverlapCircle(groundCheckOffset + (Vector2)playerTransform.position, groundCheckRadius, LayerMask.GetMask("Ground"));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(groundCheckOffset + (Vector2)playerTransform.position, groundCheckRadius);
    }
}

