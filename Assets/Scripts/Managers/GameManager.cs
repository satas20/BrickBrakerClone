using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEditor.SceneManagement;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;
    public GameState currentState;
    public static event Action<GameState> OnGameStateChanged;
    public List<GameObject> bricks;
    public List<GameObject> balls;

    [Header("PowerUps")] 
    [SerializeField] private GameObject[] powerUpPrefs;
    [SerializeField] private int[] probabilites;
    
    
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
        bricks = new List<GameObject>();
    }

    private void Start()
    {
        UpdateGameState(GameState.Waiting);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            DOTween.KillAll();
            string currentScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentScene);

        }
        if (bricks.Count == 0){ 
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

    public void SpawnPowerUp(Vector3 target)
    {
        int random1 = Random.Range(0, powerUpPrefs.Length);
        int random2=  Random.Range(0, 100);

        if (random2 < probabilites[random1])
        {
            Instantiate(powerUpPrefs[random1],target,quaternion.identity);
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
