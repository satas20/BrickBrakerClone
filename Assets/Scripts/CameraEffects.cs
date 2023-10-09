using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class CameraEffects : MonoBehaviour
{
    [SerializeField] float strenght;
    
    [SerializeField] float duration;
    private Vector3 _originalPosition;

    private void Start()
    {
        _originalPosition = transform.position;
        BallScript.instance.ballCollision += ScreenShake;
    }

    private void ScreenShake(object sender, EventArgs e)
    {
        transform.DOShakePosition(duration, strenght).OnComplete(()=>transform.position=_originalPosition);
        
    }

    

}
