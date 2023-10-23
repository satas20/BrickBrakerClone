using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;


public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;
    [SerializeField] private float downDuration;

    [SerializeField] Transform spawnPoint;
    [SerializeField] private List<GameObject> wavePrefabs;
    private void Awake()
    {
        Instance= this;
    }

    void Start()
    {
        StartCoroutine(SpawnTile());
    }

    IEnumerator SpawnTile()
    {
        while (true)
        {
            
            Instantiate(wavePrefabs[ Random.Range(0, wavePrefabs.Count)], spawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(5f);
        }
        
    }
}
