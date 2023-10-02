using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int health;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (health == 1) { 
            Destroy(gameObject);
        return; }
        health--;
    }
}
