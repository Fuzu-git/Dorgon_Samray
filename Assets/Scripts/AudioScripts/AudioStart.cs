using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioStart : MonoBehaviour
{
    
    public List<AudioClip> startSoundList = new List<AudioClip>();
    public AudioSource startAudioSource;

    public List<AudioClip> barkSoundList = new List<AudioClip>();
    public AudioSource barkAudioSource; 
    
    void Start()
    {
        var randomAudio = Random.Range(0, startSoundList.Count); 
        startAudioSource.clip =startSoundList[randomAudio];
        startAudioSource.Play();
        
    }

    private void Update()
    {
        StartCoroutine(Barking());
    }

    private IEnumerator Barking()
    {
        yield return new WaitForSeconds(45);
        var randomAudio = Random.Range(0, barkSoundList.Count);
        barkAudioSource.clip = barkSoundList[randomAudio]; 
        barkAudioSource.Play();
        yield return new WaitForSeconds(2); 
    }
}
