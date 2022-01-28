using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class TerrainManager : MonoBehaviour
{
    [Header("Player 1")]
    public Tilemap GroundPlayer1, WallPlayer1;

    [Header("Player 2")]
    public Tilemap GroundPlayer2, WallPlayer2;

    [Header("Tiles")]
    Tile WhiteTile, BlackTile;

    public void GenerateTerrainFromNoise(int[,] noisePlayer1)
    {
        int width = noisePlayer1.GetLength(0), height = noisePlayer1.GetLength(1);
        int size = width * height;

        Vector3Int[] tilePositionsPlayer1 = new Vector3Int[size];
        Vector3Int[] tilePositionsPlayer2 = new Vector3Int[size];
        TileBase[] tileTypesPlayer1 = new TileBase[size];
        TileBase[] tileTypesPlayer2 = new TileBase[size];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                switch (noisePlayer1[x, y])
                {
                    // Air
                    case 0:
                        break;
                    // Ground
                    case 1:
                        break;
                    default:
                        break;
                }


            }
        }




    }
}
