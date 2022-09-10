using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider2d;
    float directionX;
    UpdateAnimations animUpdate;
    // Animation stuff
    private Animator anim;
    private enum MovementState { standing, idleStart, idle, walk, buildSpeed, running, jump, roll, crouch, dead, drown, getHit, inAir,diving, lookUp,push, slowDown, turnLeft}
    private SpriteRenderer sprite;

    [Header("Required Stuff")]
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpHeight = 10f;
    private bool isTouchingLeft;
    private bool isTouchingRight;
    private bool wallJumping;
    private float touchingLeftOrRight;
    bool facingRight = true;
    
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        directionX = Input.GetAxisRaw("Horizontal");
     
        if((!isTouchingLeft && !isTouchingRight) || isGrounded())
        {
            rigidbody2d.velocity = new Vector2(directionX * moveSpeed, rigidbody2d.velocity.y);
        }

        // Putting isGrounded after the input makes the ray not show up for some reason.
        if (isGrounded() && Input.GetButtonDown("Jump"))
        {
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpHeight);
        }


        isTouchingLeft = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y), new Vector2(0.2f, 0.9f), 0f, jumpableGround);
        isTouchingRight = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y), new Vector2(0.2f, 0.9f), 0f, jumpableGround);

        if (isTouchingLeft)
        {
            touchingLeftOrRight = 1;
        }
        else if (isTouchingRight)
        {
            touchingLeftOrRight = -1;
        }

        if(Input.GetButtonDown("Jump") && (isTouchingRight || isTouchingLeft) && !isGrounded())
        {
            wallJumping = true;
            Invoke(nameof(setJump2False), 0.08f);
        }

        if(wallJumping)
        {
            rigidbody2d.velocity = new Vector2(moveSpeed * touchingLeftOrRight, jumpHeight);
        }
    }

    public bool isGrounded()
    {
        // Draws a line that stays green while it's grounded and turns red when it's not.
        // Got this part from coding monkey

        float extraHeightText = .5f;
        RaycastHit2D raycastHit = Physics2D.Raycast(boxCollider2d.bounds.center, Vector2.down, boxCollider2d.bounds.extents.y + extraHeightText, jumpableGround);

        if(directionX < 0 && facingRight)
        {
            flip();
        }
        else if (directionX >0 && !facingRight)
        {
            flip();
        }

        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(boxCollider2d.bounds.center, Vector2.down * (boxCollider2d.bounds.extents.y + extraHeightText), rayColor);
        Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;

    }
    void setJump2False()
    {
        wallJumping = false;
    }

    void flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void updateAnimations()
    {
        MovementState state;

        if(directionX > 0f)
        {
            state = MovementState.walk;
        }
        else if (directionX < 0f)
        {
            state = MovementState.walk;
        }

        else
        {
            state = MovementState.standing;
        }

        if (rigidbody2d.velocity.y >.1f)
        {
            state = MovementState.jump;
        }

        else if(rigidbody2d.velocity.y < -.1f)
        {
            state = MovementState.diving;
        }

        anim.SetInteger("state",(int)state);
    }
}

// isGrounded using Raycast
/*
float extraHeightText = .5f;
RaycastHit2D raycastHit = Physics2D.Raycast(boxCollider2d.bounds.center, Vector2.down, boxCollider2d.bounds.extents.y + extraHeightText, jumpableGround);

// Draws a box on the player to check the function.

Color rayColor;
if (raycastHit.collider != null)
{
    rayColor = Color.green;
}
else
{
    rayColor = Color.red;
}
Debug.DrawRay(boxCollider2d.bounds.center, Vector2.down * (boxCollider2d.bounds.extents.y + extraHeightText), rayColor);
Debug.Log(raycastHit.collider);
return raycastHit.collider != null;
*/

// isGrounded using BoxCast
/*
float extraHeightText = 1f;
RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, extraHeightText, jumpableGround);

Color rayColor;
if (raycastHit.collider != null)
{
    rayColor = Color.green;
}
else
{
    rayColor = Color.red;
}
Debug.DrawRay(boxCollider2d.bounds.center + new Vector3(boxCollider2d.bounds.extents.x, 0), Vector2.down * (boxCollider2d.bounds.extents.y + extraHeightText), rayColor);
Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.extents.x, 0), Vector2.down * (boxCollider2d.bounds.extents.y + extraHeightText), rayColor);
Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.extents.x, boxCollider2d.bounds.extents.y + extraHeightText), Vector2.right * (boxCollider2d.bounds.extents.x * 2f), rayColor);
Debug.Log(raycastHit.collider);
return raycastHit.collider != null;
*/

// Personal attempt on flipping the player
/*
if (Input.GetKeyDown(KeyCode.A))
{
    transform.Rotate(0, 180, 0);
}
if (Input.GetKeyDown(KeyCode.D))
{
    transform.Rotate(0, 0, 0);
}
*/