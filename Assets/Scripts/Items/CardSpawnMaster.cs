using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawnMaster : MonoBehaviour
{
    private static CardSpawnMaster instance;
    public static CardSpawnMaster Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    [SerializeField]
    private List<ActiveItemSO> activeItems = new List<ActiveItemSO>();

    public ActiveItemSO GetRandomActiveItem(int rank)
    {
        ActiveItemSO activeItem = Instantiate(activeItems[Random.Range(0, activeItems.Count)]);
        activeItem.cooldownTimer = 0f;
        activeItem.cooldown = activeItem.cooldown + Random.Range(-0.5f, 0.5f);
        return activeItem;
    }

    [SerializeField]
    private List<PassiveItemSO> passiveItems = new List<PassiveItemSO>();

    public PassiveItemSO GetRandomPassiveItem(int rank)
    {
        return Instantiate(passiveItems[Random.Range(0, passiveItems.Count)]);
    }
}
