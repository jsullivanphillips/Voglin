using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Round", menuName = "Enemies/RoundSO")]
public class RoundSO : ScriptableObject
{
    [System.Serializable]
    public class WaveEntry
    {
        [Range(2,30)]
        public float startTime;
        public WaveSO wave;
    }

    public List<WaveEntry> waves = new List<WaveEntry>();
}

