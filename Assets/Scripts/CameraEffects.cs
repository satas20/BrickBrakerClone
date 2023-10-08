using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class CameraEffects : MonoBehaviour
{
    [SerializeField] float strenght;
    
    [SerializeField] float duration;


    public void ScreenShake( )
    {
        transform.DOShakePosition(duration, strenght);
        transform.DOShakePosition(duration, strenght);

    }

}
