using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private enum AnimState {spin, collect};
    private Animator anim;
    private int ringCount;
    int RingValue = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ringCount < 0)
        {
            ringCount = 0;
        }
        //UpdateAnimations();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            RingCounter.instance.ChangeScore(RingValue);
            ringCount = ringCount + 1;
            Destroy(gameObject);
        }
    }
    //private void UpdateAnimations()
    //{
    //    AnimState state;

    //    state = AnimState.spin;

    //    anim.SetInteger("state", (int)state);

    //}
}
