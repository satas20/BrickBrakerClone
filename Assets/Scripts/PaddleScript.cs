using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PaddleScript : MonoBehaviour
{
    //clamping values for the paddle.
    [SerializeField] private float maxPosx;
    [SerializeField] float maxBounceAngle;
    //Effect objects.
    [SerializeField] private ParticleSystem confetiParticle;
    [SerializeField] private GameObject mouth;

    private Camera _mainCamera;
    private AudioSource _audioSource;
    
    private Tweener _mouthTween;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        HandleMovement();
    }

    // Moves the paddle to the mouse  x position.
    private void HandleMovement() 
    {
        float padPos= _mainCamera.ScreenToWorldPoint(Input.mousePosition).x; // taking mouse position according to camera.
        
        // Assigning and clamping the paddle position according to the screen size.
        transform.position = new Vector2(Mathf.Clamp(padPos,-maxPosx,maxPosx),transform.position.y) ; 
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ball"))
        {
            return;
        }
        makeMouthSmile();
        _audioSource.Play();
        playConfeti(collision);
        HandleBallBounce(collision);
    }

}
