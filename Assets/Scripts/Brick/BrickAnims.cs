using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using System;

public class BrickAnims : MonoBehaviour
{
    
    //Brick variables.
    private Vector3 _originalScale;
    private Vector3 _originalPosition;
    
    //Animation Tweens.    
    private Tweener _shakeTween;
    private Sequence _jumpTween;
    
   
    
    
    private void Start()
    {
        //Setting values for the brick.
       
        _originalScale = transform.localScale;
        
        
        //Subscribing to ballCollision  event.
        EventManager.Instance.BallCollision += BrickShake;
        EventManager.Instance.BallCollision += BrickJump;


        //EventManager.Instance.WaveMove += KillTweens;

    }

    private void KillTweens(object sender, EventArgs e)
    {
        KillTweens();
    }


    public void KillTweens()
    {
        
        _shakeTween.Kill();
        _jumpTween.Kill();
        
    }
    public void UnSub()
    {
        
        EventManager.Instance.BallCollision -= BrickShake;
        EventManager.Instance.BallCollision -= BrickJump;
        
        
        //EventManager.Instance.WaveMove -= KillTweens;
        
    }
    
    //Makes Brick a little jump.
    public void BrickJump(object sender, EventArgs e)
    {
       
        if (_jumpTween != null && _jumpTween.IsActive())
        {
            transform.localPosition = Vector3.zero;
            _jumpTween.Kill();
        }

        _jumpTween = transform.DOJump(transform.position, 0.2f, 1, 0.2f, false).OnComplete(() =>
        {
            transform.DOMove(transform.parent.position, 0.1f);
        });
    }
    
    //Shakes the brick.
    public void BrickShake(object sender, EventArgs e)
    {
       
        if (_shakeTween != null && _shakeTween.IsActive())
        {
            _shakeTween.Kill();
        }
        _shakeTween=transform.DOShakeScale(0.2f,0.1f,20,90,true).OnComplete(() =>
        {
            transform.DOScale(_originalScale, 0.1f);
        });
    }
    
    //Darkens the brick color to black.
    public void DarkenBrick()
    {
        GetComponent<SpriteRenderer>().DOColor(Color.black, 0.5f);
    }
    //Throws and rotates the brick in the direction of the ball using rigidbody physics collision point.
    public void ThrowBrick(Collision2D collision2D)
    {
        
        Rigidbody2D rb = transform.parent.GetComponent<Rigidbody2D>();
        transform.parent.GetComponent<BoxCollider2D>().isTrigger = true;
        rb.gravityScale = 1;
        rb.isKinematic = false;
        Vector2 force = transform.parent.transform.position - collision2D.transform.position;
        rb.AddForce(force.normalized * 8, ForceMode2D.Impulse);
        rb.AddTorque(force.x*3, ForceMode2D.Impulse);
       
    }
    
    
    

   

   
}
