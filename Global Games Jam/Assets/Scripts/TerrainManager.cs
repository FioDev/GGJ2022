// Enable/disable fio gen
// #define DO_FIO_GEN

using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System;

public class TerrainManager : MonoBehaviour
{
    [Header("Player 1")]
    public Tilemap SolidBlocksPlayer1;
    public Tilemap PlatformsPlayer1;
    public Tilemap HazardsPlayer1;
    public Tilemap BackgroundPlayer1;

    [Header("Player 2")]
    public Tilemap SolidBlocksPlayer2;
    public Tilemap PlatformsPlayer2;
    public Tilemap HazardsPlayer2;
    public Tilemap BackgroundPlayer2;

    [Header("Tiles")]
    public TileBase Player1Tile;
    public TileBase Player1BackgroundTile;
    public TileBase Player1Platform;
    public TileBase Player1Hazards;

    [Space]
    public TileBase Player2Tile;
    public TileBase Player2BackgroundTile;
    public TileBase Player2Platform;
    public TileBase Player2Hazards;


    [Header("Settings")]
    public TerrainSettings Settings;

    private void Start()
    {
        UnityEngine.Random.InitState(0);
        GenerateAllTerrain();
    }

    protected void GenerateAllTerrain()
    {
        DateTime before = DateTime.Now;

        SolidBlocksPlayer1.ClearAllTiles();
        SolidBlocksPlayer2.ClearAllTiles();
        BackgroundPlayer1.ClearAllTiles();
        BackgroundPlayer2.ClearAllTiles();
        HazardsPlayer1.ClearAllTiles();
        HazardsPlayer2.ClearAllTiles();
        BackgroundPlayer1.ClearAllTiles();
        BackgroundPlayer2.ClearAllTiles();

        // Solid blocks for the edges
        List<Vector3Int> borders = new List<Vector3Int>();
        List<TileBase> p1Borders = new List<TileBase>();
        List<TileBase> p2Borders = new List<TileBase>();

        // Background
        List<Vector3Int> background = new List<Vector3Int>();
        List<TileBase> p1Walls = new List<TileBase>();
        List<TileBase> p2Walls = new List<TileBase>();

        // Platforms
        List<Vector3Int> platformPositions = new List<Vector3Int>();
        List<TileBase> p1PlatformTiles = new List<TileBase>();
        List<TileBase> p2PlatformTiles = new List<TileBase>();

        // Hazards
        List<Vector3Int> hazardPositions = new List<Vector3Int>();
        List<TileBase> p1hazards = new List<TileBase>();
        List<TileBase> p2hazards = new List<TileBase>();

        int xMin = -Settings.Width / 2;
        int xMax = Settings.Width / 2 - 1;

        // Offset the positions so that the map is centered at 0, 0
        for (int y = -Settings.Height / 2; y < Settings.Height / 2; y++)
        {
            // Borders
            borders.Add(new Vector3Int(xMin, y, 0));
            borders.Add(new Vector3Int(xMax, y, 0));
            p1Borders.Add(Player1Tile);
            p1Borders.Add(Player1Tile);
            p2Borders.Add(Player2Tile);
            p2Borders.Add(Player2Tile);

            // Background
            for (int i = xMin; i <= xMax; i++)
            {
                background.Add(new Vector3Int(i, y, 0));
                p1Walls.Add(Player1BackgroundTile);
                p2Walls.Add(Player2BackgroundTile);
            }
        }

        // Set border tiles
        SolidBlocksPlayer1.SetTiles(borders.ToArray(), p1Borders.ToArray());
        SolidBlocksPlayer2.SetTiles(borders.ToArray(), p2Borders.ToArray());
        BackgroundPlayer1.SetTiles(background.ToArray(), p1Walls.ToArray());
        BackgroundPlayer2.SetTiles(background.ToArray(), p2Walls.ToArray());



#if DO_FIO_GEN

        for (int y = -Settings.Height / 2; y < Settings.Height / 2; y++)
        {
            List<TileType>[] tiles = TilemapGenerator.Instance.GenerateTileRow(Settings.Width, 1, y);


            string outputLog = "";

            foreach (TileType t in tiles[2])
            {
                if (t == TileType.Air)
                {
                    outputLog += "O";
                }
                else if (t == TileType.Ground)
                {
                    outputLog += "X";
                }
            }

            Debug.Log(outputLog);

            for (int i = 0; i < tiles[2].Count; i++)
            {

                Debug.Log("HERE");
                switch (tiles[2][i])
                {
                    case TileType.Air:
                        break;
                    case TileType.Ground:
                        platformPositions.Add(new Vector3Int(i, y, 0));
                        platformTiles.Add(PlatformPlayer1);
                        
                        break;
                }
            }
        }
#else
        int GetRandomBetweenMinMax(int min, int max)
        {
            return (int)(UnityEngine.Random.value * (max - min) + min);
        }

        for (int y = -Settings.Height / 2; y < Settings.Height / 2; y += GetRandomBetweenMinMax(Settings.MinPlatformDistanceY, Settings.MaxPlatformDistanceY))
        {
            int sign = (int)(UnityEngine.Random.value * 2) - 1;
            int x = 0 + sign * GetRandomBetweenMinMax(Settings.MinPlatformDistanceX, Settings.MaxPlatformDistanceX);

            int platformWidth = GetRandomBetweenMinMax(Settings.MinPlatformWidth, Settings.MaxPlatformWidth);

            for (int i = 0; i < platformWidth; i++)
            {
                int newX = x + i - (platformWidth / 2);

                if (newX > xMin && newX < xMax)
                {
                    platformPositions.Add(new Vector3Int(newX, y, 0));
                    p1PlatformTiles.Add(Player1Platform);
                    p2PlatformTiles.Add(Player2Platform);


                    // Just do spikes here for now
                    if(UnityEngine.Random.value < 0.2f)
                    {
                        hazardPositions.Add(new Vector3Int(newX, y + 1, 0));
                        p1hazards.Add(Player1Hazards);
                        p2hazards.Add(Player2Hazards);
                    }
                }
            }
        }
#endif
        HazardsPlayer1.SetTiles(hazardPositions.ToArray(), p1hazards.ToArray());
        HazardsPlayer2.SetTiles(hazardPositions.ToArray(), p2hazards.ToArray());

        PlatformsPlayer1.SetTiles(platformPositions.ToArray(), p1PlatformTiles.ToArray());
        PlatformsPlayer2.SetTiles(platformPositions.ToArray(), p2PlatformTiles.ToArray());

        Debug.Log($"Generated {Settings.Width}x{Settings.Height} tiles of terrain in {(DateTime.Now - before).TotalSeconds} seconds");
    }


}
