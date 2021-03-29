using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [SerializeField]
    private AudioSource aud;
    [SerializeField]
    private AudioClip footStep;

    private void AnimFootStep()
    {
        aud.PlayOneShot(footStep);
    }
}
