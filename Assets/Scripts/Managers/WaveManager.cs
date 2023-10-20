using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;
    [SerializeField] private float downDuration;

    private void Awake()
    {
        Instance= this;
    }

    void Start()
    {
        //StartCoroutine(MoveDown());
    }

   
}
