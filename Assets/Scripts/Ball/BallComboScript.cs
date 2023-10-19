using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallComboScript : MonoBehaviour
{
    private AudioManager _audioManager;
    //Audio clips
    [SerializeField] private AudioClip[] plingSounds;
    [SerializeField] private AudioClip  ballWall;
    //Time variables for combo.
    [SerializeField] private  float comboTimer;
    private float timer=0;
    private int comboCount=0;
    
    private void Start()
    {
        _audioManager= AudioManager.Instance;
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
        //_audioSource.PlayOneShot(plingSounds[comboCount]);
        _audioManager.PlayComboSound(comboCount);
        if(comboCount<plingSounds.Length-1)
            comboCount++;
        
    }
    
}
