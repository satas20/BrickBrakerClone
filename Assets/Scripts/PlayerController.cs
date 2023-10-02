using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float maxPosx;
    [SerializeField] float maxBounceAngle;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }


    private void HandleMovement() 
    {
        float padPos= Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        
        transform.position = new Vector2(Mathf.Clamp(padPos,-maxPosx,maxPosx),transform.position.y) ;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ball"))
        {
            return;
        }

        Rigidbody2D ball = collision.rigidbody;
        Collider2D paddle = collision.otherCollider;

        // Gather information about the collision
        Vector2 ballDirection = ball.velocity.normalized;
        Vector2 contactDistance = paddle.bounds.center - ball.transform.position;

        // Rotate the direction of the ball based on the contact distance
        // to make the gameplay more dynamic and interesting
        float bounceAngle = (contactDistance.x / paddle.bounds.size.x) * maxBounceAngle;
        ballDirection = Quaternion.AngleAxis(bounceAngle, Vector3.forward) * ballDirection;
        Debug.Log(ballDirection);
        // Re-apply the new direction to the ball
        ball.velocity = ballDirection * ball.velocity.magnitude;
    }
}
