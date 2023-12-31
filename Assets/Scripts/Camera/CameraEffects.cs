using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.Serialization;

public class CameraEffects : MonoBehaviour
{
    //Camera shake variables.
    [SerializeField] private float strength;
    [SerializeField] private float duration;
    
    
    private Vector3 _originalPosition;

    private void Start()
    {
        //Setting values.
        _originalPosition = transform.position;
        
        //Subscribing to ballCollision  event.
        BallScript.Instance.BallCollision += ScreenShake; 
        //transform.DOJump( new Vector3(0,3.75f,-10),1 ,1,0.5f).SetEase(Ease.InBack);
    }

    //Shakes the camera.
    private void ScreenShake(object sender, EventArgs e)
    {
        transform.DOShakePosition(duration, strength).OnComplete(()=>transform.position=_originalPosition);
        
    }

    

}
