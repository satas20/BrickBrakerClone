using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //Uı Panels
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject tapToStartPanel;
    [SerializeField] private GameObject winPanel;


    private void Awake()
    {
        GameManager.OnGameStateChanged+=GameManager_OnGameStateChanged;
    }

    //Setting panels active according to game state.
    private void GameManager_OnGameStateChanged(GameManager.GameState state)
    {
        switch (state)
        {
            case GameManager.GameState.Waiting:
                tapToStartPanel.transform.DOShakePosition(10, 10, 10, 10, false, true);
                gameOverPanel.SetActive(false);
                pausePanel.SetActive(false);
                tapToStartPanel.SetActive(true);
                winPanel.SetActive(false);
                break;
            case GameManager.GameState.Playing:
                gameOverPanel.SetActive(false);
                pausePanel.SetActive(false);
                tapToStartPanel.SetActive(false);
                winPanel.SetActive(false);
                break;
            case GameManager.GameState.GameOver:
                gameOverPanel.transform.DOShakePosition(10, 10, 10, 10, false, true);

                gameOverPanel.SetActive(true);
                pausePanel.SetActive(false);
                tapToStartPanel.SetActive(false);
                winPanel.SetActive(false);
                break;
            case GameManager.GameState.Paused:
                pausePanel.transform.DOShakePosition(10, 10, 10, 10, false, true);

                gameOverPanel.SetActive(false);
                pausePanel.SetActive(true);
                tapToStartPanel.SetActive(false);
                winPanel.SetActive(false);
                break;
            case GameManager.GameState.Win:
                winPanel.transform.DOShakePosition(10, 10, 10, 10, false, true);

                gameOverPanel.SetActive(false);
                pausePanel.SetActive(false);
                tapToStartPanel.SetActive(false);
                winPanel.SetActive(true);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

   

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged-=GameManager_OnGameStateChanged;
    }
}
