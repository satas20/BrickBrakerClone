using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class BallAnims : MonoBehaviour
{
    [SerializeField] private GameObject smokeParticle;
    [SerializeField] private ParticleSystem winParticle;
    [SerializeField] private ParticleSystem loseParticle;

    private Rigidbody2D _rb;
    private Vector3 _originalScale;
    private Color _orginalColor;

    //Animation Tweens.
    private Tweener _scaleTween;
    private Tweener _strechTween;
    private Tweener _woobleTween;
    private Tweener _colorTween;
    private TrailRenderer _trailRenderer;


    private void Start()
    {
        _trailRenderer = GetComponent<TrailRenderer>();
        //Setting values.
        _orginalColor = GetComponent<SpriteRenderer>().color;
        _originalScale = transform.localScale;
        _rb = GetComponent<Rigidbody2D>();
    }
    
    
    //Scales the ball effect.
    public void ScaleObject(){
        if (_scaleTween != null && _scaleTween.IsActive())
        {
            _scaleTween.Kill();
        }
        _scaleTween =transform.DOScale(new Vector3(2,3.5f,2), 0.1f).OnComplete(() =>
        {
            transform.DOScale(Vector3.one, 0.1f).OnComplete(()=> {
                WobbleBall();
            });
        });
    }
    
    //Wobble effect for the ball.
    public void WobbleBall(){
        if (_woobleTween != null && _scaleTween.IsActive())
        {
            _strechTween.Kill();
        }
        _woobleTween = transform.DOShakeScale(0.4f, 1, 20, 90, true).OnComplete(()=> {

            transform.DOScale(Vector3.one, 0.1f);
        });
    }
    
    //Highlight effect for the ball.
    public void HighlightBall(){
        if (_colorTween != null && _scaleTween.IsActive())
        {
            _colorTween.Kill();
        }
        _trailRenderer.startColor = Color.white;
        _trailRenderer.endColor = Color.white;

        _colorTween =GetComponent<SpriteRenderer>().DOColor(Color.white, 0.1f).OnComplete(()=>
        { 
            _trailRenderer.startColor = new Color(1f, 0.56f, 0f);
            _trailRenderer.endColor = new Color(1f, 0.56f, 0f);
    
            GetComponent<SpriteRenderer>().DOColor(_orginalColor,0.2f);    
        });
        
    }
    
    //Creating particle and facing it to the ball.
    
    public void PlantParticle(Collision2D collision)
    {
        // I want to face particle to the collision point.
        Transform particle =Instantiate(smokeParticle.transform, collision.GetContact(0).point, quaternion.identity);
        particle.LookAt(transform);
    }
    public void PlayWinParticle()
    {
        winParticle.Play();
    }
    public void PlayLoseParticle()
    {
        loseParticle.Play();
    }
    
}
