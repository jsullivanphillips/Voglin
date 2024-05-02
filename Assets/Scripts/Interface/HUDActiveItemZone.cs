using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDActiveItemZone : MonoBehaviour
{
    public static HUDActiveItemZone Instance { get; private set; }

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

    Dictionary<int, ActiveCard> activeCards = new Dictionary<int, ActiveCard>();
    [SerializeField]
    Transform _ActiveItemZone;
    [SerializeField]
    GameObject _ActiveCardPrefab;


    public void AddActiveItem(ActiveItemSO activeItem, int id)
    {
        Debug.Log("Adding active item to HUD");
        // Add active item to HUD
        GameObject activeCard = Instantiate(_ActiveCardPrefab, _ActiveItemZone);
        ActiveCard activeCardComponent = activeCard.GetComponent<ActiveCard>();
        activeCardComponent.SetId(id);
        activeCardComponent.SetActiveItemSO(activeItem);
        activeCardComponent.SetIsDraggable(false);
        activeCardComponent.isInRack = true;
        activeCards[id] = activeCardComponent;
    }

    public void RemoveActiveItem(int id)
    {
        Debug.Log("Removing active item from HUD");
        // Remove active item from HUD
        Destroy(activeCards[id].gameObject);
        activeCards.Remove(id);
    }

    public void UpdateCooldown(int id, float cooldownPercentage)
    {
        activeCards[id].SetCooldownPercentage(cooldownPercentage);
    }
}
