using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource _audioSource;
    //Audio clips
    [SerializeField] private AudioClip[] plingSounds;
    [SerializeField] private AudioClip  ballWall;
    [SerializeField] private AudioClip  ballPaddle;
    [SerializeField] private AudioClip  paddleHit;
    [SerializeField] private AudioClip  powerUp;
    [SerializeField] private AudioClip  laserShoot;
    
    public static AudioManager Instance;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayComboSound(int comboCount)
    {
        
        _audioSource.PlayOneShot(plingSounds[comboCount]);
    }
    public void PlayBallWallSound()
    {
        _audioSource.PlayOneShot(ballWall);
    }

    public void PlayBallPaddle()
    {
        
        _audioSource.PlayOneShot(ballPaddle);
    }

    public void PlayPaddleHit()
    {
        _audioSource.PlayOneShot(paddleHit);

    }
    public void PlayPowerUp()
    {
        _audioSource.PlayOneShot(powerUp);

    }
    public void PlayLaserShoot()
    {
        _audioSource.PlayOneShot(laserShoot);
    }
}
