using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFX : MonoBehaviour
{
    public AudioClip clickFx;
    public AudioSource myFx;

    public void ClickSound()
    {
        myFx.PlayOneShot(clickFx);
    }
}
