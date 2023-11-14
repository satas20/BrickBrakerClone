using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void loadLevel1()
    {
        Debug.Log("replay");
        SceneManager.LoadScene("Level1");
    }
    public void loadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void pause()
    {
        if(GameManager.Instance.currentState==GameManager.GameState.Playing)
            GameManager.Instance.UpdateGameState(GameManager.GameState.Paused);
        else if (GameManager.Instance.currentState == GameManager.GameState.Paused)
            GameManager.Instance.UpdateGameState(GameManager.GameState.Playing);
    }

    public void mute()
    {
        if (AudioListener.volume > 0)
            AudioListener.volume = 0;
        else
        {
            AudioListener.volume = 1;
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
    
}
