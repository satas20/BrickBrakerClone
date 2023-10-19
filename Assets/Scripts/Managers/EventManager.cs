using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
    
    public event EventHandler BallCollision;
    public event EventHandler WaveMove; 
    private void Awake()
    {
        Instance = this;
    }

    public void InvokeWaveMove()
    {
        
        WaveMove?.Invoke(this, EventArgs.Empty);
    }
    public void InvokeBallCollision()
    {
        BallCollision?.Invoke(this, EventArgs.Empty);
    }
}
