using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.Serialization;
using Update = UnityEngine.PlayerLoop.Update;

public class BrickScript : MonoBehaviour
{
    [SerializeField]private int health;
    //Brick variables.
    

    [SerializeField] private float downDuration;
    [SerializeField]private BrickAnims _brickRenderer;
    [SerializeField] private bool isOnMenu=false;
    
    
    private void Start()
    {
        
        GameManager.Instance.bricks.Add(gameObject);
       
    }

    private void Update()
    {
        
        if (GameManager.Instance.currentState == GameManager.GameState.Playing)
        {
            if(isOnMenu){return;}
            transform.Translate(Vector3.down*Time.deltaTime,Space.World);
        }
    
    }

    //Reduces the health of the brick and calls BrakeBrick it if health is 0.
    private void Hit(Collision2D collision)
    {
        health--;
        if (health == 0){
            BrickBreak( collision);
        }    
    
    }

    
   
   
    private void BrickBreak(Collision2D collision2D)
    {
        GameManager.Instance.bricks.Remove(gameObject);
        GameManager.Instance.SpawnPowerUp(gameObject.transform.position);
        
        _brickRenderer.KillTweens();
        _brickRenderer.UnSub();
        if(collision2D!=null)
            _brickRenderer.ThrowBrick(collision2D);
        _brickRenderer.DarkenBrick();
        
        
        transform.DOScale(0,0.5f).OnComplete(()=>
        {
            _brickRenderer.KillTweens();
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

        if (other.gameObject.CompareTag("Paddle"))
        {
            BrickBreak(other);
            other.gameObject.GetComponent<PaddleScript>().GetHit();
        }

        if (other.gameObject.CompareTag("GameOver"))
        {
            BrickBreak(null);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Laser"))
        {
            BrickBreak(null);
            
        }
        
    }

   
}
