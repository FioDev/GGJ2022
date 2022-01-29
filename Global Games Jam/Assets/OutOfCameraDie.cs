using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfCameraDie : MonoBehaviour
{

    void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log(col.tag + "Died");
    }

}