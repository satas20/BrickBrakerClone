using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class BallScript : MonoBehaviour
{
    public static BallScript instance;
    //public UnityEvent ballCollision;
    public event EventHandler ballCollision;
    
    [SerializeField] float maxSpeed;
    [SerializeField] float startSpeed;
    [SerializeField] GameObject ballRenderer;

    private float _speed=10;
    private Rigidbody2D rb;

    private Vector3 _originalScale;
    private Color _orginalColor;

    private Tweener _scaleTween;
    private Tweener _strechTween;
    private Tweener _woobleTween;
    private Tweener _colorTween;

    private void Awake()
    {
         instance = this;
    }

    void Start()
    {
        _orginalColor = ballRenderer.GetComponent<SpriteRenderer>().color;
        _originalScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        Invoke(nameof(SetRandomTrajectory), 1);
    }

   
    private void FixedUpdate()
    {
        
        rb.velocity = rb.velocity.normalized * _speed;
        FaceVelocity();
    }
    

    private void SetRandomTrajectory() 
    {
        Vector2 force = new Vector2(Random.Range(-2f, 2f), 1);
        
        rb.AddForce(force.normalized* _speed, ForceMode2D.Impulse);
        transform.parent = null;
    }
    private void ScaleObject(){
        if (_scaleTween != null && _scaleTween.IsActive())
        {
            _scaleTween.Kill();
        }
        _scaleTween =ballRenderer.transform.DOScale(new Vector3(2,3.5f,2), 0.1f).OnComplete(() =>
        {
            ballRenderer.transform.DOScale(Vector3.one, 0.1f).OnComplete(()=> {
                WobbleBall();
            });
        });
    }
    private void FaceVelocity()
    {
        // Get the velocity vector of the 2D ball
        Vector2 velocity = rb.velocity;

        // Calculate the target position by adding the velocity to the current position
        Vector3 targetPosition = (Vector3)transform.position + new Vector3(velocity.x, velocity.y, 0);

        // Make the 2D ball face the direction of the velocity vector
        transform.up = targetPosition - transform.position;
    }
    
    private void WobbleBall(){
        if (_woobleTween != null && _scaleTween.IsActive())
        {
            _strechTween.Kill();
        }
        _woobleTween = ballRenderer.transform.DOShakeScale(0.4f, 1, 20, 90, true).OnComplete(()=> {

            ballRenderer.transform.DOScale(Vector3.one, 0.1f);
        });
    }
    private void HighlightBall(){
        if (_colorTween != null && _scaleTween.IsActive())
        {
            _colorTween.Kill();
        }
        GetComponent<TrailRenderer>().startColor = Color.white;
        GetComponent<TrailRenderer>().endColor = Color.white;

        _colorTween =ballRenderer.GetComponent<SpriteRenderer>().DOColor(Color.white, 0.1f).OnComplete(()=>
        { 
            GetComponent<TrailRenderer>().startColor = new Color(1f, 0.56f, 0f);
            GetComponent<TrailRenderer>().endColor = new Color(1f, 0.56f, 0f);
    
            ballRenderer.GetComponent<SpriteRenderer>().DOColor(_orginalColor,0.2f);    
        });
        
    }

    private void ScreenShake(){ }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //FaceVelocity();FaceVelocity Called in update 
        //WobbleBall(); Wobble process Starts end of the ScaleObject animation
        ScaleObject();
        HighlightBall();
        ballCollision?.Invoke(this,EventArgs.Empty);
    }
}
