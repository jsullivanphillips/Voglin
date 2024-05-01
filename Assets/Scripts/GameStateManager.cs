using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Playing,
    Paused
}
public class GameStateManager : MonoBehaviour
{
    public GameObject _PauseMenuUI;

    public static GameStateManager Instance { get; private set; }

    public GameState CurrentGameState { get; private set; }

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (CurrentGameState == GameState.Playing) 
            { 
                PauseGame(); 
            }
            else if (CurrentGameState == GameState.Paused) 
            {
                ResumeGame(); 
            } 
        }
    }

    private void Start()
    {
        CurrentGameState = GameState.Playing;
    }

    public void PauseGame()
    {
        CurrentGameState = GameState.Paused;
        _PauseMenuUI.SetActive(true);
    }

    public void ResumeGame()
    {
        CurrentGameState = GameState.Playing;
        _PauseMenuUI.SetActive(false);
    }

    public bool IsPaused()
    {
        return CurrentGameState == GameState.Paused;
    }

}
