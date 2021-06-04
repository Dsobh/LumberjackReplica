using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip bushHitSound, cutSound, buttonSound;
    private AudioSource _audioSource;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        _audioSource = this.GetComponent<AudioSource>();
    }

    public void PlayCut()
    {
        _audioSource.PlayOneShot(cutSound);
    }

    public void PlayGameOver()
    {
        _audioSource.PlayOneShot(bushHitSound);
    }

    public void PlayButtonSound()
    {
        _audioSource.PlayOneShot(buttonSound);
    }

    
}
