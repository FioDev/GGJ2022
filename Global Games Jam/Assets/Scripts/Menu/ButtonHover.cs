using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHover : MonoBehaviour
{
    public void OnMouseOver()
    {
        FindObjectOfType<AudioManager>().Play("ButtonHover");
    }
    
    public void Click()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }
}
