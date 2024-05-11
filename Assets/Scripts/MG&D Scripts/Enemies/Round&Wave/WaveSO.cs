using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WaveSpawnType
{
    Random,
    Line,
    Circle,
    Clump
}

[CreateAssetMenu(fileName = "New Wave", menuName = "Enemies/WaveSO")]
public class WaveSO : ScriptableObject
{
    public MobSO mob;
    public int mobCount;
    public WaveSpawnType spawnType;
    public float timeBetweenSpawns;
}
