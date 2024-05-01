using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// This file is responsible for managing the cards and items on the crafting table.


public class Card
{
    public Guid Id { get; private set; }
    public GameObject _gameObject { get; private set; }
    public DraggableCard _draggableCard { get; private set; }

    public Card(GameObject gameObject, DraggableCard draggableCard)
    {
        Id = Guid.NewGuid();
        _gameObject = gameObject;
        _draggableCard = draggableCard;
        // Initialize other card properties...
    }
}

public class CraftingTableItemManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _CardPrefab;
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

    private Dictionary<Guid, Card> _cards = new Dictionary<Guid, Card>();

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

    // Testing funzies
    public void Create3CardsBtn()
    {
        StartCoroutine(Create3Cards());
    }

    public IEnumerator Create3Cards()
    {
        Vector3 randomPositionWithinBounds = new Vector3(
            Random.Range(_SpawnBoundsBox.rect.xMin, _SpawnBoundsBox.rect.xMax),
            Random.Range(_SpawnBoundsBox.rect.yMin, _SpawnBoundsBox.rect.yMax),
            0f
        );

        randomPositionWithinBounds = _SpawnBoundsBox.TransformPoint(randomPositionWithinBounds);

        for (int i = 0; i < 3; i++)
        {
            CreateCard(randomPositionWithinBounds + (i * Vector3.right * 100f));
            yield return new WaitForSeconds(0.1f);
        }
    }
    // ^ Delete after

    public void CreateCard(Vector2 position)
    {
        GameObject cardObject = Instantiate(_CardPrefab, position, Quaternion.identity, _ItemMat);
        DraggableCard draggableCardScript = cardObject.GetComponent<DraggableCard>();

        Card card = new Card(cardObject, draggableCardScript);
        _cards.Add(card.Id, card);

       draggableCardScript.boundsBox = _BoundsBox;
       draggableCardScript.SetGuid(card.Id);
    }

    public void CreateCard()
    {
        Vector3 randomPositionWithinBounds = new Vector3(
            Random.Range(_SpawnBoundsBox.rect.xMin, _SpawnBoundsBox.rect.xMax),
            Random.Range(_SpawnBoundsBox.rect.yMin, _SpawnBoundsBox.rect.yMax),
            0f
        );

        randomPositionWithinBounds = _SpawnBoundsBox.TransformPoint(randomPositionWithinBounds);

        GameObject cardObject = Instantiate(_CardPrefab, randomPositionWithinBounds, Quaternion.identity, _ItemMat);
        DraggableCard draggableCardScript = cardObject.GetComponent<DraggableCard>();

        Card card = new Card(cardObject, draggableCardScript);
        _cards.Add(card.Id, card);

       draggableCardScript.boundsBox = _BoundsBox;
       draggableCardScript.SetGuid(card.Id);
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
        }
        else
        {
            Vector3 randomPositionWithinBounds = new Vector3(
                Random.Range(_SpawnBoundsBox.rect.xMin, _SpawnBoundsBox.rect.xMax),
                Random.Range(_SpawnBoundsBox.rect.yMin, _SpawnBoundsBox.rect.yMax),
                0f
            );
            randomPositionWithinBounds = _SpawnBoundsBox.TransformPoint(randomPositionWithinBounds);
            
            GameObject orbObject = Instantiate(_OrbPrefab, randomPositionWithinBounds, Quaternion.identity, _ItemMat);
            DraggableOrb draggableOrbScript = orbObject.GetComponent<DraggableOrb>();
        
            draggableOrbScript.boundsBox = _BoundsBox;
        }
    
       
    }

    public void RemoveCard(Guid cardId)
    {
        if (_cards.ContainsKey(cardId))
        {
            Card card = _cards[cardId];
            _cards.Remove(cardId);
            Destroy(card._gameObject);
        }
    }
}
