using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private float score=0f;
    

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.currentState == GameManager.GameState.Playing)
        {
            score += Time.deltaTime;
            
        }

        WaveManager.Instance.waveNum = score switch
        {
            < 20 => 1,
            < 40 => 2,
            _ => 3
        };
        scoreText.text = score.ToString("0");
    }
}
