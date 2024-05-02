using System;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;

public class DraggableOrb : DraggableObject
{
    int rank = 1;

    public void SetRank(int _rank)
    {
        rank = _rank;
    }

    public int GetRank()
    {
        return rank;
    }
}
