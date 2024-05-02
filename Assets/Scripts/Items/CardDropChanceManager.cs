using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardDropChanceManager : MonoBehaviour
{
    public static CardDropChanceManager Instance { get; private set; }

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

    public List<Tuple<CardType, int>> GetOrbDrops(int rank)
    {
        List<Tuple<CardType, int>> drops = new List<Tuple<CardType, int>>();
        switch (rank)
        {
            case 1:
                for (int i = 0; i < 2; i ++)
                {
                    if(Random.Range(0, 100) < 20)
                        drops.Add(new Tuple<CardType, int>(CardType.Active, 1));
                    else
                        drops.Add(new Tuple<CardType, int>(CardType.Passive, 1));
                }
                break;
            default:
                drops.Add(new Tuple<CardType, int>(CardType.Passive, 1));
                break;
        }
        return drops;
    }

}
