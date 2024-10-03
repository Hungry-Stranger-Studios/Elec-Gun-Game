using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerMovement : MonoBehaviour
{

    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;
    private Rigidbody2D playerRigidbody;
    private Transform playerTransform;
    private BoxCollider2D playerCollider;

    [SerializeField] private AnimationCurve accelerationCurve;
    [SerializeField] private AnimationCurve decelerationCurve;
    private float time;

    [Header("Walk/Sprint Values")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    private float moveSpeed;
    private bool isRunning;
    private float movementDirectionX;
    private bool onGround;
    private bool isAccelerating;
    private bool isDecelerating;
    private int lastMoveDirection;


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
        moveSpeed = 5f;
        movementDirectionX = 0;
        onGround = false;
        isDecelerating = false;
}

    private void Update()
    {
        //update ground state
        if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            onGround = true;
        else
            onGround = false;

        //movement is done in update because it is an update method rather than an event method
        //this will give you the vector2 containing the movement input (already normalized)
        movementDirectionX = playerInputActions.Player.Move.ReadValue<Vector2>().x;
        if (movementDirectionX > 0)
        {
            lastMoveDirection = 1;
        }
        else if (movementDirectionX < 0)
        {
            lastMoveDirection = -1;
        }

        if (!isDecelerating)
        {
            //changing speed based on animation curve
            moveSpeed = accelerationCurve.Evaluate(time) * walkSpeed;
        }
        else
        {
            moveSpeed = decelerationCurve.Evaluate(time) * walkSpeed;
        }
        time += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        //decides what kind of movement to perform
        if (!isDecelerating)
        {
            //playerRigidbody.MovePosition((Vector2)playerTransform.position + new Vector2(movementDirection.x * (moveSpeed) * Time.deltaTime, 0));
            playerRigidbody.velocity = new Vector2(movementDirectionX * moveSpeed, playerRigidbody.velocity.y);
        }
        else
        {
            //BUG: This cause a burst of movement in the opposite direction if the player has been stopped.
            //Possible fix: instead of multiplying by moveSpeed, use the rigidbody.velocity value.
            //i.e., playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x * lastMoveDirection, playerRigidbody.velocity.y);
            playerRigidbody.velocity = new Vector2(lastMoveDirection * moveSpeed, playerRigidbody.velocity.y);    
        }
        //if using rigidbody addForce movement
        //playerRigidbody.AddForce(new Vector2(movementDirection.x, 0)  * moveSpeed); 
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            time = 0f;
            isDecelerating = false;
        }
        if (context.performed)
        {
            isDecelerating = false;
        }
        else if (context.canceled)
        {
            time = 0f;
            isDecelerating = true;
        }
    }
    public void Jump(InputAction.CallbackContext context) //This is an EVENT method. This means that the below will happen ONLY on Jump.performed
    {
        //perform jumping
        if (onGround)
        {
            playerRigidbody.AddForce(Vector2.up * 3f, ForceMode2D.Impulse);
            onGround = false;
        }
    }

    public void sprintToggle(InputAction.CallbackContext context)
    {
        //If the player presses sprint, and they are 
        if (context.started)
        {
            isRunning = true;
        }
        else if(context.canceled)
        {
            isRunning = false;
        }      
    }
}

