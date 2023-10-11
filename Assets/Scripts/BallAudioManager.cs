using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAudioManager : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip[] plingSounds;
    [SerializeField] private AudioClip  ballWall;
    [SerializeField] private  float comboTimer;
    private float timer=0;
    private int comboCount=0;
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    

    // Update is called once per frame
    void Update()
    {
        
        timer += Time.deltaTime;
        if (timer > comboTimer)
        {
            comboCount = 0;
        }
    }

    public void PlayPling()
    {
        timer = 0;
        _audioSource.PlayOneShot(plingSounds[comboCount]);
        comboCount++;
        
    }

    public void PlayBallWall()
    {
        _audioSource.PlayOneShot(ballWall);
    }
}
