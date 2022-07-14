using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerData PD;

    [Header("Move")]
    float horizontalInput;
    public float moveSpeed;
    [Range(0, .3f)] public float movementSmoothing = .05f;
    public float walkBackCoeff = .5f;
    Rigidbody2D rb;
    Vector3 velocity = Vector3.zero;

    [Header("Jump")]
    public float jumpForce;
    public float airDragMultiplier;
    public LayerMask groundLayer;

    [Header("Ground Check")]
    bool isGrounded;
    public Transform groundCheckPos;
    public float groundCheckRadius;

    [Header("Graphics")]
    public SpriteRenderer playerSprite;
    public Animator anim;
    public float minimalFlipSensitivity = 0.5f;
    string currentAnimation;
    const string playerIdle = "Player_Idle_Anim";
    const string playerJump = "Player_Jump_Anim";
    const string playerWalk = "Player_Walk_Anim";
    const string playerWalkBack = "Player_WalkBack_Anim";

    //Inputs
    PlayerInput inputActions;
    InputAction aimInput;
    Vector2 lookPosition;
    Vector2 lastLookPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inputActions = GetComponent<PlayerInput>();
        PD = GetComponent<PlayerData>();
        aimInput = inputActions.actions["Aim"];
        lastLookPosition = Vector2.right;
    }


    void Update()
    {
        if (PD.canMove)
        {
            Move();
            Flip();
        }
        if (inputActions.actions["Jump"].triggered) Jump();
    }

    private void FixedUpdate()
    {
        CheckSurroundings();
    }

    void Move()
    {
        Vector3 targetVelocity;

        if (isGrounded)
        {
            // Move the character by finding the target 
            if(lastLookPosition.x * horizontalInput > 0) targetVelocity = new Vector2(horizontalInput * Time.deltaTime * moveSpeed * 100, rb.velocity.y);
            else targetVelocity = new Vector2(horizontalInput * Time.deltaTime * moveSpeed * 100 * walkBackCoeff, rb.velocity.y);

            if (lastLookPosition.x * horizontalInput > 0) ChangeAnimationState(playerWalk);
            else if (lastLookPosition.x * horizontalInput < 0) ChangeAnimationState(playerWalkBack);
            else ChangeAnimationState(playerIdle);
        }
        else
        {
            // Move the character by finding the target velocity
            targetVelocity = new Vector2(horizontalInput * Time.deltaTime * moveSpeed * 100 * airDragMultiplier, rb.velocity.y);
            ChangeAnimationState(playerJump);
        }

        // And then smoothing it out and applying it to the character
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);

    }

    public void Jump()
    {
        if (isGrounded && !PD.isDead)
        {
            //rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    public void OnMove(InputAction.CallbackContext ctx) => horizontalInput = ctx.ReadValue<float>();

    void Flip()
    {
        lookPosition = aimInput.ReadValue<Vector2>();

        if (lookPosition.x > minimalFlipSensitivity || lookPosition.x < -minimalFlipSensitivity)
        {
            if (lookPosition.x < minimalFlipSensitivity) playerSprite.flipX = true;
            else playerSprite.flipX = false;
        }

        if(lookPosition != Vector2.zero)
        {
            lastLookPosition = lookPosition;
        }
    }
    
    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPos.position, groundCheckRadius, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheckPos.position, groundCheckRadius);
    }

    public void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;

        anim.Play(newAnimation);
        print(newAnimation);

        currentAnimation = newAnimation;
    }
}
