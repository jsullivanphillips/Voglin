using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDPassiveItemZone : MonoBehaviour
{
    public static HUDPassiveItemZone Instance { get; private set; }

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

    Dictionary<int, PassiveCard> passiveCards = new Dictionary<int, PassiveCard>();
    [SerializeField]
    Transform _PassiveItemZone;
    [SerializeField]
    GameObject _PassiveCardPrefab;

    public void AddPassiveItem(PassiveItemSO passiveItem, int id)
    {
        Debug.Log("Adding passive item to HUD");
        // Add passive item to HUD
        GameObject passiveCard = Instantiate(_PassiveCardPrefab, _PassiveItemZone);
        passiveCard.transform.localScale = new Vector3(0.75f, 0.75f, 1);
        PassiveCard passiveCardComponent = passiveCard.GetComponent<PassiveCard>();
        passiveCardComponent.SetId(id);
        passiveCardComponent.SetPassiveItemSO(passiveItem);
        passiveCardComponent.SetIsDraggable(false);
        passiveCardComponent.isInRack = true;
        passiveCards[id] = passiveCardComponent;
    }

    public void RemovePassiveItem(int id)
    {
        Debug.Log("Removing passive item from HUD");
        // Remove passive item from HUD
        Destroy(passiveCards[id].gameObject);
        passiveCards.Remove(id);
    }
}
