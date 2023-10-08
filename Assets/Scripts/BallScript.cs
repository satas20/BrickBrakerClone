using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class BallScript : MonoBehaviour
{
    public UnityEvent ballCollision;
    [SerializeField] float maxSpeed;
    [SerializeField] float startSpeed;
    [SerializeField] GameObject ballRenderer;

    private float speed=10;
    private Rigidbody2D rb;

    private Vector3 _originalScale;
    private Color _orginalColor;

    private Tweener scaleTween;
    private Tweener strechTween;
    private Tweener woobleTween;
    private Tweener colorTween;

    void Start()
    {
        _orginalColor = ballRenderer.GetComponent<SpriteRenderer>().color;
        _originalScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        Invoke(nameof(setRandomTrajectory), 1);
    }

   
    private void FixedUpdate()
    {
        
        rb.velocity = rb.velocity.normalized * speed;
        FaceVelocity();
    }
    

    private void setRandomTrajectory() 
    {
        Vector2 force = new Vector2(Random.Range(-2f, 2f), 1);
        
        rb.AddForce(force.normalized* speed, ForceMode2D.Impulse);
        transform.parent = null;
    }
    private void ScaleObject(){
        if (scaleTween != null && scaleTween.IsActive())
        {
            scaleTween.Kill();
        }
        scaleTween =ballRenderer.transform.DOScale(new Vector3(2,3.5f,2), 0.1f).OnComplete(() =>
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
        if (woobleTween != null && scaleTween.IsActive())
        {
            strechTween.Kill();
        }
        woobleTween = ballRenderer.transform.DOShakeScale(0.4f, 1, 20, 90, true).OnComplete(()=> {

            ballRenderer.transform.DOScale(Vector3.one, 0.1f);
        });
    }
    private void HiglightBall(){
        if (colorTween != null && scaleTween.IsActive())
        {
            colorTween.Kill();
        }
        colorTween =ballRenderer.GetComponent<SpriteRenderer>().DOColor(Color.white, 0.1f).OnComplete(()=>
        { 
            ballRenderer.GetComponent<SpriteRenderer>().DOColor(_orginalColor,0.2f);    
        });

    }

    private void ScreenShake(){ }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ballCollision.Invoke();
        //FaceVelocity();FaceVelocity Called in update 
        //WobbleBall(); Wobble process Starts end of the ScaleObject animation
        ScaleObject();
        HiglightBall();
    }
}
