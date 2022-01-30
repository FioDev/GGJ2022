using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerrainMvmt : MonoBehaviour
{
    public float MaximumSpeed = 50;
    public float direction = 1;
    public float TimeUntilMaxSpeedSeconds = 30;
    public AnimationCurve SpeedOverTime;

    Vector2 cameraMotion;
    float totalTime = 0;

    float speedValue = 0;

    //public Text speedText;

    // Update is called once per frame
    void Update()
    {
        totalTime += Time.deltaTime;

        speedValue = MaximumSpeed * SpeedOverTime.Evaluate(totalTime / TimeUntilMaxSpeedSeconds);
        cameraMotion = new Vector2(0f, speedValue);
        transform.Translate(cameraMotion * Time.deltaTime * direction);
        if(speedValue >= 10){
            //speedText.text = speedValue.ToString().Substring(0,2);
        } else {

        //speedText.text = speedValue.ToString().Substring(0,3);
        }
    }
}
