using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class İndicatorScript : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    // Start is called before the first frame update
    public Transform platform; // Platformun referansı
    public float yOffset = 0.5f; // Indicator'ın platformun altında olacağı yükseklik

    private void Update()
    {
         
    }
}
