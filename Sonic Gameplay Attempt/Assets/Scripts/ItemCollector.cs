using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    private enum AnimState {spin, collect};
    private Animator anim;
    private int ringCount;
    
    public static ItemCollector instance;
    public TextMeshProUGUI text;
    int score;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
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
    public void ChangeScore(int RingValue)
    {
        score = RingValue;
        text.text = score.ToString();
        // Removed the x part of the line above.
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // change this part so that the player has the script rather than the rings
        if (other.gameObject.CompareTag("Player"))
        {
            ItemCollector.instance.UpdateRingCount(1);
            Destroy(gameObject);
        }
    }

    public int RingCount
    {
        get { return ringCount; }
   
    }
    public void UpdateRingCount(int value)
    {
        ringCount = ringCount + value;
        ChangeScore(ringCount);
    }
   


    //private void UpdateAnimations()
    //{
    //    AnimState state;

    //    state = AnimState.spin;

    //    anim.SetInteger("state", (int)state);

    //}
}
