using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendPowerUp : MonoBehaviour
{
    [SerializeField] private float duration;

    IEnumerator PickUp(Collider2D other)
    {
        GameObject player = other.gameObject;
        Vector3 orginalScale=other.gameObject.transform.localScale;
        player.transform.localScale = orginalScale + new Vector3(orginalScale.x, 0, 0); 
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(duration);
        player.transform.localScale = orginalScale;
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Paddle"))
        {
            StartCoroutine(PickUp(other));            
        }
    }
}
