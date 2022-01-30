// Enable/disable fio gen
// #define DO_FIO_GEN

using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System;
using System.Collections;

[RequireComponent(typeof(Grid))]
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
    public TileBase Player1HazardsUp;
    public TileBase Player1HazardsLeft;
    public TileBase Player1HazardsRight;


    [Space]
    public TileBase Player2Tile;
    public TileBase Player2BackgroundTile;
    public TileBase Player2Platform;
    public TileBase Player2HazardsUp;
    public TileBase Player2HazardsLeft;
    public TileBase Player2HazardsRight;


    private Vector3Int lastPlatformPosition;
    private int lastWallSpikesY;

    [Header("Settings")]
    public TerrainSettings Settings;
    private int seed = 0;

    [Header("Cameras")]
    public List<Transform> Cameras;

    [Header("Powerups")]
    public Transform PowerupParent;
    public GameObject PowerupPrefabPlayer1;
    public GameObject PowerupPrefabPlayer2;


    private Dictionary<int, bool> LevelsGenerated = new Dictionary<int, bool>();
    private Grid grid;


    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("Music");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        grid = GetComponent<Grid>();

        GenerateAllTerrain();
    }

    protected void CheckGenerateMoreTerrain()
    {
        StartCoroutine(WaitForCheckGenerateTerrain());
    }

    protected IEnumerator WaitForCheckGenerateTerrain()
    {
        foreach (Transform camera in Cameras)
        {
            Vector3Int cameraCentre = grid.WorldToCell(camera.position);

            for (int i = -Settings.EndlessTerrainGenerationRadius; i < Settings.EndlessTerrainGenerationRadius; i++)
            {
                int y = cameraCentre.y + i;

                if (!LevelsGenerated.ContainsKey(y))
                {
                    // Generate one layer of terrain
                    GenerateTerrainForHeight(y);

                    // Wait for next frame before continuing 
                    yield return null;
                }
            }
        }
    }

    protected void GenerateTerrainForHeight(int y)
    {
        System.Random r = new System.Random(seed * y);

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

        // Do a random check to see if there should be a platform on this level
        // OR if there hasn't been one for a while
        int yDistanceToLastPlatform = Mathf.Abs(y - lastPlatformPosition.y);

        bool doPlatformOnLayer =
            (r.NextDouble() < Settings.NewPlatformChance ||
         yDistanceToLastPlatform >= Settings.MaxPlatformDistanceY) &&
         yDistanceToLastPlatform >= Settings.MinPlatformDistanceY;

        //Debug.Log($"y={y} yDist={yDistanceToLastPlatform} do={doPlatformOnLayer}");


        if (doPlatformOnLayer)
        {
            // Left or right
            int sign = r.Next(0, 2) == 0 ? -1 : 1;

            // Platform pos
            int platformWidth = r.Next(Settings.MinPlatformWidth, Settings.MaxPlatformWidth);
            // Ensure the centre is in the correct position so that the whole platform can fit on the screen
            int platformCentre = Mathf.Clamp(lastPlatformPosition.x + sign * r.Next(Settings.MinPlatformDistanceX, Settings.MaxPlatformDistanceX),
                xMin + (platformWidth / 2) + 1, xMax - (platformWidth / 2) - 1);

            for (int i = 0; i < platformWidth; i++)
            {
                int newX = platformCentre - (platformWidth / 2) + i;

                if (newX > xMin && newX < xMax)
                {
                    platformPositions.Add(new Vector3Int(newX, y, 0));
                    p1PlatformTiles.Add(Player1Platform);
                    p2PlatformTiles.Add(Player2Platform);

                    // Record this position
                    lastPlatformPosition = new Vector3Int(newX, y, 0);
                }
            }

            // Chance for spikes to be on top of the platform
            if (r.NextDouble() < Settings.SpikeOnPlatformChance)
            {
                // Add random number of spikes on top
                for (int i = r.Next(1, platformWidth); i < platformWidth - 1; i++)
                {
                    int newX = platformCentre - (platformWidth / 2) + i;

                    if (newX > xMin && newX < xMax)
                    {
                        hazardPositions.Add(new Vector3Int(newX, y + 1, 0));
                        p1hazards.Add(Player1HazardsUp);
                        p2hazards.Add(Player2HazardsUp);
                    }
                }
            }
            // If no spikes, try doing a powerup instead
            else if (r.NextDouble() < Settings.PowerupOnPlatformChance)
            {
                Vector3Int powerupTile = new Vector3Int(platformCentre, y + 1, 0);
                Vector3 position = grid.GetCellCenterWorld(powerupTile);

                Instantiate(PowerupPrefabPlayer1, position, Quaternion.identity, PowerupParent);
                Instantiate(PowerupPrefabPlayer2, position, Quaternion.identity, PowerupParent);
            }
        }

        // Chance to do spikes on the wall
        if (r.NextDouble() < Settings.SpikeOnWallChance)
        {
            // Get left or right position
            int xPos = r.Next(0, 2) == 0 ? xMin + 1 : xMax - 1;
            int numberOfSpikes = r.Next(Settings.MinSpikesOnWallGroup, Settings.MaxSpikesOnWallGroup);

            if (Mathf.Abs(y - numberOfSpikes / 2 - lastWallSpikesY) >= Settings.MinWallSpikeDistance)
            {
                TileBase p1Rotated;
                TileBase p2Rotated;

                // Left
                if (xPos == xMin + 1)
                {
                    p1Rotated = Player1HazardsLeft;
                    p2Rotated = Player2HazardsLeft;
                }
                // Right
                else
                {
                    p1Rotated = Player1HazardsRight;
                    p2Rotated = Player2HazardsRight;
                }

                // Add the spikes
                for (int i = 0; i < numberOfSpikes; i++)
                {
                    int newY = y - (numberOfSpikes / 2) + i;

                    hazardPositions.Add(new Vector3Int(xPos, newY, 0));
                    p1hazards.Add(p1Rotated);
                    p2hazards.Add(p2Rotated);
                }

                // Record the position of the top most spike
                lastWallSpikesY = y + numberOfSpikes / 2;
            }
        }

#endif

        // Now set the tiles
        BackgroundPlayer1.SetTiles(background.ToArray(), p1Walls.ToArray());
        BackgroundPlayer2.SetTiles(background.ToArray(), p2Walls.ToArray());
        HazardsPlayer1.SetTiles(hazardPositions.ToArray(), p1hazards.ToArray());
        HazardsPlayer2.SetTiles(hazardPositions.ToArray(), p2hazards.ToArray());
        PlatformsPlayer1.SetTiles(platformPositions.ToArray(), p1PlatformTiles.ToArray());
        PlatformsPlayer2.SetTiles(platformPositions.ToArray(), p2PlatformTiles.ToArray());
        SolidBlocksPlayer1.SetTiles(borders.ToArray(), p1Borders.ToArray());
        SolidBlocksPlayer2.SetTiles(borders.ToArray(), p2Borders.ToArray());

        LevelsGenerated[y] = true;
    }

    protected void GenerateAllTerrain()
    {
        DateTime before = DateTime.Now;

        // Choose seed
        seed = Settings.DoRandomSeed ? Environment.TickCount : Settings.Seed;

        lastPlatformPosition = new Vector3Int(0, 0, 0);

        // Offset the positions so that the map is centered at 0, 0
        for (int y = -Settings.InitialHeightToGenerate / 2; y < Settings.InitialHeightToGenerate / 2; y++)
        {
            GenerateTerrainForHeight(y);
        }

        // Ensure there is a 3x2 platform at 0,0 for the player to spawn at
        for (int i = -1; i <= 1; i++)
        {
            PlatformsPlayer1.SetTile(new Vector3Int(i, 0, 0), Player1Platform);
            PlatformsPlayer1.SetTile(new Vector3Int(i, 1, 0), null);
            PlatformsPlayer1.SetTile(new Vector3Int(i, -1, 0), Player1Platform);
            HazardsPlayer1.SetTile(new Vector3Int(i, 0, 0), null);
            HazardsPlayer1.SetTile(new Vector3Int(i, -1, 0), null);
            HazardsPlayer1.SetTile(new Vector3Int(i, 1, 0), null);
            HazardsPlayer1.SetTile(new Vector3Int(i, 2, 0), null);

            PlatformsPlayer2.SetTile(new Vector3Int(i, 0, 0), Player2Platform);
            PlatformsPlayer2.SetTile(new Vector3Int(i, 1, 0), null);
            PlatformsPlayer2.SetTile(new Vector3Int(i, -1, 0), Player2Platform);
            HazardsPlayer2.SetTile(new Vector3Int(i, 0, 0), null);
            HazardsPlayer2.SetTile(new Vector3Int(i, -1, 0), null);
            HazardsPlayer2.SetTile(new Vector3Int(i, 1, 0), null);
            HazardsPlayer2.SetTile(new Vector3Int(i, 2, 0), null);
        }

        Debug.Log($"Generated {Settings.Width}x{Settings.InitialHeightToGenerate} tiles of terrain in {(DateTime.Now - before).TotalSeconds} seconds for seed {seed}");

        InvokeRepeating("CheckGenerateMoreTerrain", Settings.SecondsBetweenGenerationChecks, Settings.SecondsBetweenGenerationChecks);
    }


}
