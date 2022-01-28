using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;


public class TerrainManager : MonoBehaviour
{
    [Header("Player 1")]
    public Tilemap GroundPlayer1, WallPlayer1;

    [Header("Player 2")]
    public Tilemap GroundPlayer2, WallPlayer2;

    [Header("Tiles")]
    public TileBase Player1Ground, Player2Ground;


    private void Start()
    {
        TileType[,] noise = new TileType[5, 10];
        int width = noise.GetLength(0), height = noise.GetLength(1);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if(x == 0 || y == 0 || x == width - 1 || x == height - 1)
                {
                    noise[x, y] = TileType.Ground;
                }
            }
        }

        GenerateTerrainFromNoise(noise);
    }

    public enum TileType
    {
        Air,
        Ground,
        Obstacle,
        Collectable
    }


    public void GenerateTerrainFromNoise(TileType[,] noisePlayer1)
    {
        int width = noisePlayer1.GetLength(0), height = noisePlayer1.GetLength(1);

        List<Vector3Int> tilePositionsPlayer1 = new List<Vector3Int>();
        List<Vector3Int> tilePositionsPlayer2 = new List<Vector3Int>();
        List<TileBase> groundLayerTileTypesPlayer1 = new List<TileBase>();
        List<TileBase> groundLayerTileTypesPlayer2 = new List<TileBase>();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);

                switch (noisePlayer1[x, y])
                {
                    case TileType.Air:
                        tilePositionsPlayer2.Add(position);
                        groundLayerTileTypesPlayer2.Add(Player2Ground);
                        break;
                    case TileType.Ground:
                        tilePositionsPlayer1.Add(position);
                        groundLayerTileTypesPlayer1.Add(Player1Ground);
                        break;
                }
            }
        }



        GroundPlayer1.SetTiles(tilePositionsPlayer1.ToArray(), groundLayerTileTypesPlayer1.ToArray());
        GroundPlayer2.SetTiles(tilePositionsPlayer2.ToArray(), groundLayerTileTypesPlayer2.ToArray());


    }
}
