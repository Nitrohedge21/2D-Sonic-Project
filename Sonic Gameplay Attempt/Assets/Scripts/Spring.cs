using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    private Animator anim;
    private enum AnimState { down, up}
    AnimState state;
    public float jumpForce = 20;
    [SerializeField] private AudioSource SpringFX;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        state = AnimState.up;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimations();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce,ForceMode2D.Impulse);
            state = AnimState.down;
            SpringFX.Play();
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            state = AnimState.up;
        }
    }

    void UpdateAnimations()
    {
        anim.SetInteger("state", (int)state);
    }
}
