using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMoPowerUp : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private SpriteRenderer renderer;

    IEnumerator PickUp(Collider2D other)
    {
        Time.timeScale = 0.5f;
        Time.fixedDeltaTime  = Time.timeScale * 0.01f;
        renderer.enabled = false;
        yield return new WaitForSeconds(duration);
        Time.timeScale = 1;
        Time.fixedDeltaTime  = Time.timeScale * 0.01f;
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
