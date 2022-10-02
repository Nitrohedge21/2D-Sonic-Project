using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private enum AnimState { idle, spin, sonic}
    private Animator anim;
    AnimState state;
    bool sonicPassed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimations();
    }

    void Timer()
    {
        new WaitForSecondsRealtime(1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            sonicPassed = true;
        }

        //why the fuck is this not working????????

    }
    void UpdateAnimations()
    {
        if(sonicPassed = true)
        {
            Debug.Log("Sonic has passed the goal post");
            state = AnimState.spin;
        }
    }
   
}
