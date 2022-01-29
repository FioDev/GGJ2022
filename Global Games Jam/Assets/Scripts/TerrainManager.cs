using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System;

[Serializable]
[CreateAssetMenu]
public class TerrainSettings : ScriptableObject
{ 

}



public class TerrainManager : MonoBehaviour
{
    [Header("Player 1")]
    public Tilemap GroundPlayer1, WallPlayer1;

    [Header("Player 2")]
    public Tilemap GroundPlayer2, WallPlayer2;

    [Header("Tiles")]
    public TileBase GreyTile, BlackTile, WhiteTile;
    [Space]
    public TileBase SmallPlatform;

    private void Start()
    {
        UnityEngine.Random.InitState(0);
        GenerateAllTerrain(10, 1000, new Vector2Int(2, 5), new Vector2Int(1, 3), new Vector2Int(2, 5));
    }


    protected void GenerateAllTerrain(uint width, uint height, Vector2Int minMaxYPlatformDistance, Vector2Int minMaxXPlatformDistance, Vector2Int minMaxPlatformWidth)
    {
        DateTime before = DateTime.Now;

        GroundPlayer1.ClearAllTiles();
        GroundPlayer2.ClearAllTiles();
        WallPlayer1.ClearAllTiles();
        WallPlayer2.ClearAllTiles();

        List<Vector3Int> borders = new List<Vector3Int>();
        List<TileBase> blackBorders = new List<TileBase>();
        List<TileBase> whiteBorders = new List<TileBase>();

        List<Vector3Int> background = new List<Vector3Int>();
        List<TileBase> blackWalls = new List<TileBase>();
        List<TileBase> whiteWalls = new List<TileBase>();

        int xMin = -(int)width / 2;
        int xMax = (int)width / 2 - 1;

        // Offset the positions so that the map is centered at 0, 0
        for (int y = -(int)height / 2; y < height / 2; y++)
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
        GroundPlayer1.SetTiles(borders.ToArray(), blackBorders.ToArray());
        GroundPlayer2.SetTiles(borders.ToArray(), whiteBorders.ToArray());
        WallPlayer1.SetTiles(background.ToArray(), whiteWalls.ToArray());
        WallPlayer2.SetTiles(background.ToArray(), blackWalls.ToArray());


        List<Vector3Int> platformPositions = new List<Vector3Int>();
        List<TileBase> platformTiles = new List<TileBase>();

        int GetRandomBetweenMinMax(Vector2Int minMax)
        {
            return (int)(UnityEngine.Random.value * (minMax.y - minMax.x) + minMax.x);
        }

        for (int y = -(int)height / 2; y < height / 2; y += GetRandomBetweenMinMax(minMaxYPlatformDistance))
        {
            int sign = (int)(UnityEngine.Random.value * 2) - 1;
            int x = 0 + sign * GetRandomBetweenMinMax(minMaxXPlatformDistance);

            int platformWidth = GetRandomBetweenMinMax(minMaxPlatformWidth);

            for (int i = 0; i < platformWidth; i++)
            {
                int newX = x + i - (platformWidth / 2);

                if (newX > xMin && newX < xMax)
                {
                    platformPositions.Add(new Vector3Int(newX, y, 0));
                    platformTiles.Add(SmallPlatform);
                }
            }
        }

        GroundPlayer1.SetTiles(platformPositions.ToArray(), platformTiles.ToArray());
        GroundPlayer2.SetTiles(platformPositions.ToArray(), platformTiles.ToArray());

        Debug.Log($"Generated {width}x{height} tiles of terrain in {(DateTime.Now - before).TotalSeconds} seconds");
    }


}
