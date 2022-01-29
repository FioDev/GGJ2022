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
        List<TileType>[] x = new List<TileType>[2];
        x = GenerateTileRow(10, 2, Time.deltaTime);

        string outputLog = "";

        foreach (TileType y in x[2])
        {
            if (y == TileType.Air)
            {
                outputLog += "O";
            } else if (y == TileType.Ground)
            {
                outputLog += "X";
            }
        }

        Debug.Log(outputLog);
    }

    //Width in tiles. Playerwidth in tiles (IN THE SAME SCALE AS WIDTH), height to generate as a float (Any scale you want)
    public List<TileType>[] GenerateTileRow(int width, int playerWidth, float heightToGenerate)
    {
        List<TileType>[] temp = new List<TileType>[2];
        
        for (int layer = 0; layer < 2; layer++)
        {
            switch (layer)
            {


                //TODO
                //THIS IS BROKEN
                //Layer overflows a bit and im not sure how to fix it
                //Sadge
                case 0:

                    //Get a set of values from Proc Noise that represents tiles
                    foreach (float x in noiseGen.GetNoiseAtHeight(width, heightToGenerate))
                    {
                        //Control ground / air gaps
                        if (x > 0.75)
                        {
                            Debug.Log("layer is currently " + layer);
                            temp[layer].Add(TileType.Ground);
                        }
                        else
                        {
                            temp[layer].Add(TileType.Air);
                        }
                    }

                    temp[layer] = ValidateGaps(temp[layer], playerWidth);

                    break;


                case 1:
                    
                    //Check previous layer for ground below. Generate a random number between 1 and 20.
                    //If you roll a nat 20, spawn a collectable over the ground that would be there
                    foreach (TileType x in temp[layer + 1])
                    {
                        int y = Random.Range(1, 20);
                        if (y == 20) //Roll a 20, get a collectable (if spawnable in current position (there is ground under you))
                        {
                            if (x == TileType.Ground)
                            {
                                temp[layer].Add(TileType.Collectable);
                            } else
                            {
                                temp[layer].Add(TileType.Air);
                            }
                        } else 
                        {
                            temp[layer].Add(TileType.Air);
                        }
                    }

                    break;

                case 2:


                    for (int i = 0; i < width; i++)
                    {
                        temp[layer].Add(TileType.Air);
                    }

                    break;
            }
        }

        return temp;
    }

    private List<TileType> ValidateGaps(List<TileType> x, int targetWidth)
    {

        //This currently does absolutely nothing
        //it SHOULD go through x (which is a list of tile types along a row) and record the maximum gap
        //If the maximum gap on that row is larger than targetWidth (the width of a player or otherwise minimum gap) just return x
        //If its less, change the next few or previous few tile types to air until validation passes
        //Possibly do this recursivley.
        //It might work
        //you never know
        return x;
    }
}
