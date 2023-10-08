using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Brick : MonoBehaviour
{
    public int health;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Ball")
            return;
        Hit();
    }
    private void Hit()
    {
        health--;
        if (health == 0){
            BrickBreak();
        }    
    
    }
    private void BrickBreak(){

        try {
            transform.DOScale(0,1);
        }
        catch { }

        Invoke(nameof(DestroyBrick), 1.01f);
    }
    private void DestroyBrick() {
        Destroy(gameObject);
    }
}
