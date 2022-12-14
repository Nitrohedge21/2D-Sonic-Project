using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goalpost : MonoBehaviour
{
    private enum AnimState { idle, spin, sonic}
    private Animator anim;
    AnimState state;
    [SerializeField] private AudioSource SoundFX;
    int passed = 0;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        UpdateAnimations();
    }

    void SFX()
    {
        SoundFX.Play();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && passed <= 0)
        {
            state = AnimState.spin;
            SFX();
            passed += 1;
            Debug.Log("Sonic collided with the goalpost");
            //Eggman side spins for 8 times and then switches to sonic which then lands on sonic's side.
            if(passed > 1)
            {
                passed = 1;
            }

        }

        //why the fuck is this not working????????
        //me from a few hours later here, i had to reference it properly by adding the getcomponent on start and the updateanimations function.

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && passed >= 1)
        {
            state = AnimState.sonic;
        }
    }

    void UpdateAnimations()
    {
        anim.SetInteger("state", (int)state);
    }
}
