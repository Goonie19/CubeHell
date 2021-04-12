using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnFx : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource myFx;

    public AudioClip select;

    public AudioClip click;

    public void selectSound()
    {
        myFx.PlayOneShot(select);
    }

    public void clickSound()
    {
        myFx.PlayOneShot(click);
    }
}