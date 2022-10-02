using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider2d;
    private CircleCollider2D circleCollider2d;
    float directionX;
    // Animation stuff
    private Animator anim;
    private enum MovementState { standing, idleStart, idle, walk, buildSpeed, running, jump, roll, crouch, dead, drown, getHit, inAir,diving, lookUp,push, slowDown, turnLeft}
    private SpriteRenderer sprite;

    [Header("Required Stuff")]
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpHeight = 10f;
    [SerializeField] private float maxSpeed = 15f;
    private float minSpeed = 10f;

    private bool isTouchingLeft;
    private bool isTouchingRight;
    private bool wallJumping;
    private float touchingLeftOrRight;
    bool facingRight = true;
    //[HideInInspector]  //THIS WAS A DUMB THING TO DO BECAUSE I STILL HAVE TO ASSIGN IT IN THE INSPECTOR GRAHHHHHHHHHHHHHHHH
    public ItemCollector CollectorScript;
    MovementState state;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        circleCollider2d = GetComponent<CircleCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        circleCollider2d.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Changed GetAxisRaws into GetAxis to get a smoother movement and slowdown.
        directionX = Input.GetAxis("Horizontal");
     
        if((!isTouchingLeft && !isTouchingRight) || isGrounded())
        {
            rigidbody2d.velocity = new Vector2(directionX * moveSpeed, rigidbody2d.velocity.y);
            //The player gains speed over time instead of input, gonna try to figure out how to fix this.
            if (Input.GetAxis("Horizontal") != 0)
            {
                //Added != 0 here so that i could use bool in an if statement. Thanks to logicandchaos from Unity answers.
                moveSpeed += 0.01f;
            }
            else
            {
                moveSpeed -= 0.1f;
            }
            
            if(moveSpeed < minSpeed)
            {
                moveSpeed = minSpeed;
            }
            if (moveSpeed > maxSpeed)
            {
                moveSpeed = maxSpeed;
            }
        }

        // Putting isGrounded after the input makes the ray not show up for some reason.
        if (isGrounded() && Input.GetButtonDown("Jump"))
        {
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpHeight);
        }


        // If you ever change the box collider of the player, adjust the x float value of the new vector2s.
        isTouchingLeft = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y), new Vector2(0.5f, 0.9f), 0f, jumpableGround);
        isTouchingRight = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y), new Vector2(0.5f, 0.9f), 0f, jumpableGround);

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
            Invoke(nameof(SetJump2False), 0.08f);
        }

        if(wallJumping)
        {
            rigidbody2d.velocity = new Vector2(moveSpeed * touchingLeftOrRight, jumpHeight);
        }

        UpdateAnimations();
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
        //Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;

    }
    void SetJump2False()
    {
        wallJumping = false;
    }

    void flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void UpdateAnimations()
    {
        //MovementState state;      //Moved this to the first lines with variables and stuff so that I can access it anywhere.
        //switch(state)
        //{

        //}
        // Might be able to use a switch case here.

        if(directionX > 0f && moveSpeed <= 11f)
        {
            state = MovementState.walk;
        }
        else if (directionX < 0f && moveSpeed <= 11f)
        {
            state = MovementState.walk;
        }

        else if (directionX > 0f && moveSpeed <= 12f)
        {
            state = MovementState.buildSpeed;
        }
        else if (directionX < 0f && moveSpeed <= 12f)
        {
            state = MovementState.buildSpeed;
        }

        else if (directionX > 0f && moveSpeed <= 20f)
        {
            state = MovementState.running;
        }
        else if (directionX < 0f && moveSpeed <= 20f)
        {
            state = MovementState.running;
        }


        else
        {
            state = MovementState.standing;
        }

        if (rigidbody2d.velocity.y >.1f && !isGrounded())
        {
            state = MovementState.jump;
        }

        else if(rigidbody2d.velocity.y < -.1f && !isGrounded())
        {
            state = MovementState.diving;
            //Changed this to inAir but this part still needs work.
        }

        if(isTouchingLeft && isGrounded()/* && Input.GetKeyDown(KeyCode.LeftArrow)*/)
        {
            //Needs more work, especially with the collision.
            state = MovementState.push;
        }

        if (isTouchingRight && isGrounded() /*&& Input.GetKeyDown(KeyCode.RightArrow)*/)
        {
            state = MovementState.push;
        }
        if(Input.GetKeyDown(KeyCode.DownArrow) && isGrounded())
        {
            state = MovementState.crouch;
        }

        if(!isGrounded() && state != MovementState.jump && state != MovementState.inAir)
        {
            state = MovementState.diving;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && moveSpeed > 13f && isGrounded())
        {
            boxCollider2d.enabled = false;
            circleCollider2d.enabled = true;
            state = MovementState.roll;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && moveSpeed > 13f && !isGrounded())
        {
            boxCollider2d.enabled = true;
            circleCollider2d.enabled = false;
        }

        anim.SetInteger("state",(int)state);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Badniks"))
        {
            //state = MovementState.getHit;
            //I honestly don't know why the fuck it doesn't work when i do it like the line above??????
            anim.Play("GettingHit");

            int Temp = CollectorScript.RingCount;

            ItemCollector.instance.UpdateRingCount(-1);
            
            Debug.Log("ring decreased");

        }

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

//random shit that were changed or cut
/*
//if (other.gameObject.CompareTag("Badniks"))
//{
//    Debug.Log("Sonic got hit by the badnik");
//    //ringCount = ringCount - 5;
//    //Need to find a way to reference this so that sonic loses rings.
//}
*/