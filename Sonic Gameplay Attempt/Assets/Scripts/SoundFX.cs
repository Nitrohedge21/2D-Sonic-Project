using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFX : MonoBehaviour
{
    [SerializeField]private AudioSource sfx;
  
    void SFX()
    {
        sfx.Play();

    }
}
