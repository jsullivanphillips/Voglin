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

    private void DisplayCards(List<ComponentSO> components)
    {
        GameStateManager.Instance.PauseGame();
        _Levelup_Screen.SetActive(true);
        for (int i = 0; i < components.Count; i++)
        {
            GameObject newCard = Instantiate(_DisplayCardPrefab, _CardSpots[i]);
            _DisplayCards.Add(newCard);
            newCard.transform.localPosition = Vector3.zero;
            newCard.transform.localScale = new Vector3(2f,2f,1f);
            newCard.GetComponent<DisplayCard>().SetComponentSO(components[i]);
        }
    }

    public void DisplayCardsForRound(int round)
    {
        List<ComponentSO> items = new List<ComponentSO>();
        switch (round % 3)
        {
            default:
                for (int i = 0; i < 3; i++)
                {
                    items.Add(ItemDatabase.Instance.GetRandomComponent());
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
