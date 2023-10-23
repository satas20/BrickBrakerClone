using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;
using UnityEngine.Serialization;

public class PaddleScript : MonoBehaviour
{
    public int health=3;

    private bool _isInvicible=false;

    [FormerlySerializedAs("renderer")] [SerializeField] private GameObject spriteRenderer;
    //clamping values for the paddle.
    [SerializeField] private float maxPosx;
    [SerializeField] float maxBounceAngle;
    //Effect objects.
    [SerializeField] private ParticleSystem confetiParticle;
    [SerializeField] private GameObject mouth;
    
    
    private Camera _mainCamera;
    private Vector2 _screenBounds;
    private Tweener _mouthTween;

    private void Start()
    {
        _mainCamera = Camera.main;
        _screenBounds = _mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _mainCamera.transform.position.z));
    }

    private void Update()
    {
       
        if (GameManager.Instance.currentState == GameManager.GameState.Playing)
        {
            HandleMovement();
        }
    }
    
    // Moves the paddle to the mouse  x position.
    private void HandleMovement() 
    {
        float padPos= _mainCamera.ScreenToWorldPoint(Input.mousePosition).x; // taking mouse position according to camera.
        
        
        // Assigning and clamping the paddle position according to the screen size.
        transform.position = new Vector2(Mathf.Clamp(padPos,-_screenBounds.x + transform.localScale.x/1.5f,_screenBounds.x-transform.localScale.x/1.5f),transform.position.y) ; 
    }

    public void GetHit()
    {
        if(_isInvicible)
            return;
        _isInvicible = true;
        health--;
        ShakePaddle();
        StartCoroutine(FlashAfterDamage());
        EventManager.Instance.InvokePaddleHit();
        if (health == 0)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.GameOver);
        }
    }
    //Bending the ball angle according to the landing position on the paddle using rigidbody of the ball.
    private void HandleBallBounce(Collision2D collision)
    {
        Rigidbody2D ball = collision.rigidbody;
        Collider2D paddle = collision.otherCollider;

        // Gather information about the collision
        Vector2 ballDirection = ball.velocity.normalized;
        Vector2 contactDistance = paddle.bounds.center - ball.transform.position;

        // Rotate the direction of the ball based on the contact distance
        // to make the gameplay more dynamic and interesting
        float bounceAngle = (contactDistance.x / paddle.bounds.size.x) * maxBounceAngle;
        ballDirection = Quaternion.AngleAxis(bounceAngle, Vector3.forward) * ballDirection;
        // Re-apply the new direction to the ball
        ball.velocity = ballDirection * ball.velocity.magnitude;
    }
    # region Effects

    private IEnumerator FlashAfterDamage()
    {
        float flashDelay = 0.01f;
        SpriteRenderer spriteRenderer = this.spriteRenderer.GetComponent<SpriteRenderer>();
        
        for (int i = 0; i < 10; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(flashDelay);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(flashDelay);
        }

        _isInvicible = false;
    }
    private void ShakePaddle()
    {
        spriteRenderer.transform.DOShakeScale(0.2f, 1f, 20, 90, true);
    }
    //plays the confeti particle system at the collision point.
    private void playConfeti(Collision2D collision)
    {
        confetiParticle.transform.position = collision.GetContact(0).point;
        confetiParticle.Play();
    }
    
    //Makes the mouth smile using scaleY.
    private void makeMouthSmile()
    {
        if (_mouthTween != null && _mouthTween.IsActive())
        {
            _mouthTween.Kill();
        }
        _mouthTween=mouth.transform.DOScaleY(1, 0.2f).OnComplete(() =>
        {
            _mouthTween=mouth.transform.DOScaleY(-1, 8);
        });
    }
    # endregion
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ball"))
        {
            return;
        }
        makeMouthSmile();
        AudioManager.Instance.PlayBallPaddle();
        playConfeti(collision);
        HandleBallBounce(collision);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "BallAddPowerUp":
                break;
            case "BallMultiplyPowerUp":
                break;
            case "PaddleExtendPowerUp":
                break;
            case "NoLoosePowerUp":
                break;
            case "SlowMoPowerUp":
                break;
            
                
        }
    }
}
