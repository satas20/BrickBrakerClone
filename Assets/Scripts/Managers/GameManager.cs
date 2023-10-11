using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;
    public GameState currentState;
    public static event Action<GameState> OnGameStateChanged;
    [SerializeField] private GameObject[] bricks;
    public int brickCount;
    public enum GameState
    {
        Waiting,
        Playing,
        GameOver,
        Paused,
        Win,
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        brickCount= bricks.Length;
        UpdateGameState(GameState.Waiting);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            DOTween.KillAll();
            SceneManager.LoadScene("Level1");

        }
        if (brickCount == 0){ 
            UpdateGameState(GameState.Win);
            return; 
        }
        if(currentState==GameManager.GameState.Waiting&& Input.GetMouseButtonDown(0))
        {
            UpdateGameState(GameManager.GameState.Playing);
        }
        if( currentState== GameState.Playing&&Input.GetKeyDown(KeyCode.Escape))
        {
            UpdateGameState(GameManager.GameState.Paused);
        }
        else if( currentState== GameState.Paused&&Input.GetKeyDown(KeyCode.Escape))
        {
            UpdateGameState(GameManager.GameState.Playing);
        }
        
    }

    public void UpdateGameState(GameState nextState)
    {
        Debug.Log("current state : "+ nextState.ToString());
        currentState = nextState;
        switch (currentState)
        {
            case GameState.Waiting:
                HandleWaiting();
                break;  
            case GameState.Playing:
                HandlePlaying();
                break;
            case GameState.GameOver:
                HandleGameOver();
                break;
            case GameState.Paused:
                HandlePaused();
                break;
            case GameState.Win:
                
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        OnGameStateChanged?.Invoke(currentState);
    }

    private void HandleWaiting()
    {
        
        
    }
    private void HandlePlaying()
    {
        
    }
    private void HandleGameOver()
    {
        
    }
    private void HandlePaused()
    {
        
    }
    
    
}
