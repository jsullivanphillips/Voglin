using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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

public class CraftingTableCardManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _CardPrefab;
    [SerializeField]
    private Transform _CardContainer;
    [SerializeField]
    private RectTransform _BoundsBox;
    [SerializeField]
    private RectTransform _SpawnBoundsBox;

    private Dictionary<Guid, Card> _cards = new Dictionary<Guid, Card>();

    // Testing funzies
    public void Create3CardsBtn()
    {
        StartCoroutine(Create3Cards());
    }

    public IEnumerator Create3Cards()
    {
        for (int i = 0; i < 3; i++)
        {
            CreateCard();
            yield return new WaitForSeconds(0.5f);
        }
    }
    // ^ Delete after

    public void CreateCard()
    {
        Vector3 randomPositionWithinBounds = new Vector3(
            Random.Range(_SpawnBoundsBox.rect.xMin, _SpawnBoundsBox.rect.xMax),
            Random.Range(_SpawnBoundsBox.rect.yMin, _SpawnBoundsBox.rect.yMax),
            0f
        );

        randomPositionWithinBounds = _SpawnBoundsBox.TransformPoint(randomPositionWithinBounds);

        GameObject cardObject = Instantiate(_CardPrefab, randomPositionWithinBounds, Quaternion.identity, _CardContainer);
        DraggableCard draggableCardScript = cardObject.GetComponent<DraggableCard>();

        Card card = new Card(cardObject, draggableCardScript);
        _cards.Add(card.Id, card);

       draggableCardScript.boundsBox = _BoundsBox;
       draggableCardScript.SetGuid(card.Id);
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
