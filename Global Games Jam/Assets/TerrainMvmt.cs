using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMvmt : MonoBehaviour
{
    public float MaximumSpeed = 50;
    public float direction = 1;
    public float TimeUntilMaxSpeedSeconds = 30;
    public AnimationCurve SpeedOverTime;

    Vector2 cameraMotion;
    float totalTime = 0;

    // Update is called once per frame
    void Update()
    {
        totalTime += Time.deltaTime;

        cameraMotion = new Vector2(0f, MaximumSpeed * SpeedOverTime.Evaluate(totalTime / TimeUntilMaxSpeedSeconds));
        transform.Translate(cameraMotion * Time.deltaTime * direction);
    }
}
