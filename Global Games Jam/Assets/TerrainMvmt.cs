using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMvmt : MonoBehaviour
{
    public float stepLength = 10;

    public float direction = 1;

    Vector2 cameraMotion;
    // Update is called once per frame
    void Update()
    {
        cameraMotion = new Vector2( 0f, stepLength);
        transform.Translate(cameraMotion * Time.deltaTime * direction);
    }
}
