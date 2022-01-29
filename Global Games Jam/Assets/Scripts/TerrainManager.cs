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
    public TileBase GreyTile;
    public TileBase BlackTile;
    public TileBase WhiteTile;
    public TileBase PlatformPlayer1;

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

        List<Vector3Int> borders = new List<Vector3Int>();
        List<TileBase> blackBorders = new List<TileBase>();
        List<TileBase> whiteBorders = new List<TileBase>();

        List<Vector3Int> background = new List<Vector3Int>();
        List<TileBase> blackWalls = new List<TileBase>();
        List<TileBase> whiteWalls = new List<TileBase>();

        int xMin = -Settings.Width / 2;
        int xMax = Settings.Width / 2 - 1;

        // Offset the positions so that the map is centered at 0, 0
        for (int y = -Settings.Height / 2; y < Settings.Height / 2; y++)
        {
            // Borders
            borders.Add(new Vector3Int(xMin, y, 0));
            borders.Add(new Vector3Int(xMax, y, 0));
            blackBorders.Add(BlackTile);
            blackBorders.Add(BlackTile);
            whiteBorders.Add(WhiteTile);
            whiteBorders.Add(WhiteTile);

            // Background
            for (int i = xMin; i <= xMax; i++)
            {
                background.Add(new Vector3Int(i, y, 0));
                blackWalls.Add(BlackTile);
                whiteWalls.Add(WhiteTile);
            }
        }

        // Set border tiles
        SolidBlocksPlayer1.SetTiles(borders.ToArray(), blackBorders.ToArray());
        SolidBlocksPlayer2.SetTiles(borders.ToArray(), whiteBorders.ToArray());
        BackgroundPlayer1.SetTiles(background.ToArray(), whiteWalls.ToArray());
        BackgroundPlayer2.SetTiles(background.ToArray(), blackWalls.ToArray());


        List<Vector3Int> platformPositions = new List<Vector3Int>();
        List<TileBase> platformTiles = new List<TileBase>();

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
                    platformTiles.Add(PlatformPlayer1);
                }
            }
        }

        PlatformsPlayer1.SetTiles(platformPositions.ToArray(), platformTiles.ToArray());
        PlatformsPlayer2.SetTiles(platformPositions.ToArray(), platformTiles.ToArray());

        Debug.Log($"Generated {Settings.Width}x{Settings.Height} tiles of terrain in {(DateTime.Now - before).TotalSeconds} seconds");
    }


}
