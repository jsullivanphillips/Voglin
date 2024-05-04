using System;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;

public class DraggableOrb : DraggableObject
{
    Rarity rarity = Rarity.Poor;

    public void SetRarity(Rarity rarity)
    {
        this.rarity = rarity;
    }

    public Rarity GetRarity()
    {
        return rarity;
    }
}
