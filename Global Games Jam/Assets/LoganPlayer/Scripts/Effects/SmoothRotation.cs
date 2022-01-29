 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 
 public class SmoothRotation : MonoBehaviour {
 
     // Use this for initialization
     public float speed = 1f;
     public float RotAngleZ = 45;
     
     // Update is called once per frame
     void Update () 
     {
         float rZ = Mathf.SmoothStep(-45,RotAngleZ,Mathf.PingPong(Time.time * speed,1));
         transform.rotation = Quaternion.Euler(0,0,rZ);
     }
 }