using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Brick : MonoBehaviour
{
    public int health;
    private Tweener _shakeTween;
    private Sequence _jumpTween;

    private Vector3 _originalScale;
    private Vector3 _originalPosition;


    private void Start()
    {
        _originalPosition = transform.position;
        _originalScale = transform.localScale;
        BallScript.instance.ballCollision += BrickShake;
        BallScript.instance.ballCollision += BrickJump;

    }

    
    private void Hit(Collision2D collision)
    {
        health--;
        if (health == 0){
            BrickBreak( collision);
        }    
    
    }
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
    private void BrickBreak(Collision2D collision2D){
        BallScript.instance.ballCollision -= BrickShake;
        BallScript.instance.ballCollision -= BrickJump;
        _shakeTween.Kill();
        _jumpTween.Kill();
        ThrowBrick(collision2D);
        DarkenBrick();
        transform.DOScale(0,1).OnComplete(()=>
        {
            DestroyBrick();
        });
    }

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

    private void DarkenBrick()
    {
        GetComponent<SpriteRenderer>().DOColor(Color.black, 0.85f);
    }
    private void DestroyBrick( ){
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Hit(other);
        }
    }
}
