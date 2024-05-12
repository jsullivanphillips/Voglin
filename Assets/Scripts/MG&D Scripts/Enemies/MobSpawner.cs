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

    

    
    // TESTING

    public void RemoveSpawnsBeforeTime(float time)
    {
        spawnList.RemoveAll(spawn => spawn.Time < time);
    }

    public void ClearSpawns()
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
                PrepareRandomWave(waveSO, waveStartTime);
                break;
            // case WaveSpawnType.Line:
            //     SpawnLineWave(waveSO);
            //     break;
            // case WaveSpawnType.Circle:
            //     SpawnCircleWave(waveSO);
            //     break;
            case WaveSpawnType.Clump:
                PrepareClumpWave(waveSO, waveStartTime);
                break;
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

    private void PrepareRandomWave(WaveSO waveSO, float waveStartTime)
    {
        MobSO mob = Instantiate(waveSO.mob);
        mob.health = waveSO.mob.health + (round * 2);
        for (int i = 0; i < waveSO.mobCount; i++)
        {
            float spawnTime = _RoundManager._MaxRoundTime - ((i) * waveSO.timeBetweenSpawns) - waveStartTime;
            AddSpawn(spawnTime, mob, GetRandomSpawnPosition());
        }
    }

    private void PrepareClumpWave(WaveSO waveSO, float waveStartTime)
    {
        MobSO mob = Instantiate(waveSO.mob);
        mob.health = waveSO.mob.health + (round * 2);
        List<Vector3> spawnPositions = GetClumpSpawnPositions(waveSO.mobCount);

        for (int i = 0; i < waveSO.mobCount; i++)
        {
            float spawnTime = _RoundManager._MaxRoundTime - waveStartTime;

            if(i > spawnPositions.Count)
                break;

            AddSpawn(spawnTime, mob, spawnPositions[i]);
        }
    }

    public void AddSpawn(float time, MobSO mob, Vector3 position)
    {
        var spawnData = new SpawnData(time, mob, position);
        spawnList.Add(spawnData);
        StartCoroutine(SpawnAtTime(spawnData));
    }

    private IEnumerator SpawnAtTime(SpawnData spawnData)
    {
        while (_RoundManager.RoundTime > spawnData.Time)
        {
            if (GameStateManager.Instance.GetGameState() == GameState.Paused )
            {
                yield return null;
            }
            else
            {
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

    private List<Vector3> GetClumpSpawnPositions(int count)
    {

        List<Vector3> spawnPositions = new List<Vector3>();
        Vector3 clumpCenter = GetRandomSpawnPosition();

        for (int i = 0; i < count; i++)
        {
            float spawnRadius = 1 * count;
            Vector3 spawnPosition = new Vector3(
                Random.Range(clumpCenter.x - spawnRadius, clumpCenter.x + spawnRadius),
                Random.Range(clumpCenter.y - spawnRadius, clumpCenter.y + spawnRadius),
                0f
            );
            spawnPositions.Add(spawnPosition);
        }

        return spawnPositions;
    }

}
