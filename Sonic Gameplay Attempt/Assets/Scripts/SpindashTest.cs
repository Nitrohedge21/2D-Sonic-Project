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

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow) && Input.GetKeyDown(KeyCode.A) /*&& playerController.isGrounded()*/)
        {
            rb2d.AddForce(transform.forward* thrust, ForceMode2D.Impulse);
            
        }
    }
}

//rb2d.GetComponent<PlayerController>.isGrounded();
