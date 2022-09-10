using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpindashTest : MonoBehaviour
{
    private Rigidbody2D rb2d;
    PlayerController playerController;
    private float thrust = 1f;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerController = rb2d.GetComponent<PlayerController>();
    }

    // Changed the isGrounded function to public to be able to access it in here.
    // tried making it protected but couldn't figure out how to access it.
    void Update()
    {
        if(playerController.isGrounded() && Input.GetKeyDown(KeyCode.DownArrow) && Input.GetKeyDown(KeyCode.A))
        {
            rb2d.AddForce(transform.forward* thrust, ForceMode2D.Impulse);
            
        }
        else
        {

        }
    }
}

//rb2d.GetComponent<PlayerController>.isGrounded();
