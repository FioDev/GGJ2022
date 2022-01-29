using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu]
public class TerrainSettings : ScriptableObject
{
    [Min(0)]
    public int Width = 19;
    [Min(0)]
    public int Height = 1000;
    [Min(0)]
    public int MinPlatformDistanceY = 2;
    [Min(0)]
    public int MaxPlatformDistanceY = 5;
    [Min(0)]
    public int MinPlatformDistanceX = 1;
    [Min(0)]
    public int MaxPlatformDistanceX = 3;
    [Min(0)]
    public int MinPlatformWidth = 1;
    [Min(0)]
    public int MaxPlatformWidth = 5;
}
