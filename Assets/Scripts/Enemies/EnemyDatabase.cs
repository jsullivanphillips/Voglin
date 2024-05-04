using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDatabase : MonoBehaviour
{
    public static EnemyDatabase Instance { get; private set; }

    public List<MobSO> mobs = new List<MobSO>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public MobSO GetMobByName(string mobName)
    {
        foreach (MobSO mob in mobs)
        {
            if (mob.mobName == mobName)
            {
                return Instantiate(mob);
            }
        }
        return null;
    }
}
