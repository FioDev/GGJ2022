using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System;


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
        GenerateAllTerrain(10, 1000, new Vector2Int(2, 5), new Vector2Int(2, 5));
    }

    protected void GenerateAllTerrain(uint width, uint height, Vector2Int minMaxYDistanceBetweenPlatforms, Vector2Int minMaxPlatformWidth)
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

        // Offset the positions so that the map is centered at 0, 0
        for (int y = -(int)height / 2; y < height / 2; y++)
        {
            int xMin = -(int)width / 2;
            int xMax = (int)width / 2 - 1;

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

        int GetRandomYDistanceToNextPlatform()
        {
            return (int)(UnityEngine.Random.value * (minMaxYDistanceBetweenPlatforms.y - minMaxYDistanceBetweenPlatforms.x) + minMaxYDistanceBetweenPlatforms.x);
        }

        int GetRandomPlatformWidth()
        {
            return (int)(UnityEngine.Random.value * (minMaxPlatformWidth.y - minMaxPlatformWidth.x) + minMaxPlatformWidth.x);
        }

        for (int y = -(int)height / 2; y < height / 2; y += GetRandomYDistanceToNextPlatform())
        {
            int x = 0;
            int platformWidth = GetRandomPlatformWidth();

            for (int i = -platformWidth / 2; i < platformWidth / 2; i++)
            {
                platformPositions.Add(new Vector3Int(x + i, y, 0));
                platformTiles.Add(SmallPlatform);
            }
        }

        GroundPlayer1.SetTiles(platformPositions.ToArray(), platformTiles.ToArray());
        GroundPlayer2.SetTiles(platformPositions.ToArray(), platformTiles.ToArray());

        Debug.Log($"Generated {width}x{height} tiles of terrain in {(DateTime.Now - before).TotalSeconds} seconds");
    }





    protected class Tiles
    {
        public List<Vector3Int> Positions = new List<Vector3Int>();
        public List<TileBase> TilesTypes = new List<TileBase>();
    }

    /*    protected void GetAllTilesFromTilemap(in Tilemap tilemap, ref Tiles player1Walls, ref Tiles player2Walls, ref Tiles player1Ground, ref Tiles player2Ground)
        {
            // Compress the bounds first to reduce unnecessary calls
            tilemap.CompressBounds();

            // Get an iterator for the bounds of the tilemap 
            BoundsInt.PositionEnumerator p = tilemap.cellBounds.allPositionsWithin.GetEnumerator();
            while (p.MoveNext())
            {
                Vector3Int current = p.Current;
                // Get the tile
                TileBase t = tilemap.GetTile(current);
                if (t != null)
                {
                    if(t == Player1Ground)
                    {

                    }
                    else if(t == Player2Ground)
                    {

                    }
                    else if(t == Player1Wall)
                    {

                    }
                    else if (t == Player2Wall)
                    {

                    }


                }
            }
        }*/


    public enum TileType
    {
        Air,
        Ground,
        Obstacle,
        Collectable
    }


    /*    public void GenerateTerrainFromNoise(TileType[,] noisePlayer1)
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


        }*/
}
