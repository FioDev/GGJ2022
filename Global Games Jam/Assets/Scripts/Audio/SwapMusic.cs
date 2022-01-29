using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapMusic : MonoBehaviour
{

    public string prevMusic;

    public bool activated = false;
    public bool swap;
    public string nextMusic;
    void OnTriggerEnter2D()
    {
        if (!activated)
        {
            FindObjectOfType<AudioManager>().Stop(prevMusic);

            if (swap)
            {
                FindObjectOfType<AudioManager>().Play(nextMusic);
            }

            activated = true;
        }
    }
}
