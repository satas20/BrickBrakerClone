using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    //Uı Panels
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject tapToStartPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private TextMeshProUGUI paddleHealth;
    [SerializeField] private TextMeshProUGUI laserCount;

    private PaddleScript paddle;
    private void Awake()
    {
        GameManager.OnGameStateChanged+=GameManager_OnGameStateChanged;
     
       
    }

    private void Start()
    {
        EventManager.Instance.PaddleHit+=UpdatePaddleHealth;  
        
        UpdatePaddleHealth(this, EventArgs.Empty);
    }

    private void UpdatePaddleHealth(object sender, EventArgs e)
    {
        
        paddle=FindObjectOfType<PaddleScript>(); 
        laserCount.text = paddle.laserCount.ToString();
        paddleHealth.text = paddle.health.ToString();    
    }

    //Setting panels active according to game state.
    private void GameManager_OnGameStateChanged(GameManager.GameState state)
    {
        if(tapToStartPanel==null){return;}
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
