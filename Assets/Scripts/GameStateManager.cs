using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Playing,
    Paused,
    InbetweenRounds,
    ChoosingNewAbility,
    GameOver
}
public class GameStateManager : MonoBehaviour
{
    public GameObject _PauseMenuUI;

    public static GameStateManager Instance { get; private set; }

    public GameState CurrentGameState { get; private set; }

    [SerializeField]
    private bool resumeAfterDelay = false;
    [SerializeField]
    private GameObject _YouWinUI;

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

    void Start()
    {
        Application.targetFrameRate = 144;
    }

    public void PauseGame()
    {
        CurrentGameState = GameState.Paused;
        _PauseMenuUI.SetActive(true);
    }

    public void ResumeGame()
    {
        if(resumeAfterDelay)
        {
            StartCoroutine(ResumeAfterDelay(0.5f));
        }
        else
        {
            CurrentGameState = GameState.Playing;
            _PauseMenuUI.SetActive(false);
        }
        
    }

    public void SetGameState(GameState gameState)
    {
        CurrentGameState = gameState;
        if(gameState == GameState.GameOver)
        {
            _YouWinUI.SetActive(true);
        }
    }

    public GameState GetGameState()
    {
        return CurrentGameState;
    }
    
    private IEnumerator ResumeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
    
        CurrentGameState = GameState.Playing;
        _PauseMenuUI.SetActive(false);
    }

    public bool IsPaused()
    {
        return CurrentGameState == GameState.Paused;
    }

}
