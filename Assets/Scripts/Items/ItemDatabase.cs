using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance { get; private set; }

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
        foreach (ItemSO item in items)
        {
            switch (item.rarity)
            {
                case Rarity.Poor:
                    PoorItems.Add(item);
                    break;
                case Rarity.Common:
                    CommonItems.Add(item);
                    break;
                case Rarity.Uncommon:
                    UncommonItems.Add(item);
                    break;
                case Rarity.Rare:
                    RareItems.Add(item);
                    break;
                case Rarity.Epic:
                    EpicItems.Add(item);
                    break;
                case Rarity.Legendary:
                    LegendaryItems.Add(item);
                    break;
            }
        }
    }

    public List<ItemSO> items = new List<ItemSO>();
    private List<ItemSO> PoorItems = new List<ItemSO>();
    private List<ItemSO> CommonItems = new List<ItemSO>();
    private List<ItemSO> UncommonItems = new List<ItemSO>();
    private List<ItemSO> RareItems = new List<ItemSO>();
    private List<ItemSO> EpicItems = new List<ItemSO>();
    private List<ItemSO> LegendaryItems = new List<ItemSO>();

    public ItemSO GetRandomItemAtRarity(Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.Poor:
                return Instantiate(PoorItems[Random.Range(0, PoorItems.Count)]);
            case Rarity.Common:
                return Instantiate(CommonItems[Random.Range(0, CommonItems.Count)]);
            case Rarity.Uncommon:
                return Instantiate(UncommonItems[Random.Range(0, UncommonItems.Count)]);
            case Rarity.Rare:
                return Instantiate(RareItems[Random.Range(0, RareItems.Count)]);
            case Rarity.Epic:
                return Instantiate(EpicItems[Random.Range(0, EpicItems.Count)]);
            case Rarity.Legendary:
                return Instantiate(LegendaryItems[Random.Range(0, LegendaryItems.Count)]);
            default:
                return null;
        }
    }

    public ItemSO GetRandomActiveItemAtRarity(Rarity rarity)
    {
        ItemSO item = GetRandomItemAtRarity(rarity);
        if (item.cardType == CardType.Active)
        {
            return Instantiate(item);
        }
        else
        {
            return GetRandomActiveItemAtRarity(rarity);
        }
    }

    public ItemSO GetRandomPassiveItemAtRarity(Rarity rarity)
    {
        ItemSO item = GetRandomItemAtRarity(rarity);
        if (item.cardType == CardType.Passive)
        {
            return Instantiate(item);
        }
        else
        {
            return GetRandomPassiveItemAtRarity(rarity);
        }
    }
   
}
