using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField] float maxSpeed;
    [SerializeField] float startSpeed;

    private float speed=10;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke(nameof(setRandomTrajectory), 1);
    }

   
    private void FixedUpdate()
    {
        if (Mathf.Abs(rb.velocity.y)< 0.05f) {
            rb.AddForce(new Vector2(0, Random.Range(-1, 1)));
                }
        rb.velocity = rb.velocity.normalized * speed;
    }
    private void HandleBounce(Vector2 collision) 
    {
        
        Vector2 dir = collision - new Vector2(transform.position.x, transform.position.y); ;
        Debug.Log(dir);
        if(Mathf.Abs(dir.x) > (Mathf.Abs(dir.y))) 
        {
            rb.velocity = new(-rb.velocity.x, rb.velocity.y);
        }
        else 
        {
            rb.velocity = new(rb.velocity.x,- rb.velocity.y);

        }
    }

    private void setRandomTrajectory() 
    {
        Vector2 force = new Vector2(Random.Range(-2f, 2f), -1);
        
        rb.AddForce(force.normalized*speed,ForceMode2D.Impulse);

    }
  
    
    

}
