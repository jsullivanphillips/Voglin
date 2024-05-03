using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// This file is responsible for managing the cards and items on the crafting table.


public class Card
{
    public int Id { get; private set; }
    public GameObject _gameObject { get; private set; }
    public DraggableCard _draggableCard { get; private set; }

    public Card(GameObject gameObject, DraggableCard draggableCard)
    {
        Id = Random.Range(0, int.MaxValue);
        _gameObject = gameObject;
        _draggableCard = draggableCard;
        // Initialize other card properties...
    }
}

public class CraftingTableItemManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _ActiveCardPrefab;
    [SerializeField]
    private GameObject _PassiveCardPrefab;
    [SerializeField]
    private GameObject _OrbPrefab;
    [SerializeField]
    private Transform _ItemMat;
    [SerializeField]
    private RectTransform _BoundsBox;
    [SerializeField]
    private RectTransform _SpawnBoundsBox;
    [SerializeField]
    private Rect _SpawnBoundsDefault;
    [SerializeField]
    private RectTransform _DisposalBoundsBox;

    private Dictionary<int, Card> _cards = new Dictionary<int, Card>();

    private Dictionary<int, ActiveCard> _activeCards = new Dictionary<int, ActiveCard>();

    public static CraftingTableItemManager Instance { get; private set; }

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

    public void SpawnActiveCard(ActiveItemSO newActiveItemSO)
    {
        Vector3 position = GetRandomPositionOnMat();
        GameObject cardObject = Instantiate(_ActiveCardPrefab, position, Quaternion.identity, _ItemMat);
        ActiveCard activeCardScript = cardObject.GetComponent<ActiveCard>();

        Card card = new Card(cardObject, activeCardScript);
        _cards.Add(card.Id, card);
        _activeCards.Add(card.Id, activeCardScript);

       activeCardScript.boundsBox = _BoundsBox;
       activeCardScript.SetId(card.Id);
       activeCardScript.itemMat = _ItemMat;
       activeCardScript.SetCardType(CardType.Active);
       activeCardScript.SetItemSO(newActiveItemSO);

       activeCardScript.SetActiveItemSO(newActiveItemSO);
    }

    public void SpawnActiveCard(ActiveItemSO newActiveItemSO, Vector3 position)
    {
        GameObject cardObject = Instantiate(_ActiveCardPrefab, position, Quaternion.identity, _ItemMat);
        ActiveCard activeCardScript = cardObject.GetComponent<ActiveCard>();

        Card card = new Card(cardObject, activeCardScript);
        _cards.Add(card.Id, card);
        _activeCards.Add(card.Id, activeCardScript);
        Debug.Log("NewCard Id: " + card.Id);

       activeCardScript.boundsBox = _BoundsBox;
       activeCardScript.SetId(card.Id);
       activeCardScript.itemMat = _ItemMat;
       activeCardScript.SetCardType(CardType.Active);
       activeCardScript.SetItemSO(newActiveItemSO);

       activeCardScript.SetActiveItemSO(newActiveItemSO);
    }

    public void SpawnAndEquipActiveCard(ActiveItemSO newActiveItemSO)
    {
        Vector3 position = GetRandomPositionOnMat();
        GameObject cardObject = Instantiate(_ActiveCardPrefab, position, Quaternion.identity, _ItemMat);
        ActiveCard activeCardScript = cardObject.GetComponent<ActiveCard>();

        Card card = new Card(cardObject, activeCardScript);
        _cards.Add(card.Id, card);
        _activeCards.Add(card.Id, activeCardScript);

       activeCardScript.boundsBox = _BoundsBox;
       activeCardScript.SetId(card.Id);
       activeCardScript.itemMat = _ItemMat;
       activeCardScript.SetCardType(CardType.Active);
       activeCardScript.SetItemSO(newActiveItemSO);

       activeCardScript.SetActiveItemSO(newActiveItemSO);
       activeCardScript.EquipCard();
    }

    public void SpawnPassiveCard(PassiveItemSO newPassiveItemSO)
    {
        Vector3 position = GetRandomPositionOnMat();
        GameObject cardObject = Instantiate(_PassiveCardPrefab, position, Quaternion.identity, _ItemMat);
        PassiveCard passiveCardScript = cardObject.GetComponent<PassiveCard>();

        Card card = new Card(cardObject, passiveCardScript);
        _cards.Add(card.Id, card);

        passiveCardScript.boundsBox = _BoundsBox;
        passiveCardScript.SetId(card.Id);
        passiveCardScript.itemMat = _ItemMat;
        passiveCardScript.SetCardType(CardType.Passive);
        passiveCardScript.SetItemSO(newPassiveItemSO);

        passiveCardScript.SetPassiveItemSO(newPassiveItemSO);
    }

    public void SpawnPassiveCard(PassiveItemSO newPassiveItemSO, Vector3 position)
    {
        GameObject cardObject = Instantiate(_PassiveCardPrefab, position, Quaternion.identity, _ItemMat);
        PassiveCard passiveCardScript = cardObject.GetComponent<PassiveCard>();

        Card card = new Card(cardObject, passiveCardScript);
        _cards.Add(card.Id, card);

        passiveCardScript.boundsBox = _BoundsBox;
        passiveCardScript.SetId(card.Id);
        passiveCardScript.itemMat = _ItemMat;
        passiveCardScript.SetCardType(CardType.Passive);
        passiveCardScript.SetItemSO(newPassiveItemSO);

        passiveCardScript.SetPassiveItemSO(newPassiveItemSO);
    }

    public void SpawnAndEquipPassiveCard(PassiveItemSO newPassiveItemSO)
    {
        Vector3 position = GetRandomPositionOnMat();
        GameObject cardObject = Instantiate(_PassiveCardPrefab, position, Quaternion.identity, _ItemMat);
        PassiveCard passiveCardScript = cardObject.GetComponent<PassiveCard>();

        Card card = new Card(cardObject, passiveCardScript);
        _cards.Add(card.Id, card);

        passiveCardScript.boundsBox = _BoundsBox;
        passiveCardScript.SetId(card.Id);
        passiveCardScript.itemMat = _ItemMat;
        passiveCardScript.SetCardType(CardType.Passive);
        passiveCardScript.SetItemSO(newPassiveItemSO);

        passiveCardScript.SetPassiveItemSO(newPassiveItemSO);
        passiveCardScript.EquipCard();
    }

    public void CreateOrb()
    {
        if(!_SpawnBoundsBox.gameObject.activeInHierarchy)
        {
            Vector3 randomPositionWithinBounds = new Vector3(
                    Random.Range( -_SpawnBoundsDefault.xMax / 2, _SpawnBoundsDefault.xMax / 2),
                    Random.Range(- _SpawnBoundsDefault.yMax / 2, _SpawnBoundsDefault.yMax / 2),
                    0f
                );
            
            
            GameObject orbObject = Instantiate(_OrbPrefab, randomPositionWithinBounds, Quaternion.identity);
            orbObject.transform.SetParent(_ItemMat, false);
            DraggableOrb draggableOrbScript = orbObject.GetComponent<DraggableOrb>();
        
            draggableOrbScript.boundsBox = _BoundsBox;
            draggableOrbScript.itemMat = _ItemMat;
        }
        else
        {
            Vector3 randomPositionWithinBounds = GetRandomPositionOnMat();
            
            GameObject orbObject = Instantiate(_OrbPrefab, randomPositionWithinBounds, Quaternion.identity, _ItemMat);
            DraggableOrb draggableOrbScript = orbObject.GetComponent<DraggableOrb>();
        
            draggableOrbScript.boundsBox = _BoundsBox;
            draggableOrbScript.itemMat = _ItemMat;
        }
    
       
    }

    private Vector3 GetRandomPositionOnMat()
    {
        Vector3 randomPositionWithinBounds = new Vector3(
            Random.Range(_SpawnBoundsBox.rect.xMin, _SpawnBoundsBox.rect.xMax),
            Random.Range(_SpawnBoundsBox.rect.yMin, _SpawnBoundsBox.rect.yMax),
            0f
        );
        randomPositionWithinBounds = _SpawnBoundsBox.TransformPoint(randomPositionWithinBounds);
        
        return randomPositionWithinBounds;
    }

    public void UpdateCooldown(int id, float cooldownPercentage)
    {
        _activeCards[id].SetCooldownPercentage(cooldownPercentage);
    }

    public void RemoveCard(int id)
    {
        if (_cards.ContainsKey(id))
        {
            Card card = _cards[id];
            _cards.Remove(id);
            Destroy(card._gameObject);
        }
    }
}
