using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeFollow : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        
        FollowTarget();
    }
    
    //Makes the eye follow the ball calculating the direction vector.
    private void FollowTarget(){
        Vector3 ball = BallScript.Instance.transform.position;
            
        Vector2 direction = new Vector2(
            (ball.x - transform.position.x),
            (ball.y - transform.position.y)     
        );
        transform.up = direction;
    }
}
