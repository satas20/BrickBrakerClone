using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BrickScript : MonoBehaviour
{
    //Brick variables.
    private int _health;
    private Vector3 _originalScale;
    private Vector3 _originalPosition;
    
    //Animation Tweens.    
    private Tweener _shakeTween;
    private Sequence _jumpTween;



    private void Start()
    {
        //Setting values for the brick.
        _originalPosition = transform.position;
        _originalScale = transform.localScale;
        
        
        //Subscribing to ballCollision  event.
        BallScript.Instance.BallCollision += BrickShake;
        BallScript.Instance.BallCollision += BrickJump;

    }

    //Reduces the health of the brick and calls BrakeBrick it if health is 0.
    private void Hit(Collision2D collision)
    {
        _health--;
        if (_health == 0){
            BrickBreak( collision);
        }    
    
    }
    
    //Makes Brick a little jump.
    private void BrickJump(object sender, EventArgs e)
    {
       
        if (_jumpTween != null && _jumpTween.IsActive())
        {
            _jumpTween.Kill();
        }

        _jumpTween = transform.DOJump(transform.position, 0.2f, 1, 0.2f, false).OnComplete(() =>
        {
            transform.DOMove(_originalPosition, 0.1f);
        });
    }
    
    //Shakes the brick.
    private void BrickShake(object sender, EventArgs e)
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
    
    //Throws and rotates the brick in the direction of the ball using rigidbody physics collision point.
    private void ThrowBrick(Collision2D collision2D)
    {
        
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 5;
        rb.isKinematic = false;
        Vector2 force = transform.position - collision2D.transform.position;
        
        rb.AddForce(force.normalized * 10, ForceMode2D.Impulse);
        rb.AddTorque(force.x, ForceMode2D.Impulse);
        GetComponent<BoxCollider2D>().isTrigger = true;
    }
    
    //Darkens the brick color to black.
    private void DarkenBrick()
    {
        GetComponent<SpriteRenderer>().DOColor(Color.black, 0.85f);
    }
    
    //Kills the animations, calls the Bricks braking animations and destroys when completed.
    private void BrickBreak(Collision2D collision2D){
        BallScript.Instance.BallCollision -= BrickShake;
        BallScript.Instance.BallCollision -= BrickJump;
        _shakeTween.Kill();
        _jumpTween.Kill();
        ThrowBrick(collision2D);
        DarkenBrick();
        transform.DOScale(0,1).OnComplete(()=>
        {
            Destroy(gameObject);
        });
    }
    
    //Checks if the collision is with the ball and calls Hit.
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Hit(other);
        }
    }
}
