using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    Air,
    Ground,
    Obstacle,
    Collectable
}

public class TilemapGenerator : MonoBehaviour
{

    //Variables
    //Component references
    ProcNoise noiseGen;

    //Singleton
    TilemapGenerator instance;

    private void Awake()
    {
        //Singleton
        if (instance != null)
        {
            instance = this;
        }

        noiseGen = new ProcNoise();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<TileType> GenerateTileRow(int width, float heightToGenerate)
    {
        List<TileType> temp = new List<TileType>();

        //Get a set of values from Proc Noise that represents tiles
        foreach (float x in noiseGen.GetNoiseAtHeight(width, heightToGenerate))
        {
            //Control ground / air gaps
            if (x > 0.75)
            {
                temp.Add(TileType.Ground);
            } else
            {
                temp.Add(TileType.Air);
            }
        }


        return temp;
    }
}
