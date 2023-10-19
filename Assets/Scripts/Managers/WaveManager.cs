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

    public IEnumerator MoveDown()
    {
        while (true)
        {
            yield return new WaitForSeconds(downDuration);
            EventManager.Instance.InvokeWaveMove();
            foreach (GameObject brick in GameManager.Instance.bricks)
            {
                brick.transform.DOLocalMoveY(brick.transform.localPosition.y - 1.5f, 0.6f, false).SetEase(Ease.InOutQuad);
            }
            
            
            
            
        }
    }
}
