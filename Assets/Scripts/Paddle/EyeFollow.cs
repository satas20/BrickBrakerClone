using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeFollow : MonoBehaviour
{
    private void Update()
    {
        FollowTarget();
    }
    
    //Makes the eye follow the ball calculating the direction vector.
    private void FollowTarget(){
        if(GameManager.Instance.balls.Count==0) return;
        Vector3 ball = GameManager.Instance.balls[0].transform.position;
            
        Vector2 direction = new Vector2(
            (ball.x - transform.position.x),
            (ball.y - transform.position.y)     
        );
        transform.up = direction;
    }
}
