using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BallScript : MonoBehaviour
{
    public static BallScript Instance; // Singleton
    public event EventHandler BallCollision; // Event that calls when ball collides with something.
    
    [SerializeField] private float speed=10; // Ball speed.
    [SerializeField] GameObject ballRenderer; // Ball sprite rendered as a child.
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

    private void Awake()
    {
         Instance = this;
         GameManager.OnGameStateChanged+=GameManager_OnGameStateChanged;

    }
    
    private void GameManager_OnGameStateChanged(GameManager.GameState state)
    {
        switch (state)
        {
            case GameManager.GameState.Waiting:
             
                break;
            case GameManager.GameState.Playing:
              Invoke(nameof(SetRandomTrajectory), 1);
              speed = 10;
                break;
            case GameManager.GameState.GameOver:
                loseParticle.Play();
                speed = 0;
                break;
            case GameManager.GameState.Paused:
                speed = 0;
                WobbleBall();
                HighlightBall();
                break;
            case GameManager.GameState.Win :
                ballRenderer.SetActive(false);
                speed = 50;
                ScaleObject();
                winParticle.Play();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    void Start()
    {
        _trailRenderer = GetComponent<TrailRenderer>();
        //Setting values.
        _orginalColor = ballRenderer.GetComponent<SpriteRenderer>().color;
        _originalScale = transform.localScale;
        _rb = GetComponent<Rigidbody2D>();
        //Invoke(nameof(SetRandomTrajectory), 1);
    }

   
    private void FixedUpdate()
    {
        
        _rb.velocity = _rb.velocity.normalized * speed;
        FaceVelocity();
    }
    
    //Adds  random force to the ball called at start of the game. 
    private void SetRandomTrajectory() 
    {
        Vector2 force = new Vector2(Random.Range(-2f, 2f), 1);
        
        _rb.AddForce(force.normalized* speed, ForceMode2D.Impulse);
        transform.parent = null;
    }
    
    //Scale effect for the ball.
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
    
    //Faces the ball to the direction of the velocity vector.
    private void FaceVelocity()
    {
        // Get the velocity vector of the 2D ball
        Vector2 velocity = _rb.velocity;

        // Calculate the target position by adding the velocity to the current position
        Vector3 targetPosition = (Vector3)transform.position + new Vector3(velocity.x, velocity.y, 0);

        // Make the 2D ball face the direction of the velocity vector
        transform.up = targetPosition - transform.position;
    }
    
    //Wobble effect for the ball.
    private void WobbleBall(){
        if (_woobleTween != null && _scaleTween.IsActive())
        {
            _strechTween.Kill();
        }
        _woobleTween = ballRenderer.transform.DOShakeScale(0.4f, 1, 20, 90, true).OnComplete(()=> {

            ballRenderer.transform.DOScale(Vector3.one, 0.1f);
        });
    }
    
    //Highlight effect for the ball.
    private void HighlightBall(){
        if (_colorTween != null && _scaleTween.IsActive())
        {
            _colorTween.Kill();
        }
        _trailRenderer.startColor = Color.white;
        _trailRenderer.endColor = Color.white;

        _colorTween =ballRenderer.GetComponent<SpriteRenderer>().DOColor(Color.white, 0.1f).OnComplete(()=>
        { 
            _trailRenderer.startColor = new Color(1f, 0.56f, 0f);
            _trailRenderer.endColor = new Color(1f, 0.56f, 0f);
    
            ballRenderer.GetComponent<SpriteRenderer>().DOColor(_orginalColor,0.2f);    
        });
        
    }

   //Creating particle and facing it to the ball.
    private void PlantParticle(Collision2D collision)
    {
        // I want to face particle to the collision point.
        Transform particle =Instantiate(smokeParticle.transform, collision.GetContact(0).point, quaternion.identity);
        particle.LookAt(transform);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Brick"))
        {
            GetComponent<BallAudioManager>().PlayPling();
        }
        else
        {
            GetComponent<BallAudioManager>().PlayBallWall();
        }
        //FaceVelocity(); FaceVelocity Called in update 
        //WobbleBall(); Wobble process Starts end of the ScaleObject animation
        ScaleObject();
        HighlightBall();
        PlantParticle(collision);
        if (collision.gameObject.CompareTag("GameOver")&&GameManager.Instance.currentState==GameManager.GameState.Playing)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.GameOver);
            return;
        }
        BallCollision?.Invoke(this,EventArgs.Empty); 
    }
    private void OnDestroy()
    {
        GameManager.OnGameStateChanged-=GameManager_OnGameStateChanged;
    }
}
