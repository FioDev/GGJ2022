using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu]
public class TerrainSettings : ScriptableObject
{
    [Header("Map Size")]
    [Min(1)]
    public int Width = 19;
    [Min(1)]
    public int InitialHeightToGenerate = 500;
    [Min(1)]
    public int EndlessTerrainGenerationRadius = 100;
    [Min(1)]
    public int SecondsBetweenGenerationChecks = 1;

    [Header("Seed")]
    public bool DoRandomSeed = true;
    public int Seed = 0;

    [Header("Platforms")]
    [Range(0, 1)]
    public float NewPlatformChance = 0.25f;
    [Min(1)]
    public int MinPlatformDistanceY = 2;
    [Min(1)]
    public int MaxPlatformDistanceY = 3;
    [Min(1)]
    public int MinPlatformDistanceX = 2;
    [Min(2)]
    public int MaxPlatformDistanceX = 7;
    [Min(1)]
    public int MinPlatformWidth = 3;
    [Min(2)]
    public int MaxPlatformWidth = 9;

    [Header("Spikes")]
    [Range(0, 1)]
    public float SpikeOnPlatformChance = 0.1f;

    [Range(0, 1)]
    public float SpikeOnWallChance = 0.3f;
    [Min(0)]
    public int MinWallSpikeDistance = 2;

    [Min(0)]
    public int MinSpikesOnWallGroup = 2;
    [Min(0)]
    public int MaxSpikesOnWallGroup = 4;

    [Header("Powerups")]
    [Range(0, 1)]
    public float PowerupOnPlatformChance = 0.1f;

}
