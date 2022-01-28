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

    //Width in tiles. Playerwidth in tiles (IN THE SAME SCALE AS WIDTH), height to generate as a float (Any scale you want)
    public List<TileType>[] GenerateTileRow(int width, int playerWidth, float heightToGenerate)
    {
        List<TileType>[] temp = new List<TileType>[2];
        
        for (int layer = 2; layer > -1; layer--)
        {
            switch (layer)
            {
                case 0:

                    foreach (TileType x in temp[layer - 1])
                    {
                        temp[layer].Add(TileType.Air);
                    }

                    break;

                case 1:
                    
                    //Check previous layer for ground below. Generate a random number between 1 and 20.
                    //If you roll a nat 20, spawn a collectable over the ground that would be there
                    foreach (TileType x in temp[layer - 1])
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

                    //Get a set of values from Proc Noise that represents tiles
                    foreach (float x in noiseGen.GetNoiseAtHeight(width, heightToGenerate))
                    {
                        //Control ground / air gaps
                        if (x > 0.75)
                        {
                            temp[layer].Add(TileType.Ground);
                        }
                        else
                        {
                            temp[layer].Add(TileType.Air);
                        }
                    }

                    temp[layer] = ValidateGaps(temp[layer], playerWidth);
                    
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
