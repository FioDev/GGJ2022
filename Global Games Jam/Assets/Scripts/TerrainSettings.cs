using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu]
public class TerrainSettings : ScriptableObject
{
    [Header("Map Size")]
    [Min(0)]
    public int Width = 19;
    [Min(0)]
    public int Height = 1000;

    [Header("Platforms")]
    [Range(0, 1)]
    public float NewPlatformChance = 0.3f;
    [Min(0)]
    public int MinPlatformDistanceY = 2;
    [Min(0)]
    public int MaxPlatformDistanceY = 5;
    [Min(0)]
    public int MinPlatformDistanceX = 1;
    [Min(0)]
    public int MaxPlatformDistanceX = 10;
    [Min(0)]
    public int MinPlatformWidth = 1;
    [Min(0)]
    public int MaxPlatformWidth = 9;

    [Header("Spikes")]
    [Range(0, 1)]
    public float SpikeOnPlatformChance = 0.2f;

    [Range(0, 1)]
    public float SpikeOnWallChance = 0.2f;
    [Min(0)]
    public int MinWallSpikeDistance = 2;

}
