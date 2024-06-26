using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundManager : MonoBehaviour
{
    private int round = 0;
    [SerializeField]
    MobSpawner _MobSpawner;

    [SerializeField]
    private TMP_Text _timerText;

    [SerializeField]
    private List<RoundSO> _Rounds;

    private float _roundTime = 30f;

    public float _MaxRoundTime = 35f;

    public float RoundTime
    {
        get { return _roundTime; }
        private set { _roundTime = value; }
    }

    private void Start()
    {
        _roundTime = _MaxRoundTime;
        DoRound(round);
    }

    private void DoRound(int round)
    {
        // If there are no more rounds, Game Over!
        if(round >= _Rounds.Count)
        {
            GameStateManager.Instance.SetGameState(GameState.GameOver);
            return;
        }

        // Display 3 new items to choose from
        ChooseNewCardManager.Instance.DisplayItemsRewards();

        _MobSpawner.SetRoundSO(Instantiate(_Rounds[round]), round);
    }

    public void RoundComplete()
    {
        round++;
        _MobSpawner.ClearSpawns();
        DoRound(round);
    }

    void Update()
    {
        if (GameStateManager.Instance.GetGameState() == GameState.Paused)
        {
            return;
        }
        _roundTime -= Time.deltaTime;
        _timerText.text = "Round: " + (round + 1).ToString() + " \nTime: " + Mathf.Round(_roundTime).ToString();
        if(_roundTime <= 0)
        {
            _roundTime = _MaxRoundTime;
            RoundComplete();
        }
    }
}
