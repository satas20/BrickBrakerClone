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
    [SerializeField] private List<GameObject> wavePrefabs1;
    [SerializeField] private List<GameObject> wavePrefabs2;
    [SerializeField] private List<GameObject> wavePrefabs3;
    public int waveNum=1; 
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
            switch (waveNum)
            {
                case 1:
                    Instantiate(wavePrefabs1[ Random.Range(0, wavePrefabs1.Count)], spawnPoint.position, Quaternion.identity);
                    break;
                case 2:
                    Instantiate(wavePrefabs2[ Random.Range(0, wavePrefabs2.Count)], spawnPoint.position, Quaternion.identity);
                    break;
                case 3:
                    Instantiate(wavePrefabs3[ Random.Range(0, wavePrefabs3.Count)], spawnPoint.position, Quaternion.identity);
                    break;
            }

            yield return new WaitForSeconds(5f);
        }
        
    }
}
