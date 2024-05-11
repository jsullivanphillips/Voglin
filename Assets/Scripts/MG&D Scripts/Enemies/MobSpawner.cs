using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnData
{
    public float Time;
    public MobSO Mob;
    public Vector3 Position;

   

    public SpawnData(float time, MobSO mob, Vector3 position)
    {
        Time = time;
        Mob = mob;
        Position = position;
    }
}

public class MobSpawner : MonoBehaviour
{

    private int round;

    [SerializeField]
    private Transform _spawnBounds;

    [SerializeField]
    private GameObject _SpawnForeshadowPrefab;

    [SerializeField]
    private RoundManager _RoundManager;

    private RoundSO roundSO;
    private List<RoundSO.WaveEntry> waves = new List<RoundSO.WaveEntry>();

    private List<SpawnData> spawnList = new List<SpawnData>();

    private void Update()
    {
        if (GameStateManager.Instance.GetGameState() == GameState.Paused)
        {
            return;
        }
        //DoWave();
    }

    // public void AddSpawn(float time, MobSO mob, Vector3 position)
    // {
    //     spawnList.Add(new SpawnData(time, mob, position));
    // }

    // TESTING THIS OUT
    public void AddSpawn(float time, MobSO mob, Vector3 position)
    {
        var spawnData = new SpawnData(time, mob, position);
        spawnList.Add(spawnData);
        StartCoroutine(SpawnAtTime(spawnData));
    }

    private IEnumerator SpawnAtTime(SpawnData spawnData)
    {
        // Calculate the target time for spawning based on the countdown
       
    
        // Wait until the round time is less than or equal to the target time
        while (_RoundManager.RoundTime > spawnData.Time)
        {
            // Check if the game is paused or in between rounds
            if (GameStateManager.Instance.GetGameState() == GameState.Paused )
            {
                yield return null; // Wait for the next frame without advancing the wait time
            }
            else
            {
                // Wait for a short period before checking again to reduce performance impact
                yield return new WaitForSeconds(0.1f);
            }
        }
    
        // Check if the spawn data is still in the list before spawning
        if (spawnList.Contains(spawnData))
        {
            SpawnMob(spawnData.Mob, spawnData.Position);
            spawnList.Remove(spawnData); // Remove after spawning
        }
    }
    // TESTING

    public void RemoveSpawnsBeforeTime(float time)
    {
        spawnList.RemoveAll(spawn => spawn.Time < time);
    }

    public void RemoveAllSpawns()
    {
        spawnList.Clear();
    }

    private void DoWave()
    {
        float currentTime = _RoundManager.RoundTime;

        List<SpawnData> spawnsToRemove = new List<SpawnData>();

        foreach (var spawn in spawnList)
        {
            Debug.Log("Checking spawn time: " + spawn.Time + " against current time: " + currentTime);
            if (currentTime <= spawn.Time)
            {
                SpawnMob(spawn.Mob, spawn.Position);
                spawnsToRemove.Add(spawn);
            }
        }

        foreach (var spawn in spawnsToRemove)
        {
            spawnList.Remove(spawn);
        }
    }

    private void SetupWave(WaveSO waveSO, float waveStartTime)
    {
        switch (waveSO.spawnType)
        {
            case WaveSpawnType.Random:
                SetupRandomWave(waveSO, waveStartTime);
                break;
            // case WaveSpawnType.Line:
            //     SpawnLineWave(waveSO);
            //     break;
            // case WaveSpawnType.Circle:
            //     SpawnCircleWave(waveSO);
            //     break;
            // case WaveSpawnType.Clump:
            //     SpawnClumpWave(waveSO);
            //     break;
        }
    }

    public void DestroyAllMobs()
    {
        GameObject[] mobs = GameObject.FindGameObjectsWithTag("Mob");
        foreach (GameObject mob in mobs)
        {
            Destroy(mob);
        }
    }

    public void SetRoundSO(RoundSO roundSO, int round)
    {
        this.round = round;
        this.roundSO = roundSO;
        waves.Clear();
        waves.AddRange(roundSO.waves);

        foreach (RoundSO.WaveEntry waveEntry in waves)
        {
            SetupWave(Instantiate(waveEntry.wave), waveEntry.startTime);
        }
    }

    private void SetupRandomWave(WaveSO waveSO, float waveStartTime)
    {
        MobSO mob = Instantiate(waveSO.mob);
        mob.health = waveSO.mob.health + (round * 2);
        for (int i = 0; i < waveSO.mobCount; i++)
        {
            float spawnTime = _RoundManager._MaxRoundTime - ((i) * waveSO.timeBetweenSpawns) - waveStartTime;
            AddSpawn(spawnTime, mob, GetRandomSpawnPosition());
        }
    }

    private void SpawnMob(MobSO mobToSpawn, Vector3 spawnLocation)
    {
        Instantiate(_SpawnForeshadowPrefab, spawnLocation, Quaternion.identity);
        _SpawnForeshadowPrefab.GetComponent<SpawnForeshadow>()._MobPrefab = mobToSpawn.mobPrefab;
        _SpawnForeshadowPrefab.GetComponent<SpawnForeshadow>().mobSO = mobToSpawn;
    }


    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 position = new Vector3(
            Random.Range(_spawnBounds.position.x - _spawnBounds.localScale.x / 2, _spawnBounds.position.x + _spawnBounds.localScale.x / 2),
            Random.Range(_spawnBounds.position.y - _spawnBounds.localScale.y / 2, _spawnBounds.position.y + _spawnBounds.localScale.y / 2),
            Random.Range(_spawnBounds.position.z - _spawnBounds.localScale.z / 2, _spawnBounds.position.z + _spawnBounds.localScale.z / 2)
        );
        return position;
    }

}
