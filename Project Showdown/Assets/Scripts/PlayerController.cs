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

    [Header("Falling")]
    public AnimationCurve fallingMultiplier;
    ConstantForce2D cf;
    float lastVelocity;
    float fallTimer;

    [Header("Crouch")]
    public float crouchSensitivity;
    float verticalInput;
    public bool isCrouching;

    [Header("Colliders")]
    public BoxCollider2D ownCollider;
    [Space]
    public Vector2 standUpColliderOffset;
    public Vector2 standUpColliderSize;
    [Space]
    public Vector2 crouchColliderOffset;
    public Vector2 crouchColliderSize;

    [Header("Ground Check")]
    public bool isGrounded;
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
    const string playerSpecialPower = "Player_SpecialPower_Anim";
    const string playerCrouch = "Player_Crouch_Anim";
    public bool animLock;

    //Inputs
    PlayerInput inputActions;
    InputAction aimInput;
    Vector2 lookPosition;
    Vector2 lastLookPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cf = GetComponent<ConstantForce2D>();
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

        Falling();
        Crouch();
    }

    private void FixedUpdate()
    {
        CheckSurroundings();
    }

    void Move()
    {
        Vector3 targetVelocity;

        if (verticalInput < crouchSensitivity && isGrounded) isCrouching = true;
        if (verticalInput >= crouchSensitivity || !isGrounded) isCrouching = false;

        if (!isCrouching)
        {
            if (isGrounded)
            {
                // Move the character by finding the target 
                if (lastLookPosition.x * horizontalInput > 0) targetVelocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
                else targetVelocity = new Vector2(horizontalInput * moveSpeed * walkBackCoeff, rb.velocity.y);

                if (lastLookPosition.x * horizontalInput > 0) ChangeAnimationState(playerWalk);
                else if (lastLookPosition.x * horizontalInput < 0) ChangeAnimationState(playerWalkBack);
                else ChangeAnimationState(playerIdle);
            }
            else
            {
                // Move the character by finding the target velocity
                targetVelocity = new Vector2(horizontalInput * moveSpeed * airDragMultiplier, rb.velocity.y);
                ChangeAnimationState(playerJump);
            }
        }
        else
        {
            targetVelocity = Vector2.zero;
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

    void Falling()
    {
        if(lastVelocity > 0 && rb.velocity.y < 0 && !isGrounded)
        {
            fallTimer += Time.deltaTime;
            cf.relativeForce = new Vector2(cf.relativeForce.x, -fallingMultiplier.Evaluate(fallTimer));
        }

        if(isGrounded || rb.velocity.y >= 0)
        {
            lastVelocity = rb.velocity.y;
            fallTimer = 0.0f;
            cf.relativeForce = new Vector2(cf.relativeForce.x, -fallingMultiplier.Evaluate(fallTimer));
        }
    }

    public void OnMove(InputAction.CallbackContext ctx) => horizontalInput = ctx.ReadValue<float>();
    public void OnCrouch(InputAction.CallbackContext ctx) => verticalInput = ctx.ReadValue<float>();

    void Flip()
    {
        lookPosition = aimInput.ReadValue<Vector2>();

        if (lookPosition.magnitude > minimalFlipSensitivity || lookPosition.magnitude < -minimalFlipSensitivity)
        {
            if (lookPosition.x < 0) playerSprite.flipX = true;
            else playerSprite.flipX = false;

            lastLookPosition = lookPosition;
        }
    }

    private void Crouch()
    {
        if (isCrouching)
        {
            ChangeAnimationState(playerCrouch);
            ownCollider.offset = crouchColliderOffset;
            ownCollider.size = crouchColliderSize;
            if (PD.ownGun != null) PD.ownGun.transform.localPosition = PD.character.gunPos + new Vector3(crouchColliderOffset.x, crouchColliderOffset.y, 0.0f);
        }
        else
        {
            ownCollider.offset = standUpColliderOffset;
            ownCollider.size = standUpColliderSize;
            if (PD.ownGun != null) PD.ownGun.transform.localPosition = PD.character.gunPos;
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
        if (currentAnimation == newAnimation || animLock) return;

        anim.Play(newAnimation);
        //print(newAnimation);

        currentAnimation = newAnimation;
    }
}
