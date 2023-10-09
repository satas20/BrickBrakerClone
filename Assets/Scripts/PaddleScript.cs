using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour
{
    [SerializeField] private float maxPosx;
    [SerializeField] float maxBounceAngle;
    
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    void Update()
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ball"))
        {
            return;
        }

        HandleBallBounce(collision);
    }

}
