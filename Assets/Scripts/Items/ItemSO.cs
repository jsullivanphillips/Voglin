using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Rarity
{
    Poor,
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}
public class ItemSO : ScriptableObject
{
    public Sprite itemSprite;
    public string itemName;
    public Rarity rarity;
    public int id;
    public CardType cardType;
}
        