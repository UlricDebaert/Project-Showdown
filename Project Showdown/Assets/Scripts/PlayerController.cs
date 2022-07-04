using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    float horizontalInput;
    public float moveSpeed;
    [Range(0, .3f)] public float movementSmoothing = .05f;
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
    const string playerIdle = "PlayerIdle_Anim";
    const string playerJump = "PlayerJump_Anim";
    const string playerWalk = "PlayerWalk_Anim";

    PlayerInput inputActions;
    InputAction aimInput;
    Vector2 lookPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inputActions = GetComponent<PlayerInput>();
        aimInput = inputActions.actions["Aim"];
    }


    void Update()
    {
        Move();
        Flip();
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
            // Move the character by finding the target velocity
            targetVelocity = new Vector2(horizontalInput * Time.deltaTime * moveSpeed * 100, rb.velocity.y);
        }
        else
        {
            // Move the character by finding the target velocity
            targetVelocity = new Vector2(horizontalInput * Time.deltaTime * moveSpeed * 100 * airDragMultiplier, rb.velocity.y);
        }

        // And then smoothing it out and applying it to the character
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);
    }

    public void Jump()
    {
        if (isGrounded)
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
    }
    
    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPos.position, groundCheckRadius, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheckPos.position, groundCheckRadius);
    }
}
