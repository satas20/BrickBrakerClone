using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class BallScript : MonoBehaviour
{
   
    [SerializeField] private float speed=10; // Ball speed.
    private float currentSpeed;
    [SerializeField] private BallAnims _ballAnims;
    //[SerializeField] GameObject ballRenderer; // Ball sprite rendered as a child.
    
    private Rigidbody2D _rb;
    
  

   

    private void Awake()
    {
         
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
              currentSpeed = speed;
                break;
            case GameManager.GameState.GameOver:
               
                currentSpeed = 0;
                break;
            case GameManager.GameState.Paused:
                currentSpeed = 0;
                
                break;
            case GameManager.GameState.Win :
                currentSpeed = 50;
                
                _ballAnims.PlayWinParticle();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    void Start()
    {
        GameManager.Instance.balls.Add(gameObject);
        
        
        _rb = GetComponent<Rigidbody2D>();
        currentSpeed = speed;
        //Invoke(nameof(SetRandomTrajectory), 1);
    }

   
    private void FixedUpdate()
    {
        
        _rb.velocity = _rb.velocity.normalized * currentSpeed;
        FaceVelocity();
    }
    
    //Adds  random force to the ball called at start of the game. 
    private void SetRandomTrajectory() 
    {
        Vector2 force = new Vector2(Random.Range(-2f, 2f), 1);
        
        _rb.AddForce(force.normalized* speed, ForceMode2D.Impulse);
        transform.parent = null;
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
   
 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Brick"))
        {
            GetComponent<BallComboScript>().PlayPling();
        }
        else
        {
            AudioManager.Instance.PlayBallWallSound();
        }
     
        //ScaleObject();
        //HighlightBall();
        //PlantParticle(collision);
        _ballAnims.ScaleObject();
        _ballAnims.HighlightBall();
        _ballAnims.PlantParticle(collision);
        if (collision.gameObject.CompareTag("GameOver")&&GameManager.Instance.currentState==GameManager.GameState.Playing)
        {
            GameManager.Instance.balls.Remove(gameObject);
            _ballAnims.PlayLoseParticle();
            currentSpeed = 0;
            if (GameManager.Instance.balls.Count == 0)
            {
                
                GameManager.Instance.UpdateGameState(GameManager.GameState.GameOver);
                return;
            }
            else
            {
                //Destroy(gameObject);
            }
        }
      EventManager.Instance.InvokeBallCollision();
    }
    private void OnDestroy()
    {
        GameManager.OnGameStateChanged-=GameManager_OnGameStateChanged;
    }
}
