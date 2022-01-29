using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcNoise
{
    public float perlinScale = 1.0f; //Spacing of texture map
    public float gain = 0.5f; //ammount to brighten / darken texture map by

    //The purpose of this is to return an array of values that represents the noise at x,y coordinate
    //2 numbers will be provided.
    //The map width, in int, which is the number of samples to take along an the noise (equidistant from eachother, on x axis)
    //The current Y offset, which is how far the perlin noise is "down or up".

    public List<float> GetNoiseAtHeight(int sampleWidth, float yOffset)
    {
        List<float> temp = new List<float>();
        
        //set up loop for samplewidth, sample equidistantly, add to list
        
        for (int i = 0; i < sampleWidth; i++)
        {
            float current = 0;
            current = Mathf.PerlinNoise(perlinScale * (sampleWidth / 1), yOffset);
            current += gain; //Adjust noise gain

            //Re-clamp, as the gain could throw it above 1 or below 0
            current = Mathf.Clamp01(current);

            temp.Add(current);
        }

        return temp;
    }
}
