using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepOnLoad : MonoBehaviour
{
    private static GameObject instance;

    //Don't destroy this object on load, makes sure duplicates do not appear by deleting them if the object already exists elsewhere
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
            instance = gameObject;
        else
            Destroy(gameObject);
    }
}