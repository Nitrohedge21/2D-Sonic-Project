using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpindashTest : MonoBehaviour
{
    private Rigidbody2D rb2d;
    PlayerController playerController;
    private float dashSpeed = 10f;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        rb2d = playerController.GetComponent<Rigidbody2D>();
    }

    // Changed the isGrounded function to public to be able to access it in here.
    // tried making it protected but couldn't figure out how to access it.
    void Update()
    {
     
        if(/*Input.GetKeyDown(KeyCode.DownArrow) &&*/ Input.GetKeyDown(KeyCode.Z) &&playerController.isGrounded())
        {
            rb2d.AddForce(transform.forward* dashSpeed);
            //The line above is not working because i can't reference the object's instance properly.
            Debug.Log("Spindash input test");
            
        }
    }
}


//rb2d = playerController.GetComponent<Rigidbody2D>();
//rb2d = GameObject.Find("Player").GetComponent<Rigidbody2D>();

//rb2d.GetComponent<PlayerController>.isGrounded();
//playerController = rb2d.GetComponent<PlayerController>();
//if(playerController.isGrounded())
//{
//    rb2d.AddForce(transform.forward * dashSpeed);
//    Debug.Log("Test");
//}