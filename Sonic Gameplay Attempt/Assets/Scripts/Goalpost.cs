using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goalpost : MonoBehaviour
{
    private enum AnimState { idle, spin, sonic}
    private Animator anim;
    AnimState state;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        UpdateAnimations();
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            
            anim.Play("Spin");
            Debug.Log("Sonic collided with the goalpost");

        }

        //why the fuck is this not working????????
        //me from a few hours later here, i had to reference it properly by adding the getcomponent on start and the updateanimations function.

    }

    void UpdateAnimations()
    {
        anim.SetInteger("state", (int)state);
    }
}
