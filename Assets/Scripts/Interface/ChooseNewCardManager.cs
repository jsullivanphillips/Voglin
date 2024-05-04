using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseNewCardManager : MonoBehaviour
{
    public static ChooseNewCardManager Instance { get; private set; }

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

    [SerializeField]
    GameObject _Levelup_Screen;
    [SerializeField] 
    GameObject _DisplayCardPrefab;
    [SerializeField]
    Transform[] _CardSpots = new Transform[3];

    private List<GameObject> _DisplayCards = new List<GameObject>();

    private void DisplayCards(List<ItemSO> items)
    {
        GameStateManager.Instance.PauseGame();
        _Levelup_Screen.SetActive(true);
        for (int i = 0; i < items.Count; i++)
        {
            GameObject newCard = Instantiate(_DisplayCardPrefab, _CardSpots[i]);
            _DisplayCards.Add(newCard);
            newCard.transform.localPosition = Vector3.zero;
            if(items[i] is ActiveItemSO)
            {
                ActiveItemSO activeItem = items[i] as ActiveItemSO;
                activeItem.cooldown = Random.Range(activeItem.cooldownRange.min, activeItem.cooldownRange.max);
            }
            newCard.GetComponent<DisplayCard>().SetItemSO(items[i]);
        }
    }

    public void DisplayCardsForLevel(int level)
    {
        List<ItemSO> items = new List<ItemSO>();
        switch (level)
        {
            case 0:
                for (int i = 0; i < 3; i++)
                {
                    items.Add(ItemDatabase.Instance.GetRandomPassiveItemAtRarity(0));
                }
                break;
            default:
                for (int i = 0; i < 3; i++)
                {
                    items.Add(ItemDatabase.Instance.GetRandomPassiveItemAtRarity(0));
                }
                break;
        
        }
        DisplayCards(items);
    }

    public void DisplayCardsForRound(int round)
    {
        List<ItemSO> items = new List<ItemSO>();
        switch (round % 3)
        {
            case 0:
                for (int i = 0; i < 3; i++)
                {
                    items.Add(ItemDatabase.Instance.GetRandomActiveItemAtRarity(Rarity.Poor));
                }
                break;
            case 1:
                for (int i = 0; i < 3; i++)
                {
                    items.Add(ItemDatabase.Instance.GetRandomActiveItemAtRarity(Rarity.Poor));
                }
                break;
            case 2:
                for (int i = 0; i < 3; i++)
                {
                    items.Add(ItemDatabase.Instance.GetRandomPassiveItemAtRarity(Rarity.Uncommon));
                }
                break;
            default:
                for (int i = 0; i < 3; i++)
                {
                    items.Add(ItemDatabase.Instance.GetRandomPassiveItemAtRarity(Rarity.Poor));
                }
                break;
        
        }
        DisplayCards(items);
    }

    public void CloseDisplay()
    {
        _Levelup_Screen.SetActive(false);
        foreach (GameObject card in _DisplayCards)
        {
            Destroy(card);
        }
        _DisplayCards.Clear();
    }


}
