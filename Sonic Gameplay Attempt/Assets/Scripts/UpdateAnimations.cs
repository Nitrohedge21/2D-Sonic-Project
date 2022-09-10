using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Trying to seperate the functions into different scripts to clean up the code a bit.
public class UpdateAnimations : MonoBehaviour
{
    PlayerController player;
    private Animator anim;
    private Rigidbody2D rb2d;
    private enum MovementState { standing, idleStart, idle, walk, buildSpeed, running, jump, roll, crouch, dead, drown, getHit, inAir, diving, lookUp, push, slowDown, turnLeft }

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void updateAnimations()
    //{
    //    MovementState state;

    //    if (player.directionX > 0f)
    //    {
    //        state = MovementState.walk;
    //    }
    //    else if (directionX < 0f)
    //    {
    //        state = MovementState.walk;
    //    }

    //    else
    //    {
    //        state = MovementState.standing;
    //    }

    //    if (rb2d.velocity.y > .1f)
    //    {
    //        state = MovementState.jump;
    //    }

    //    else if (rb2d.velocity.y < -.1f)
    //    {
    //        state = MovementState.diving;
    //    }

    //    anim.SetInteger("state", (int)state);
    //}

}
