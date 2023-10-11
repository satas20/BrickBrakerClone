using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAudioManager : MonoBehaviour
{
    private AudioSource _audioSource;
    //Audio clips
    [SerializeField] private AudioClip[] plingSounds;
    [SerializeField] private AudioClip  ballWall;
    //Time variables for combo.
    [SerializeField] private  float comboTimer;
    private float timer=0;
    private int comboCount=0;
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    

    // Update is called once per frame
    private void Update()
    {
        //Combo timer counts how much time has passed since the last pling sound.
        timer += Time.deltaTime;
        if (timer > comboTimer)
        {
            comboCount = 0;
        }
    }
    //Plays the pling sound according to the combo count.
    public void PlayPling()
    {
        timer = 0;
        _audioSource.PlayOneShot(plingSounds[comboCount]);
        comboCount++;
        
    }
    //Plays the ball wall sound.
    public void PlayBallWall()
    {
        _audioSource.PlayOneShot(ballWall);
    }
}
